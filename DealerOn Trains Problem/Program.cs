using System;
using System.IO;

namespace DealerOn_Trains_Problem
{
    class Program
    {
        // Splits input by ', ' and stores split into array/list
        // Check that each string in array is valid input
        // Return string array if input is valid, otherwise null
        /// <summary>
        /// A method that parses the user's input. Checks if the user input is correctly
        /// formatted for use by the program.
        /// </summary>
        /// <param name="txt">The string containing all of the characters found in input.txt</param>
        /// <returns>String array containing all the elements in the list of strings found in the 
        /// user input, if the input is in the correct format. Otherwise, returns null.</returns>
        static string[] ParseInput(string txt)
        {
            // Assume input strings are separated by commas
            string[] input = txt.Split(",");
            string[] parsedInput = new string[input.Length];
            char[] trimChars = { '\n', ' ', '\t', '\r', ',' };

            for(int i = 0; i < input.Length; i++)
            {
                // Remove any whitespace characters or extra commas
                string temp = input[i].Trim(trimChars);
                int num;

                // Other assumptions made about input:
                    // First two characters of each string are letters
                    // Remaining substring forms a number (integer)
                if(Char.IsLetter(temp[0]) && Char.IsLetter(temp[1]) && Int32.TryParse(temp[2..], out num))
                    parsedInput[i] = temp;
                else return null;
            }
            return parsedInput;
        }


        // Creates a directed graph to represent the train routes of the town. Iterates through 
        // each string element of the input array to create up to two new towns and a new route between
        // those two towns (if a route doesn't exist between them already).
        /// <summary>
        /// Initializes and returns a new DirectedGraph object.
        /// </summary>
        /// <param name="input">The user input, converted into a string array. Each element of the array
        /// is used to create up to two new Node objects and an Edge object.</param>
        /// <returns>A new DirectedGraph object initialized using the user input.</returns>
        static DirectedGraph CreateMap(string[] input)
        {
            DirectedGraph map = new DirectedGraph();
            foreach(string route in input)
            {
                Node startTown = map.AddNode(Char.ToUpper(route[0]));
                Node endTown = map.AddNode(Char.ToUpper(route[1]));
                int weight = Int32.Parse(route[2..]);
                startTown.AddEdge(endTown, weight);
            }
            return map;
        }

        static void Main(string[] args)
        {
            // Changes the current directory to the first "DealerOn Trains Problem" directory
            // (where all the .cs files are located)
            Environment.CurrentDirectory = @"..\..\..\";
            string txt, filePath = Directory.GetCurrentDirectory() + @"\input.txt";
            if (!File.Exists(filePath))
            {
                // Error message if the file is not found in the current directory.
                // Program terminates after printing this message.
                Console.WriteLine("Error: \"input.txt\" file does not exist in current directory.");
                Console.WriteLine("Please include an \"input.txt\" file in the following directory:");
                Console.WriteLine(Directory.GetCurrentDirectory());
                return;
            }

            txt = File.ReadAllText(filePath);
            Console.WriteLine("Contents of input.txt:\n" + txt + '\n');
            string[] input = ParseInput(txt);   // input.txt contents are parsed
            if (input == null)
            {
                // If the input.txt contents are not in the correct format, this error message is displayed.
                // Program also terminates after printing this message.
                Console.WriteLine("Error: \"input.txt\" contains improper input formatting.");
                Console.WriteLine("This program assumes that all input is a list of two letters " +
                    "and an integer for each entry (ex. \"AE5\"), with a comma used to separate" +
                    "every entry.");
                Console.WriteLine("Example: \"AE5, CB10, DA2\"");
                return;
            }

            DirectedGraph trainMap = CreateMap(input);
            for(int i = 1; i <= 10; i++)
                OutputClass.CaseHandler(i, trainMap);
        }
    }
}
