using Model;
using System.Collections.Generic;

namespace Model{
    public class Drink{
        public Drink(){
            Sizes = new List<Size>();
            extras = new List<Extra>();
            Name = "";
        }
        public string Name{get;set;}
        public List<Size> Sizes{get;set;}
        public List<Extra> extras{get;set;}
    }
}