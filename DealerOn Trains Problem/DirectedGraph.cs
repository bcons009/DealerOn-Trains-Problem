using System;
using System.Collections.Generic;

// TODO: C# standard of set/get for variables

namespace DealerOn_Trains_Problem
{
    /// <summary>
    /// A class for an object that represents a directed graph. Contains a list
    /// of Node objects representing all the nodes within the graph, as well as 
    /// setter and getter methods for those nodes.
    /// </summary>
	public class DirectedGraph
	{
		public List<Node> Nodes { get; set; }

		public DirectedGraph()
		{
			Nodes = new List<Node>();
		}

        /// <summary>
        /// Creates a new Node object to add to the graph.
        /// </summary>
        /// <remarks>
        /// Will instead return a pre-existing Node object if the node name provided matches 
        /// the name of a pre-existing node.
        /// </remarks>
        /// <param name="name">A character representing the name of the new node.</param>
        /// <returns>Returns either the newly created Node object, or a Node object with the
        /// same name as the param "name".</returns>
		public Node AddNode(char name)
        {
            Node node = GetNode(name);
			if (node == null)
			{
                Node newNode = new Node { Name = name };
                Nodes.Add(newNode);
                return newNode;
			}
            else return node;
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
			foreach(Node node in Nodes)
            {
				if(node.Name == name)
					return node;
            }
			return null;
        }
	}
}
