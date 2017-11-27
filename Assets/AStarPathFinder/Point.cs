using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeenGames.Utils
{
    // Replace the System.Drawing one so we can use this in Silverlight
    public class Point
    {
        private int _x;
        private int _y;

        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int X { get { return _x; } set { _x = value; } }
        public int Y { get { return _y; } set { _y = value; } }

        // For debugging
        public override string ToString()
        {
            return string.Format("{0}, {1}", X, Y);
        }
    }
}
