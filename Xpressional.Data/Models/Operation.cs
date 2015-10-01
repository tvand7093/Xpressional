using System;
using Xpressional.Data.Interfaces;

namespace Xpressional.Data.Models
{
	internal class Operation : IMapping<char, OperationType>
	{
		public int CompareTo (IMapping<char, OperationType> other)
		{
			var result = -1;
			if (this.Letter == other.Letter && this.Mapping == other.Mapping) {
				result = 0;
			}

			if (this.Letter > other.Letter && this.Mapping == other.Mapping) {
				result = 1;
			}

			if (this.Letter == other.Letter && this.Mapping > other.Mapping) {
				result = 1;
			}

			if (this.Letter < other.Letter && this.Mapping == other.Mapping) {
				result = -1;
			}

			if (this.Letter == other.Letter && this.Mapping < other.Mapping) {
				result = -1;
			}
			return result;
		}

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

