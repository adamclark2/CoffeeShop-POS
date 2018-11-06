using System.Collections.Generic;
using Model;
using System.Runtime.Serialization;

namespace DataRepository{

    /**
        A DataRepo for all the Drinks, Food, and Extras.
        This repo can be serialized and de-serizlized to/from JSON
     */
    [DataContract]
    public class JsonDataRepo : DataRepo{

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
            return this.Food;
        }
        public List<Drink> getAllDrinks(){
            return Drinks;
        }
        public List<DrinkExtra> getAllDrinkExtras(){
            return DrinkExtras;
        }

        public Food getFoodWithName(string name){
            return null;
        }
        public Drink getDrinkWithName(string name){
            return null;
        }
        public DrinkExtra getDrinkExtrasWithName(string name){
            return null;
        }
    }
}
