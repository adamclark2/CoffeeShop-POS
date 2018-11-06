﻿using System;
using DataRepository;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Console_App
{
    class Program
    {
        private static ConsoleMenu menu;
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

            menu = new ConsoleMenu();

            OpenFile(args[0]);
            PrintData();
            ProcessInput();
        }

        static void PrintData(){
            System.Console.Write("\n\nThe first food is: ");
            System.Console.Write(DataRepoFactory.Repo.getAllFoods()[0].Name);
            System.Console.Write("\n\n");
            DataRepoFactory.Repo.getAllFoods()[0].print();
        }

        static void ProcessInput(){
            while(true){
                string ln = System.Console.ReadLine();
                menu.processLine(ln);
            }
        }

        static void OpenFile(string fileName){
            if(!File.Exists(fileName)){
                System.Console.Write("File doesn't exist\n");
                System.Environment.Exit(2);
            }

            try{
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JsonDataRepo));
                FileStream fs = File.Open(fileName, FileMode.Open);
                fs.Position = 0;
                JsonDataRepo r = (JsonDataRepo) ser.ReadObject(fs);
                DataRepoFactory.setupFactory(r);
            }catch (Exception e){
                System.Console.Write("Cannot open the file specified\n");
                System.Environment.Exit(2);
            }
            
        }
    }
}
