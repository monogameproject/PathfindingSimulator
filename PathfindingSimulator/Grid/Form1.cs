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
    public partial class Form1 : Form
    {
       
        private GridManager visualManager;
       
        public Form1()
        {
            InitializeComponent();

            //Sets the client size
            ClientSize = new Size(800, 800);

            //Instantiates the visual manager
            visualManager = new GridManager(CreateGraphics(), this.DisplayRectangle);

        }

        private void Loop_Tick(object sender, EventArgs e)
        {
            //Draws all our cells
            visualManager.GameLoop();
            
        }
    }
}
