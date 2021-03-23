using System;
using System.Collections.Generic;

namespace DealerOn_Trains_Problem
{
	public static class OutputClass
	{
		private const string NO_ROUTE = "NO SUCH ROUTE";

		public static void CaseHandler(int i, DirectedGraph map)
        {
			switch (i)
            {
				case 1:
					DistanceCalc(i, new char[] { 'A', 'B', 'C' }, map);
					break;
				case 2:
					DistanceCalc(i, new char[] { 'A', 'D' }, map);
					break;
				case 3:
					DistanceCalc(i, new char[] { 'A', 'D', 'C' }, map);
					break;
				case 4:
					DistanceCalc(i, new char[] { 'A', 'E', 'B', 'C', 'D' }, map);
					break;
				case 5:
					DistanceCalc(i, new char[] { 'A', 'E', 'D' }, map);
					break;
				case 6:
					NumTripsByStops(i, 'C', 'C', 3, "max", map);
					break;
				case 7:
					NumTripsByStops(i, 'A', 'C', 4, "exactly", map);
					break;
				case 8:
					ShortestRoute(i, 'A', 'C', map);
					break;
				case 9:
					ShortestRoute(i, 'B', 'B', map);
					break;
				case 10:
					NumTripsMaxDistance(i, 'C', 'C', 30, map);
					break;
				default:
					break;
			}
        }

		private static void Print(int caseNumber, string result)
		{
			string output = "Output #" + caseNumber + ": " + result;
			Console.WriteLine(output);
		}

		private static void DistanceCalc(int outputNum, char[] townNames, DirectedGraph map)
        {
			int totalDistance = 0;
			Node prevTown = null;

			foreach(char town in townNames)
            {
				Node currentTown = map.GetNode(town);
				if (currentTown == null)
				{
					Print(outputNum, NO_ROUTE);
					return;
				}

				if (prevTown != null)
				{
					Edge edge = prevTown.FindEdgeTo(currentTown);
					if(edge == null)
                    {
						Print(outputNum, NO_ROUTE);
						return;
					}
					totalDistance += edge.Weight;
				}
				prevTown = currentTown;
			}

			Print(outputNum, totalDistance.ToString());
		}

		private static void NumTripsByStops(int outputNum, char startTown, 
			char endTown, int numStops, string compareMode, DirectedGraph map)
        {
			// compareMode = "max" or "exactly"
			Node startNode = map.GetNode(startTown);
			if (numStops < 0)
            {
				Print(outputNum, "ERROR: Cannot calculate routes using a negative number of stops.");
				return;
            }

			// Iterative DFS with stack
			Stack<Tuple<Node, int>> stack = new Stack<Tuple<Node, int>>();
			stack.Push(new Tuple<Node, int>(startNode, 0));
			bool firstIteration = true;
			int routeCount = 0;

			while (stack.Count > 0)
            {
				Node currentNode = stack.Peek().Item1;
				int stopsCounter = stack.Peek().Item2;
				stack.Pop();
				if (currentNode.Name == endTown && !firstIteration &&
					(compareMode == "max" || (compareMode == "exactly" && stopsCounter == numStops)))
                {
					routeCount++;
				}
				stopsCounter++;
				firstIteration = false;

				if(stopsCounter <= numStops)
					foreach (Edge edge in currentNode.Edges)
						stack.Push(new Tuple<Node, int>(edge.Node, stopsCounter));
            }

			Print(outputNum, routeCount.ToString());
        }

		private static void ShortestRoute(int outputNum, char startTown, char endTown, DirectedGraph map)
        {
			Node startNode = map.GetNode(startTown);
			int minDistance = Int32.MaxValue;

			// Iterative DFS with stack
			Stack<Tuple<Node, int, int>> stack = new Stack<Tuple<Node, int, int>>();
			stack.Push(new Tuple<Node, int, int>(startNode, 0, 0));
			bool firstIteration = true;

			while (stack.Count > 0)
			{
				Node currentNode = stack.Peek().Item1;
				int stopsCounter = stack.Peek().Item2;
				int distanceCount = stack.Peek().Item3;
				stack.Pop();
				if (currentNode.Name == endTown && !firstIteration && distanceCount < minDistance)
					minDistance = distanceCount;
				stopsCounter++;

				if (stopsCounter <= map.Nodes.Count && (firstIteration || currentNode.Name != endTown))
				{
					foreach (Edge edge in currentNode.Edges)
						stack.Push(new Tuple<Node, int, int>(edge.Node, stopsCounter, distanceCount + edge.Weight));
					firstIteration = false;
				}
			}

			if (minDistance == Int32.MaxValue)
				Print(outputNum, NO_ROUTE);
			else
				Print(outputNum, minDistance.ToString());
		}

		private static void NumTripsMaxDistance(int outputNum, char startTown, 
			char endTown, int maxDistance, DirectedGraph map)
		{
			Node startNode = map.GetNode(startTown);
			int routeCount = 0;

			// Iterative DFS with stack
			Stack<Tuple<Node, int>> stack = new Stack<Tuple<Node, int>>();
			stack.Push(new Tuple<Node, int>(startNode,  0));
			bool firstIteration = true;

			while (stack.Count > 0)
			{
				Node currentNode = stack.Peek().Item1;
				int distanceCount = stack.Peek().Item2;
				stack.Pop();

				if (distanceCount < maxDistance)
                {
					if (currentNode.Name == endTown && !firstIteration)
						routeCount++;
					foreach (Edge edge in currentNode.Edges)
						stack.Push(new Tuple<Node, int>(edge.Node, distanceCount + edge.Weight));
					firstIteration = false;
				}
			}

			Print(outputNum, routeCount.ToString());
		}

		/*
		private static void DFS(Stack<Tuple<Node, int, Nullable<int>>> stack, string mode, int count)
        {
			bool firstIteration = true;
			int minDistance, distanceCount, stopsCount, routeCount = 0;
			if (mode == "Shortest Route")
				minDistance = Int32.MaxValue;

			while (stack.Count > 0)
            {
				Node currentNode = stack.Peek().Item1;
				if (mode != "Max Distance")
					stopsCount = stack.Peek().Item2;
				else
					distanceCount = stack.Peek().Item2;
				if (mode == "Shortest Route")
					distanceCount = (int) stack.Peek().Item3;


			}
        }
		*/
	}
}
