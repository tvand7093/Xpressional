using System;
using System.Collections.Generic;
using Xpressional.Data.Models;
using System.Linq;

namespace Xpressional.Data.Graphs
{
	/// <summary>
	/// Provides functionality to create a graph and manipulate it.
	/// </summary>
	public sealed class Graph
	{
		/// <summary>
		/// Gets or sets the start state for the graph.
		/// </summary>
		/// <value>The start state.</value>
		public GraphState StartState { get; set; }

		/// <summary>
		/// Gets the state count.
		/// </summary>
		/// <value>The state count.</value>
		public int StateCount { get; private set; }

		readonly bool updateStateNumber;

		/// <summary>
		/// Creates a new instance of a Graph/NFA. Provides methods for Unions, Kleen, and Concat operations.
		/// </summary>
		public Graph ()
		{
			StartState = null;
			StateCount = 1;
			updateStateNumber = true;
		}

		/// <summary>
		/// Creates a new instance of a Graph/NFA. Provides methods for Unions, Kleen, and Concat operations.
		/// </summary>
		public Graph (bool updateStateAfterOp)
		{
			StartState = null;
			StateCount = 1;
			updateStateNumber = updateStateAfterOp;
		}

		/// <summary>
		/// Renumbers the states for when graphs are merged or updated in any way.
		/// </summary>
		/// <param name="initState">The initial state used as a base for the count.</param>
		internal void RenumberStates() {
			RenumberStates (StartState);
		}

		/// <summary>
		/// Renumbers the states for the specified graph.
		/// </summary>
		/// <param name="initState">State to start with.</param>
		/// <param name="visited">A list of states already visited.</param>
		static void RenumberStates(GraphState initState, List<GraphState> visited = null) {
			if (initState == null)
				throw new NullReferenceException ("Initial State must not be null.");

			if (visited == null) {
				visited = new List<GraphState> ();
			}

			visited.Add (initState);
			//not final, so continue like normal.

			foreach (var connection in initState.Out) {
				//check the outgoing connections
				if (!visited.Contains(connection.End)) {
					connection.End.StateNumber = visited.Count;
					RenumberStates(connection.End, visited);
				}
			}
		}


		/// <summary>
		/// Finds all of the final states in the graph.
		/// </summary>
		/// <returns>The final states that were found.</returns>
		/// <param name="initState">The root state to start the search at.</param>
		/// <param name="visited">The rlist of states already visited.</param>
		public List<GraphState> FindFinalStates(GraphState initState,
			List<GraphState> visited = null) {
			if (initState == null)
				throw new NullReferenceException ("Initial State must not be null.");

			var finals = new List<GraphState> ();

			if (visited == null) {
				visited = new List<GraphState> ();
			}

			//check if we have seen this before
			var alreadyFound = visited.FirstOrDefault (s => s.Id == initState.Id);

			//not found yet, so if final, add it to our list.
			if(alreadyFound == null && initState.IsFinal){
				//add to list of final states
				finals.Add(initState);
			}

			//add to the visited list
			visited.Add (initState);

			//now go over sub-states to see if they are final or not.
			foreach (var connection in initState.Out) {
				if (visited.FirstOrDefault (s => s.Id == connection.End.Id) == null) {
					//check the outgoing connections
					var nested = FindFinalStates(connection.End, visited);
					//add the results from nested search to final result
					finals.AddRange (nested);
				}
			}

			return finals;
		}

		/// <summary>
		/// Creates a Union between this graph and a new graph created for the specified connector.
		/// </summary>
		/// <param name="toUnion">the word to use for a new graph to union with this one.</param>
		public Graph Union(Word toUnion){
			var baseGraph = Graph.CreateNewGraph (toUnion);
			return Union (baseGraph);
		}

