using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Grid.CellType;

namespace Grid
{
    class Wizard
    {
        List<Cell> openList;
        List<Cell> closedList;
        Point wizardPosition;

        public Cell Position;
        //{
        //    get { return Position; }
        //    set { Position = value; }
        //}

        public Wizard(Cell startPosition)
        {
            this.Position = startPosition;
            this.wizardPosition = startPosition.Position;

        }

        public void Render(Graphics dc)
        {
            dc.FillRectangle(new SolidBrush(Color.White), Position.BoundingRectangle);
            dc.DrawRectangle(new Pen(Color.Black), Position.BoundingRectangle);
            dc.DrawImage(Image.FromFile(@"Images\Wizard.png"), Position.BoundingRectangle);
        }

        public void Astar(Cell start, Cell goal)
        {
            openList = new List<Cell>();
            closedList = new List<Cell>();

            start = Position;
            openList.Add(start);

            start.CalculateF(goal);
            while (openList.Count() == 0)
            {

            }
        }
    }
}
