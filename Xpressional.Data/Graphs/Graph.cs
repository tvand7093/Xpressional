using System;
using System.Collections.Generic;
using Xpressional.Data.Models;

namespace Xpressional.Data.Graphs
{
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

		public Graph ()
		{
			StartState = null;
			StateCount = 1;
		}

		/// <summary>
		/// Renumbers the states for when graphs are merged or updated in any way.
		/// </summary>
		/// <param name="initState">The initial state used as a base for the count.</param>
		void RenumberStates(GraphState initState) {
			if (initState == null)
				throw new NullReferenceException ("Initial State must not be null.");

//			if (initState.StateNumber == -1) {
//				//set to 0 as new start state.
//				initState.StateNumber = 1;
//			}

			//not final, so continue like normal.
			foreach (var connection in initState.Out) {
				//check the outgoing connections
				connection.End.StateNumber = initState.StateNumber + 1;
				RenumberStates(connection.End);
			}
		}

		/// <summary>
		/// Finds all of the final states in the graph.
		/// </summary>
		/// <returns>The final states that were found.</returns>
		/// <param name="initState">The root state to start the search at.</param>
		public List<GraphState> FindFinalStates(GraphState initState) {
			if (initState == null)
				throw new NullReferenceException ("Initial State must not be null.");

			var finals = new List<GraphState> ();

			if(initState.IsFinal){
				//add to list of final states
				finals.Add(initState);
			}
			//not final, so continue like normal.
			foreach (var connection in initState.Out) {
				//check the outgoing connections
				var nested = FindFinalStates(connection.End);
				//add the results from nested search to final result
				finals.AddRange (nested);
			}

			return finals;
		}

		/// <summary>
		/// Performs the Union on two NDFA's. 
		/// </summary>
		/// <param name="m2">The NDFA to Union with.</param>
		/// <returns>>A new NDFA that is the union of the two graphs.</returns>
		public Graph Union(Graph m2){
			var m = new Graph ();
			var m1 = this;

			//new q0
			m.StartState = new GraphState ();
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
			RenumberStates (m.StartState);
			return m;
		}

		/// <summary>
		/// Concatinates two NDFA's.
		/// </summary>
		/// <param name="m2">The second NDFA to concatinate with.</param>
		/// <returns>The new NDFA that is this and m2 concatinated.</returns>
		public Graph Concat(Graph m2) {
			var m = new Graph ();
			var m1 = this;

			var m1FinalStates = m1.FindFinalStates (m1.StartState);

			foreach (var finalState in m1FinalStates) {
				//create a new connection to m2 initial state
				var m1FinalTom2Initial = new GraphStateConnection () {
					Start = finalState,
					End = m2.StartState,
					ConnectedBy = Word.Epsilon
				};

				//make the connection
				finalState.Out.Add (m1FinalTom2Initial);

				//kill m1 final states and leave m2 as finals for m
				finalState.IsFinal = false;
			}

			//update state count for new graph
			m.StartState = m1.StartState;
			m.StateCount = m1.StateCount + m2.StateCount;
			RenumberStates (m.StartState);
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
				StateNumber = m1.StateCount
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

