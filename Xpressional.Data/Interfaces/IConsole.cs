using System;

namespace Xpressional.Data.Interfaces
{
	/// <summary>
	/// Provides functionality for writing to a console.
	/// </summary>
	public interface IConsole
	{
		void WriteLine(string content);
		void WriteLine(string format, params string[] param);
	}
}

