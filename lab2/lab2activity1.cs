using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string input = "Hello    World     My name   is Rahat    Hussain      !";
        string pattern = "\\s+";
        string replacement = " ";

        Regex rgx = new Regex(pattern);
        string result = rgx.Replace(input, replacement);

        Console.WriteLine("Original String: " + input);
        Console.WriteLine("Replacement String: " + result);
    }
}