using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grid
{
    class DFS
    {
        public DFS()
        {

        }

        public Cell RunDFS(List<Cell> grid, Cell goal, Cell Start)
        {
            //Stack that contains all edges
            Stack<Edge> stack = new Stack<Edge>();

            //Adds the first edge to the stack, the first edge always points on itself
            foreach (Cell c in grid)
            {
                if (c == Start)
                {
                    //The starting node(Entrance, points at itself)
                    stack.Push(new Edge(c, c));
                    c.Visited = true; //Sets the starting node as visited(we assume that index 0 is Entrance)
                }
            }
            while (stack.Count > 0) //As long as we have edges to explorer
            {
                Edge currentEdge = stack.Pop(); //Gets the next edge so that we can examine it

                if (currentEdge.To.Equals(goal)) //If the edge leads to the destination/goal
                {
                    return currentEdge.To; //Return the destination node
                }

                currentEdge.To.Visited = true; //Marks the node at the end of the edge as visited as that we only explorer it once

                foreach (Edge e in currentEdge.To.MyEdges) //For all edges on the curreteEdge's end node
                {
                    if (!e.To.Visited) //If the edges end node isn't visited
                    {
                        e.To.Parent = e.From; //Sets the end node's parent as the node we came from, so that we can backtrack
                        stack.Push(e); //Pushes the edge to the stack
                    }

                }
            }

            return null;
        }

        public List<Cell> TrackPath(Cell cell, Cell start)
        {
            List<Cell> path = new List<Cell>();
            while (!cell.Equals(start))
            {
                path.Add(cell);
                cell = cell.Parent;
            }
            path.Add(start);
            path.Reverse();

            return path;
        }
    }
}
