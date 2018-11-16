using Model;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DataAbstration;
using System.Text;

namespace Model{

    /*
        A representation of a payment

        Payments have multiple tenders
        E.g
        Pay with two credit cards
        Pay with cash only
        Pay with cash & check
     */
     [DataContract]
    public class Payment{
        [DataMember]
        public List<Tender> PaymentMethods{get;set;} = new List<Tender>();

        [DataMember]
        public double AmountDue{get;set;}

        [DataMember]
        public double Change{
            get{
                double change = AmountDue - AmountPayed;
                return change < 0 ? change : 0.00;
            }
            set{
                // no op to satisfy [DataContract]
                return;
            }
        }

        [DataMember]
        public double AmountPayed{
            get{
                double payed = 0;
                foreach(Tender t in PaymentMethods){
                    payed += t.Amount;
                }
                return payed;
            }

            set{
                // no op to satisfy [DataContract]
                return;
            }
        }

        [DataMember]
        public bool HasPayedInFullOrMore{
            get {
                return AmountPayed >= AmountDue;
            }

            set{
                // no op to satisfy [DataContract]
                return;
            }
        }

        public Payment(){}

        public Tender addTender(TenderTypes types, double amount, string code){
            Tender t = new Tender(types, amount, code);
            PaymentMethods.Add(t);
            return t;
        }

        public Tender addTender(TenderTypes types, double amount){
            return addTender(types, amount, "**NOT SPECIFIED**");
        }
    }
}