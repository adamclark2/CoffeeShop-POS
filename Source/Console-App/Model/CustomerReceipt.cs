using Model;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DataAbstration;

namespace Model{
    [DataContract]
    public class CustomerReceipt{
        [DataMember]
        public Order order{get;set;} = new Order();

        [DataMember]
        public List<OrderedItem> items{get;set;} = new List<OrderedItem>();


        /**
            Add orders contained in an order string
         */
        public void addOrders(string orderStr){
            System.Console.Write("ADD ORDER");
            string[] orders = orderStr.TrimStart().Split(" ");
            for(int i = 1; i < orders.Length;i++){
                System.Console.Write("Loop!");
                Food  f = DaoFactory.DAO.getFood(orders[i]);
                Drink d = DaoFactory.DAO.getDrink(orders[i]);
                if(f == null && d == null){
                    // Error
                    System.Console.Write("\n ERROR item isn't in database!\n");
                }

                string size = orders[++i];
                List<Extra> extras = new List<Extra>();
                OrderedItem oo = null;

                // Index was on size, now its on the extras
                i++; 
                if(f == null){
                    while(i < orders.Length && !orders[i].Equals(",")){
                        Extra ex = DaoFactory.DAO.getDrinkExtra(orders[i++]);
                        if(ex != null){extras.Add(ex);}
                    }
                    oo = new OrderedItem(d, extras, size);
                }else{
                    while(i < orders.Length &&!orders[i].Equals(",")){
                        Extra ex = f.getExtra(orders[i++]);
                        if(ex != null){extras.Add(ex);}
                    }
                    oo = new OrderedItem(f, extras, size);
                }

                // Index was on ',' or end of file
                // Now we are past end of array or
                // the beginning or the next order
                i++; 

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