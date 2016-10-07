using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Grid.CellType;

namespace Grid
{
    class MyAStar
    {


        public void AStar(Cell start, CellType goal)
        {
            List<Cell> closedSet = new List<Cell>();

            List<Cell> openSet = new List<Cell>();

            openSet.Add(start);

            Cell parrent;

            gscore[start] = 0;
            //Step 1:
            //Placer start cell i openSet listen.
            //Step 2:
            //placer alle omkringliggende celler i openSet listen.
            //Step 3:
            //Udregn F for hver cell i openSet
            //Step 4:
            //Sorter openSet listen efter F, og træk den mindste værdi ud
            //Step 5:
            //Placer den valgte celle og den man kommer fra i closedSet, 
            //Step 6:
            //Start igen fra step 2
            int gScore = 0;

            int fScore = 0;

            openSet.Add()

        }

        public int HeuristicCostEstimate(Cell start, Cell goal)
        {
            int heuristic = 0;

            int xStart = start.position.X;
            int yStart = start.position.Y;

            return 0;

        }
    }
}
