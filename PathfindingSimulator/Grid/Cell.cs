using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Grid.CellType;

namespace Grid
{
    enum CellType { EMPTY, PORTAL, STORM, ICE, FOREST, WALL, STORMKEY, ICEKEY, PATH, FORESTPATH }

    class Cell
    {

        private Point position;

        private int cellSize;

        private Image sprite;

        CellType myType = EMPTY;

        private bool walkable;

        private int f, g, h;

        private Cell parent;

        private List<Edge> myEdges = new List<Edge>();

        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle(position.X * cellSize, position.Y * cellSize, cellSize, cellSize);
            }
        }
        public Cell Parent
        {
            get
            {
                return parent;
            }

            set
            {
                parent = value;
            }
        }
        public CellType MyType
        {
            get { return myType; }
            set { myType = value; }
        }
        public Point Position
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

        public bool Walkable
        {
            get
            {
                return walkable;
            }

            set
            {
                walkable = value;
            }
        }

        public Image Sprite
        {
            get
            {
                return sprite;
            }

            set
            {
                sprite = value;
            }
        }

        public int F
        {
            get
            {
                return f;
            }

            set
            {
                f = value;
            }
        }

        public int G
        {
            get
            {
                return g;
            }

            set
            {
                g = value;
            }
        }

        public int H
        {
            get
            {
                return h;
            }

            set
            {
                h = value;
            }
        }

        public List<Edge> MyEdges
        {
            get
            {
                return myEdges;
            }

            set
            {
                myEdges = value;
            }
        }

        /// <summary>
        /// The cell's constructor
        /// </summary>
        /// <param name="position">The cell's grid position</param>
        /// <param name="size">The cell's size</param>
        public Cell(Point position, int size)
        {
            this.position = position;

            this.cellSize = size;
            parent = this;

        }

        /// <summary>
        /// Renders the cell
        /// </summary>
        /// <param name="dc">The graphic context</param>
        public void Render(Graphics dc)
        {
            dc.FillRectangle(new SolidBrush(Color.White), BoundingRectangle);

            dc.DrawRectangle(new Pen(Color.Black), BoundingRectangle);

            if (sprite != null)
            {
                dc.DrawImage(sprite, BoundingRectangle);
            }


#if DEBUG
            dc.DrawString(string.Format("{0}", position), new Font("Arial", 7, FontStyle.Regular), new SolidBrush(Color.Black), position.X * cellSize, (position.Y * cellSize) + 10);
            dc.DrawString(string.Format("F {0} \nG {1} \nH {2}", f, g, h, parent.Position), new Font("Arial", 7, FontStyle.Regular), new SolidBrush(Color.Red), position.X * cellSize, (position.Y * cellSize) + 20);

#endif
        }

        public int Calculate(Cell goal)
        {
            //g
            Point diff = new Point(position.X - Parent.Position.X, position.Y - Parent.Position.Y);

            if (Math.Abs(diff.X) == 1 && Math.Abs(diff.Y) == 1)
            {
                g = Parent.G + 14;
            }
            else if (diff.X == 0 && diff.Y == 0 )
            {
                g = Parent.G + 0;
            }
            else 
            {
                g = Parent.G + 10;
            }

            //h
            diff = new Point(Math.Abs(goal.Position.X - position.X), Math.Abs(goal.Position.Y - position.Y));
            h = (diff.X + diff.Y) * 10;
            

            //f
            f = g + h;
           
            return f;
        }
        public void AddEdge(Cell Other)
        {
            myEdges.Add(new Edge(this, Other));
        }
    }
}
