﻿namespace _2023_day_08;

internal class Program
{
    static void Main(string[] args)
    {
        bool testMode = true;
        IList<string> input = testMode ? GetTestInput() : GetPuzzleInput();


    }

    static IList<string> GetPuzzleInput()
    {
        string file = Path.Combine(Environment.CurrentDirectory, "puzzle-input.txt");
        using StreamReader sr = new StreamReader(file);
        List<string> input = [];

        while (!sr.EndOfStream)
        {
            input.Add(sr.ReadLine()!);
        }

        return input;
    }

    static IList<string> GetTestInput()
    {
        return [
            "32T3K 765",
            "T55J5 684",
            "KK677 28",
            "KTJJT 220",
            "QQQJA 483"
            ];
    }

}
