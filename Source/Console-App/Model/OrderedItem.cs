using Model;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

namespace Model{
    [DataContract]
    public enum OrderErrors{
        TOO_MANY_SIZE,
        NO_SIZE_SPECIFIED,
        NULL_ITEM
    }

    [DataContract]
    public class OrderedItem{
        [DataMember]
        public string Name{get;set;}

        [DataMember]
        public string Size;

        [DataMember]
        public string type;

        [DataMember]
        public double SizeCost;

        [DataMember]
        public List<Extra> extras{get;set;}

        public List<OrderErrors> errors{get;} = new List<OrderErrors>();

        public double Price 
        {
            get {
                double _price = SizeCost;
                if(extras != null){
                    foreach(Extra e in extras){
                        _price += e.Price;
                    }
                }

                return _price;
            }
        }

        public OrderedItem(Drink d, List<Extra> e, string size){
            if(d == null){
                errors.Add(OrderErrors.NULL_ITEM);
            }else{
                this.Name = d.Name;
                System.Console.Write("Name: " + d.Name + "\n");
                this.type = "drink";
                this.Size = size;

                int matches = 0;
                foreach(Size s in d.Sizes){
                    if(s.Name.Equals(size)){
                        SizeCost = s.Price;
                        matches++;
                    }
                }
                this.extras = new List<Extra>(e);

                if(matches == 0){
                    errors.Add(OrderErrors.NO_SIZE_SPECIFIED);
                }else if(matches > 1){
                    errors.Add(OrderErrors.TOO_MANY_SIZE);
                }
            }
        }

        public OrderedItem(Food f, List<Extra> e, string size){
            if(f == null){
                errors.Add(OrderErrors.NULL_ITEM);
            }else{
                this.Name = f.Name;
                this.type = "food";
                this.Size = size;

                int matches = 0;
                foreach(Size s in f.Sizes){
                    if(s.Name.Equals(size)){
                        SizeCost = s.Price;
                        matches++;
                    }
                }
                this.extras = new List<Extra>(e);

                if(matches == 0){
                    errors.Add(OrderErrors.NO_SIZE_SPECIFIED);
                }else if(matches > 1){
                    errors.Add(OrderErrors.TOO_MANY_SIZE);
                }
            }
        }
    }
}