﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grid
{
    public partial class Form2 : Form
    {
        Form1 aStar;
        Form3 BFS;
        Form4 DFS;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            aStar = new Form1();
            aStar.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            BFS = new Form3();
            BFS.Show();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DFS = new Form4();
            DFS.Show();
        }
    }
}
