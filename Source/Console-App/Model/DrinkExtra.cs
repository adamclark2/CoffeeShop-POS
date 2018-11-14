using Model;
using System.Collections.Generic;

namespace Model{
    /**
        A add on to a drink
        For example:
        A coffee can have a flavorshot
     */
    public class Extra{
        public Extra(){}
        public Extra(string name, double price){
            Name = name;
            Price = price;
        }
        public string Name{get;set;}
        public double Price{get;set;}
    }
}