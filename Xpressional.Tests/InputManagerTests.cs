using System;
using NUnit.Framework;
using Xpressional.Data.Managers;
using FluentAssertions;
using Xpressional.Data.Models;
using Xpressional.Data.Exceptions;

namespace Xpressional.Tests
{
	[TestFixture]
	public class InputManagerTests
	{
		[TestFixture]
		public class ParseExpression
		{
			[Test]
			[ExpectedException(typeof(InvalidLanguageException))]
			public void BadInputNotInLanguage(){
				const string input = "1";
				var inputManager = new InputManager ();
				inputManager.ParseExpression (input);
			}

			[Test]
			[ExpectedException(typeof(InvalidLanguageException))]
			public void BadInputNoOperation(){
				const string input = "ab";
				var inputManager = new InputManager ();
				inputManager.ParseExpression (input);
			}

			[Test]
			[ExpectedException(typeof(InvalidLanguageException))]
			public void BadInputNotEnoughArgsForOp(){
				const string input = "a+";
				var inputManager = new InputManager ();
				inputManager.ParseExpression (input);
			}

			[Test]
			[ExpectedException(typeof(InvalidLanguageException))]
			public void BadInputOffBalanceOperation(){
				const string input = "ab+ba";
				var inputManager = new InputManager ();
				inputManager.ParseExpression (input);
			}

			[TestFixture]
			public class ConcatExpression
			{
				[Test]
				public void CorrectStateCount(){
					const string expression = "ab&";
					var parser = new InputManager ();
					var output = parser.ParseExpression (expression);
					output.StateCount.Should ().Be (4);
				}

				[Test]
				public void CorrectFinalStateCount(){
					const string expression = "ab&";
					var parser = new InputManager ();
					var output = parser.ParseExpression (expression);
					var finalStateCount = output.FindFinalStates (output.StartState);
					finalStateCount.Count.Should ().Be (1);
				}

				[Test]
				public void CorrectFinalStates(){
					const string expression = "ab&";
					var parser = new InputManager ();
					var output = parser.ParseExpression (expression);
					var start = output.StartState;
					var a = start.Out [0].End;
					var b = a.Out [0].End;
					var final = b.Out [0].End;

					final.IsFinal.Should ().BeTrue ();
				}
			}

			[TestFixture]
			public class UnionExpression
			{
				[Test]
				public void CorrectStateCount_SingleOp(){
					const string expression = "ab+";
					var parser = new InputManager ();
					var output = parser.ParseExpression(expression);
					output.StateCount.Should ().Be (6);
				}

				[Test]
				public void CorrectFinalStateCount_SingleOp(){
					const string expression = "ab+";
					var parser = new InputManager ();
					var output = parser.ParseExpression (expression);
					var finalStateCount = output.FindFinalStates (output.StartState);
					finalStateCount.Count.Should ().Be (1);
				}

				[Test]
				public void CorrectFinalState_SingleOp(){
					const string expression = "ab+";
					var parser = new InputManager ();
					var output = parser.ParseExpression (expression);
					var start = output.StartState;
					var a = start.Out [0].End;
					var oldAFinal = a.Out [0].End;
					var aFinal = oldAFinal.Out [0].End;

					var b = start.Out [0].End;
					var oldBFinal = b.Out [0].End;
					var bFinal = oldBFinal.Out [0].End;

					aFinal.IsFinal.Should ().BeTrue ();
					bFinal.IsFinal.Should ().BeTrue ();
					aFinal.Should ().Be (bFinal);
				}

				[Test]
				public void HasCorrectEpsilonFromStart_SingleOp(){
					const string expression = "ab+";
					var parser = new InputManager ();
					var output = parser.ParseExpression (expression);
					var start = output.StartState;
					start.Out.Count.Should ().Be (2);
					start.Out [0].ConnectedBy.Should ().Be (Word.Epsilon);
					start.Out [1].ConnectedBy.Should ().Be (Word.Epsilon);
				}

				[Test]
				public void HasCorrectEpsilonFromStart_MultipleOp(){
					const string expression = "ab+b+";
					var parser = new InputManager ();
					var output = parser.ParseExpression (expression);
					var start = output.StartState;
					start.Out.Count.Should ().Be (2);
					start.Out [0].ConnectedBy.Should ().Be (Word.Epsilon);
					start.Out [1].ConnectedBy.Should ().Be (Word.Epsilon);
				}

				[Test]
				public void HasCorrectStateCount_MultipleOp(){
					const string expression = "ab+b+";
					var parser = new InputManager ();
					var output = parser.ParseExpression (expression);
					var finals = output.FindFinalStates (output.StartState);
					finals.Count.Should ().Be (1);
				}

