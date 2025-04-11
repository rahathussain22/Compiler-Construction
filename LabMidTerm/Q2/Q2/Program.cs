// question 2
using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter your code (e.g., 'var a1 = 12@; float b2 = 3.14$$;'):");
        string inputCode = Console.ReadLine();

        // Updated regex to match:
        // 1. Variables starting with a/b/c followed by digits
        // 2. Values containing at least one special character (@#$%^&)
        string pattern = @"\b(?:var|float|int|string)\s+([abc]\d+)\s*=\s*([^;]*[@#$%^&][^;]*);";

        MatchCollection matches = Regex.Matches(inputCode, pattern);

        Console.WriteLine("\nExtracted Variables:");
        foreach (Match match in matches)
        {
            if (match.Groups.Count == 3)
            {
                string varName = match.Groups[1].Value;
                string varValue = match.Groups[2].Value.Trim();
                Console.WriteLine($"{varName} = {varValue}");
            }
        }

        if (matches.Count == 0)
            Console.WriteLine("No matching variables found.");
    }
}
