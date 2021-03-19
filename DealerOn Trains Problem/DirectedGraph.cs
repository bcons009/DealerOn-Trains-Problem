using System;
using System.Collections.Generic;

namespace DealerOn_Trains_Problem
{
    public class Edge : IEquatable<Edge>
    {
        public Node node { get; set; }

        public int weight { get; set; }

        public override int GetHashCode()
        {
            return node.GetName();
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
            return (this.node.GetName().Equals(other.node.GetName()));
        }
    }

    public class Node
    {
        private char name;
        private List<Edge> edges;

        public Node(char name)
        {
            this.name = name;
            edges = new List<Edge>();
        }

        public char GetName()
        {
            return name;
        }

        // Add a new edge leading out from the node to another node.
        // Returns false if the user tries to add an edge to the same node
            // (creating a single-node loop).
        // If the user tries to add an edge to a node that already has an edge 
            // from the current node, the node's weight will be updated with
            // the new weight specified in the argument int weight.
        public bool AddEdge(Node n, int weight)
        {
            if (n.GetName() == name)
            {
                return false;
            }
            foreach (Edge e in edges)
            {
                if (n.GetName() == e.node.GetName())
                {
                    e.weight = weight;
                    return true;
                }
            }

            edges.Add(new Edge() { node = n, weight = weight});
            return true;
        }

        // Returns the Edge between the current Node and the Node
            // specified in the argument Node n.
        // Returns null if there is no Edge found between the current
            // Node and Node n.
        public Edge FindEdgeTo(Node n)
        {
            foreach (Edge e in edges)
            {
                if (n.GetName() == e.node.GetName())
                    return e;
            }
            return null;
        }
    }

	public class DirectedGraph
	{
		private List<Node> nodes;

		public DirectedGraph()
		{
			nodes = new List<Node>();
		}

		public bool AddNode(char name)
        {
			if (GetNode(name) == null)
			{
				nodes.Add(new Node(name));
				return true;
			}
			else return false;
        }

        public Node GetNode(char name)
        {
			foreach(Node n in nodes)
            {
				if(n.GetName() == name)
					return n;
            }
			return null;
        }
	}
}
