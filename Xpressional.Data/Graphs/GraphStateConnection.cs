using System;
using Xpressional.Data.Models;

namespace Xpressional.Data.Graphs
{
	public sealed class GraphStateConnection
	{
		/// <summary>
		/// Gets or sets the begining of the connection.
		/// </summary>
		/// <value>The start of the connection.</value>
		public GraphState Start { get; set; }

		/// <summary>
		/// Gets or sets the way the two nodes are connected, like E, null, a, b, etc.
		/// </summary>
		/// <value>The connection info.</value>
		public Word ConnectedBy {get;set;}

		/// <summary>
		/// Gets or sets the ending of the connection.
		/// </summary>
		/// <value>The end of the connection.</value>
		public GraphState End { get; set; }

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="Xpressional.Data.Graphs.GraphStateConnection"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="Xpressional.Data.Graphs.GraphStateConnection"/>.</returns>
		public override string ToString ()
		{
			var startFinal = "";
			var endFinal = "";
			if (Start.IsFinal) {
				startFinal = " F";
			}

			if (End.IsFinal) {
				endFinal = " F";
			}
			//create a nice output for this state.
			var output = string.Format ("(q{0}{1}, {2}) ==> q{3}",
				Start.StateNumber, 
				startFinal,
				ConnectedBy.Letter,
				End.StateNumber + endFinal);

			return output;
		}

		public GraphStateConnection ()
		{
			ConnectedBy = null;
			Start = End = null;
		}
	}
}

