using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class PacketProcessing : Processor
    {
        public PacketProcessing(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[], long[]>)Solve);

        public long[] Solve(long bufferSize, 
            long[] arrivalTimes, 
            long[] processingTimes)
        {
            List<Packet> packets = arrivalTimes.Zip(processingTimes, (a, p) => new Packet(a, p, -1)).ToList();
            for (int i = 0; i < packets.Count; i++)
            {
                packets[i].Idx = i;
            }
            Queue<Packet> buffer = new Queue<Packet>((int)bufferSize);
            if (packets.Count == 0)
                return new long[] { };
            long time = packets[0].ArrivalTime;
            for (int i = 0; i < packets.Count; i++)
            {
                if(buffer.Any() && packets[i].ArrivalTime >= time + buffer.First().ProcessingTime)
                {
                    var p = buffer.Dequeue();
                    packets[p.Idx].BeginTime = Math.Max(p.ArrivalTime, time);
                    time = Math.Max(p.ArrivalTime, time) + p.ProcessingTime;
                }
                if (buffer.Count < bufferSize )
                {
                    buffer.Enqueue(packets[i]);
                }
            }
            while (buffer.Any())
            {
                var p = buffer.Dequeue();
                packets[p.Idx].BeginTime = Math.Max(p.ArrivalTime, time);
                time = Math.Max(p.ArrivalTime, time) + p.ProcessingTime;
            }

            return packets.Select(x => x.BeginTime).ToArray();
        }
    }
}
