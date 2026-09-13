namespace cyoa_core.src
{
    class StoryDefinition
    {
        public StoryDefinition(Node start, List<Chapter> chapters)
        {
            Start = start;
            Chapters = chapters;
            NodeToChapter = PopulateNTC();
            NodeToChapter[Start].EntryNodes.Add(Start);
        }

        public Node Start { get; set; }
        public List<Chapter> Chapters { get; set; }
        public Dictionary<Node, Chapter> NodeToChapter { get; set; }

        private Dictionary<Node, Chapter> PopulateNTC()
        {
            Dictionary<Node, Chapter> ntc = [];

            foreach (var chapter in Chapters)
            {
                foreach (var node in chapter.Nodes)
                {
                    ntc[node] = chapter;
                    // Since I'm already looping through all the nodes
                    TagEntryNode(node);
                }
            }

            return ntc;
        }

        public void TagEntryNode(Node node)
        {
            if (node is Page p)
            {
                if (p.Next != null && NodeToChapter[p.Next] != NodeToChapter[p])
                {
                    NodeToChapter[p.Next].EntryNodes.Add(p.Next);
                }
            }
            else if (node is Branch b)
            {
                foreach (var choice in b.Choices)
                {
                    if (NodeToChapter[choice.Next] != NodeToChapter[b])
                    {
                        NodeToChapter[choice.Next].EntryNodes.Add(choice.Next);
                    }
                }
            }
            else if (node is ConditionalBranch c)
            {
                foreach (var cr in c.Routes)
                {
                    if (NodeToChapter[cr.Then] != NodeToChapter[c])
                    {
                        NodeToChapter[cr.Then].EntryNodes.Add(cr.Then);
                    }
                }

                if (NodeToChapter[c.Default] != NodeToChapter[c])
                {
                    NodeToChapter[c.Default].EntryNodes.Add(c.Default);
                }
            }
        }
    }
}
