namespace _2023_day_08;

internal class Program
{
    static void Main(string[] args)
    {
        bool testMode = false;
        IList<string> input = testMode ? GetTestInput() : GetPuzzleInput();

        string instructionList = input[0];

        Dictionary<string, Node> nodeList = new();
        for (int i = 2; i < input.Count; i++)
        {
            string id = input[i][0..3];
            string left = input[i][7..10];
            string right = input[i][12..15];
            nodeList.Add(id, new Node(id, left, right));
        }

        string[] curLocations = nodeList.Keys.Where(s => s[2] == 'A').ToArray();

        // After some testing, and over 1e9 iterations with no answer,
        // I performed some analysis on the data set. 
        // I learned this:
        // Each ghost only arrives at an exit after nk moves,
        // where k is the length of the instruction list
        // and n is some positive integer. 
        // That ghost then finds another (or, more likely, the same)
        // exit at 2nk, 3nk, 4nk, etc steps. 
        // The probability of this occurring randomly is... insane. 
        // The instructions did not mention this. 
        // New solution baased on this:
        // Deduce each ghost's n-value. 
        // Calculate least-common-denominator of them. 
        // Multiply by k => answer. 

        long[] nValues = new long[curLocations.Length];

        for (long i = 0; i < curLocations.Length; i++)
        {
            string thisLocation = curLocations[i];
            long j = 0;
            while (thisLocation[2] != 'Z')
            {
                j++;
                for (int k = 0; k < instructionList.Length; k++)
                {
                    char instruction = instructionList[k];
                    thisLocation = instruction switch
                    {
                        'R' => nodeList[thisLocation].Right,
                        'L' => nodeList[thisLocation].Left,
                        _ => throw new InvalidOperationException()
                    };
                }
            }
            nValues[i] = j;
        }

        long lcm = LCM(nValues);
        Console.WriteLine(lcm);
        long result = instructionList.Length * lcm;
        Console.WriteLine(result);
    }

    static long GCD(long a, long b)
    {
        if (b == 0)
            return a;
        return GCD(b, a % b);
    }

    static long LCM(long a, long b)
    {
        return Math.Abs(a * b) / GCD(a, b);
    }

    static long LCM(long[] numbers)
    {
        return numbers.Aggregate(LCM);
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
        // not doing test inputs for this one
        throw new NotImplementedException();
    }
}
