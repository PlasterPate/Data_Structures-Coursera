using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A5
{
    class PointData : IComparable<PointData>
    {
        public long value = 0;
        public int type = 0;
        public int index = 0;
        public PointData(long value, int type)
        {
            this.value = value;
            this.type = type;
        }

        public PointData(long value, int type, int index)
        {
            this.value = value;
            this.type = type;
            this.index = index;
        }

        public int CompareTo(PointData other)
        {
            if (this.value == other.value)
                return this.type.CompareTo(other.type);
            return this.value.CompareTo(other.value);
                
        }
    }
}
