using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a constant (integer or floating-point number):");
        string userInput = Console.ReadLine();

        // Regular expression for constants (digits and floating-point numbers)
        string pattern = @"^[0-9]+(\.[0-9]+)?([eE][+-]?[0-9]+)?$";

        if (Regex.IsMatch(userInput, pattern))
        {
            Console.WriteLine($"'{userInput}' is a valid constant.");
        }
        else
        {
            Console.WriteLine($"'{userInput}' is NOT a valid constant.");
        }
    }
}
