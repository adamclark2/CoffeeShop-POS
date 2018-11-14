using System;
using DataAbstration;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Model
{
    public class Tokenizer{
        public static List<List<string>> tokenizeString(string orderStr){
            List<List<string>> orders = new List<List<string>>();
            List<string> tokens = new List<string>();
            char[] arr = orderStr.ToCharArray();

            int i = 0;
            StringBuilder bldr = new StringBuilder();
            while(i < arr.Length){
                if(arr[i].Equals(' ')){
                    string s = bldr.ToString();
                    tokens.Add(s);
                    bldr.Clear();
                    i++;
                }else if(arr[i].Equals('\"')){
                    i++;
                    // Keep adding until quote
                    while(i < arr.Length && arr[i] != '\"'){
                        bldr.Append(arr[i++]);
                    }
                    if(i > arr.Length){
                        // Error!
                        System.Console.Write("Lexing error!, You didn't end a quote!\n");
                    }
                    i++;
                }else if(arr[i].Equals(',')){
                    string s = bldr.ToString();
                    tokens.Add(s);
                    bldr.Clear();

                    orders.Add(tokens);
                    tokens = new List<string>();
                    tokens.Add(".");

                    i++;
                    while(arr[i].Equals(' ')){
                        i++;
                    }
                }else{
                    bldr.Append(arr[i++]);
                }
            }
            tokens.Add(bldr.ToString());
            orders.Add(tokens);

            return orders;
        }
    }
}