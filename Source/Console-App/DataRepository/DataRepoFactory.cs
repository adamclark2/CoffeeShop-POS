using System.Collections.Generic;
using Model;
using DataRepository;

namespace DataRepository{

    /**
        A helper object to allow for all parts of the application
        to discover the DataRepo.

        Typical workflow:
        Create Repo >> setupFactory() >> Any part of the app can get the DataRepo

        Why:
        Let's say we wanted to switch from Json files to SQL
        we would create a SQLDataRepo give that to the factory
        and our application would behave the same way
     */
    public class DataRepoFactory{
        public static DataRepo Repo{get;set;}

        public static void setupFactory(DataRepo repo){
            DataRepoFactory.Repo = repo;
        }
    }
}
