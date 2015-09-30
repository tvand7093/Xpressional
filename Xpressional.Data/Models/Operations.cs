using System;
using System.Collections.Generic;

namespace Xpressional.Data.Models
{
	sealed class Operations : List<Operation>
	{
		public Operations ()
		{
			Add (new Operation ('*', OperationType.Kleene));
			Add (new Operation ('&', OperationType.Union));
			Add (new Operation ('+', OperationType.Concat));
		}
	}
}

