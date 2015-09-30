using System;

namespace Xpressional.Data.Exceptions
{
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

