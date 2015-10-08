using System;
using System.IO;
using Xpressional.Data.Managers;

namespace Xpressional
{
	class MainClass
	{
		/// <summary>
		/// Processes the input document and outputs all state tables.
		/// </summary>
		/// <param name="args">The command line arguements</param>
		static void ProcessDocument(string[] args){
			var output = new OutputManager (new ConsoleHelper ());
			//get file contents
			var fileContents = File.ReadAllLines (args.Length != 0 ? args[0] : "test_expressions.txt");
			var inputManager = new InputManager ();

			//foreach line in the file, make a graph then print it.
			foreach (var line in fileContents) {
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine ("======= BEGIN GRAPH =======");
				Console.WriteLine ("Input Expression: " + line);
				Console.ResetColor ();
				//make the graph
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
				//try to process the document
				ProcessDocument (args);

				Console.WriteLine("Press any key to close.");
				Console.ReadKey();
			}
			catch (Exception e)
			{
				//error occurred, output to user.
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine ("There was an error while processing the document: " + e.Message);
				Console.ResetColor ();
			}
		}
	}
}
