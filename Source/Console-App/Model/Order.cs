using Model;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Model{
    [DataContract]
    public class Order{
        [DataMember]
        public double Price{get;set;}

        [DataMember]
        public string Error{get;set;}
    }
}