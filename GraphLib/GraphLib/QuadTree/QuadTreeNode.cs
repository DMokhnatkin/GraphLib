using System.Collections.Generic;

namespace Graph.QuadTree
{
    /// <typeparam name="TValue">Type of values which stores in tree</typeparam>
    public class QuadTreeNode<TValue> where TValue : IQuadObject
    {
        /// <summary>
        /// Maxium values which can be stored in one node
        /// </summary>
        public static int valCapacity = 4;

        public Rectangle Bound { get; private set; }

        internal QuadTreeNode<TValue> leftTop = null;
        internal QuadTreeNode<TValue> rightTop = null;
        internal QuadTreeNode<TValue> leftDown = null;
        internal QuadTreeNode<TValue> rightDown = null;

        // Collection of values
        List<TValue> objects = new List<TValue>();

        public QuadTreeNode(Rectangle _bound)
        {
            Bound = _bound;
        }

        internal void Subdivide()
        {
            Rectangle northWestBound = new Rectangle(Bound.X1, Bound.Y1, 
                (Bound.X1 + Bound.X2) / 2.0f,
                (Bound.Y1 + Bound.Y2) / 2.0f);
            Rectangle northEastBound = new Rectangle(Bound.X2, Bound.Y2,
                (Bound.X1 + Bound.X2) / 2.0f,
                (Bound.Y1 + Bound.Y2) / 2.0f);
            Rectangle southWestBound = new Rectangle(Bound.X3, Bound.Y3,
                (Bound.X1 + Bound.X2) / 2.0f,
                (Bound.Y1 + Bound.Y2) / 2.0f);
            Rectangle southEastBound = new Rectangle(Bound.X4, Bound.Y4,
                (Bound.X1 + Bound.X2) / 2.0f,
                (Bound.Y1 + Bound.Y2) / 2.0f);
            leftTop = new QuadTreeNode<TValue>(northWestBound);
            rightTop = new QuadTreeNode<TValue>(northEastBound);
            leftDown = new QuadTreeNode<TValue>(southWestBound);
            rightDown = new QuadTreeNode<TValue>(southEastBound);
        }

        /// <summary>
        /// Insert value
        /// </summary>
        /// <param name="x">X coord to insert</param>
        /// <param name="y">Y coord to insert</param>
        /// <param name="obj"></param>
        public bool Insert(TValue obj)
        {
            if (!Bound.ContainsPoint(obj.X, obj.Y))
            {
                return false;
            }
            // If space is enough, add val
            if (objects.Count <= valCapacity)
            {
                objects.Add(obj);
                return true;
            }

            // Divide bound and insert val in one of created nodes
            if (leftTop == null)
                Subdivide();
            if (leftTop.Insert(obj)) return true;
            if (rightTop.Insert(obj)) return true;
            if (leftDown.Insert(obj)) return true;
            if (rightDown.Insert(obj)) return true;
            return false;
        }

        /// <summary>
        /// Remove object from tree
        /// </summary>
        public bool Remove(TValue obj)
        {
            if (!Bound.ContainsPoint(obj.X, obj.Y))
                return false;
            if (objects.Remove(obj))
            {
                return true;
            }
            else
            {
                if (leftTop.Remove(obj)) return true;
                if (rightTop.Remove(obj)) return true;
                if (leftDown.Remove(obj)) return true;
                if (rightDown.Remove(obj)) return true;
            }
            return false;
        }

        /// <summary>
        /// Get points which are in range
        /// </summary>
        public List<TValue> QueryRange(Rectangle range)
        {
            List<TValue> res = new List<TValue>();
            if (!Bound.Intersects(range))
                return res;
            foreach (TValue z in objects)
            {
                if (range.ContainsPoint(z.X, z.Y))
                    res.Add(z);
            }
            if (leftTop == null)
                return res;
            res.AddRange(leftTop.QueryRange(range));
            res.AddRange(rightTop.QueryRange(range));
            res.AddRange(leftDown.QueryRange(range));
            res.AddRange(rightDown.QueryRange(range));
            return res;
        }
    }
}
