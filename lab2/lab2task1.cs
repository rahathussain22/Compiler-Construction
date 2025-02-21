using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a string containing logical operators:");
        string userInput = Console.ReadLine();

        // Regular expression pattern for logical operators: &&, ||, !
        string pattern = @"\&\&|\|\||!";

        // Find all matches
        MatchCollection matches = Regex.Matches(userInput, pattern);

        if (matches.Count > 0)
        {
            Console.WriteLine("Found logical operators:");
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
        }
        else
        {
            Console.WriteLine("No logical operators found.");
        }
    }
}