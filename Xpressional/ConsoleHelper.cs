using System;
using Xpressional.Data.Interfaces;

namespace Xpressional
{
	public class ConsoleHelper : IConsole
	{
		#region IConsole implementation


		public void WriteLine (string content)
		{
			Console.WriteLine (content);
		}


		public void WriteLine (string format, params string[] param)
		{
			Console.WriteLine (format, param);
		}

		#endregion
	}
}

