using System;
using Xpressional.Data.Graphs;
using Xpressional.Data.Interfaces;
using Xpressional.Data.Models;
using System.Text;
using System.Linq;

namespace Xpressional.Data.Managers
{
	public sealed class OutputManager
	{
		IConsole Console { get; set; }
		Language Words {get;set;}

		void PrintHeader(){
			var header = "\t";

			foreach (var word in Words) {
				header += string.Format("{0} | \t", word.Letter);
			}
			//print out the header
			Console.WriteLine(header);
			Console.WriteLine ("------------------------------");
		}

		void PrintRow(GraphState currentState, StringBuilder toOutput){
			var defaultConnection = new GraphStateConnection () {
				ConnectedBy = new Word()
			};

			//initial state (q1)
			toOutput.AppendFormat ("q{0}", currentState.StateNumber);
			if (currentState.IsFinal) {
				toOutput.Append (" S");
			}
			toOutput.Append (" | \t");

			var connectionWords = currentState.Out;


			for (int i = 0; i < Words.Count; i++) {
				//current letter to print out
				var letter = Words[i];

				var connectedLetter = connectionWords
					.DefaultIfEmpty (defaultConnection)
					.FirstOrDefault (w => w.ConnectedBy.Letter == letter.Letter);

				//prints the connecting letter or 
				if (connectedLetter == defaultConnection) {
					//ouput nothing
					toOutput.Append ("   | \t");
				} else {
					//output the state
					toOutput.AppendFormat ("q{0} | \t", connectedLetter.End.StateNumber);
				}
			}
			Console.WriteLine (toOutput.ToString ());
		}

		void PrintGraph(Graph toPrint){
			//print states
			var toOutput = new StringBuilder();

			foreach (var state in toPrint.StartState.Out) {
				PrintRow (state.End, toOutput);
			}
		}

		public void PrintNDFA(Graph toPrint){
			PrintHeader ();

			PrintGraph (toPrint);

		}

		public OutputManager (IConsole console)
		{
			Console = console;
			Words = new Language ();
		}
	}
}

