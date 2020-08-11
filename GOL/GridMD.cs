using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOL
{
    public partial class GridMD : Form
    {
        public GridMD()
        {
            InitializeComponent();
        }
        //value from numberupdown for the width
        public decimal xSize
        {
            get
            {
                return xAxisNumUpDown.Value;
            }
            set
            {
                xAxisNumUpDown.Value = value;
            }
        }
        //Value from numberupdown for the height
        public decimal ySize
        {
            get
            {
                return yAxisNumUpDown.Value;
            }
            set
            {
                yAxisNumUpDown.Value = value;
            }
        }
    }
}
