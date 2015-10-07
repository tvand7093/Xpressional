using System;
using Xpressional.Data.Managers;
using System.IO;
using Xpressional.Data.Graphs;

namespace Xpressional
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var output = new OutputManager (new ConsoleHelper ());
			//var fileContents = File.ReadAllLines (args [0]);
			var inputManager = new InputManager ();
			Graph baseGraph = null;


			//ab+*a&b&a*&*ab+&ab+&

			// this causes overflow: ab+*a&b&a*
			// however, this does not: ab+*a&b&

			baseGraph = inputManager.ParseExpression ("ab+*a&b&*", baseGraph);
			output.Print (baseGraph);
			Console.ReadKey ();

//			foreach (var line in fileContents) {
//				baseGraph = inputManager.ParseExpression (line, baseGraph);
//				output.Print (baseGraph);
//			}
		}
	}
}
