using System;

namespace Xpressional.Data.Interfaces
{
	public interface IMapping<L, M> 
	{
		M Mapping { get; }
		L Letter { get; }
	}
}

