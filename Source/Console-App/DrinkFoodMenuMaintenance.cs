using System;
using System.IO;
using System.Collections.Generic;
using DataAbstration;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Text;
using Model;

namespace Console_App
{
    /**
        A holder class for the drink/food menu options
        to maintain said menu
     */
    public static class DrinkFoodMenuMaintenance{
        /**
            Add function pointers to the map

            menu-list              => menuListDelegate
            menu-remove-drink      => menuRemoveDrinkDelegate
            menu-remove-food       => menuRemoveFoodDelegate
            menu-remove-drinkextra => menuRemoveDrinkExtraDelegate
         */
        public static void addMenuOptions(Dictionary<string, ConsoleMenuDelegate> consoleDelegates){
            consoleDelegates.Add("menu-list",  menuListDelegate);
            consoleDelegates.Add("menu-remove-drink",       menuRemoveDrinkDelegate);
            consoleDelegates.Add("menu-remove-food",        menuRemoveFoodDelegate);
            consoleDelegates.Add("menu-remove-drinkextra",  menuRemoveDrinkExtraDelegate);

            consoleDelegates.Add("menu-add-drink",          (string args) => menuAddItemDelegate("drink", args));
            consoleDelegates.Add("menu-add-drink-size",     (string args) => menuAddItemDelegate("drink-size", args));
            consoleDelegates.Add("menu-add-drink-extra",    (string args) => menuAddItemDelegate("drink-extra", args));
            consoleDelegates.Add("menu-add-food",           (string args) => menuAddItemDelegate("food", args));
            consoleDelegates.Add("menu-add-food-size",      (string args) => menuAddItemDelegate("food-size", args));
            consoleDelegates.Add("menu-add-food-extra",     (string args) => menuAddItemDelegate("food-extra", args));
        }

        public static void menuAddItemDelegate(string type, string args){
            List<List<string>> tokens = Tokenizer.tokenizeString(args);
            if(tokens.Count < 1 || tokens.Count > 3){
                Console.Write("Invalid arguments\n");
                return;
            }

            // Get a (string, double, bool(error)) tuple
            // From the token list
            Func<List<List<string>>, Tuple<string, double, Boolean>> getStrDouble = 
                (List<List<string>> arg) => {
                    double d;
                    bool success = double.TryParse(arg[0][2], out d);
                    Tuple<string, double, Boolean> ret = new Tuple<string, double, Boolean>(arg[0][1],d, success);
                    return ret;
                }
            ;

            // Get a (string, string, double, bool(error)) tuple
            // From the token list
            // For example
            // item-add-size {name of item} {size name} {size price}
            //                 Item1          Item2         Item3
            // Error is true if somthing went wrong
            Func<List<List<string>>, Tuple<string, string, double, Boolean>> getStrStrDouble = 
                (List<List<string>> arg) => {
                    double d;
                    bool success = double.TryParse(arg[0][3], out d);
                    Tuple<string,string,double,bool> ret 
                        = new Tuple<string,string,double,bool>(arg[0][1],arg[0][2], d, success);
                    return ret;
                }
            ;

            string name = tokens[0][1];
            Tuple<string,string,double,bool> tup2;
            Tuple<string, double, Boolean> tup1;

            switch (type){
                // menu-add-drink {drink name}
                case "drink":
                    Drink d = new Drink();
                    d.Name = name;
                    if(DaoFactory.DAO.addDrink(d)){
                        Console.Write("We have successfully added the drink [{0}]", name);
                        Console.Write("Please add the size with menu-add-drink-size\nSee 'help' for details");
                    }else{
                        Console.Write("We couldn't add the drink, does another one exist under the same name?\n");
                    }
                return;

                // menu-add-drink-size {drink name} {size name} {size price}
                case "drink-size":
                    tup2 = getStrStrDouble(tokens);
                    if(tup2.Item4){
                        Size sz = new Size(tup2.Item2, tup2.Item3);
                        Console.Write("The drink is ::{0}::\n\n", tup2.Item1);
                        Drink mod = DaoFactory.DAO.getDrink(tup2.Item1);
                        if(mod != null){
                            mod.Sizes.Add(sz);
                        }else{
                            Console.Write("That drink name isn't in our system");
                        }
                    }else{
                        Console.Write("Prices have to be a number\n");
                    }
                return;

                // menu-add-drink-extra {extra name} {extra price}
                case "drink-extra":
                    tup1 = getStrDouble(tokens);
                    Extra ee = new Extra(tup1.Item1, tup1.Item2);
                    DaoFactory.DAO.addDrinkExtra(ee);
                break;

                // menu-add-drink {food name}
                case "food":
                    Food ff = new Food();
                    ff.Name = name;
                    if(DaoFactory.DAO.addFood(ff)){
                        Console.Write("We have successfully added the food [{0}]", name);
                        Console.Write("Please add the size with menu-add-food-size\nSee 'help' for details");
                    }else{
                        Console.Write("We couldn't add the food, does another one exist under the same name?\n");
                    }
                break;

                // menu-add-food-size {food name} {size name} {size price}
                case "food-size":
                    tup2 = getStrStrDouble(tokens);
                    if(tup2.Item4){
                        Size sz = new Size(tup2.Item2, tup2.Item3);
                        Food modFood = DaoFactory.DAO.getFood(tup2.Item1);
                        modFood.Sizes.Add(sz);
                    }else{
                        Console.Write("Prices have to be a number\n");
                    }
                break;

                // menu-add-food-extra {food name} {size name} {size price}
                case "food-extra":
                    tup2 = getStrStrDouble(tokens);
                    if(tup2.Item4){
                        Extra exFood = new Extra(tup2.Item2, tup2.Item3);
                        Food modFood = DaoFactory.DAO.getFood(tup2.Item1);
                        modFood.Extras.Add(exFood);
                    }else{
                        Console.Write("Prices have to be a number\n");
                    }
                break;
            }
        }
        

