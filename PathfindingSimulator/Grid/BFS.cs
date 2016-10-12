using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grid
{
    class BFS
    {
        public BFS()
        {

        }

        public Cell RunBFS(List<Cell> grid, Cell goal, Cell Start)
        {
            //Queue that contains all edges
            Queue<Edge> queue = new Queue<Edge>();
            foreach (Cell c in grid)
            {
                if(c == Start)
                {
                    //The starting node(Entrance, points at itself)
                    queue.Enqueue(new Edge(c, c));
                    c.Visited = true; //Sets the starting node as visited(we assume that index 0 is Entrance)
                }
            }


            while (queue.Count > 0) //As long as the queue contains edges (we still have edges to explorer)
            {
                Edge currentEdge = queue.Dequeue(); //Dequeues an edge from the queue so that we can examine it

                if (currentEdge.To.Equals(goal)) //Checks if the current edge leads to the goal
                {
                    return currentEdge.To;

                }
                foreach (Edge edge in currentEdge.To.MyEdges) //Examines all the edges on the currenEdge's endNode
                {
                    if (!edge.To.Visited) //If the edge haven't been visited
                    {
                        edge.To.Visited = true; //Mark it as visited so that we only explorer it once
                        queue.Enqueue(edge); //Enqueue the edge, so that we can explorer it later
                        edge.To.Parent = edge.From; //Set the endNodes parent, so that we can backtrack later
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
