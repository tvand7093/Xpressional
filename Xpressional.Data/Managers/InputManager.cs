using System;
using System.IO;
using System.Collections.Generic;
using Xpressional.Data.Models;
using System.Linq;
using Xpressional.Data.Exceptions;
using System.Diagnostics;
using Xpressional.Data.Graphs;

namespace Xpressional.Data.Managers
{
	public class InputManager
	{

		public Graph ParseExpression(string expression, Graph initGraph = null){
			var lang = new Language ();
			var ops = new Operations ();

			var opStack = new Stack<Word>();
			foreach (char letter in expression) {

				var inLanguage = lang
					.DefaultIfEmpty(null)
					.FirstOrDefault (w => w.Letter == letter);
				var inOps = ops
					.DefaultIfEmpty(null)
					.FirstOrDefault(w => w.Letter == letter);

				if(inLanguage == null && inOps == null){
					throw new InvalidLanguageException("Invalid input: " + letter);
				}

				if(inLanguage != null){
					opStack.Push(inLanguage);
					continue;
				}
				else{
					if (opStack.Count != inOps.MinArgCount && initGraph == null) {
						throw new InvalidLanguageException (
							string.Format(
								"Unbalanced arguement count for operation (expected {0}, but got {1}.",
								inOps.MinArgCount, opStack.Count
							));
					}
					//operation, so stop, and process
					initGraph = ProcessOperation(initGraph, inOps, opStack);
				}
			}

			if (opStack.Count > 0) {
				throw new InvalidLanguageException ("No operation was specified in the given expression.");
			}

			initGraph.RenumberStates ();

			return initGraph;
		}

		Graph ProcessOperation(Graph initGraph, Operation op, Stack<Word> opStack){
			// create base graph if not done already.
			if(initGraph == null){
				//start of our graph
				initGraph = Graph.CreateNewGraph(opStack.Pop());
			}

			if (op.Mapping == OperationType.Kleene) {
				initGraph = initGraph.Kleene ();
			}
				
			//now process each operation 
			while(opStack.Count != 0){
				var wordToAdd = opStack.Pop();

				if(op.Mapping == OperationType.Concat)
					initGraph = initGraph.Concat(wordToAdd);
//				else if(op.Mapping == OperationType.Kleene)
//					initGraph = initGraph.Kleene();
				else if(op.Mapping == OperationType.Union)
					initGraph = initGraph.Union(wordToAdd);
			}
			return initGraph;
		}
	}
}

