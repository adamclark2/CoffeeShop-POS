using Model;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DataAbstration;
using System.Text;

namespace Model{

    /*
        A tender is a payment method used to make a payment

        Types include:
        Credit Card
        Cash
        Check
     */
    [DataContract]
    public class Tender{
        [DataMember]
       public TenderTypes Type{set;get;}

       [DataMember]
       public double Amount{get;set;}

       [DataMember]
       public string Code{set;get;}

       ///Constructor
       public Tender(TenderTypes type, double amount, string code){
           Amount = amount;
           Type = type;
           Code = code;
       }

       /// Constructor
       public Tender(){}

       override public string ToString(){
           string code = (Type == TenderTypes.CASH ? "" : "[" + Code + "]");
           return string.Format("${0:N2}   ", Amount)  + Type + code;
       }
    }

    public enum TenderTypes{
        CASH,
        CREDIT_CARD,
        CHECK
    }
}