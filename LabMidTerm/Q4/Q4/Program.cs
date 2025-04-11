using System;
using System.Collections.Generic;
using System.Linq;

class GrammarAnalyzer
{
    public HashSet<string> NonTerminals { get; } = new HashSet<string>();
    private Dictionary<string, List<List<string>>> rules = new Dictionary<string, List<List<string>>>();
    private HashSet<string> terminals = new HashSet<string>();
    private Dictionary<string, HashSet<string>> firstSets = new Dictionary<string, HashSet<string>>();
    private Dictionary<string, HashSet<string>> followSets = new Dictionary<string, HashSet<string>>();

    public void AddRule(string ruleInput)
    {
        var parts = ruleInput.Split(new[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2)
            throw new ArgumentException("Invalid rule format");

        string lhs = parts[0].Trim();
        NonTerminals.Add(lhs);

        var productions = parts[1].Split('|')
            .Select(p => p.Trim())
            .Where(p => !string.IsNullOrEmpty(p))
            .ToList();

        foreach (var prod in productions)
        {
            var symbols = prod.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var symbol in symbols)
            {
                if (symbol != "ε" && !symbol.StartsWith("'") && !char.IsUpper(symbol[0]))
                {
                    terminals.Add(symbol);
                }
            }

            if (!rules.ContainsKey(lhs))
                rules[lhs] = new List<List<string>>();

            rules[lhs].Add(symbols);
        }
    }

    public bool HasLeftRecursion()
    {
        foreach (var nt in NonTerminals)
        {
            foreach (var production in rules[nt])
            {
                if (production.Count > 0 && production[0] == nt)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void ComputeFirstSets()
    {
        // Initialize FIRST sets
        foreach (var nt in NonTerminals)
        {
            firstSets[nt] = new HashSet<string>();
        }

        bool changed;
        do
        {
            changed = false;
            foreach (var nt in NonTerminals)
            {
                foreach (var production in rules[nt])
                {
                    foreach (var symbol in production)
                    {
                        if (terminals.Contains(symbol) || symbol == "ε")
                        {
                            if (firstSets[nt].Add(symbol))
                                changed = true;
                            break;
                        }
                        else if (NonTerminals.Contains(symbol))
                        {
                            var countBefore = firstSets[nt].Count;
                            firstSets[nt].UnionWith(firstSets[symbol].Where(s => s != "ε"));
                            if (firstSets[nt].Count > countBefore)
                                changed = true;

                            if (!firstSets[symbol].Contains("ε"))
                                break;
                        }
                    }
                }
            }
        } while (changed);
    }

    public void ComputeFollowSets()
    {
        foreach (var nt in NonTerminals)
        {
            followSets[nt] = new HashSet<string>();
        }

        followSets["E"].Add("$");

        bool changed;
        do
        {
            changed = false;
            foreach (var nt in NonTerminals)
            {
                foreach (var production in rules[nt])
                {
                    for (int i = 0; i < production.Count; i++)
                    {
                        var symbol = production[i];
                        if (!NonTerminals.Contains(symbol))
                            continue;

                        if (i < production.Count - 1)
                        {
                            var next = production[i + 1];
                            if (terminals.Contains(next))
                            {
                                if (followSets[symbol].Add(next))
                                    changed = true;
                            }
                            else
                            {
                                var countBefore = followSets[symbol].Count;
                                followSets[symbol].UnionWith(firstSets[next].Where(s => s != "ε"));
                                if (followSets[symbol].Count > countBefore)
                                    changed = true;

                                if (firstSets[next].Contains("ε"))
                                {
                                    countBefore = followSets[symbol].Count;
                                    followSets[symbol].UnionWith(followSets[nt]);
                                    if (followSets[symbol].Count > countBefore)
                                        changed = true;
                                }
                            }
                        }
                        else
                        {
                            var countBefore = followSets[symbol].Count;
                            followSets[symbol].UnionWith(followSets[nt]);
                            if (followSets[symbol].Count > countBefore)
                                changed = true;
                        }
                    }
                }
            }
        } while (changed);
    }

    public void PrintFirstAndFollowSets()
    {
        Console.WriteLine("\nFIRST Sets:");
        foreach (var nt in NonTerminals.OrderBy(n => n))
        {
            Console.WriteLine($"FIRST({nt}) = {{ {string.Join(", ", firstSets[nt].OrderBy(x => x))} }}");
        }

        Console.WriteLine("\nFOLLOW Sets:");
        foreach (var nt in NonTerminals.OrderBy(n => n))
        {
            Console.WriteLine($"FOLLOW({nt}) = {{ {string.Join(", ", followSets[nt].OrderBy(x => x))} }}");
        }
    }
}

class Program
{
    static void Main()
    {
        GrammarAnalyzer analyzer = new GrammarAnalyzer();

        Console.WriteLine("Enter grammar rules (one per line, e.g. 'E -> T X')");
        Console.WriteLine("Enter 'done' when finished\n");

        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine()?.Trim() ?? "";

            if (input.ToLower() == "done")
                break;

            try
            {
                analyzer.AddRule(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        if (analyzer.HasLeftRecursion())
        {
            Console.WriteLine("Grammar invalid for top-down parsing: Left recursion detected.");
            return;
        }

        analyzer.ComputeFirstSets();
        analyzer.ComputeFollowSets();
        analyzer.PrintFirstAndFollowSets();
    }
}