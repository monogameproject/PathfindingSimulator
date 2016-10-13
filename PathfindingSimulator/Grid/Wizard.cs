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
        //private Cell position;
        public Cell Position;
        //{
        //    get { return position; }
        //    set { position = value; }
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

        //public void Astar(Cell start, Cell goal)
        //{
        //    openList = new List<Cell>();
        //    closedList = new List<Cell>();

        //    start = Position;
        //    openList.Add(start);

        //    start.Calculate(goal, "f");
        //    while (openList.Count() != 0)
        //    {
        //        foreach (Cell item in GridManager.Grid)
        //        {
        //            //Adds horizontal and vertical neighbours
        //            if (start.Position.X + 1 == item.Position.X && start.Position.Y == item.Position.Y || start.Position.X - 1 == item.Position.X && start.Position.Y == item.Position.Y || start.Position.Y + 1 == item.Position.Y && start.Position.X == item.Position.X || start.Position.Y - 1 == item.Position.Y && start.Position.X == item.Position.X)
        //            {
        //                if (item.Walkable)
        //                {
        //                    openList.Add(item);
        //                    item.Parent = start;
        //                    if (item.Calculate(goal, "f") < item.Calculate(goal, "h"))
        //                    {
        //                        closedList.Add(item.Parent);
        //                        Position = item.Parent;
        //                    }
        //                }
        //            }
        //            //Finds diagonal neighbours
        //            if (start.Position.X - 1 == item.Position.X && start.Position.Y - 1 == item.Position.Y || start.Position.X + 1 == item.Position.X && start.Position.Y + 1 == item.Position.Y || start.Position.X - 1 == item.Position.X && start.Position.Y + 1 == item.Position.Y || start.Position.X + 1 == item.Position.X && start.Position.Y - 1 == item.Position.Y)
        //            {
        //                if (item.Walkable)
        //                {
        //                    openList.Add(item);
        //                    item.Parent = start;
        //                    if (item.Calculate(goal, "f") < item.Calculate(goal, "h"))
        //                    {
        //                        closedList.Add(item.Parent);
        //                        Position = item.Parent;
        //                    }
        //                }
        //            }
        //        }
        //        foreach (Cell item in openList)
        //        {
        //            if (item == SetGoal(STORMKEY)) //Set goal
        //            {
        //                SetGoal(STORMKEY);
        //            }
        //            closedList.Add(Position);
        //            item.Calculate(goal, "g");
        //            item.Calculate(goal, "h");
        //            item.Calculate(goal, "f");
        //        }

        //            openList.Remove(Position);
        //    }
        //}

        //public Cell SetGoal(CellType cellType)
        //{
        //    Cell test;
        //    switch (cellType)
        //    {
        //        case STORMKEY:
        //            foreach (Cell item in GridManager.Grid)
        //            {
        //                if (item.MyType == CellType.STORMKEY)
        //                {
        //                    cellType = CellType.STORM;
        //                    return test = item;
        //                }
        //            }
        //            break;
        //        case STORM:
        //            foreach (Cell item in GridManager.Grid)
        //            {
        //                if (item.MyType == CellType.STORM)
        //                {
        //                    cellType = CellType.ICEKEY;
        //                    return test = item;
        //                }
        //            }
        //            break;
        //        case ICEKEY:
        //            foreach (Cell item in GridManager.Grid)
        //            {
        //                if (item.MyType == CellType.ICEKEY)
        //                {
        //                    cellType = CellType.ICE;
        //                    return test = item;
        //                }
        //            }
        //            break;

        //    }
        //    return test = new Cell(new Point(0,0), 80);
        //}
        public Cell goals(CellType celltype)
        {
            Cell error = new Cell(new Point(-1, -1), 80);
            foreach (Cell item in GridManager.Grid)
            {
                if (item.MyType == celltype)
                {
                    return item;


                }

            }
            return error;
        }

        public void ClausAstar(Cell start)
        {
            Queue<Cell> goal = new Queue<Cell>();
            goal.Enqueue(goals(STORMKEY));
            goal.Enqueue(goals(STORM));
            goal.Enqueue(goals(ICEKEY));
            goal.Enqueue(goals(ICE));

            openList = new List<Cell>();
            closedList = new List<Cell>();

            //start = Position;

            openList.Add(start);
            while (openList.Count != 0)
            {
                Cell q = Position;

                openList.Remove(q);

                List<Cell> successor = new List<Cell>();
                //generate 8 successor
                foreach (Cell item in GridManager.Grid)
                {
                    item.Calculate(goal.ElementAt(0));
                    //Adds horizontal and vertical neighbours
                    if (q.Position.X + 1 == item.Position.X && q.Position.Y == item.Position.Y || q.Position.X - 1 == item.Position.X && q.Position.Y == item.Position.Y || q.Position.Y + 1 == item.Position.Y && q.Position.X == item.Position.X || q.Position.Y - 1 == item.Position.Y && q.Position.X == item.Position.X)
                    {
                        if (item.Walkable && !closedList.Contains(item))
                        {

                            successor.Add(item);
                            // item.Parent = q;
                        }
                    }
                    //Finds diagonal neighbours
                    if (q.Position.X - 1 == item.Position.X && q.Position.Y - 1 == item.Position.Y || q.Position.X + 1 == item.Position.X && q.Position.Y + 1 == item.Position.Y || q.Position.X - 1 == item.Position.X && q.Position.Y + 1 == item.Position.Y || q.Position.X + 1 == item.Position.X && q.Position.Y - 1 == item.Position.Y)
                    {
                        if (item.Walkable && !closedList.Contains(item))
                        {
                            successor.Add(item);
                            //item.Parent = q;

                        }
                    }
                }

                //Cell prevBest=new Cell(new Point(-1, -1), 80);
                //prevBest=q;
                foreach (Cell neighbours in successor)
                {
                    if (neighbours.MyType == goal.ElementAt(0).MyType)
                    {
                        goal.Dequeue();
                    }

                    neighbours.Calculate(goal.ElementAt(0));
                    //foreach (Cell item in successor)
                    //{
                    if (openList.Contains(neighbours))
                    {
                        //if gscore neighbour > move + current.g
                        //neighbour.gscore = move+current.g paret = current
                       
                    }
                    //}
                    //at alve den closed som jeg burde fucker med closed list så den adder infinite
                    if (closedList.Contains(neighbours))
                    {
                      

                    }
                    else
                    {
                        
                            openList.Add(neighbours);

                            neighbours.Parent = q;
                        

                    }
                    if (neighbours.F < q.F)
                    {
                        q = neighbours;
                        Position = q;
                    }
                }

                    closedList.Add(q);
                



            }
        }
    }
}
