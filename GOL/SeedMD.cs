﻿using System;
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
    public partial class SeedMD : Form
    {
        public SeedMD()
        {
            InitializeComponent();
        }

       public decimal Seed
        {
            get
            {
                return numericUpDown1.Value;
            }
            set
            {
                numericUpDown1.Value = value;
            }
        }
    }
}
