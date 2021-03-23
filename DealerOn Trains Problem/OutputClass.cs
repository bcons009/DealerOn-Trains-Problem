using System;
using System.Collections.Generic;

namespace DealerOn_Trains_Problem
{
	public static class OutputClass
	{
		private const string NO_ROUTE = "NO SUCH ROUTE";

		/// <summary>
		/// Method used to redirect the program to the necessary output case method
		/// depending on what output # the program is on.
		/// </summary>
		/// <param name="outputNum">The output case number the program is currently on.</param>
		/// <param name="map">The DirectedGraph representing the train routes.</param>
		public static void CaseHandler(int outputNum, DirectedGraph map)
        {
			switch (outputNum)
            {
				case 1:
					DistanceCalc(outputNum, new char[] { 'A', 'B', 'C' }, map);
					break;
				case 2:
					DistanceCalc(outputNum, new char[] { 'A', 'D' }, map);
					break;
				case 3:
					DistanceCalc(outputNum, new char[] { 'A', 'D', 'C' }, map);
					break;
				case 4:
					DistanceCalc(outputNum, new char[] { 'A', 'E', 'B', 'C', 'D' }, map);
					break;
				case 5:
					DistanceCalc(outputNum, new char[] { 'A', 'E', 'D' }, map);
					break;
				case 6:
					NumTripsByStops(outputNum, 'C', 'C', 3, "max", map);
					break;
				case 7:
					NumTripsByStops(outputNum, 'A', 'C', 4, "exactly", map);
					break;
				case 8:
					ShortestRoute(outputNum, 'A', 'C', map);
					break;
				case 9:
					ShortestRoute(outputNum, 'B', 'B', map);
					break;
				case 10:
					NumTripsMaxDistance(outputNum, 'C', 'C', 30, map);
					break;
				default:
					break;
			}
        }

		/// <summary>
		/// Helper method for printing output to the console once an output method has
		/// completed its calculations.
		/// </summary>
		/// <param name="outputNum">The output case number the program is currently on.</param>
		/// <param name="result">The result calculated by another output method for the 
		/// current output case.</param>
		private static void Print(int outputNum, string result)
		{
			string output = "Output #" + outputNum + ": " + result;
			Console.WriteLine(output);
		}

		/// <summary>
		/// Calculates the total distance of a route using the edge weights between nodes.
		/// </summary>
		/// <param name="outputNum">The output case number the program is currently on.</param>
		/// <param name="townNames">An array containing the list of towns for which a route
		/// needs to be calculated.</param>
		/// <param name="map">The DirectedGraph object representing the map of train routes.</param>
		private static void DistanceCalc(int outputNum, char[] townNames, DirectedGraph map)
        {
			int totalDistance = 0;
			Node prevTown = null;

			// This loop searches for all of the town nodes and the edges connecting them 
			// in DirectedGraph map.
			foreach (char town in townNames)
            {
				Node currentTown = map.GetNode(town);
				if (currentTown == null)
				{
					// If at least one town specified in townNames does not have a Node object
					// representing it in DirectedGraph map, this error message is thrown.
					Print(outputNum, NO_ROUTE);
					return;
				}

				if (prevTown != null)
				{
					Edge edge = prevTown.FindEdgeTo(currentTown);
					if(edge == null)
                    {
						// If two consecutive towns specified in townNames do not have an Edge object
						// connecting them in DirectedGraph map, this error message is thrown.
						Print(outputNum, NO_ROUTE);
						return;
					}
					totalDistance += edge.Weight;
				}
				prevTown = currentTown;
			}

			Print(outputNum, totalDistance.ToString());
		}

		/// <summary>
		/// Calculates how many routes exist from one town to another with either at most or
		/// exactly n number of stops.
		/// </summary>
		/// <param name="outputNum">The output case number the program is currently on.</param>
		/// <param name="startTown">The starting town where route calculations begin.</param>
		/// <param name="endTown">The final town in a valid route.</param>
		/// <param name="numStops">Either the exact number or maximum number of stops a route 
		/// can have, depending on the value of compareMode.</param>
		/// <param name="compareMode">The string specifying what calculation this method will
		/// be performing. If set to "max", the method will calculate the number of routes that
		/// have at most numStops number of stops. If set to "exactly", will calculate the number
		/// of routes that have exactly numStops number of stops.</param>
		/// <param name="map">The DirectedGraph object representing the map of train routes.</param>
		private static void NumTripsByStops(int outputNum, char startTown, 
			char endTown, int numStops, string compareMode, DirectedGraph map)
        {
			Node startNode = map.GetNode(startTown);
			if (numStops < 0)
            {
				Print(outputNum, "ERROR: Cannot calculate routes using a negative number of stops.");
				return;
            }

			// Iterative DFS with stack
				// The second item in each tuple counts how many stops the route
				// has already made from that Node.
				// Very important, since the same Node can be accessed 
				// multiple times by multiple routes.
			Stack<Tuple<Node, int>> stack = new Stack<Tuple<Node, int>>();
			stack.Push(new Tuple<Node, int>(startNode, 0));

			// Is the program in its first iteration of the loop?
				// Used to prevent cases where startTown = endTown and a route with
				// 0 stops is recognized.
			bool firstIteration = true;
			int routeCount = 0;

			while (stack.Count > 0)
            {
				Node currentNode = stack.Peek().Item1;
				int stopsCounter = stack.Peek().Item2;
				stack.Pop();
				// If the current node is the end town, then a route has reached from the start town
				// to the end town within numStops stops.
					// Check the compareMode value. For compareMode = "exactly", only increment if 
					// the route got to the end town in exactly numStops stops.
				if (currentNode.Name == endTown && !firstIteration &&
					(compareMode == "max" || (compareMode == "exactly" && stopsCounter == numStops)))
                {
					routeCount++;
				}
				stopsCounter++;
				firstIteration = false;

				// If the current route's number of stops <= the number of stops
				// specified for the problem, add all adjacent Nodes to the stack with
				// the number of stops incremented for those possible routes.
				if(stopsCounter <= numStops)
					foreach (Edge edge in currentNode.Edges)
						stack.Push(new Tuple<Node, int>(edge.Node, stopsCounter));
            }

			Print(outputNum, routeCount.ToString());
        }

