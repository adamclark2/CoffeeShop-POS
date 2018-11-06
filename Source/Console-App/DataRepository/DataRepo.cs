using System.Collections.Generic;
using Model;

namespace DataRepository{
    /**
        Somthing that holds all Foods, all Drinks, and all Drink Extras.

        This is implemented in JsonDataRepo, if we wanted to use SQL
        we would create a seperate repo.
     */
    public interface DataRepo{
        List<Food> getAllFoods();
        List<Drink> getAllDrinks();
        List<DrinkExtra> getAllDrinkExtras();

        Food getFoodWithName(string name);
        Drink getDrinkWithName(string name);
        DrinkExtra getDrinkExtrasWithName(string name);
    }
}
