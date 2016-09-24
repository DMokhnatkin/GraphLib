using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph.SimpleGraph
{
    public class UnweightedEdge
    {
        int _id1;
        int _id2;

        public int Id1 { get { return _id1; } }

        public int Id2 { get { return _id2; } }

        public UnweightedEdge(int id1, int id2)
        {
            _id1 = id1;
            _id2 = id2;
        }
    }
}
