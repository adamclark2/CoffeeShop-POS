using System;
using System.Collections.Generic;

namespace Console_App
{
    delegate void ConsoleMenuDelegate(string args);

    public class ConsoleMenu{
        private static Dictionary<string, ConsoleMenuDelegate> consoleDelegates = new Dictionary<string,ConsoleMenuDelegate>();

        public ConsoleMenu(){
            consoleDelegates = new Dictionary<string,ConsoleMenuDelegate>();
            consoleDelegates.Add("exit",  exitDelegate);
            consoleDelegates.Add("order", orderDelegate);
            consoleDelegates.Add("help",  helpDelegate);
        }

        public void processLine(string line){
            string[] spaceDelim = line.Split(' ');
            if(consoleDelegates.ContainsKey(spaceDelim[0])){
                consoleDelegates[spaceDelim[0]](line);
            }else{
                Console.Write("We can't process your command!\ntype 'help' to see all available commands\n");
            }
        }

        private void exitDelegate(string args){
            Console.Write("Thanks, have a good day!\n");
            Environment.Exit(0);
        }

        private void orderDelegate(string args){
            Console.Write("Thanks, for your orderhave a good day!\n");
        }

        private void helpDelegate(string args){
            Console.Write("Help:\n");
            Console.Write("   order [item name] [extra 1] [extra 2] ...\n");
            Console.Write("   order [item name] [extra 1] [extra n],order [item name] [extra 1] [extra n] ...\n");
            Console.Write("   exit\n");
            Console.Write("   help\n");
        }
    }

}