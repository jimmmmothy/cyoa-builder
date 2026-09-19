namespace cyoa_core.src
{
    public class StoryDefinition
    {
        public StoryDefinition(Node start, List<Chapter> chapters)
        {
            Start = start;
            Chapters = chapters;
            VariableDeclarations = [];
            NodeToChapter = PopulateNTC();
            NodeToChapter[Start].EntryNodes.Add(Start);
            TagEntryNodes();
        }

        public Node Start { get; set; }
        public List<Chapter> Chapters { get; set; }
        public Dictionary<Node, Chapter> NodeToChapter { get; set; }
        public List<Variable> VariableDeclarations { get; set; }

        private Dictionary<Node, Chapter> PopulateNTC()
        {
            Dictionary<Node, Chapter> ntc = [];

            foreach (var chapter in Chapters)
            {
                foreach (var node in chapter.Nodes)
                {
                    ntc[node] = chapter;
                }
            }

            return ntc;
        }

        // Unfortunately I couldn't tag entry nodes during the same loop that 
        // populates NTC since node.Next will almost always be a node which hasn't 
        // been added to NTC yet, so I can't index it 
        private void TagEntryNodes()
        {
            foreach (var node in NodeToChapter.Keys)
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
}
