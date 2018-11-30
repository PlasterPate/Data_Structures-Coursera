namespace A9
{
    public class Thread
    {
        public int Idx { get; set; }
        public long Time { get; set; }

        public Thread(int i, long time)
        {
            Idx = i;
            Time = time;
        }
    }
}