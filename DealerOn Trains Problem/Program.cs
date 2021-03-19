using System;
using System.IO;

namespace DealerOn_Trains_Problem
{

    // Need two additional classes:
        // DirectedGraph (object, the directed graph itself)
        // OutputHandler (static class, handles all 10 output cases)

    class Program
    {
        // Splits input by ', ' and stores split into array/list
        // Check that each string in array is valid input
        // Return string array if input is valid, otherwise null
        static string[] ParseInput(string txt)
        {
            string[] input = txt.Split(",");
            string[] parsedInput = new string[input.Length];
            char[] trimChars = { '\n', ' ', '\t', '\r', ',' };

            for(int i = 0; i < input.Length; i++)
            {
                string temp = input[i].Trim(trimChars);
                int num;

                if(Char.IsLetter(temp[0]) && Char.IsLetter(temp[1]) && Int32.TryParse(temp[2..], out num))
                    parsedInput[i] = temp;
                else return null;
            }
            return parsedInput;
        }

        // Initializes new DirectedGraph object
        // Iterates through input array to add new nodes and/or edges
        // returns a new DirectedGraph object
        static DirectedGraph CreateMap(string[] input)
        {
            DirectedGraph map = new DirectedGraph();
            foreach(string str in input)
            {
                Node n = map.AddNode(str[0]);
                Node m = map.AddNode(str[1]);
                int weight = Int32.Parse(str[2..]);

                if (m != null)
                {
                    n.AddEdge(m, weight);
                }                
            }
            return map;
        }

        // void OutputCalls(DirectedGraph map)
            // make 10 calls to the static "OutputHandler" class, one for each test case

        // move to static "OutputHandler" class
        void Print(int n, string result) {
            string output = "Output #" + n + ": " + result;
            Console.WriteLine(output);
        }


        // Make sure to add line warning about auto-closing console window
        static void Main(string[] args)
        {
            Environment.CurrentDirectory = @"..\..\..\";
            string filePath = Directory.GetCurrentDirectory() + @"\input.txt";
            string txt;
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: \"input.txt\" file does not exist in current directory.");
                Console.WriteLine("Please include an \"input.txt\" file in the following directory:");
                Console.WriteLine(Directory.GetCurrentDirectory());
                return;
            }

            txt = File.ReadAllText(filePath);
            Console.WriteLine("Contents of input.txt:\n" + txt + '\n');
            string[] input = ParseInput(txt);
            if (input == null)
            {
                Console.WriteLine("Error: \"input.txt\" contains improper input formatting.");
                Console.WriteLine("This program assumes that all input is a list of two letters " +
                    "and an integer for each entry (ex. \"AE5\"), with a comma used to separate" +
                    "every entry.");
                Console.WriteLine("Example: \"AE5, CB10, DA2\"");
                return;
            }

            DirectedGraph trainMap = CreateMap(input);

            // OutputCalls(trainMap);
        }
    }
}
