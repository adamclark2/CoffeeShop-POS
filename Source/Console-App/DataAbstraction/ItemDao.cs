using System.Collections.Generic;
using Model;

namespace DataAbstration{
    /**
        Somthing that holds all Foods, all Drinks, and all Drink Extras.

        This is a object used to access data (Data access object)

        This is implemented in JsonDao, if we wanted to use SQL
        we would create a seperate dao.
     */
    public interface ItemDao{
        List<Food> getAllFoods();
        List<Drink> getAllDrinks();
        List<Extra> getAllDrinkExtras();

        Food getFood(string name);
        Drink getDrink(string name);
        Extra getDrinkExtra(string name);
    }
}
