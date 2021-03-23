using System;
using System.Collections.Generic;

namespace DealerOn_Trains_Problem
{
    /// <summary>
    /// A class for an object that represents a node in a directed graph.
    /// Includes the node's name and a list of all the edges pointing outward from the node.
    /// Also includes setter and getter functions for edges in the edge list.
    /// </summary>
    public class Node
    {
        /// <value>The character representing the node's name</value>
        public char Name { get; set; }
        /// <value>The list of edges pointing outward from the node.</value>
        public List<Edge> Edges { get; set; }

        public Node()
        {
            Edges = new List<Edge>();
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
        /// <param name="m">A Node object that will be connected 
        /// to the current node via the new edge</param>
        /// <param name="weight">A number representing the weight of the new edge</param>
        /// <returns>Boolean; returns true if an edge was created or updated, otherwise false.</returns>
        public bool AddEdge(Node newNode, int weight)
        {
            if (newNode.Name == this.Name)
            {
                return false;
            }
            foreach (Edge edge in Edges)
            {
                if (newNode.Name == edge.Node.Name)
                {
                    edge.Weight = weight;
                    return true;
                }
            }

            Edges.Add(new Edge() { Node = newNode, Weight = weight });
            return true;
        }

        /// <summary>
        /// Finds and returns the Edge between the current Node and the 
        /// Node specified in the argument Node n, if such an Edge exists.
        /// </summary>
        /// <param name="n">The Node object to search for when looking
        /// through all the edges in the edges list.</param>
        /// <returns>
        /// The Edge between the current Node and the Node
        /// specified in the argument Node n. Returns null if there is no Edge 
        /// found between the current Node and Node n.
        /// </returns>
        public Edge FindEdgeTo(Node targetNode)
        {
            foreach (Edge edge in Edges)
            {
                if (targetNode.Name == edge.Node.Name)
                    return edge;
            }
            return null;
        }
    }

}