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
        bool endLoop = false;
        private bool isDone = false;
        List<Cell> openList;
        List<Cell> closedList;
        private static List<Cell> aStarPath = new List<Cell>();
        Point wizardPosition;
        private Cell position;
        public Cell Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public Wizard(Cell startPosition)
        {
            this.position = startPosition;
            this.wizardPosition = startPosition.Position;
        }

        public void Render(Graphics dc)
        {
            dc.FillRectangle(new SolidBrush(Color.White), position.BoundingRectangle);
            dc.DrawRectangle(new Pen(Color.Black), position.BoundingRectangle);
            dc.DrawImage(Image.FromFile(@"Images\Wizard.png"), position.BoundingRectangle);
        }



        public List<Cell> ClausAstar(Cell start, Cell target)
        {


            openList = new List<Cell>();
            closedList = new List<Cell>();

            //start = position;

            openList.Add(start);
            while (openList.Count > 0)
            {
                foreach (Cell item in openList)
                {
                    item.Mycolor = Color.Green;
                }
                foreach (Cell item in closedList)
                {
                    item.Mycolor = Color.Blue;
                }
                Cell q = position;

                if (q.MyType == target.MyType)
                {
                    Cell backTrackNode = q;
                    //aStarPath = closedList;
                    while (backTrackNode != null)
                    {
                        aStarPath.Add(backTrackNode);
                        backTrackNode = backTrackNode.Parent;

                        if (backTrackNode == start)
                            break;
                    }
                    closedList.Clear();
                    openList.Clear();
                    aStarPath.Reverse();
                    return aStarPath;
                }
               
                openList.Remove(q);

                List<Cell> successor = new List<Cell>();
                //generate 8 successor
                foreach (Cell item in GridManager.Grid)
                {
                    //Adds horizontal and vertical neighbours
                    if (q.Position.X + 1 == item.Position.X && q.Position.Y == item.Position.Y || q.Position.X - 1 == item.Position.X && q.Position.Y == item.Position.Y || q.Position.Y + 1 == item.Position.Y && q.Position.X == item.Position.X || q.Position.Y - 1 == item.Position.Y && q.Position.X == item.Position.X)
                    {
                        if (item.Walkable && !closedList.Contains(item))
                        {
                            successor.Add(item);
                        }
                    }
                    //Finds diagonal neighbours
                    if (q.Position.X - 1 == item.Position.X && q.Position.Y - 1 == item.Position.Y || q.Position.X + 1 == item.Position.X && q.Position.Y + 1 == item.Position.Y || q.Position.X - 1 == item.Position.X && q.Position.Y + 1 == item.Position.Y || q.Position.X + 1 == item.Position.X && q.Position.Y - 1 == item.Position.Y)
                    {
                        if (item.Walkable && !closedList.Contains(item))
                        {
                            successor.Add(item);
                        }
                    }
                }


                foreach (Cell neighbour in successor)
                {
                    if (!closedList.Contains(neighbour)) //Sorts out the false/unreacable neibourghs
                    {
                        if (!openList.Contains(neighbour))
                        {
                            openList.Add(neighbour);
                            neighbour.Parent = q;
                            neighbour.Calculate(target, q);
                        }
                        else
                        {
                            //calculates the relative position to the chosenCell
                            int cost;
                            Point diff = new Point(neighbour.Position.X - q.Position.X, neighbour.Position.Y - q.Position.Y); //diff

                            if (Math.Abs(diff.X) == 1 && Math.Abs(diff.Y) == 1)
                            {
                                cost = 14;
                            }
                            else if (diff.X == 0 && diff.Y == 0)
                            {
                                cost = 0;
                            }
                            else
                            {
                                cost = 10;
                            }
                            //checks if chosenCell is a better parent than the old one
                            if (neighbour.G > q.G)
                            {
                                neighbour.Parent = q;
                                neighbour.Calculate(target, q);
                            }
                        }
                    }

                    if (neighbour.Calculate(target, q) <= q.Calculate(target, q) && position.MyType != target.MyType) // checks which cell has the lowest f
                    {
                        position = neighbour;

                        if (neighbour.Calculate(target, q) < q.Calculate(target, q))
                        {
                            position = neighbour;

                        }

                    }

                }
                if (!closedList.Contains(q))
                {
                    closedList.Add(q);

                }
            }
            return null;
        }

        public void Move(Cell newPosition)
        {
            Position = newPosition;
        }
    }
}
