using System;
using System.Collections.Generic;

namespace Xpressional.Data.Models
{
	/// <summary>
	/// Represents the list of valid operations for our expressions.
	/// </summary>
	sealed class Operations : List<Operation>
	{
		public Operations ()
		{
			Add (new Operation ('*', OperationType.Kleene, 1));
			Add (new Operation ('&', OperationType.Concat, 2));
			Add (new Operation ('+', OperationType.Union, 2));
		}
	}
}

