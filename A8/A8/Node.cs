using System.Collections.Generic;

namespace A8
{
    public class Node
    {
        public long Value { get; set; }
        public long Height { get; set; }
        public List<long> Childs { get; set; }

        public Node(long value)
        {
            Value = value;
            Childs = new List<long>();
        }

        public void AddChild(long child)
        {
            Childs.Add(child);
        }
    }
}