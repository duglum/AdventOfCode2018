using System;
using System.Collections.Generic;
using System.Linq;

namespace Day08
{
    public class Node
    {
        private List<Node> ChildNodes { get; set; } = new List<Node>();
        private List<int> MetaData { get; set; } = new List<int>();

        public Node(IList<string> info)
        {
            var childNodes = int.Parse(info[0]);
            info.RemoveAt(0);

            var metaDataEntries = int.Parse(info[0]);
            info.RemoveAt(0);

            for (var i = 0; i < childNodes; i++)
            {
                ChildNodes.Add(new Node(info));
            }

            for (var i = 0; i < metaDataEntries; i++)
            {
                MetaData.Add(int.Parse(info[0]));
                info.RemoveAt(0);
            }
        }

        public int GetSumMetaDataEntries()
        {
            var sum = 0;
            foreach (var node in ChildNodes)
            {
                sum += node.GetSumMetaDataEntries();
            }

            sum += MetaData.Sum(m => m);

            return sum;
        }

        public int GetValue()
        {
            if (ChildNodes.Count == 0)
            {
                return MetaData.Sum(m => m);
            }

            var value = 0;
            foreach (var metaData in MetaData)
            {
                if (metaData != 0)
                {
                    try
                    {
                        value += ChildNodes.ElementAt(metaData - 1).GetValue();
                    }
                    catch (Exception)
                    {
                        // node does not exist, do not care :-)
                    }
                }
            }

            return value;
        }
    }
}