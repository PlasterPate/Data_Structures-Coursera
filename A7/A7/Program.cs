using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A7
{
    class Program
    {
        static void Main(string[] args)
        {
            MaximizingArithmeticExpression a = new MaximizingArithmeticExpression("abc");
            a.Solve("9*1+6");
        }
    }
}
