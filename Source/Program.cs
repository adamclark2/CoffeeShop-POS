﻿using System;
using DataAbstration;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Console_App
{
    /**
        The 'Program' class sets up the Console_App
     */
    class Program
    {
        private static ConsoleMenu menu;
        public static string number = "ERROR";

        static void Main(string[] args)
        {
            if(args.Length == 0){
                System.Console.Write("Please specify a JSON file on the command line\n");
                System.Environment.Exit(1);
            }

            if(args.Length > 1){
                System.Console.Write("Please specify only ONE JSON file on the command line\n");
                System.Environment.Exit(2);
            }

            if(!File.Exists(args[0])){
                System.Console.Write("File doesn't exist\n");
                System.Environment.Exit(2);
            }

            char[] arr = args[0].ToCharArray();
            int endIdx = -1;
            int startIdx = -1;
            for(int i = arr.Length - 1; i >= 0;i--){
                if(endIdx == -1 && arr[i].Equals('.')){
                    endIdx = i;
                }else if(startIdx == -1 && arr[i].Equals('.')){
                    startIdx = i + 1;
                }
            }
            Program.number = args[0].Substring(startIdx, endIdx - startIdx);



            menu = new ConsoleMenu();

            OpenFile(args[0]);
            ProcessInput();
        }

        /**
            Print the first food in the Dao
         */
        static void PrintData(){
            System.Console.Write("\n\nThe first food is: ");
            System.Console.Write(DaoFactory.DAO.getAllFoods()[0].Name);
            System.Console.Write("\n\n");
            DaoFactory.DAO.getAllFoods()[0].print();
        }

        /**
            Begin processing input, 
            this should be done after setup
         */
        static void ProcessInput(){
            menu.printWelcomeMessage();
            while(true){
                Console.Write("Enter a Command:\n");
                string ln = System.Console.ReadLine();
                menu.processLine(ln);
            }
        }

        /**
            Open the JSON file, load it into a Dao, and tell the Factory
            for service discovery. See DaoFactory for more information.
         */
        static void OpenFile(string fileName){
            try{
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JsonDao));
                FileStream fs = File.Open(fileName, FileMode.Open);
                fs.Position = 0;
                JsonDao r = (JsonDao) ser.ReadObject(fs);
                DaoFactory.setupFactory(r);
            }catch{
                System.Console.Write("Cannot open the file specified\n");
                System.Environment.Exit(2);
            }
            
        }
    }
}
