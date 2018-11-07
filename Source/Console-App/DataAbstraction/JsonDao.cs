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
        private List<Extra> DrinkExtras;


        public List<Food> getAllFoods(){
            return this.Food != null ? this.Food : new List<Food>();
        }
        public List<Drink> getAllDrinks(){
            return Drinks != null ? this.Drinks : new List<Drink>();
        }
        public List<Extra> getAllDrinkExtras(){
            return DrinkExtras != null ? this.DrinkExtras : new List<Extra>();
        }

        public Food getFood(string name){
            foreach(Food f in getAllFoods()){
                if(f.Name.Equals(name)){
                    return f;
                }
            }
            return null;
        }
        public Drink getDrink(string name){
            foreach(Drink d in getAllDrinks()){
                if(d.Name.Equals(name)){
                    return d;
                }
            }
            return null;
        }
        public Extra getDrinkExtra(string name){
            foreach(Extra e in getAllDrinkExtras()){
                if(e.Name.Equals(name)){
                    return e;
                }
            }
            return null;
        }
    }
}
