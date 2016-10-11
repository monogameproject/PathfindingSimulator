using System;
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
    public partial class Form3 : Form
    {

        private GridManager visualManager;

        public Form3()
        {
            InitializeComponent();

            //Sets the client size
            ClientSize = new Size(800, 800);

            //Instantiates the visual manager
            visualManager = new GridManager(CreateGraphics(), this.DisplayRectangle, 1);
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            visualManager.GameLoop();
        }
    }
}
