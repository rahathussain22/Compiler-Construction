using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a string containing relational operators:");
        string userInput = Console.ReadLine();

        // Regular expression pattern for relational operators: ==, !=, >, <, >=, <=
        string pattern = @"==|!=|>=|<=|>|<";

        // Find all matches
        MatchCollection matches = Regex.Matches(userInput, pattern);

        if (matches.Count > 0)
        {
            Console.WriteLine("Found relational operators:");
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
        }
        else
        {
            Console.WriteLine("No relational operators found.");
        }
    }
}