				[Test]
				public void HasCorrectGraph_MultipleOp(){
					const string expression = "ab+b+";

					var parser = new InputManager ();
					var output = parser.ParseExpression (expression);

					var firstPartStart = output.StartState.Out [0];
					var a = firstPartStart.End.Out [0];
					var oldAFinal = a.End.Out [0];
					var aFinal = oldAFinal.End.Out [0];

					var b = firstPartStart.End.Out [0];
					var oldBFinal = b.End.Out [0];

					var topFinal = aFinal.End;
					var topNewEnd = topFinal.Out [0];
					topNewEnd.ConnectedBy.Should ().Be (Word.Epsilon);
					topNewEnd.End.IsFinal.Should ().BeTrue ();

					//lower half of graph, a.k.a  a 'b' by itself connected to a new final state.
					var secondPartStart = output.StartState.Out [1];
					secondPartStart.ConnectedBy.Should ().Be (Word.Epsilon);
					var secondPartOldFinish = secondPartStart.End.Out [0];
					secondPartOldFinish.ConnectedBy.Should ().Be (new Word ('b', "b"));
					var finish = secondPartOldFinish.End.Out [0];
					finish.ConnectedBy.Should ().Be (Word.Epsilon);
					finish.End.IsFinal.Should ().BeTrue ();
					finish.End.Should ().Be (topNewEnd.End);
				}
			}

			[TestFixture]
			public class KleeneExpression
			{
				[Test]
				public void CorrectStateCount_SingleOp(){
					const string expression = "a*";
					var parser = new InputManager ();
					var output = parser.ParseExpression(expression);
					output.StateCount.Should ().Be (3);
				}

				[Test]
				public void CorrectStateCount_DoubleOp(){
					const string expression = "ab&*";
					var parser = new InputManager ();
					var output = parser.ParseExpression(expression);
					output.StateCount.Should ().Be (5);
				}

//				[Test]
//				public void CorrectFinalStateCount_SingleOp(){
//					const string expression = "ab+";
//					var parser = new InputManager ();
//					var output = parser.ParseExpression (expression);
//					var finalStateCount = output.FindFinalStates (output.StartState);
//					finalStateCount.Count.Should ().Be (1);
//				}
//
//				[Test]
//				public void CorrectFinalState_SingleOp(){
//					const string expression = "ab+";
//					var parser = new InputManager ();
//					var output = parser.ParseExpression (expression);
//					var start = output.StartState;
//					var a = start.Out [0].End;
//					var oldAFinal = a.Out [0].End;
//					var aFinal = oldAFinal.Out [0].End;
//
//					var b = start.Out [0].End;
//					var oldBFinal = b.Out [0].End;
//					var bFinal = oldBFinal.Out [0].End;
//
//					aFinal.IsFinal.Should ().BeTrue ();
//					bFinal.IsFinal.Should ().BeTrue ();
//					aFinal.Should ().Be (bFinal);
//				}
//
//				[Test]
//				public void HasCorrectEpsilonFromStart_SingleOp(){
//					const string expression = "ab+";
//					var parser = new InputManager ();
//					var output = parser.ParseExpression (expression);
//					var start = output.StartState;
//					start.Out.Count.Should ().Be (2);
//					start.Out [0].ConnectedBy.Should ().Be (Word.Epsilon);
//					start.Out [1].ConnectedBy.Should ().Be (Word.Epsilon);
//				}
//
//				[Test]
//				public void HasCorrectEpsilonFromStart_MultipleOp(){
//					const string expression = "ab+b+";
//					var parser = new InputManager ();
//					var output = parser.ParseExpression (expression);
//					var start = output.StartState;
//					start.Out.Count.Should ().Be (2);
//					start.Out [0].ConnectedBy.Should ().Be (Word.Epsilon);
//					start.Out [1].ConnectedBy.Should ().Be (Word.Epsilon);
//				}
//
//				[Test]
//				public void HasCorrectStateCount_MultipleOp(){
//					const string expression = "ab+b+";
//					var parser = new InputManager ();
//					var output = parser.ParseExpression (expression);
//					var finals = output.FindFinalStates (output.StartState);
//					finals.Count.Should ().Be (1);
//				}
//
//				[Test]
//				public void HasCorrectGraph_MultipleOp(){
//					const string expression = "ab+b+";
//
//					var parser = new InputManager ();
//					var output = parser.ParseExpression (expression);
//
//					var firstPartStart = output.StartState.Out [0];
//					var a = firstPartStart.End.Out [0];
//					var oldAFinal = a.End.Out [0];
//					var aFinal = oldAFinal.End.Out [0];
//
//					var b = firstPartStart.End.Out [0];
//					var oldBFinal = b.End.Out [0];
//
//					var topFinal = aFinal.End;
//					var topNewEnd = topFinal.Out [0];
//					topNewEnd.ConnectedBy.Should ().Be (Word.Epsilon);
//					topNewEnd.End.IsFinal.Should ().BeTrue ();
//
//					//lower half of graph, a.k.a  a 'b' by itself connected to a new final state.
//					var secondPartStart = output.StartState.Out [1];
//					secondPartStart.ConnectedBy.Should ().Be (Word.Epsilon);
//					var secondPartOldFinish = secondPartStart.End.Out [0];
//					secondPartOldFinish.ConnectedBy.Should ().Be (new Word ('b', "b"));
//					var finish = secondPartOldFinish.End.Out [0];
//					finish.ConnectedBy.Should ().Be (Word.Epsilon);
//					finish.End.IsFinal.Should ().BeTrue ();
//					finish.End.Should ().Be (topNewEnd.End);
//				}
			}
		}
	}
}

