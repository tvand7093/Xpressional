using NUnit.Framework;
using System;
using Xpressional.Data.Graphs;
using Xpressional.Data.Models;
using FluentAssertions;

namespace Xpressional.Tests
{
	[TestFixture]
	public static class GraphTests
	{
		[TestFixture]
		public class FindFinalStates
		{
			[Test]
			public void HasCorrectCount()
			{
				var m1Start = new GraphState () {
					StateNumber = 0,
					IsFinal = true
				};

				var m1 = new Graph () {
					StartState = m1Start
				};

				var finals = m1.FindFinalStates (m1.StartState);
				finals.Count.Should ().Be (1);
			}

			[Test]
			public void HasCorrectSingleFinalState()
			{
				var m1Start = new GraphState () {
					StateNumber = 0,
					IsFinal = true
				};

				var m1 = new Graph () {
					StartState = m1Start
				};

				var finals = m1.FindFinalStates (m1.StartState);
				finals [0].Should ().Be (m1Start);
			}

			[Test]
			public void CountsThreeFinals()
			{
				var m1Start = new GraphState () {
					StateNumber = 0,
					IsFinal = true
				};
				var secondFinal = new GraphState () {
					StateNumber = 1,
					IsFinal = true
				};
				var thirdFinal = new GraphState () {
					StateNumber = 2,
					IsFinal = true
				};
				var m1 = new Graph () {
					StartState = m1Start
				};

				var epsilonConnection1 = new GraphStateConnection {
					Start = m1Start,
					End = secondFinal,
					ConnectedBy = Word.Epsilon
				};

				var epsilonConnection2 = new GraphStateConnection {
					Start = secondFinal,
					End = thirdFinal,
					ConnectedBy = Word.Epsilon
				};

				m1.StartState.Out.Add (epsilonConnection1);
				secondFinal.Out.Add (epsilonConnection2);

				var finals = m1.FindFinalStates (m1.StartState);
				finals.Count.Should ().Be (3);
			}
		}

		[TestFixture]
		public class Concat 
		{
			[Test]
			public void StateNumbersCorrect()
			{
				var m1Start = new GraphState () {
					StateNumber = 0,
					IsFinal = true
				};

				var m2Start = new GraphState () {
					StateNumber = 0,
					IsFinal = true
				};

				var m1 = new Graph () {
					StartState = m1Start
				};

				var m2 = new Graph () {
					StartState = m2Start
				};

				var ndfa = m1.Concat (m2);
				ndfa.StartState.StateNumber.Should ().Be (0);
				ndfa.StartState.Out [0].End.StateNumber.Should ().Be (1);
			}

			[Test]
			public void HasCorrectTwoStateCount()
			{
				var m1Start = new GraphState () {
					StateNumber = 0,
					IsFinal = true
				};

				var m2Start = new GraphState () {
					StateNumber = 1,
					IsFinal = true
				};

				var m1 = new Graph () {
					StartState = m1Start
				};

				var m2 = new Graph () {
					StartState = m2Start
				};

				var ndfa = m1.Concat (m2);
				ndfa.StateCount.Should ().Be (2);
			}

			[Test]
			public void CreatesOneConnection()
			{
				var m1Start = new GraphState () {
					StateNumber = 0,
					IsFinal = true
				};

				var m2Start = new GraphState () {
					StateNumber = 1,
					IsFinal = true
				};

				var m1 = new Graph () {
					StartState = m1Start
				};

				var m2 = new Graph () {
					StartState = m2Start
				};

				var ndfa = m1.Concat (m2);
				ndfa.StartState.Out.Count.Should ().Be (1);
			}

			[Test]
			public void CreatesCorrectConnection()
			{
				var m1Start = new GraphState () {
					StateNumber = 0,
					IsFinal = true
				};

				var m2Start = new GraphState () {
					StateNumber = 1,
					IsFinal = true
				};

				var m1 = new Graph () {
					StartState = m1Start
				};

				var m2 = new Graph () {
					StartState = m2Start
				};

				var ndfa = m1.Concat (m2);

				ndfa.StartState.Out[0].Start.Should ().Be (m1Start);
				ndfa.StartState.Out[0].End.Should ().Be (m2Start);
				ndfa.StartState.Out[0].ConnectedBy.Letter.Should ().Be (Word.Epsilon.Letter);
				ndfa.StartState.Out[0].ConnectedBy.Mapping.Should ().Be (Word.Epsilon.Mapping);
			}

			[Test]
			public void HasCorrectFinalStates()
			{
				var m1Start = new GraphState () {
					StateNumber = 0,
					IsFinal = true
				};

				var m2Start = new GraphState () {
					StateNumber = 1,
					IsFinal = true
				};

				var m1 = new Graph () {
					StartState = m1Start
				};

				var m2 = new Graph () {
					StartState = m2Start
				};

				var ndfa = m1.Concat (m2);
				var finals = ndfa.FindFinalStates (ndfa.StartState);
				finals.Count.Should ().Be (1);
				finals [0].Should ().Be (m2Start);
			}
		}

