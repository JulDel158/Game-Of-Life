using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOL
{
    public class Cell
    {
        //boolean to check if cell is active
        public bool active { get; set; } = false;
        //int to calculate the cycles the cell has been alive
        public int life { get; set; } = 0;
        //Determines the color of an active cell
        public Color cellColor { get; set; } = Color.Cyan;
        //neighbor count
        public int nCount { get; set; } = 0;
    }
}
