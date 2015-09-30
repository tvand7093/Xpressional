using System;
using Xpressional.Data.Interfaces;

namespace Xpressional.Data.Models
{
	internal class Operation : IMapping<char, OperationType>
	{
		#region IMapping implementation

		public OperationType Mapping { get; private set; }

		public char Letter { get; private set; }

		#endregion

		public Operation (char letter, OperationType mapping)
		{
			Mapping = mapping;
			Letter = letter;
		}
	}
}

