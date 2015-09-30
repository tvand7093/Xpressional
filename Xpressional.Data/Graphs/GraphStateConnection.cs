using System;
using Xpressional.Data.Models;

namespace Xpressional.Data.Graphs
{
	public sealed class GraphStateConnection
	{
		public GraphState Start { get; set; }

		public Word ConnectedBy {get;set;}

		public GraphState End { get; set; }

		public GraphStateConnection ()
		{
			ConnectedBy = null;
			Start = End = null;
		}
	}
}

