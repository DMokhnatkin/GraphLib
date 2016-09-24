using System.Collections.Generic;

namespace Graph.QuadTree
{
    public class PointQuadTree<TValue> : ICollection<TValue> where TValue : IQuadObject
    {
        QuadTreeNode<TValue> tree;

        public Rectangle Bound { get { return tree.Bound; } }

        /// <param name="bound">Bound to initialize</param>
        public PointQuadTree(Rectangle bound)
        {
            tree = new QuadTreeNode<TValue>(bound);
        }

        public PointQuadTree()
        {
            tree = new QuadTreeNode<TValue>(new Rectangle(1, 1, -1, -1));
        }

        /// <summary>
        /// Return all object which are in bound
        /// </summary>
        public List<TValue> QueryRange(Rectangle bound)
        {
            return tree.QueryRange(bound);
        }

        /// <summary>
        /// Add item to tree
        /// </summary>
        public void Add(TValue item)
        {
            if (!tree.Bound.ContainsPoint(item.X, item.Y))
            {
                // Point is in left top corner
                // Or point is upper
                if (item.X < Bound.MaxX &&
                    item.Y > Bound.MaxY)
                {
                    Rectangle _newBound = new Rectangle(
                        Bound.MinX - Bound.Width,
                        Bound.MaxY + Bound.Height,
                        Bound.MaxX,
                        Bound.MinY);
                    QuadTreeNode<TValue> _newNode = new QuadTreeNode<TValue>(_newBound);
                    _newNode.Subdivide();
                    _newNode.rightDown = tree;
                    tree = _newNode;
                }

                // Point is in right top corner
                // Or point is to the right
                if (item.X > Bound.MaxX &&
                    item.Y > Bound.MinY)
                {
                    Rectangle _newBound = new Rectangle(
                        Bound.MinX,
                        Bound.MaxY + Bound.Height,
                        Bound.MaxX + Bound.Width,
                        Bound.MinY);
                    QuadTreeNode<TValue> _newNode = new QuadTreeNode<TValue>(_newBound);
                    _newNode.Subdivide();
                    _newNode.leftDown = tree;
                    tree = _newNode;
                }

                // Point is in left down corner
                // Or point is to the left
                if (item.X < Bound.MinX &&
                    item.Y < Bound.MaxY)
                {
                    Rectangle _newBound = new Rectangle(
                        Bound.MinX - Bound.Width,
                        Bound.MinY - Bound.Height,
                        Bound.MaxX,
                        Bound.MaxY);
                    QuadTreeNode<TValue> _newNode = new QuadTreeNode<TValue>(_newBound);
                    _newNode.Subdivide();
                    _newNode.rightTop = tree;
                    tree = _newNode;
                }

                // Point is right down corner
                // Or point is lower
                if (item.X > Bound.MinX &&
                    item.Y < Bound.MinY)
                {
                    Rectangle _newBound = new Rectangle(
                        Bound.MinX,
                        Bound.MaxY,
                        Bound.MaxX + Bound.Width,
                        Bound.MinY - Bound.Height);
                    QuadTreeNode<TValue> _newNode = new QuadTreeNode<TValue>(_newBound);
                    _newNode.Subdivide();
                    _newNode.leftTop = tree;
                    tree = _newNode;
                }

                Add(item);
                return;
            }
            tree.Insert(item);
            Count++;
        }

        /// <summary>
        /// Delete all items from tree
        /// </summary>
        public void Clear()
        {
            tree = new QuadTreeNode<TValue>(tree.Bound);
            Count = 0;
        }

        /// <summary>
        /// Does this colellection contains item
        /// </summary>
        public bool Contains(TValue item)
        {
            return tree.QueryRange(tree.Bound).Contains(item);
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            tree.QueryRange(tree.Bound).CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get;
            private set;
        }

        public bool IsReadOnly
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool Remove(TValue item)
        {
            return tree.Remove(item);
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return tree.QueryRange(tree.Bound).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
