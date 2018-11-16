using System.Collections.Generic;
using Model;
using DataAbstration;

namespace DataAbstration{

    /**
        A helper object to allow for all parts of the application
        to discover the Dao.

        Typical workflow:
        Create Dao >> setupFactory() >> Any part of the app can get the Data

        Why:
        Let's say we wanted to switch from Json files to SQL
        we would create a SQLDao give that to the factory
        and our application would behave the same way
     */
    public class DaoFactory{
        public static ItemDao DAO{get;set;}

        public static void setupFactory(ItemDao dao){
            DAO = dao;
        }
    }
}
