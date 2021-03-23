# DealerOn-Trains-Problem
My solution for the Trains problem presented in the DealerOn Development Candidate Coding Test.

## Design Explanation
The program is composed of five classes. Three of these classes (`DirectedGraph`, `Node`, and `Edge`) are used to model a directed graph in OOP. The Program class contains the program's main method, as well as methods to parse user input from a specified text file and to create a new DirectedGraph object from that parsed input. Finally, OutputClass is a static class that contains methods to handle all of the output cases. All program output is displayed on the console window.

## Assumptions regarding User Input
* The program reads the  `input.txt` file in the `DealerOn Trains Problem` directory to get the user's input. The user is free to make any changes to the text file to edit their input to the program.
* All input must be a list of strings, with each string in the format of `XYn`: `X` and `Y` being any two letters in the alphabet, and `n` being any integer. Each string in this list must be separated by a comma.
  * If these input conditions are not met, the program will detect that the input is improperly formatted and will throw an error message warning the user of their input's formatting.
  * If no `input.txt` file is found within the specified directory, the program will throw an error message warning that the program could not find the required text file.