		/// <summary>
		/// Calculates the shortest route between two towns.
		/// </summary>
		/// <remarks>
		/// Performs this calculation by finding the route between two nodes with the smallest
		/// sum of edge weights.
		/// </remarks>
		/// <param name="outputNum">The output case number the program is currently on.</param>
		/// <param name="startTown">The starting town where route calculations begin.</param>
		/// <param name="endTown">The final town in a valid route.</param>
		/// <param name="map">The DirectedGraph object representing the map of train routes.</param>
		private static void ShortestRoute(int outputNum, char startTown, char endTown, DirectedGraph map)
        {
			Node startNode = map.GetNode(startTown);
			int minDistance = Int32.MaxValue;   // init the minDistance to the highest possible value

			// Iterative DFS with stack
				// The third item in each tuple counts the distance already 
					// traveled in the current route.
					// Useful when the same node is accessed multiple times
					// by multiple routes.
				// The second item in each tuple counts how many stops the route
					// has already made from that Node.
					// Used to prevent infinite loops while minDistance is still MaxValue.
			Stack<Tuple<Node, int, int>> stack = new Stack<Tuple<Node, int, int>>();
			stack.Push(new Tuple<Node, int, int>(startNode, 0, 0));

			// Is the program in its first iteration of the loop?
			// Used to prevent cases where startTown = endTown and a route with
			// distance = 0 is recognized.
			bool firstIteration = true;

			while (stack.Count > 0)
			{
				Node currentNode = stack.Peek().Item1;
				int stopsCounter = stack.Peek().Item2;
				int distanceCount = stack.Peek().Item3;
				stack.Pop();

				// If the current node is the end town (and this isn't the loop's first iteration),
				// then a valid route was found. 
				// Compare the distanceCount of the current route to minDistance.
				if (currentNode.Name == endTown && !firstIteration && distanceCount < minDistance)
					minDistance = distanceCount;
				stopsCounter++;

				// If a valid route hasn't been found yet with the current node AND the number of stops
				// in the current route is <= the number of stops in the map...
					// ("stopsCounter <= map.Nodes.Count" is used to prevent the loop from infinitely
					// adding a cycle of nodes to the stack while minDistance = maxValue)
				if (stopsCounter <= map.Nodes.Count && (firstIteration || currentNode.Name != endTown))
				{
					// Add all adjacent nodes/towns to the stack with the updated stopsCounter and distanceCount
					foreach (Edge edge in currentNode.Edges)
						stack.Push(new Tuple<Node, int, int>(edge.Node, stopsCounter, distanceCount + edge.Weight));
					firstIteration = false;
				}
			}

			if (minDistance == Int32.MaxValue)
				Print(outputNum, NO_ROUTE);	// If a route was found, then minDistance < Int32.MaxValue
			else
				Print(outputNum, minDistance.ToString());
		}

		/// <summary>
		/// Calculates the number of routes from one town to another within a given distance.
		/// </summary>
		/// <param name="outputNum">The output case number the program is currently on.</param>
		/// <param name="startTown">The starting town where route calculations begin.</param>
		/// <param name="endTown">The final town in a valid route.</param>
		/// <param name="maxDistance">This value - 1 is the maximum distance a valid route can have.</param>
		/// <param name="map">The DirectedGraph object representing the map of train routes.</param>
		private static void NumTripsMaxDistance(int outputNum, char startTown, 
			char endTown, int maxDistance, DirectedGraph map)
		{
			Node startNode = map.GetNode(startTown);
			int routeCount = 0;

			// Iterative DFS with stack
				// The second item in each tuple counts the distance already 
				// traveled in the current route.
				// Useful when the same node is accessed multiple times
				// by multiple routes.
			Stack<Tuple<Node, int>> stack = new Stack<Tuple<Node, int>>();
			stack.Push(new Tuple<Node, int>(startNode,  0));

			// Is the program in its first iteration of the loop?
			// Used to prevent cases where startTown = endTown and a route with
			// distance = 0 is recognized.
			bool firstIteration = true;

			while (stack.Count > 0)
			{
				Node currentNode = stack.Peek().Item1;
				int distanceCount = stack.Peek().Item2;
				stack.Pop();

				// If the distance travelled by the current route < maxDistance...
				if (distanceCount < maxDistance)
                {
					// If the current node is the end town (and this isn't the loop's first iteration),
					// then a valid route was found. 
					if (currentNode.Name == endTown && !firstIteration)
						routeCount++;
					
					// Add all adjacent nodes/towns to the stack with the updated distanceCount
					foreach (Edge edge in currentNode.Edges)
						stack.Push(new Tuple<Node, int>(edge.Node, distanceCount + edge.Weight));
					firstIteration = false;
				}
			}

			Print(outputNum, routeCount.ToString());
		}
	}
}
