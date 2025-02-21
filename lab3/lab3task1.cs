using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string[] inputs = { "123.45", "-12.34", "0.123", "999999", "1234567", "123.456", "-12345.6" };
        string pattern = "^-?\\d{1,6}(\\.\\d{1,5})?$";
        
        Console.WriteLine("Floating Point Number Validation (Max Length 6):\n");
        foreach (string input in inputs)
        {
            bool isMatch = Regex.IsMatch(input, pattern);
            Console.WriteLine($"Input: {input} => Valid: {isMatch}");
        }
    }
}