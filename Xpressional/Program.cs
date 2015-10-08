using System;
using Xpressional.Data.Managers;
using System.IO;
using Xpressional.Data.Graphs;

namespace Xpressional
{
	class MainClass
	{
		static void DoWork(string[] args){
			var output = new OutputManager (new ConsoleHelper ());
			var fileContents = File.ReadAllLines (args.Length != 0 ? args[0] : "test_expressions.txt");
			var inputManager = new InputManager ();

			foreach (var line in fileContents) {
				Console.WriteLine ("======= BEGIN GRAPH =======");
				Console.WriteLine ("Input Expression: " + line);

				var outputGraph = inputManager.ParseExpression (line);
				output.Print (outputGraph);

				Console.WriteLine ("======= END GRAPH =======");
			}
		}

		public static void Main (string[] args)
		{
			DoWork (args);
		}
	}
}
