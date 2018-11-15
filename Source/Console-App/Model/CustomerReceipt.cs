using Model;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DataAbstration;
using System.Text;

namespace Model{

    /*
        A representation of a Customers Receipt

        This is a DataContract so it can be serialized/de-serialized
        to/from json
     */
    [DataContract]
    public class CustomerReceipt{
        [DataMember]
        public Order order{get;set;} = new Order();

        [DataMember]
        public List<OrderedItem> items{get;set;} = new List<OrderedItem>();


        /**
            Add orders contained in an order string

            The string should be of the form
            order {item name} {item size} {optional item extra} {optional item extra}, {item name} .....

            See Tokenizer for more information about tokens
         */
        public void addOrders(string orderStr){
            List<List<string>> orders = Tokenizer.tokenizeString(orderStr.TrimStart());

            foreach(List<string> tokens in orders){
                Food  f = DaoFactory.DAO.getFood(tokens[1]);
                Drink d = DaoFactory.DAO.getDrink(tokens[1]);
                string size = tokens[2];

                if(f == null && d == null){
                    // Error
                    System.Console.Write("\n ERROR item isn't in database![" + tokens[1] + "]\n");
                    order.Error += "The item [" + tokens[1] + "] doesn't exist in the data.\n";
                }else{
                    List<Extra> extras = new List<Extra>();
                    OrderedItem oo = null;

                    int i = 3;
                    if(f == null){
                        while(i < tokens.Count && !tokens[i].Equals(",")){
                            Extra ex = DaoFactory.DAO.getDrinkExtra(tokens[i++]);
                            if(ex != null){extras.Add(ex);}
                        }
                        oo = new OrderedItem(d, extras, size);
                    }else{
                        while(i < tokens.Count &&!tokens[i].Equals(",")){
                            Extra ex = f.getExtra(tokens[i++]);
                            if(ex != null){extras.Add(ex);}
                        }
                        oo = new OrderedItem(f, extras, size);
                    }

                    items.Add(oo);
                    order.Price += oo.Price;
                    foreach(OrderErrors ee in oo.errors){
                        if(ee == OrderErrors.NULL_ITEM){
                            order.Error += "The item [" + oo.Name + "] doesn't exist in the data.\n";
                        }

                        if(ee == OrderErrors.NO_SIZE_SPECIFIED){
                            order.Error += "The size [" + oo.Size + "] doesn't exist for item [" + oo.Name + "]\n";
                        }

                        /* The json file probably had a size listed twice or more...
                            We used an arbitrary cost bassed off the ones in the file
                        */
                        if(ee == OrderErrors.TOO_MANY_SIZE){
                            order.Error += "The size [" + oo.Size + "] exists multiple times for item [" + oo.Name + "]\n";
                        }
                    }
                }
            }
        }

        /*
            Get a string representation of the customer receipt

            Suitable for printing to the console
         */
        override public string ToString(){
            return "Price: " + string.Format("${0:N2}", order.Price) + "\n";
        }
    }
}