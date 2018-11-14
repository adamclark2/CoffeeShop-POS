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
            Console.Write("Commands:\n");
            Console.Write("   order [item name] [extra 1] [extra 2] ...\n");
            Console.Write("   order [item name] [extra 1] [extra n],order [item name] [extra 1] [extra n] ...\n");
            Console.Write("   exit\n");
            Console.Write("   help\n");
        }
    }

}