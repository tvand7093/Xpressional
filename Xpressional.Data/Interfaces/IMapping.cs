using System;

namespace Xpressional.Data.Interfaces
{
	/// <summary>
	/// Provides a way to map one type to another.
	/// </summary>
	public interface IMapping<L, M> : IComparable<IMapping<L,M>>
	{
		/// <summary>
		/// The mapped value.
		/// </summary>
		/// <value>The mapping.</value>
		M Mapping { get; }

		/// <summary>
		/// Gets the base object for the mapping..
		/// </summary>
		L Letter { get; }
	}
}