		[TestFixture]
		public class Union 
		{
			[Test]
			public void StateNumbersCorrect()
			{
				var m1Start = new GraphState () {
					StateNumber = 0,
					IsFinal = true
				};

				var m2Start = new GraphState () {
					StateNumber = 0,
					IsFinal = true
				};

				var m1 = new Graph () {
					StartState = m1Start
				};

				var m2 = new Graph () {
					StartState = m2Start
				};

				var ndfa = m1.Union (m2);
				ndfa.StartState.StateNumber.Should ().Be (0);
				ndfa.StartState.Out [0].End.StateNumber.Should ().Be (1);
			}

			[Test]
			public void HasCorrectThreeStateCount()
			{
				var m1Start = new GraphState () {
					StateNumber = 0,
					IsFinal = true
				};

				var m2Start = new GraphState () {
					StateNumber = 1,
					IsFinal = true
				};

				var m1 = new Graph () {
					StartState = m1Start
				};

				var m2 = new Graph () {
					StartState = m2Start
				};

				var ndfa = m1.Union (m2);
				ndfa.StateCount.Should ().Be (3);
			}

			[Test]
			public void CreatesTwoConnections()
			{
				var m1Start = new GraphState () {
					StateNumber = 0,
					IsFinal = true
				};

				var m2Start = new GraphState () {
					StateNumber = 1,
					IsFinal = true
				};

				var m1 = new Graph () {
					StartState = m1Start
				};

				var m2 = new Graph () {
					StartState = m2Start
				};

				var ndfa = m1.Union (m2);
				ndfa.StartState.Out.Count.Should ().Be (2);
			}

			[Test]
			public void CreatesCorrectEpsilonConnections()
			{
				var m1Start = new GraphState () {
					StateNumber = 0,
					IsFinal = true
				};

				var m2Start = new GraphState () {
					StateNumber = 1,
					IsFinal = true
				};

				var m1 = new Graph () {
					StartState = m1Start
				};

				var m2 = new Graph () {
					StartState = m2Start
				};

				var ndfa = m1.Union (m2);

				ndfa.StartState.Out[0].Start.Should ().Be (ndfa.StartState);
				ndfa.StartState.Out[0].End.Should ().Be (m1Start);
				ndfa.StartState.Out[0].ConnectedBy.Letter.Should ().Be (Word.Epsilon.Letter);
				ndfa.StartState.Out[0].ConnectedBy.Mapping.Should ().Be (Word.Epsilon.Mapping);

				ndfa.StartState.Out[1].Start.Should ().Be (ndfa.StartState);
				ndfa.StartState.Out[1].End.Should ().Be (m2Start);
				ndfa.StartState.Out[1].ConnectedBy.Letter.Should ().Be (Word.Epsilon.Letter);
				ndfa.StartState.Out[1].ConnectedBy.Mapping.Should ().Be (Word.Epsilon.Mapping);
			}
		}
			
		[TestFixture]
		public class Kleene 
		{
			[Test]
			public void StateNumbersCorrect()
			{
				var m1 = new Graph () {
					StartState =  new GraphState () {
						StateNumber = 0,
						IsFinal = true
					}
				};

				var ndfa = m1.Kleene ();
				ndfa.StartState.StateNumber.Should ().Be (2);
				ndfa.StartState.Out [0].End.StateNumber.Should ().Be (1);
			}

			[Test]
			public void NewInitialShouldBeFinal()
			{
				var m1Start = new GraphState () {
					StateNumber = 0,
					IsFinal = true
				};

				var m1 = new Graph () {
					StartState = m1Start
				};

				var ndfa = m1.Kleene ();
				ndfa.StartState.IsFinal.Should ().BeTrue ();
			}

			[Test]
			public void HasLoopConnection()
			{
				var m1Start = new GraphState () {
					StateNumber = 0,
					IsFinal = true
				};

				var m1 = new Graph () {
					StartState = m1Start
				};

				var ndfa = m1.Kleene();
				ndfa.StartState.Out [0].End.Out [0].End.Should ().Be (ndfa.StartState);
			}

//			[Test]
//			public void CreatesCorrectConnection()
//			{
//				var m1Start = new GraphState () {
//					StateNumber = 0,
//					IsFinal = true
//				};
//
//				var m2Start = new GraphState () {
//					StateNumber = 1,
//					IsFinal = true
//				};
//
//				var m1 = new Graph () {
//					StartState = m1Start
//				};
//
//				var m2 = new Graph () {
//					StartState = m2Start
//				};
//
//				var ndfa = m1.Kleene ();
//
//				ndfa.StartState.Out[0].Start.Should ().Be (m1Start);
//				ndfa.StartState.Out[0].End.Should ().Be (m2Start);
//				ndfa.StartState.Out[0].ConnectedBy.Letter.Should ().Be (Word.Epsilon.Letter);
//				ndfa.StartState.Out[0].ConnectedBy.Mapping.Should ().Be (Word.Epsilon.Mapping);
//			}
//
//			[Test]
//			public void HasCorrectFinalStates()
//			{
//				var m1Start = new GraphState () {
//					StateNumber = 0,
//					IsFinal = true
//				};
//
//				var m2Start = new GraphState () {
//					StateNumber = 1,
//					IsFinal = true
//				};
//
//				var m1 = new Graph () {
//					StartState = m1Start
//				};
//
//				var m2 = new Graph () {
//					StartState = m2Start
//				};
//
//				var ndfa = m1.Kleene ();
//				var finals = ndfa.FindFinalStates (ndfa.StartState);
//				finals.Count.Should ().Be (1);
//				finals [0].Should ().Be (m2Start);
//			}
		}
	}
}

