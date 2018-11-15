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
        This is a function type, parts of the console menu are 'delegated'
        to helper functions
     */
    public delegate void ConsoleMenuDelegate(string args);

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
            consoleDelegates.Add("q",          exitDelegate);
            consoleDelegates.Add("order",      orderDelegate);
            consoleDelegates.Add("help",       helpDelegate);

            DrinkFoodMenuMaintenance.addMenuOptions(consoleDelegates);
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
                try{
                    consoleDelegates[spaceDelim[0]](line);
                }catch {
                    Console.Write("The command spceified can't be completed due to a system error.\n");
                }
                Console.Write("\n");
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
            CustomerReceipt receipt = new CustomerReceipt();
            receipt.addOrders(args);

            if(!Directory.Exists("outputs")){
                Directory.CreateDirectory("outputs");
            }
            
            FileStream fs = File.Create("outputs/ADAM.CLARK." + Program.number + ".json");
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Order));
            XmlDictionaryWriter wr = JsonReaderWriterFactory.CreateJsonWriter(fs,Encoding.UTF8, true,true, "  ");
            ser.WriteObject(wr, receipt.order);
            wr.Flush();
            wr.Close();
            fs.Close();


            fs = File.Create("outputs/ADAM.CLARK."+ Program.number +".receipt.json");
            wr = JsonReaderWriterFactory.CreateJsonWriter(fs,Encoding.UTF8, true,true, "  ");
            ser = new DataContractJsonSerializer(typeof(CustomerReceipt));
            ser.WriteObject(wr, receipt);
            wr.Flush();
            wr.Close();
            fs.Close();

            Console.Write("Receipt:\n{0}\n", receipt);
            Console.Write("\nThanks, for your order have a good day!\n");
        }

        /**
            Print a message to the console describing available
            commands in the menu and msc help information
         */
        private void helpDelegate(string args){
            Console.Write("\nTo use a command, at the prompt enter a command then press enter\n\n");
            Console.Write("Commands:\n");
            Console.Write("   Order an Item:\n");
            Console.Write("      order [item name] [size] [extra 1] ...\n");
            Console.Write("      order [item name] [size] [extra ...],order [item name] [size] [extra n] ...\n");
            Console.Write("      order [item name] [size] [extra 1] [extra 2],order [item name] [size],order [item name] [size] [extra 1] [extra 2] [extra 3] ...\n");
            Console.Write("\n");            
            Console.Write("   Exit Application:\n");
            Console.Write("      exit\n");
            Console.Write("      q\n");
            Console.Write("\n");            
            Console.Write("   Display help prompt:\n");
            Console.Write("      help\n");

            Console.Write("\n");
            Console.Write("   Display the menu:\n");
            Console.Write("      menu-list\n");

            Console.Write("\n");
            Console.Write("   Add somthing to the menu:\n");
            Console.Write("      menu-add-food [item name]\n");
            Console.Write("      menu-add-food-size [food name] [size name] [item price]\n");
            Console.Write("      menu-add-food-extra [food name] [extra name] [price]\n");
            Console.Write("\n");
            Console.Write("      menu-add-drink [drink name]\n");
            Console.Write("      menu-add-drink-size [drink name] [size name] [price]\n");
            Console.Write("      menu-add-drink-extra [extra name] [price]\n");

            Console.Write("\n");
            Console.Write("   Remove somthing from the menu:\n");
            Console.Write("      menu-remove-drink [drink name]:\n");
            Console.Write("      menu-remove-food [food name]:\n");
            Console.Write("      menu-remove-drinkextra [extra name]:\n");

        }
    }

}