using Model;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DataAbstration;
using System.Text;

namespace Model{
    [DataContract]
    public class CustomerReceipt{
        [DataMember]
        public Order order{get;set;} = new Order();

        [DataMember]
        public List<OrderedItem> items{get;set;} = new List<OrderedItem>();

        private List<List<string>> tokenizeOrderString(string orderStr){
            System.Console.Write("Tokenize!\n");
            List<List<string>> orders = new List<List<string>>();
            List<string> tokens = new List<string>();
            char[] arr = orderStr.ToCharArray();

            int i = 0;
            StringBuilder bldr = new StringBuilder();
            while(i < arr.Length){
                if(arr[i].Equals(' ')){
                    string s = bldr.ToString();
                    System.Console.Write("Adding tok:" + s + "\n");
                    tokens.Add(s);
                    bldr.Clear();
                    i++;
                }else if(arr[i].Equals('\"')){
                    i++;
                    // Keep adding until quote
                    while(i < arr.Length && arr[i] != '\"'){
                        bldr.Append(arr[i++]);
                    }
                    if(i > arr.Length){
                        // Error!
                        System.Console.Write("Lexing error!\n");
                    }
                    System.Console.Write("---" + bldr.ToString() + "===\n\n");
                    i++;
                }else if(arr[i].Equals(',')){
                    string s = bldr.ToString();
                    tokens.Add(s);
                    System.Console.Write("Adding tok:" + s + "\n");
                    bldr.Clear();

                    orders.Add(tokens);
                    System.Console.Write("----------\n");
                    tokens = new List<string>();
                    tokens.Add(".");

                    i++;
                    while(arr[i].Equals(' ')){
                        i++;
                    }
                }else{
                    bldr.Append(arr[i++]);
                }
            }
            tokens.Add(bldr.ToString());
            orders.Add(tokens);

            System.Console.Write("Tokenize!\n");
            return orders;
        }


        /**
            Add orders contained in an order string
         */
        public void addOrders(string orderStr){
            System.Console.Write("ADD ORDER");
            List<List<string>> orders = tokenizeOrderString(orderStr.TrimStart());

            foreach(List<string> tokens in orders){
                System.Console.Write("Loop!");
                Food  f = DaoFactory.DAO.getFood(tokens[1]);
                Drink d = DaoFactory.DAO.getDrink(tokens[1]);
                string size = tokens[2];

                if(f == null && d == null){
                    // Error
                    System.Console.Write("\n ERROR item isn't in database![" + tokens[1] + "]\n");
                }
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
                        order.Error += "The item [" + oo.Name + "] doesn't exit in the data.\n";
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
}