using System;
using DataAbstration;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Model
{

    /*
        Convert a string into a List<List<string>>

        Symbols:
        Space is the delimiter for tokens, unless it's in a quote set
        Quote can be used to create a token with spaces "Hello World" is one token
        Comma ',' denotes the end of a token list, e.g the List<string> inside the return value



        Examples:
        order "coffee" small "espresso shot"
        List<List<string>>[0][0] := order
        List<List<string>>[0][1] := coffee
        List<List<string>>[0][2] := small
        List<List<string>>[0][3] := espresso shot

        order "coffee" small "espresso shot","muffin" big
        List<List<string>>[0][0] := order
        List<List<string>>[0][1] := coffee
        List<List<string>>[0][2] := small
        List<List<string>>[0][3] := espresso shot

        List<List<string>>[1][0] := "" // This is a filler to preserve spacing
        List<List<string>>[1][1] := muffin
        List<List<string>>[1][2] := big
     */
    public class Tokenizer{

        /*
            The method to tokenize a String. See class description.
         */
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
                    while(arr[i].Equals(' ')){i++;}
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
                        while(arr[i].Equals(' ')){i++;}
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