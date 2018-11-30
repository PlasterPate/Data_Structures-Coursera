namespace A9
{
    internal class Table
    {
        public long Size { get; set; }
        public int Link { get; set; }

        public Table(int link, long size)
        {
            Link = link;
            Size = size;
        }
    }
}