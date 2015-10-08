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
		/// <summary>
		/// Parses a given expression into a graph.
		/// </summary>
		/// <returns>The built graph.</returns>
		/// <param name="expression">The expression to create the graph from.</param>
		/// <param name="initGraph">The graph to append too. Used for recursion.</param>
		public Graph ParseExpression(string expression, Graph initGraph = null){
			var lang = new Language ();
			var ops = new Operations ();
			expression = expression.Trim ();
			var opStack = new Stack<Word>();

			foreach (char letter in expression) {
				//find the language mapping
				var inLanguage = lang
					.DefaultIfEmpty(null)
					.FirstOrDefault (w => w.Letter == letter);

				//find the operation mapping
				var inOps = ops
					.DefaultIfEmpty(null)
					.FirstOrDefault(w => w.Letter == letter);

				//not in the language or operators
				if(inLanguage == null && inOps == null){
					throw new InvalidLanguageException("Invalid input: " + letter);
				}

				//it is valid language input, so add it to the list.
				if(inLanguage != null){
					opStack.Push(inLanguage);
					continue;
				}
				else{
					//not a letter, so must be an operation
					//verify that it has the correct arg count for that type of operation.
					if (opStack.Count != inOps.MinArgCount && initGraph == null) {
						throw new InvalidLanguageException (
							string.Format(
								"Unbalanced arguement count for operation (expected {0}, but got {1}.",
								inOps.MinArgCount, opStack.Count
							));
					}
					//operation, so stop, and process graph
					initGraph = ProcessOperation(initGraph, inOps, opStack);
				}
			}

			//expression was given with no operation, so error out.
			if (opStack.Count > 0) {
				throw new InvalidLanguageException ("No operation was specified in the given expression.");
			}

			//once done with graph, renumber the states.
			initGraph.RenumberStates ();

			//return the completed graph.
			return initGraph;
		}

		Graph ProcessOperation(Graph initGraph, Operation op, Stack<Word> opStack){
			// create base graph if not done already.
			if(initGraph == null){
				//start of our graph
				initGraph = Graph.CreateNewGraph(opStack.Pop());
			}

			//if kleene operation, do it here.
			if (op.Mapping == OperationType.Kleene) {
				initGraph = initGraph.Kleene ();
			}
				
			//now process each operation 
			while(opStack.Count != 0){
				//get next letter to work with.
				var wordToAdd = opStack.Pop();

				//depending on the mapped type, do operation on graph
				//with the new letter.
				if(op.Mapping == OperationType.Concat)
					initGraph = initGraph.Concat(wordToAdd);
				else if(op.Mapping == OperationType.Union)
					initGraph = initGraph.Union(wordToAdd);
			}

			//return the created graph.
			return initGraph;
		}
	}
}

