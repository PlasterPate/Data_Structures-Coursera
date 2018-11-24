namespace A8
{
    internal class Packet
    {
        private long arrivalTime;
        private long processingTime;
        private long beginTime;
        private int idx;

        public long ArrivalTime { get => arrivalTime; set => arrivalTime = value; }
        public long ProcessingTime { get => processingTime; set => processingTime = value; }
        public long BeginTime { get => beginTime; set => beginTime = value; }
        public int Idx { get => idx; set => idx = value; }

        public Packet(long arrivalTime, long processingTime, int beginTime)
        {
            this.ArrivalTime = arrivalTime;
            this.ProcessingTime = processingTime;
            this.BeginTime = beginTime;
        }
    }
}