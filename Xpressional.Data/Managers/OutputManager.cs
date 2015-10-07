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

		void PrintStatesOutputs(GraphState currentState,
			List<GraphState> visited, List<string> output){

			visited.Add (currentState);

			foreach (var state in currentState.Out) {

				//Console.WriteLine (state.ToString ());
				output.Add(state.ToString());
				if (!visited.Contains (state.End)) {
					//not seen, so keep going.
					PrintStatesOutputs(state.End, visited, output);
				}
			}

		}

		public void Print(Graph toPrint){
			//if state has been visited, don't do it again to avoid SO

			List<GraphState> visited = new List<GraphState> ();
			List<string> output = new List<string> ();

			Console.WriteLine ("Start state is: q" + toPrint.StartState.StateNumber);
				

			PrintStatesOutputs (toPrint.StartState, visited, output);

			foreach (var row in output.OrderBy (s => s)) {
				Console.WriteLine (row);
			}
		}


		public OutputManager (IConsole console)
		{
			Console = console;
		}
	}
}

