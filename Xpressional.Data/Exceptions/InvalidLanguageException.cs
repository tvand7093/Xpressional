using System;

namespace Xpressional.Data.Exceptions
{
	/// <summary>
	/// Represents a language issue like bad input, bad operation balance, etc...
	/// </summary>
	public class InvalidLanguageException : Exception
	{
		public InvalidLanguageException () : base("The language is unrecognized.")
		{
		}

		public InvalidLanguageException (string message) : base(message)
		{
		}

		public InvalidLanguageException (string message, Exception inner) : base(message, inner)
		{
		}
	}
}

