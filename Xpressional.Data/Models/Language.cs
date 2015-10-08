using System;
using Xpressional.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Xpressional.Data.Models
{
	/// <summary>
	/// A list of letters that represents the valid language of the system.
	/// </summary>
	sealed class Language : List<Word>
	{
		public Language ()
		{
			Add (new Word ('a'));
			Add (new Word ('b'));
			Add (new Word ('c'));
			Add (new Word ('d'));
			Add (new Word ('e'));
			Add (Word.Epsilon);
			Add(Word.Null);
		}
	}
}

