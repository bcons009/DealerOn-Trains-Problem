using System;

namespace DealerOn_Trains_Problem
{

    // Need two additional classes:
        // DirectedGraph (object, the directed graph itself)
        // OutputHandler (static class, handles all 10 output cases)

    class Program
    {
        // DirectedGraph CreateMap( text input from .txt )
            // Splits input by ', ' and stores split into array/list
            // Initializes new DirectedGraph object
            // Iterates through input array to add new nodes and/or edges
            // returns a new DirectedGraph object

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
            Console.WriteLine("Hello World!");

            // read input.txt, store as string (input_s)
            // DirectedGraph trainMap = CreateMap(input_s);
            // OutputCalls(trainMap);
        }
    }
}
