using System;

namespace Xpressional.Data.Interfaces
{
	public interface IConsole
	{
		void WriteLine(string content);
		void WriteLine(string format, params string[] param);
	}
}

