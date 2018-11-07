using System.Collections.Generic;
using Model;

namespace DataAbstration{
    /**
        
     */
    public class DataHelpers{
        public static List<string> getAllDrinkSizes(){
            ItemDao dao = DaoFactory.DAO;
            Dictionary<string, string> di = new Dictionary<string,string>();

            List<Drink> drinks = dao.getAllDrinks();
            for(int i = 0; i < drinks.Count;i++){
                List<Size> sizes = drinks[i].Sizes;
                for(int j = 0; j < sizes.Count;j++){
                    di.Add(sizes[j].Name, "");
                }
            }
            List<string> ret = new List<string>();
            foreach(string s in di.Keys){
                ret.Add(s);
            }

            return ret;
        }
    }
}
