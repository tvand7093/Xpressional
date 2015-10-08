using System;
using Xpressional.Data.Interfaces;

namespace Xpressional.Data.Models
{
	sealed class Operation : IMapping<char, OperationType>
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

		/// <summary>
		/// The operation type to perform
		/// </summary>
		/// <value>The mapping.</value>
		public OperationType Mapping { get; private set; }


		/// <summary>
		/// The letter representing the operation
		/// </summary>
		/// <value>The letter.</value>
		public char Letter { get; private set; }

		/// <summary>
		/// Minimum arguement count for the specified operation type.
		/// </summary>
		/// <value>The minimum argument count.</value>
		public int MinArgCount { get; private set; }

		#endregion

		public Operation (char letter, OperationType mapping, int argCount)
		{
			Mapping = mapping;
			Letter = letter;
			MinArgCount = argCount;
		}
	}
}

