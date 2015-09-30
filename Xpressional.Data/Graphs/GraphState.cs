using System;
using System.Collections.Generic;

namespace Xpressional.Data.Graphs
{
	public sealed class GraphState
	{
		public bool IsFinal { get; set; }
		public int StateNumber { get; set; }

		public List<GraphStateConnection> Out { get; set; }

		public GraphState ()
		{
			IsFinal = false;
			StateNumber = -1;
			Out = new List<GraphStateConnection> ();
		}
	}
}

