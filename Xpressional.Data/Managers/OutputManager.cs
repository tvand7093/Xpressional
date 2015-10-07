using System;
using Xpressional.Data.Graphs;
using Xpressional.Data.Interfaces;
using Xpressional.Data.Models;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Xpressional.Data.Managers
{
	public sealed class OutputManager
	{
		IConsole Console { get; set; }

		void PrintStatesOutputs(GraphState currentState, List<GraphState> visited){
			visited.Add (currentState);

			foreach (var state in currentState.Out) {

				var finalOrStart = string.Empty;

				if (visited.Count == 1) {
					finalOrStart += " S";
				}
				if (currentState.IsFinal) {
					finalOrStart = String.IsNullOrWhiteSpace (finalOrStart) ? " F" : "/F";
				}

				var isFinalStateForOutput = state.End.IsFinal ? " F" : string.Empty;

				Console.WriteLine ("(q{0}{1}, {2}) ==> q{3}",
					currentState.StateNumber.ToString(), 
					finalOrStart,
					state.ConnectedBy.Letter.ToString(),
					state.End.StateNumber.ToString() + isFinalStateForOutput);
				if (!visited.Contains (state.End)) {
					//not seen, so keep going.
					PrintStatesOutputs(state.End, visited);
				}
			}
		}

		public void Print(Graph toPrint){
			//if state has been visited, don't do it again to avoid SO

			List<GraphState> visited = new List<GraphState> ();
			PrintStatesOutputs (toPrint.StartState, visited);
		}


		public OutputManager (IConsole console)
		{
			Console = console;
		}
	}
}

