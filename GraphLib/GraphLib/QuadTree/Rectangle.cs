using System;

namespace Graph.QuadTree
{
    public class Rectangle
    {
        float minX;
        float maxX;
        float minY;
        float maxY;

        /// <summary>
        /// Left top point
        /// </summary>
        public float X1 { get { return minX; } }
        /// <summary>
        /// Left top point
        /// </summary>
        public float Y1 { get { return maxY; } }
        /// <summary>
        /// Right top point
        /// </summary>
        public float X2 { get { return maxX; } }
        /// <summary>
        /// Right top point
        /// </summary>
        public float Y2 { get { return maxY; } }
        /// <summary>
        /// Left bottom point
        /// </summary>
        public float X3 { get { return minX; } }
        /// <summary>
        /// Left bottom point
        /// </summary>
        public float Y3 { get { return minY; } }
        /// <summary>
        /// Right top point
        /// </summary>
        public float X4 { get { return maxX; } }
        /// <summary>
        /// Right top point
        /// </summary>
        public float Y4 { get { return minY; } }

        public float XCenter { get { return (minX + maxX) / 2.0f; } }
        public float YCenter { get { return (minY + maxY) / 2.0f; } }

        public float Width { get { return maxX - minX; } }
        public float Height { get { return maxY - minY; } }

        public float MinX { get { return minX; } }
        public float MaxX { get { return maxX; } }
        public float MinY { get { return minY; } }
        public float MaxY { get { return maxY; } }

        /// <summary>
        /// Create rectangle by 2 points
        /// </summary>
        public Rectangle(float _x1, float _y1, float _x2, float _y2)
        {
            minX = Math.Min(_x1, _x2);
            maxX = Math.Max(_x1, _x2);
            minY = Math.Min(_y1, _y2);
            maxY = Math.Max(_y1, _y2);
        }

        /// <summary>
        /// Does this rectangle contains point
        /// </summary>
        public bool ContainsPoint(float x, float y)
        {
            return (x >= minX && x <= maxX &&
                    y >= minY && y <= maxY);
        }

        /// <summary>
        /// Does intersects 2 rectangles
        /// </summary>
        public bool Intersects(Rectangle b)
        {
            return (minX < b.maxX && maxX > b.minX &&
                minY < b.maxY && maxY > b.minY);
        }

        /// <summary>
        /// Compare rectangles
        /// </summary>
        /// <param name="eps">Allowable error</param>
        public bool Equals(Rectangle second, float eps)
        {
            if (Math.Abs(minX - second.minX) <= eps &&
                Math.Abs(maxX - second.maxX) <= eps &&
                Math.Abs(minY - second.minY) <= eps &&
                Math.Abs(maxY - second.maxY) <= eps)
                return true;
            return false;
        }
    }
}
