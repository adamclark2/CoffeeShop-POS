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
                foreach(Size sz in d.Sizes){
                    Console.Write("   {0,-11}|", sz.Name);
                }
                Console.Write("\n");
                foreach(Size sz in d.Sizes){
                    Console.Write("   ${0,-10:N2}|", sz.Price);
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