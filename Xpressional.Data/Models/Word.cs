﻿using System;
using Xpressional.Data.Interfaces;

namespace Xpressional.Data.Models
{
	public sealed class Word : IMapping<char, string>
	{
		#region IWord implementation

		public string Mapping { get; private set; }
		public char Letter { get; private set; }

		bool IsSpecial {
			get {
				return Mapping != null && Letter.ToString() != Mapping;
			}
		}

		public static Word Epsilon {
			get {
				return new Word ('E', string.Empty);
			} 
		}

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
