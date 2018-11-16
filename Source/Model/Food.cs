using Model;
using System.Collections.Generic;

namespace Model{
    public class Size{
        public Size(){}
        public Size(string name, double price){
            Name = name;
            Price = price;
        }
        public string Name{get;set;}
        public double Price{get;set;}
    }
    
    public class Food{
        public string Name{get;set;}
        public List<Size> Sizes{get;set;}
        public List<Extra> Extras{get;set;}

        public Food(){
            Sizes = new List<Size>();
            Extras = new List<Extra>();
        }

        public Extra getExtra(string name){
            foreach(Extra e in Extras){
                if(e.Name.Equals(name)){
                    return e;
                }
            }
            return null;
        }

        /**
            Print the food item to the command line
         */
        public void print(){
            System.Console.Write(Name + "\n");
            System.Console.Write("   Sizes:\n");
            for (int i = 0; i < Sizes.Count; i++)
            {
                System.Console.WriteLine("      $" + Sizes[i].Price + " " + Sizes[i].Name + "\n");
            }

            System.Console.Write("   Extras:\n");
            for (int i = 0; i < Extras.Count; i++)
            {
                System.Console.WriteLine("      $" + Extras[i].Price + " " + Extras[i].Name + "\n");
            }
        }
    }
}