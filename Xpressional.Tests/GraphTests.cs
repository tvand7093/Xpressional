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
		public class Concat 
		{
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
		}
	}
}