		/// <summary>
		/// Performs the Union on two NDFA's. 
		/// </summary>
		/// <param name="m2">The NDFA to Union with.</param>
		/// <returns>>A new NDFA that is the union of the two graphs.</returns>
		public Graph Union(Graph m2){
			var m = new Graph ();
			var m1 = this;

			//new start state
			m.StartState = new GraphState ();

			//new q0
			var newFinalState = new GraphState () {
				IsFinal = true
			};
					
			//connection from q0 to q1 over epsilon
			var q0Toq1 = new GraphStateConnection () {
				ConnectedBy = Word.Epsilon,
				Start = m.StartState,
				End = m1.StartState
			};

			//connection from q0 to q2
			var q0Toq2 = new GraphStateConnection () {
				ConnectedBy = Word.Epsilon,
				Start = m.StartState,
				End = m2.StartState
			};

			//attach states.
			m.StartState.Out.Add (q0Toq1);
			m.StartState.Out.Add (q0Toq2);

			var m1Finals = FindFinalStates (m1.StartState);
			var m2Finals = FindFinalStates (m2.StartState);

			foreach (var final in m1Finals) {
				final.IsFinal = false;
				var finalConnection = new GraphStateConnection () {
					Start = final,
					End = newFinalState,
					ConnectedBy = Word.Epsilon
				};
				final.Out.Add (finalConnection);
			}

			foreach (var final in m2Finals) {
				final.IsFinal = false;
				var finalConnection = new GraphStateConnection () {
					Start = final,
					End = newFinalState,
					ConnectedBy = Word.Epsilon
				};
				final.Out.Add (finalConnection);
			}

			//update the total state count
			m.StateCount = m1.StateCount + m2.StateCount + 2;
			if (updateStateNumber) {
				RenumberStates (m.StartState);
			}
			return m;
		}

		/// <summary>
		/// Helper method to create a new graph based on a specified word.
		/// Creates a two node graph that is connected by the specified word.
		/// </summary>
		/// <returns>The new graph.</returns>
		/// <param name="fromConnection">The word to connect by.</param>
		internal static Graph CreateNewGraph(Word fromConnection){
			var graph = new Graph () {
				StartState = new GraphState() { StateNumber = 0 }
			};
			graph.StartState.Out.Add (new GraphStateConnection () {
				Start = graph.StartState,
				End = new GraphState() { StateNumber = 1, IsFinal = true },
				ConnectedBy = fromConnection
			});
			graph.StateCount = 2;
			return graph;
		}

		/// <summary>
		/// Concats this graph with a new 2 state graph that is connected by the specified word.
		/// </summary>
		/// <param name="word">The word for the new simple graph.</param>
		public Graph Concat(Word word){
			var m = Graph.CreateNewGraph (word);
			return Concat(m);
		}

		/// <summary>
		/// Concatinates two NDFA's.
		/// </summary>
		/// <param name="m1">The second NDFA to concatinate with.</param>
		/// <returns>The new graph that is this and m1 concatinated.</returns>
		public Graph Concat(Graph m1) {
			var m = new Graph ();
			var m2 = this;

			var m1FinalStates = m1.FindFinalStates (m1.StartState);

			foreach (var finalState in m1FinalStates) {
				//create a new connection to m2 initial state
				var m1FinalTom2Initial = new GraphStateConnection () {
					Start = finalState,
					End = m2.StartState,
					ConnectedBy =
						Word.Epsilon 
				};

				//make the connection
				finalState.Out.Add (m1FinalTom2Initial);

				//kill m1 final states and leave m2 as finals for m
				finalState.IsFinal = false;
			}
				
			//update state count for new graph
			m.StartState = m1.StartState;
			m.StateCount = m1.StateCount + m2.StateCount;
			if (updateStateNumber) {
				RenumberStates (m.StartState);
			}
			return m;
		}

		/// <summary>
		/// Perfoms a Kleene operation on the graph.
		/// </summary>
		/// <returns>The new graph after the Kleene operation.</returns>
		public Graph Kleene(){
			var m = new Graph ();
			var m1 = this;

			var finals = FindFinalStates (m1.StartState);
			var oldInitial = m1.StartState;

			//create new init and final state.
			var newInitial = new GraphState (){
				IsFinal = true,
				StateNumber = m1.StateCount + 1
			};

			//create connection from new initial to the old initial through epsilon
			var initialEpsilonConnection = new GraphStateConnection {
				End = oldInitial,
				Start = newInitial,
				ConnectedBy = Word.Epsilon
			};

			//add the connection to the states.
			newInitial.Out.Add (initialEpsilonConnection);

			foreach (var finalState in finals) {
				//now create epsilon connections from final to original start state
				var finalToOldInitialConnection = new GraphStateConnection {
					End = newInitial,
					Start = finalState,
					ConnectedBy = Word.Epsilon
				};

				finalState.IsFinal = false;

				//now connect them
				finalState.Out.Add(finalToOldInitialConnection);
			}

			//now attach the new init state to the graph
			m.StartState = newInitial;
			m.StateCount = m1.StateCount + 1;
			return m;
		}
	}
}

