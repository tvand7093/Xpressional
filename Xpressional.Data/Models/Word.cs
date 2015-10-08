using System;
using Xpressional.Data.Interfaces;

namespace Xpressional.Data.Models
{
	public sealed class Word : IMapping<char, string>
	{
		public int CompareTo (IMapping<char, string> other)
		{
			var result = -1;
			if (this.Letter == other.Letter && this.Mapping == other.Mapping) {
				result = 0;
			}

			if (this.Letter > other.Letter && this.Mapping == other.Mapping) {
				result = 1;
			}

			if (this.Letter == other.Letter && this.Mapping.CompareTo(other.Mapping) > 0) {
				result = 1;
			}

			if (this.Letter < other.Letter && this.Mapping.CompareTo(other.Mapping) == 0) {
				result = -1;
			}

			if (this.Letter == other.Letter && this.Mapping.CompareTo(other.Mapping) < 0) {
				result = -1;
			}

			return result;
		}

		#region IWord implementation

		/// <summary>
		/// the string value of the mapped letter
		/// </summary>
		/// <value>The mapping.</value>
		public string Mapping { get; private set; }

		/// <summary>
		/// The letter used for mappings.
		/// </summary>
		/// <value>The letter.</value>
		public char Letter { get; private set; }

		/// <summary>
		/// A helper property for getting an epsilon state.
		/// </summary>
		/// <value>The epsilon.</value>
		public static Word Epsilon {
			get {
				return new Word ('E', string.Empty);
			} 
		}

		/// <summary>
		/// A helper property for getting a null state.
		/// </summary>
		/// <value>The null.</value>
		public static Word Null {
			get {
				return new Word ('0', null);
			}
		}

		#endregion

		public Word ()
		{
			Mapping = string.Empty;
			Letter = '\0';
		}

		public Word (char letter)
		{
			Mapping = letter.ToString ();
			Letter = letter;
		}

		public Word(char letter, string mapping){
			Letter = letter;
			Mapping = mapping;
		}
	}
}

