using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOL
{
    public partial class TimerMD : Form
    {
        public TimerMD()
        {
            InitializeComponent();
        }

        public TimerMD(int time)
        {
            InitializeComponent();
            Milliseconds = time;
        }
        public decimal Milliseconds
        {
            get
            {
                return MillSecUpDown.Value;
            }
            set
            {
                MillSecUpDown.Value = value;
            }
        }
    }
}
