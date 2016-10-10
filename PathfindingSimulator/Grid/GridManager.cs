using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Grid
{
    class GridManager
    {
        private BufferedGraphics backBuffer;
        private Graphics dc;
        private Rectangle displayRectangle;

        private int cellRowCount;

        private List<Cell> grid;

        private CellType clickType;

        public GridManager(Graphics dc, Rectangle displayRectangle)
        {
            this.backBuffer = BufferedGraphicsManager.Current.Allocate(dc, displayRectangle);
            this.dc = backBuffer.Graphics;
            this.displayRectangle = displayRectangle;

            cellRowCount = 10;

            SetupWorld();
        }

        public void Render()
        {

            dc.Clear(Color.Green);

            foreach (Cell cell in grid)
            {
                cell.Render(dc);
            }

            backBuffer.Render();
        }

        public void CreateGrid()
        {
            grid = new List<Cell>();

            int cellSize = displayRectangle.Width / cellRowCount;

            for (int x = 0; x < cellRowCount; x++)
            {
                for (int y = 0; y < cellRowCount; y++)
                {
                    grid.Add(new Cell(new Point(x, y), cellSize));
                }
            }
        }


        public void GameLoop()
        {
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
            //Cell iceTower = grid.Find(node => node.Position.X == 8 && node.Position.Y == 7);
            //iceTower.MyType = CellType.ICE;
            //iceTower.Walkable = false;
            //iceTower.Sprite = Image.FromFile(@"Images\Ice_Castle.png");

            //Creates the storm tower
            //Cell stormTower = grid.Find(node => node.Position.X == 2 && node.Position.Y == 4);
            //stormTower.MyType = CellType.STORM;
            //stormTower.Walkable = false;
            //stormTower.Sprite = Image.FromFile(@"Images\Lighting_Castle.png");

            //Creates the Rocks
            //for (int x = 4; x < 7; x++)
            //{
            //    for (int y = 1; y < 7; y++)
            //    {
            //        Cell wall = grid.Find(node => node.Position.X == x && node.Position.Y == y);

            //        if (wall.MyType != CellType.WALL)
            //        {
            //            wall.MyType = CellType.WALL;
            //            wall.Walkable = false;
            //            wall.Sprite = Image.FromFile(@"Images\Rock.png");
            //        }
            //    }
            //}

            //Creates the trees
            //for (int forestX = 2; forestX < 7; forestX++)
            //{
            //    for (int forestY = 7; forestY < 10; forestY++)
            //    {
            //        if (forestY == 7 || forestY == 9)
            //        {
            //            Cell forest = grid.Find(node => node.Position.X == forestX && node.Position.Y == forestY);

            //            if (forest.MyType != CellType.FOREST)
            //            {
            //                forest.MyType = CellType.FOREST;
            //                forest.Walkable = false;
            //                forest.Sprite = Image.FromFile(@"Images\ForestPath.png");
            //            }

            //        }
            //    }
            //}
        }
    }
}
