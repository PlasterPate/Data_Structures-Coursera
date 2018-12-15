using System;
using System.Linq;
using System.Collections.Generic;
using TestCommon;

namespace A10
{
    public class Contact
    {
        public string Name;
        public int Number;

        public Contact(string name, int number)
        {
            Name = name;
            Number = number;
        }
    }

    public class PhoneBook : Processor
    {
        public PhoneBook(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string[], string[]>)Solve);

        protected List<List<Contact>> PhoneBookHashTable;

        public const int cardinality = 1000;

        public string[] Solve(string [] commands)
        {
            PhoneBookHashTable = new List<List<Contact>>(cardinality);
            for (int i = 0; i < PhoneBookHashTable.Capacity; i++)
            {
                PhoneBookHashTable.Add(new List<Contact>());
            }
            List<string> result = new List<string>();
            foreach(var cmd in commands)
            {
                var toks = cmd.Split();
                var cmdType = toks[0];
                var args = toks.Skip(1).ToArray();
                int number = int.Parse(args[0]);
                switch (cmdType)
                {
                    case "add":
                        Add(args[1], number);
                        break;
                    case "del":
                        Delete(number);
                        break;
                    case "find":
                        result.Add(Find(number));
                        break;
                }
            }
            return result.ToArray();
        }

        public int HashFunction(int n)
        {
            int a = 23, b = 2, p = 10000007;
            return ((a * n + b) % p) % cardinality;
        }

        public void Add(string name, int number)
        {
            int idx = HashFunction(number);
            for(int i=0; i<PhoneBookHashTable[idx].Count; i++)
            {
                if (PhoneBookHashTable[idx][i].Number == number)
                {
                    PhoneBookHashTable[idx][i].Name = name;
                    return;
                }
            }
            PhoneBookHashTable[idx].Add(new Contact(name, number));
        }

        public string Find(int number)
        {
            int idx = HashFunction(number);
            for (int i = 0; i < PhoneBookHashTable[idx].Count; i++)
            {
                if (PhoneBookHashTable[idx][i].Number == number)
                    return PhoneBookHashTable[idx][i].Name;             
            }
            return "not found";
        }

        public void Delete(int number)
        {
            int idx = HashFunction(number);
            for (int i = 0; i < PhoneBookHashTable[idx].Count; i++)
            {
                if (PhoneBookHashTable[idx][i].Number == number)
                {
                    PhoneBookHashTable[idx].RemoveAt(i);
                    return;
                }
            }
        }
    }
}
