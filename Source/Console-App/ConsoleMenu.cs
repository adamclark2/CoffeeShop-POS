using System;
using System.Collections.Generic;
using DataAbstration;
using Model;

namespace Console_App
{
    /**
        This is a function type, parts of the console menu are 'delegated'
        to helper functions
     */
    delegate void ConsoleMenuDelegate(string args);

    public class ConsoleMenu{

        /**
            This maps strings to functions, the menu functionality
            is delegated to the function

            'exit'   => exitDelegate
            'order'  => orderDelegate
            'help'   => helpDelegate
         */
        private static Dictionary<string, ConsoleMenuDelegate> consoleDelegates;

        public ConsoleMenu(){
            consoleDelegates = new Dictionary<string,ConsoleMenuDelegate>();
            consoleDelegates.Add("exit",       exitDelegate);
            consoleDelegates.Add("order",      orderDelegate);
            consoleDelegates.Add("menu-list",  menuListDelegate);
            consoleDelegates.Add("help",       helpDelegate);
        }

        /**
            Use the ConsoleMenu to process a line of text from the user
            If the user typed somthing invalid they will be alerted

            See functions in this classed named
            'private void *Delegate(string args)' for functionality available
         */
        public void processLine(string line){
            string[] spaceDelim = line.TrimStart().Split(' ');
            if(consoleDelegates.ContainsKey(spaceDelim[0])){
                Console.Write("[{0}]\n", spaceDelim[0]);
                consoleDelegates[spaceDelim[0]](line);
            }else{
                Console.Write("We can't process your command!\ntype 'help' to see all available commands\n");
            }
        }

        public void printWelcomeMessage(){
            Console.Write("Welcome to the Coffee Shop!\nEnter the command 'help' if you need help");

            Console.Write("\n\n");
        }

        /**
            Exit the application.
         */
        private void exitDelegate(string args){
            Console.Write("Thanks, have a good day!\n");
            Environment.Exit(0);
        }

        /**
            Order somthing
         */
        private void orderDelegate(string args){
            Console.Write("Thanks, for your order have a good day!\n");
        }

        /**
            Print a message to the console describing available
            commands in the menu and msc help information
         */
        private void helpDelegate(string args){
            Console.Write("Commands:\n");
            Console.Write("   order [item name] [extra 1] [extra 2] ...\n");
            Console.Write("   order [item name] [extra 1] [extra n],order [item name] [extra 1] [extra n] ...\n");
            Console.Write("   exit\n");
            Console.Write("   help\n");
        }

        /**
            List everything on the menu
         */
        private void menuListDelegate(string args){
            string[] spaceDelim = args.TrimStart().Split(' ');
            // List all
            listDrinks();
            listDrinkExtras();
            listFood();
            Console.Write("\n\n");
        }

        private void listDrinks(){
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

        private void listDrinkExtras(){
            Console.Write("Drink Extras:\n");
            foreach(DrinkExtra f in DaoFactory.DAO.getAllDrinkExtras()){
                Console.Write("   " + f.Name + "--" + "${0,-4:N2}", f.Price);
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        private void listFood(){
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