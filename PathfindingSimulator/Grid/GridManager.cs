﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Grid
{
    class GridManager
    {
        private bool isDone = false;
        private int algorithm;
        private BufferedGraphics backBuffer;
        public  Graphics dc;
        private Rectangle displayRectangle;
        private int cellRowCount;
        private int cellSize;
        private static List<Cell> grid;
        private List<Cell> path = new List<Cell>();
        private Wizard wizard;
        private Cell wStartCell = null;
        private BFS bfs;
        private DFS dfs;
        private Cell goal;
        int currentTileOnPath = 0;
        bool change = false;
        Queue<Cell> goalQueue = new Queue<Cell>();

        public Wizard Wizard
        {
            get { return wizard; }
            set { wizard = value; }
        }
        public static List<Cell> Grid
        {
            get
            {
                return grid;
            }

            set
            {
                grid = value;
            }
        }

        public bool IsDone
        {
            get
            {
                return isDone;
            }

            set
            {
                isDone = value;
            }
        }

        public GridManager(Graphics dc, Rectangle displayRectangle, int algorithm)
        {
            this.backBuffer = BufferedGraphicsManager.Current.Allocate(dc, displayRectangle);
            this.dc = backBuffer.Graphics;
            this.displayRectangle = displayRectangle;
            this.algorithm = algorithm;

            cellRowCount = 10;

            SetupWorld();
            foreach (Cell c in grid)
            {
                if(c.MyType == CellType.STORMKEY)
                {
                    goalQueue.Enqueue(c);
                }
            }
            foreach (Cell c in grid)
            {
                if (c.MyType == CellType.STORM)
                {
                    goalQueue.Enqueue(c);
                }
            }
            foreach (Cell c in grid)
            {
                if (c.MyType == CellType.ICEKEY)
                {
                    goalQueue.Enqueue(c);
                }
            }
            foreach (Cell c in grid)
            {
                if (c.MyType == CellType.ICE)
                {
                    goalQueue.Enqueue(c);
                    goalQueue.Enqueue(c);
                }
            }
            ChooseAlgorithm();
        }

        

        public void Render()
        {

            dc.Clear(Color.Green);

            foreach (Cell cell in grid)
            {
                cell.Render(dc);
                if (cell.Sprite == null)
                {
                    cell.Sprite = Image.FromFile(@"Images\Grass.png");
                }
            }
            wizard.Render(dc);
            backBuffer.Render();
            if (goalQueue.Count == 0)
            {
                isDone = true;
            }
        }

        public void CreateGrid()
        {
            grid = new List<Cell>();

            cellSize = displayRectangle.Width / cellRowCount;

            for (int x = 0; x < cellRowCount; x++)
            {
                for (int y = 0; y < cellRowCount; y++)
                {
                    grid.Add(new Cell(new Point(x, y), cellSize));
                }
            }
        }

        public void AddCellEdges()
        {
            for (int i = 0; i < grid.Count; i++)
            {
                if(grid[i].Walkable == true)
                {
                    if(i-1 >= 0)
                    {
                        if (i != 10 && i != 20 && i != 30 && i != 40 && i != 50 && i != 60 && i != 70 && i != 80 && i != 90)
                        {
                            grid[i - 1].AddEdge(grid[i]);
                        }
                        if (i - cellRowCount >= 0)
                        {
                            grid[i - cellRowCount].AddEdge(grid[i]);
                        }
                    }
                    if(i+2 <= grid.Count)
                    {
                        if (i != 9 && i != 19 && i != 29 && i != 39 && i != 49 && i != 59 && i != 69 && i != 79 && i != 89)
                        {
                            grid[i + 1].AddEdge(grid[i]);
                        }
                        if (i + (cellRowCount + 1) <= grid.Count)
                        {
                            grid[i + (cellRowCount)].AddEdge(grid[i]);
                        }
                    }
                }
            }
        }

        public void GameLoop()
        {
            
            if(currentTileOnPath < path.Count)
            {
                if (path[currentTileOnPath] == grid[68] && change == false)
                {
                    grid[68].MyEdges.Clear();
                    grid[68].Sprite = Image.FromFile(@"Images\Monster.png");
                    grid[68].Walkable = false;
                    change = true;
                }
                wizard.Move(path[currentTileOnPath]);
                currentTileOnPath++;
            }
            else
            {
                wizard.Position = goal;
                currentTileOnPath = 0;
                ChooseAlgorithm();
            }
            Render();
        }

        public void SetupWorld()
        {
            CreateGrid();
            SetUpCells();


        }

        private void SetUpCells()
        {
            List<Cell> emptylist = new List<Cell>();

            //Creates the portal
            Cell portal = grid.Find(node => node.Position.X == 0 && node.Position.Y == 8);
            portal.MyType = CellType.PORTAL;
            portal.Walkable = true;
            portal.Sprite = Image.FromFile(@"Images\Portal.png");

            //Creates the ice tower
            Cell iceTower = grid.Find(node => node.Position.X == 8 && node.Position.Y == 7);
            iceTower.MyType = CellType.ICE;
            iceTower.Walkable = true;
            iceTower.Sprite = Image.FromFile(@"Images\IceTower.png");

            //Creates the storm tower
            Cell stormTower = grid.Find(node => node.Position.X == 2 && node.Position.Y == 4);
            stormTower.MyType = CellType.STORM;
            stormTower.Walkable = true;
            stormTower.Sprite = Image.FromFile(@"Images\StormTower.png");

            //Creates the Rocks
            for (int x = 4; x < 7; x++)
            {
                for (int y = 1; y < 7; y++)
                {
                    Cell wall = grid.Find(node => node.Position.X == x && node.Position.Y == y);

                    if (wall.MyType != CellType.WALL)
                    {
                        wall.MyType = CellType.WALL;
                        wall.Walkable = false;
                        wall.Sprite = Image.FromFile(@"Images\Stone.png");
                    }
                }
            }

            //Creates the trees
            for (int forestX = 2; forestX < 7; forestX++)
            {
                for (int forestY = 7; forestY < 10; forestY++)
                {
                    if (forestY == 7 || forestY == 9)
                    {
                        Cell forest = grid.Find(node => node.Position.X == forestX && node.Position.Y == forestY);

                        if (forest.MyType != CellType.FOREST)
                        {
                            forest.MyType = CellType.FOREST;
                            forest.Walkable = false;
                            forest.Sprite = Image.FromFile(@"Images\Tree.png");
                        }

                    }
                }
            }


            #region PathCreation
            Cell note1 = grid.Find(node => node.Position.X == 1 && node.Position.Y == 8);
            Cell note2 = grid.Find(node => node.Position.X == 1 && node.Position.Y == 7);
            Cell note3 = grid.Find(node => node.Position.X == 1 && node.Position.Y == 6);

            Cell note4 = grid.Find(node => node.Position.X == 1 && node.Position.Y == 5);
            Cell note5 = grid.Find(node => node.Position.X == 2 && node.Position.Y == 5);
            Cell note6 = grid.Find(node => node.Position.X == 3 && node.Position.Y == 5);

            Cell note7 = grid.Find(node => node.Position.X == 3 && node.Position.Y == 4);
            Cell note8 = grid.Find(node => node.Position.X == 3 && node.Position.Y == 3);
            Cell note9 = grid.Find(node => node.Position.X == 3 && node.Position.Y == 2);
            Cell note10 = grid.Find(node => node.Position.X == 3 && node.Position.Y == 1);
            Cell note11 = grid.Find(node => node.Position.X == 3 && node.Position.Y == 0);

            Cell note12 = grid.Find(node => node.Position.X == 4 && node.Position.Y == 0);
            Cell note13 = grid.Find(node => node.Position.X == 5 && node.Position.Y == 0);
            Cell note14 = grid.Find(node => node.Position.X == 6 && node.Position.Y == 0);
            Cell note15 = grid.Find(node => node.Position.X == 7 && node.Position.Y == 0);

            Cell note16 = grid.Find(node => node.Position.X == 7 && node.Position.Y == 1);
            Cell note17 = grid.Find(node => node.Position.X == 7 && node.Position.Y == 2);
            Cell note18 = grid.Find(node => node.Position.X == 7 && node.Position.Y == 3);
            Cell note19 = grid.Find(node => node.Position.X == 7 && node.Position.Y == 4);
            Cell note20 = grid.Find(node => node.Position.X == 7 && node.Position.Y == 5);

            Cell note21 = grid.Find(node => node.Position.X == 8 && node.Position.Y == 5);
            Cell note22 = grid.Find(node => node.Position.X == 8 && node.Position.Y == 6);
            Cell note23 = grid.Find(node => node.Position.X == 8 && node.Position.Y == 8);

            Cell note24 = grid.Find(node => node.Position.X == 7 && node.Position.Y == 8);
            Cell note25 = grid.Find(node => node.Position.X == 6 && node.Position.Y == 8);
            Cell note26 = grid.Find(node => node.Position.X == 5 && node.Position.Y == 8);
            Cell note27 = grid.Find(node => node.Position.X == 4 && node.Position.Y == 8);
            Cell note28 = grid.Find(node => node.Position.X == 3 && node.Position.Y == 8);
            Cell note29 = grid.Find(node => node.Position.X == 2 && node.Position.Y == 8);

            note1.MyType = CellType.PATH;
            note2.MyType = CellType.PATH;
            note3.MyType = CellType.PATH;
            note4.MyType = CellType.PATH;
            note5.MyType = CellType.PATH;
            note6.MyType = CellType.PATH;
            note7.MyType = CellType.PATH;
            note8.MyType = CellType.PATH;
            note9.MyType = CellType.PATH;
            note10.MyType = CellType.PATH;
            note11.MyType = CellType.PATH;
            note12.MyType = CellType.PATH;
            note13.MyType = CellType.PATH;
            note14.MyType = CellType.PATH;
            note15.MyType = CellType.PATH;
            note16.MyType = CellType.PATH;
            note17.MyType = CellType.PATH;
            note18.MyType = CellType.PATH;
            note19.MyType = CellType.PATH;
            note20.MyType = CellType.PATH;
            note21.MyType = CellType.PATH;
            note22.MyType = CellType.PATH;
            note23.MyType = CellType.PATH;
            note24.MyType = CellType.PATH;

            note25.MyType = CellType.FORESTPATH;
            note26.MyType = CellType.FORESTPATH;
            note27.MyType = CellType.FORESTPATH;
            note28.MyType = CellType.FORESTPATH;
            note29.MyType = CellType.FORESTPATH;
            #endregion

            //Finds the empty celltypes and gives them images, and adds them to a list so we can place the keys
            //and adds an image to the path types
            foreach (Cell item in grid)
            {
                if (item.MyType == CellType.EMPTY)
                {
                    item.Sprite = Image.FromFile(@"Images\Grass.png");
                    item.Walkable = true;
                    emptylist.Add(item);
                }
                if (item.MyType == CellType.PATH)
                {
                    item.Walkable = true;
                    item.Sprite = Image.FromFile(@"Images\Path.png");
                }
                if (item.MyType == CellType.FORESTPATH)
                {
                    item.Walkable = true;
                    item.Sprite = Image.FromFile(@"Images\DarkDirt.png");
                }
                if (item.Position == new Point(1, 8))
                {
                    wStartCell = item;
                }


            }
            wizard = new Wizard(wStartCell);
            Random rnd = new Random();

            int rndtal = rnd.Next(0, emptylist.Count);
            Cell stormKey = emptylist[rndtal];

            rndtal = rnd.Next(0, emptylist.Count);
            Cell iceKey = emptylist[rndtal];

            stormKey.MyType = CellType.STORMKEY;
            stormKey.Walkable = true;
            stormKey.Sprite = Image.FromFile(@"Images\GoldKey.png");

            iceKey.MyType = CellType.ICEKEY;
            iceKey.Walkable = true;
            iceKey.Sprite = Image.FromFile(@"Images\BlueKey.png");
        }

        public void ChooseAlgorithm()
        {
            if(goalQueue.Count != 0)
            {
                goal = goalQueue.Dequeue();

                switch (algorithm)
                {
                    case 1:
                        wizard.ClausAstar(wStartCell);
                        break;
                    case 2:
                        foreach (Cell c in grid)
                        {
                            c.MyEdges.Clear();
                            c.Visited = false;
                        }
                        AddCellEdges();
                        bfs = new BFS();
                        Cell endGoal = bfs.RunBFS(grid, goal, wizard.Position);
                        path = bfs.TrackPath(endGoal, wizard.Position);
                        
                        
                        break;
                    case 3:
                        foreach (Cell c in grid)
                        {
                            c.MyEdges.Clear();
                            c.Visited = false;
                        }
                        AddCellEdges();
                        dfs = new DFS();
                        Cell endgoal = dfs.RunDFS(grid, goal, wizard.Position);
                        path = dfs.TrackPath(endgoal, wizard.Position);
                        break;
                    default:
                        break;
                }
            }
            else
            {
              
            }
        }
    }
}
