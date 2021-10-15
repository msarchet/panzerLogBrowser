namespace PanzerLogBrowser
{
    using System.Collections.Generic;

    public class SearchNode
    {
        public SearchNode Parent { get; set; }

        public List<SearchNode> Children { get; set; } = new List<SearchNode>();

        public string Contents { get; set; }

        public int Depth { get; set; }
    }
}
