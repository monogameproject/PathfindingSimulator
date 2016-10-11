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

            start.Calculate(goal, "f");
            while (openList.Count() != 0)
            {
                foreach (Cell item in GridManager.Grid)
                {
                    //Adds horizontal and vertical neighbours
                    if (start.Position.X + 1 == item.Position.X || start.Position.X - 1 == item.Position.X || start.Position.Y + 1 == item.Position.Y || start.Position.Y - 1 == item.Position.Y)
                    {
                        if (item.Walkable)
                        {
                            openList.Add(item);
                            item.Parent.Position = start.Position;
                        }
                    }
                    else if (start.Position.X - 1 == item.Position.X && start.Position.Y - 1 == item.Position.Y || start.Position.X + 1 == item.Position.X && start.Position.Y + 1 == item.Position.Y || start.Position.X - 1 == item.Position.X && start.Position.Y + 1 == item.Position.Y || start.Position.X + 1 == item.Position.X && start.Position.Y - 1 == item.Position.Y)
                    {
                        if (item.Walkable)
                        {
                            openList.Add(item);
                            item.Parent.Position = start.Position;
                        }
                    }
                }
                foreach (Cell item in openList)
                {
                    if (item.MyType == CellType.STORMKEY) //Set goal
                    {
                        
                    }
                    openList.Remove(Position);
                    closedList.Add(Position);
                    item.Calculate(goal, "g");
                    item.Calculate(goal, "h");
                    item.Calculate(goal, "f");
                }

            }
        }
    }
}
