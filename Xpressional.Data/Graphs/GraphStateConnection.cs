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

		public GraphStateConnection ()
		{
			ConnectedBy = null;
			Start = End = null;
		}
	}
}

