using System;
using Xpressional.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Xpressional.Data.Models
{
	public class Language : List<Word>
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

