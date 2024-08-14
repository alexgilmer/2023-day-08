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

        string curLocation = "AAA";
        int stepCount = 0;
        while (curLocation != "ZZZ" && stepCount < 1_000_000)
        {
            int instruction = stepCount % instructionList.Length;
            char direction = instructionList[instruction];

            Node curNode = nodeList[curLocation];
            curLocation = direction switch
            {
                'R' => curNode.Right,
                'L' => curNode.Left,
                _ => throw new InvalidOperationException(),
            };
            stepCount++;
        }
        Console.WriteLine(stepCount);
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
