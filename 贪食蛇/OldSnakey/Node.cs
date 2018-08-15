using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OldSnakey
{
    class Node
    {
        public int  x {get ;set ;}
        public int  y {get ;set ;}
        public Color color;

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
            color = Color.Red;
        }
        public bool IsOverLap(Node n)
        {
            if (this.x == n.x && this.y == n.y)
            {
                return true;
            
            }
            return false;
        }
    }
}
