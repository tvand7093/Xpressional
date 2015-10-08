using System;
using Xpressional.Data.Interfaces;

namespace Xpressional
{
	/// <summary>
	/// A simple class wrapper for console output.
	/// Used for interop with the PCL.
	/// </summary>
	public class ConsoleHelper : IConsole
	{
		#region IConsole implementation

		/// <summary>
		/// Writes the specified string to the console.
		/// </summary>
		/// <param name="content">the string to print.</param>
		public void WriteLine (string content)
		{
			Console.WriteLine (content);
		}

		/// <summary>
		/// Writes the specified string format to the console.
		/// </summary>
		/// <param name="format">the format string to print.</param>
		/// <param name="param">the strings to use in the format.</param>
		public void WriteLine (string format, params string[] param)
		{
			Console.WriteLine (format, param);
		}

		#endregion
	}
}

