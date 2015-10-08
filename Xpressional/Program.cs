using System;
using Xpressional.Data.Managers;
using System.IO;
using Xpressional.Data.Graphs;

namespace Xpressional
{
	class MainClass
	{
		static void ProcessDocument(string[] args){
			var output = new OutputManager (new ConsoleHelper ());
			var fileContents = File.ReadAllLines (args.Length != 0 ? args[0] : "test_expressions.txt");
			var inputManager = new InputManager ();

			foreach (var line in fileContents) {
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine ("======= BEGIN GRAPH =======");
				Console.WriteLine ("Input Expression: " + line);
				Console.ResetColor ();

				var outputGraph = inputManager.ParseExpression (line);
				output.Print (outputGraph);

				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine ("======= END GRAPH =======");
				Console.ResetColor ();

			}
		}

		public static void Main (string[] args)
		{
			try{
				ProcessDocument (args);
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine ("There was an error while processing the document: " + e.Message);
				Console.ResetColor ();
			}
		}
	}
}
