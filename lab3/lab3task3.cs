using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the document text:");
        string userInput = Console.ReadLine();

        // Regular expression to find words starting with 't' or 'm' (case-insensitive)
        string pattern = @"\b[tTmM]\w*\b";

        // Find all matches
        MatchCollection matches = Regex.Matches(userInput, pattern);

        if (matches.Count > 0)
        {
            Console.WriteLine("\nWords starting with 't' or 'm':");
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
        }
        else
        {
            Console.WriteLine("\nNo words found starting with 't' or 'm'.");
        }
    }
}