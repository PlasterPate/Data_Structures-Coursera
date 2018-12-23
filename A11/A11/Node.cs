namespace A11
{
    internal class Node
    {
        private long key;
        private long parent;
        private long left;
        private long right;
        private bool isChecked;

        public Node(long key, long left, long right)
        {
            Key = key;
            Left = left;
            Right = right;
            IsChecked = false;
        }

        public void Check()
        {
            IsChecked = true;
        }

        public long Key { get => key; set => key = value; }
        public long Parent { get => parent; set => parent = value; }
        public long Left { get => left; set => left = value; }
        public long Right { get => right; set => right = value; }
        public bool IsChecked { get => isChecked; set => isChecked = value; }
    }
}