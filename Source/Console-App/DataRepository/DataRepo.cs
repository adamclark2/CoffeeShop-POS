using System.Collections.Generic;
using Model;

namespace DataRepository{
    public interface DataRepo{
        List<Food> getAllFoods();
        List<Drink> getAllDrinks();
        List<DrinkExtra> getAllDrinkExtras();

        Food getFoodWithName(string name);
        Drink getDrinkWithName(string name);
        DrinkExtra getDrinkExtrasWithName(string name);
    }
}
