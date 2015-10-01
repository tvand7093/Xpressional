using System;
using System.Collections.Generic;

namespace Xpressional.Data.Graphs
{
	public sealed class GraphState : IComparable<GraphState>
	{
		#region IComparable implementation

		public int CompareTo (GraphState other)
		{
			var result = 0;
			if (this.IsFinal && !other.IsFinal) {
				result = 1;
			} else if (!this.IsFinal && other.IsFinal) {
				result = -1;
			}
			else if (this.StateNumber > other.StateNumber) {
				result = 1;
			} else if (this.StateNumber < other.StateNumber) {
				result = -1;
			}
			return result;
		}

		#endregion

		/// <summary>
		/// Gets or sets a value indicating whether this is a final state or not.
		/// </summary>
		/// <value><c>true</c> if this instance is final; otherwise, <c>false</c>.</value>
		public bool IsFinal { get; set; }

		/// <summary>
		/// Gets or sets the state number as in q1, q2, etc.
		/// </summary>
		/// <value>The state number.</value>
		public int StateNumber { get; set; }

		/// <summary>
		/// Gets or sets the outputs for a given state.
		/// </summary>
		/// <value>The connections that leave this state.</value>
		public List<GraphStateConnection> Out { get; set; }

		public GraphState ()
		{
			IsFinal = false;
			StateNumber = 0;
			Out = new List<GraphStateConnection> ();
		}
	}
}

