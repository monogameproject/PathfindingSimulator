using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grid
{
    class Edge
    {
        /// <summary>
        /// Region that contains all variables
        /// </summary>
        #region Variables

        private Cell from; //Starting point of the edge

        private Cell to; //Endpoint of the edge

        #endregion

        /// <summary>
        /// Region that contains all peoperties
        /// </summary>
        #region Propterties

        public Cell To
        {
            get { return to; }
            set { to = value; }
        }

        public Cell From
        {
            get { return from; }
            set { from = value; }
        }

        #endregion

        /// <summary>
        /// The edge's constructor
        /// </summary>
        /// <param name="From">The edge's starting node</param>
        /// <param name="To">The edge's endnode</param>
        public Edge(Cell From, Cell To)
        {
            from = From;
            to = To;
        }
    }
}
