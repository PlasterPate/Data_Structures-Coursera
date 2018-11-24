using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class CheckBrackets : Processor
    {
        public CheckBrackets(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, long>)Solve);

        public long Solve(string str)
        {
            Dictionary<char, char> brackets = new Dictionary<char, char>() {{ '(', ')' },
                                                                            { '[', ']' },
                                                                            { '{', '}' }};
            Stack<(char, int)> stack = new Stack<(char, int)>();
            for (int i = 0; i < str.Length; i++)
            {
                if (brackets.ContainsKey(str[i]))
                {
                    stack.Push((str[i], i));
                }
                else if (brackets.ContainsValue(str[i]))
                {
                    if (stack.Count > 0)
                    {
                        var lastElement = stack.Peek();
                        if (brackets[lastElement.Item1] == str[i])
                        {
                            stack.Pop();
                        }
                        else
                        {
                            return i + 1;
                        }
                    }
                    else
                    {
                        return i + 1;
                    }
                }
            }
            if (stack.Count == 0)
                return -1;
            else
                return stack.Peek().Item2 + 1;
        }
    }
}
