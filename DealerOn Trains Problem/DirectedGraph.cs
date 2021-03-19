using System;
using System.Collections.Generic;

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

    /// <summary>
    /// A class for an object that represents a node in a directed graph.
    /// Includes the node's name and a list of all the edges pointing outward from the node.
    /// Also includes setter and getter functions for edges in the edge list.
    /// </summary>
    public class Node
    {
        /// <value>The character representing the node's name</value>
        private char name;
        /// <value>The list of edges pointing outward from the node.</value>
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

        /// <summary>
        /// Add a new edge leading out from the node to another node.
        /// </summary>
        /// <remarks>
        /// Returns false if the user tries to add an edge to the same node
        /// (creating a single-node loop). If the user tries to add an edge to 
        /// a node that already has an edge from the current node, the node's weight
        /// will be updated with the new weight specified in the argument int weight.
        /// </remarks>
        /// <param name="n">A Node object that will be connected 
        /// to the current node via the new edge</param>
        /// <param name="weight">A number representing the weight of the new edge</param>
        /// <returns>Boolean; returns true if an edge was created or updated, otherwise false.</returns>
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

        /// <summary>
        /// Finds and returns the Edge between the current Node and the 
        /// Node specified in the argument Node n, if such an Edge exists.
        /// </summary>
        /// <param name="n">The Node object to search for when looking
        /// through all the edges in the edges list.</param>
        /// <returns>
        /// Returns the Edge between the current Node and the Node
        /// specified in the argument Node n. Returns null if there is no Edge 
        /// found between the current Node and Node n.
        /// </returns>
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

    /// <summary>
    /// A class for an object that represents a directed graph. Contains a list
    /// of Node objects representing all the nodes within the graph, as well as 
    /// setter and getter methods for those nodes.
    /// </summary>
	public class DirectedGraph
	{
		private List<Node> nodes;

		public DirectedGraph()
		{
			nodes = new List<Node>();
		}

        /// <summary>
        /// Creates a new Node object to add to the graph.
        /// </summary>
        /// <remarks>
        /// Will not create a new node if the node name provided matches the name
        /// of a pre-existing node.
        /// </remarks>
        /// <param name="name">A character representing the name of the new node.</param>
        /// <returns>Boolean; returns true if the node was created, otherwise false.</returns>
		public bool AddNode(char name)
        {
			if (GetNode(name) == null)
			{
				nodes.Add(new Node(name));
				return true;
			}
			else return false;
        }

        /// <summary>
        /// Iterates through the list of Nodes to find a node with the same name as the character
        /// provided as an argument.
        /// </summary>
        /// <param name="name">The name of the node to find within the nodes list.</param>
        /// <returns>
        /// The node with the same name as the param "name". If no such node is
        /// found, returns null.
        /// </returns>
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
