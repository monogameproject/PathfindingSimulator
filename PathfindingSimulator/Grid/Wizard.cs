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
        public Cell Position;
        //{
        //    get { return Position; }
        //    set { Position = value; }
        //}

        public Wizard(Cell startPosition)
        {
            this.Position = startPosition;
        }

        public void Astar(Cell start, Cell goal)
        {
            openList = new List<Cell>();
            closedList = new List<Cell>();

            start = Position;


        }
    }
}
