using System.Collections.Generic;
using Model;
using System.Runtime.Serialization;

namespace DataAbstration{

    /**
        A Dao for all the Drinks, Food, and Extras.
        This Dao can be serialized and de-serizlized to/from JSON
     */
    [DataContract]
    public class JsonDao : ItemDao{

        #pragma warning disable 0649
        [DataMember]
        private List<Drink> Drinks;

        #pragma warning disable 0649
        [DataMember]
        private List<Food> Food;

        #pragma warning disable 0649
        [DataMember]
        private List<DrinkExtra> DrinkExtras;


        public List<Food> getAllFoods(){
            return this.Food != null ? this.Food : new List<Food>();
        }
        public List<Drink> getAllDrinks(){
            return Drinks != null ? this.Drinks : new List<Drink>();
        }
        public List<DrinkExtra> getAllDrinkExtras(){
            return DrinkExtras != null ? this.DrinkExtras : new List<DrinkExtra>();
        }
    }
}
