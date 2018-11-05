using System.Collections.Generic;
using Model;
using DataRepository;

namespace DataRepository{
    public class DataRepoFactory{
        public static DataRepo Repo{get;set;}

        public static void setupFactory(DataRepo repo){
            DataRepoFactory.Repo = repo;
        }
    }
}
