namespace PanzerLogBroswer
{
    using PanzerLogBrowser;
    using System;
    using System.Collections.Generic;
    using System.IO;

    class Program
    {
        static SearchNode currentNode;
        static SearchNode topNode;
        static void Main(string[] args)
        {
            using (var stream = new StreamReader("bad-pawn-e2e3-sorted.log"))
            {
                var line = stream.ReadLine();
                currentNode = MakeNode(line);
                topNode = currentNode;
                while (stream.Peek() >= 0)
                {
                    line = stream.ReadLine();
                    var nextNode = MakeNode(line);


                    if (nextNode.Depth > currentNode.Depth)
                    {
                        nextNode.Parent = currentNode;
                        currentNode.Children.Add(nextNode);
                    }
                    else if (nextNode.Depth == currentNode.Depth)
                    {
                        nextNode.Parent = currentNode.Parent;
                        currentNode.Parent.Children.Add(nextNode);
                    }
                    else // <
                    {
                        while (nextNode.Depth <= currentNode.Depth)
                        {
                            currentNode = currentNode.Parent;
                        }

                        nextNode.Parent = currentNode;
                        currentNode.Children.Add(nextNode);
                    }

                    currentNode = nextNode;
                }
            }

            currentNode = topNode;
            string input = "";
            while (!input.Equals("exit"))
            {
                Console.WriteLine($"{currentNode.Depth} {currentNode.Contents}");
                int i = 0;
                foreach (var child in currentNode.Children)
                {
                    Console.WriteLine($"{i} : {child.Depth} {child.Contents }");
                    i++;
                }

                Console.WriteLine("Type number of node to go to, or up to go up a level");
                input = Console.ReadLine();

                if (int.TryParse(input, out int node) && node < i)
                {
                    currentNode = currentNode.Children[node];
                }
                else if (input == "up" && currentNode.Parent != null)
                {
                    currentNode = currentNode.Parent;
                }
                else if (input != "exit")
                {
                    Console.WriteLine("Invalid input.");
                }
            }

            return;
        }

        static SearchNode MakeNode(string line)
        {
            SearchNode newNode = new SearchNode();
            var parts = line.Split(":", 2);
            var depth = int.Parse(parts[0]);
            newNode.Depth = depth;
            newNode.Contents = parts[1];
            return newNode;
        }
    }
}