        private static void menuRemoveDrinkExtraDelegate(string args){
            string[] arr = args.Split('\"');
            if(arr.Length < 2){
                Console.Write("Please specify a drink-extra to remove\n");
                return;
            }

            if(DaoFactory.DAO.getDrinkExtra(arr[1]) == null){
                Console.Write("The drink-extra [{0}] doesn't exist\n", arr[1]);
            }else if(DaoFactory.DAO.removeDrinkExtra(arr[1]) == false){
                Console.Write("Due to a system error [{0}] couldn't be removed\n", arr[1]);
            }else{
                Console.Write("Successfully removed [{0}]\n", arr[1]);
            }
        }

        private static void menuRemoveDrinkDelegate(string args){
            string[] arr = args.Split('\"');
            if(arr.Length < 2){
                Console.Write("Please specify a drink to remove\n");
                return;
            }

            if(DaoFactory.DAO.getDrink(arr[1]) == null){
                Console.Write("The drink [{0}] doesn't exist\n", arr[1]);
            }else if(DaoFactory.DAO.removeDrink(arr[1]) == false){
                Console.Write("Due to a system error [{0}] couldn't be removed\n", arr[1]);
            }else{
                Console.Write("Successfully removed [{0}]\n", arr[1]);
            }
        }

        private static void menuRemoveFoodDelegate(string args){
            string[] arr = args.Split('\"');
            if(arr.Length < 2){
                Console.Write("Please specify a food to remove\n");
                return;
            }

            if(DaoFactory.DAO.getFood(arr[1]) == null){
                Console.Write("The food [{0}] doesn't exist\n", arr[1]);
            }else if(DaoFactory.DAO.removeFood(arr[1]) == false){
                Console.Write("Due to a system error [{0}] couldn't be removed\n", arr[1]);
            }else{
                Console.Write("Successfully removed [{0}]\n", arr[1]);
            }
        }

        /**
            List everything on the menu
         */
        private static void menuListDelegate(string args){
            string[] spaceDelim = args.TrimStart().Split(' ');
            // List all
            listDrinks();
            listDrinkExtras();
            listFood();
            Console.Write("\n\n");
        }

        private static void listDrinks(){
            Console.Write("Drinks:\n");
            foreach(Drink d in DaoFactory.DAO.getAllDrinks()){
                Console.Write("   " + d.Name + "\n");
                if(d.Sizes != null && d.Sizes.Count > 0){
                    foreach(Size sz in d.Sizes){
                    Console.Write("   {0,-11}|", sz.Name);
                    }
                    Console.Write("\n");
                    foreach(Size sz in d.Sizes){
                        Console.Write("   ${0,-10:N2}|", sz.Price);
                    }
                }
                Console.Write("\n\n");

            }
        }

        private static void listDrinkExtras(){
            Console.Write("Drink Extras:\n");
            foreach(Extra f in DaoFactory.DAO.getAllDrinkExtras()){
                Console.Write("   " + f.Name + "--" + "${0,-4:N2}", f.Price);
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        private static void listFood(){
            Console.Write("Food:\n");
            foreach(Food f in DaoFactory.DAO.getAllFoods()){
                Console.Write("   " + f.Name + "\n");
                foreach(Size sz in f.Sizes){
                    Console.Write("   {0,-11}|", sz.Name);
                }
                Console.Write("\n");
                foreach(Size sz in f.Sizes){
                    Console.Write("   ${0,-10:N2}|", sz.Price);
                }

                Console.Write("\n\n   Extras:\n   ");
                foreach(Extra ex in f.Extras){
                    Console.Write("({0} -- ${1,-4:N2})", ex.Name, ex.Price);
                }
                Console.Write("\n\n");
            }
        }
    }
}