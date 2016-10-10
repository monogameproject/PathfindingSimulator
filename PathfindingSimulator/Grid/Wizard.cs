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
        #region MortensAstar

        #region variabler
        //private bool stormkey = false;
        //private bool icekey = false;
        //private bool hasPotion;
        //private bool canIWinNow;
        //private int moveCount = 0;

        //List<Cell> returnCellList;
        //List<Cell> openList;
        //List<Cell> closedList;
        //Cell position;
        //Point actualPosition;
        //GridManager gridManager;
        //Cell nextGoal;
        #endregion

        #region propperties
        //public bool HasPotion
        //{
        //    get
        //    {
        //        return hasPotion;
        //    }

        //    set
        //    {
        //        hasPotion = value;
        //    }
        //}
        //public bool CanIWinNow
        //{
        //    get
        //    {
        //        return canIWinNow;
        //    }

        //    set
        //    {
        //        canIWinNow = value;
        //    }
        //}
        //public Cell Position
        //{
        //    get { return position; }
        //    set { position = value; }
        //}
        //public bool Stormkey
        //{
        //    get { return stormkey; }
        //    set { stormkey = value; }
        //}
        //public bool Icekey
        //{
        //    get { return icekey; }
        //    set { icekey = value; }
        //}

        #endregion



        //private List<Cell> Astar(Cell goal)
        //{
        //    List<Cell> returnCellList = new List<Cell>();
        //    openList = new List<Cell>();
        //    closedList = new List<Cell>();

        //    //Finds and adds the start to the open list
        //    Cell start = position;
        //    openList.Add(start);

        //    //Loop
        //    bool endLoop = false;
        //    do
        //    {
        //        // finds the cell with the lowest f, so we have something to work from
        //        Cell chosenCell = openList[0]; // the cell to move to the closed list
        //        foreach (Cell cell in openList) // runs through our open list
        //        {
        //            if (cell.GetFValue(goal) < chosenCell.GetFValue(goal)) // checks which cell has the lowest f
        //            {
        //                chosenCell = cell;
        //            }
        //        }

        //        //moves the cell
        //        closedList.Add(chosenCell);
        //        openList.Remove(chosenCell);

        //        //finds the false neibourghs
        //        List<Cell> falseNeibourghs = new List<Cell>();
        //        foreach (Cell n in chosenCell.Neibourghs)
        //        {
        //            if (!n.Walkable)
        //            {
        //                if (n.Position == new Point(chosenCell.Position.X - 1, chosenCell.Position.Y))
        //                {
        //                    foreach (Cell diaN in chosenCell.Neibourghs)
        //                    {
        //                        if (diaN.Position == new Point(chosenCell.Position.X - 1, chosenCell.Position.Y - 1) || diaN.Position == new Point(chosenCell.Position.X - 1, chosenCell.Position.Y + 1))
        //                        {
        //                            falseNeibourghs.Add(diaN);
        //                        }
        //                    }
        //                }
        //                if (n.Position == new Point(chosenCell.Position.X + 1, chosenCell.Position.Y))
        //                {
        //                    foreach (Cell diaN in chosenCell.Neibourghs)
        //                    {
        //                        if (diaN.Position == new Point(chosenCell.Position.X + 1, chosenCell.Position.Y - 1) || diaN.Position == new Point(chosenCell.Position.X + 1, chosenCell.Position.Y + 1))
        //                        {
        //                            falseNeibourghs.Add(diaN);
        //                        }
        //                    }
        //                }
        //                if (n.Position == new Point(chosenCell.Position.X, chosenCell.Position.Y - 1))
        //                {
        //                    foreach (Cell diaN in chosenCell.Neibourghs)
        //                    {
        //                        if (diaN.Position == new Point(chosenCell.Position.X - 1, chosenCell.Position.Y - 1) || diaN.Position == new Point(chosenCell.Position.X + 1, chosenCell.Position.Y - 1))
        //                        {
        //                            falseNeibourghs.Add(diaN);
        //                        }
        //                    }
        //                }
        //                if (n.Position == new Point(chosenCell.Position.X, chosenCell.Position.Y + 1))
        //                {
        //                    foreach (Cell diaN in chosenCell.Neibourghs)
        //                    {
        //                        if (diaN.Position == new Point(chosenCell.Position.X + 1, chosenCell.Position.Y + 1) || diaN.Position == new Point(chosenCell.Position.X - 1, chosenCell.Position.Y + 1))
        //                        {
        //                            falseNeibourghs.Add(diaN);
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        //adds the neibourghs, calculates f/g/h and adds parent
        //        foreach (Cell n in chosenCell.Neibourghs)
        //        {
        //            if (!falseNeibourghs.Contains(n) && n.Walkable && !closedList.Contains(n)) //Sorts out the false/unreacable neibourghs
        //            {
        //                if (!openList.Contains(n))
        //                {
        //                    openList.Add(n);
        //                    n.Parent = chosenCell;
        //                }
        //                else
        //                {
        //                    //calculates the relative position to the chosenCell
        //                    int cost;
        //                    Point diff = new Point(n.Position.X - chosenCell.Position.X, n.Position.Y - chosenCell.Position.Y); //diff

        //                    if (Math.Abs(diff.X) == 1 && Math.Abs(diff.Y) == 1)
        //                    {
        //                        cost = 14;
        //                    }
        //                    else if (diff.X == 0 && diff.Y == 0)
        //                    {
        //                        cost = 0;
        //                    }
        //                    else
        //                    {
        //                        cost = 10;
        //                    }
        //                    //checks if chosenCell is a better parent than the old one
        //                    if (n.G > chosenCell.G + cost)
        //                    {
        //                        n.Parent = chosenCell;
        //                    }
        //                }
        //                n.GetFValue(goal);
        //            }
        //        }

        //        //ends loop if we have arrived at the goal
        //        if (closedList.Contains(goal) || openList.Count == 0)
        //        {
        //            endLoop = true;
        //        }
        //    } while (!endLoop);

        //    //backtrack
        //    bool loopDone = false;
        //    Cell returnCell = goal;

        //    do
        //    {
        //        if (returnCell.Parent == start)
        //        {
        //            returnCellList.Add(returnCell);
        //            loopDone = true;
        //        }
        //        else
        //        {
        //            returnCellList.Add(returnCell);
        //            returnCell = returnCell.Parent;
        //        }
        //    } while (!loopDone);
        //    return returnCellList;
        //}
        #endregion


        List<Cell> openList;
        List<Cell> closedList;
        public Cell Position { get { return Position; } set { Position = value; } }

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
