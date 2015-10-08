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
		/// <summary>
		/// Gets the console object to write with.
		/// </summary>
		IConsole Console { get; set; }

		/// <summary>
		/// Recursive call to loop over states and print them.
		/// </summary>
		/// <param name="currentState">Current state.</param>
		/// <param name="visited">List of states visited.</param>
		/// <param name="output">List of state outputs.</param>
		void PrintStatesOutputs(GraphState currentState,
			List<GraphState> visited, List<string> output){

			//add this node to the visited list
			visited.Add (currentState);

			//for each connection, recurse and add the output to our master list.
			foreach (var state in currentState.Out) {
				//add the connection friendly string to the master list.
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

			//output start state
			Console.WriteLine ("Start state is: q" + toPrint.StartState.StateNumber);
				
			//find all state strings to output.
			PrintStatesOutputs (toPrint.StartState, visited, output);

			//now print each row.
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

