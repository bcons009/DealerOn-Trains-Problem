using System;
using System.Collections.Generic;
using System.Text;

namespace DealerOn_Trains_Problem
{
    /// <summary>
    /// A class for an object that represents an edge in a directed graph.
    /// Each edge contains an edge weight and a Node object that the edge is pointing to.
    /// </summary>
    /// <remarks>
    /// Includes overriden Equals and GetHashCode functions for comparing Edge objects,
    /// used when creating List<Edge>() objects.
    /// </remarks>
    public class Edge : IEquatable<Edge>
    {
        public Node Node { get; set; }

        public int Weight { get; set; }

        public override int GetHashCode()
        {
            return Node.Name;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Edge objAsEdge = obj as Edge;
            if (objAsEdge == null)
                return false;
            else
                return Equals(objAsEdge);
        }
        public bool Equals(Edge other)
        {
            if (other == null)
                return false;
            return (this.Node.Name.Equals(other.Node.Name));
        }
    }

}
