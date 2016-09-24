using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph.QuadTree
{
    /// <summary>
    /// Object which can be represented as 2d point
    /// </summary>
    public interface IQuadObject
    {
        float X { get; }
        float Y { get; }
    }
}
