using System;
using Xpressional.Data.Interfaces;
using System.Linq;
using Xpressional.Data.Exceptions;
using Xpressional.Data.Models;

namespace Xpressional.Data.Managers
{
	internal class OperationParser
	{
//		while (not end of postfix expression) { 
//			c = next character in postfix expression;
//			if (c == ’&’) { 
//				nFA2 = pop();
//				nFA1 = pop();
//				push(NFA that accepts the concatenation of L(nFA1) followed by L(nFA2));
//			} else if (c == ’+’) { 
//				nFA2 = pop();
//				nFA1 = pop();
//				push(NFA that accepts L(nFA1) + L(nFA2));
//			} else if (c == ’*’) { 
//				nFA = pop();
//				push(NFA that accepts L(nFA) star);
//			} else
//				push(NFA that accepts a single character c);
//		}

		public Operation ParseExpression (string expression)
		{
			//expression test...
			var words = new Language();
			var ops = new Operations ();

			foreach (char letter in expression) {
				var word = words.FirstOrDefault (w => w.Letter == letter);
				var op = ops.FirstOrDefault (o => o.Letter == letter);

				if (word != null) {
					//word not op
				} else if (op != null) {
					//op not word
				} else {
					//other unknown char. 
					throw new InvalidLanguageException();
				}

			}
			return new Operation ('a', OperationType.Kleene);
		}

		public OperationParser ()
		{
		}


	}
}

