using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MirSyn
{
    public class Text
    {
        int x, y;
        string mir_id;

        public Text(string mir_id, int x, int y)
        {
            this.mir_id = mir_id;
            this.x = x;
            this.y = y;
        }

        public string getMirID() { return mir_id; }
        public int getX() { return x; }
        public int getY() { return y; }

    }
}
