using System;

namespace SynDataFileGen.Lib
{
	public class Category
	{
		/// <summary>
		/// The category value
		/// </summary>
		public object Value { get; set; }

		/// <summary>
		/// Relative weight of this category in the set. Leave all weights at zero to evenly weight categories. Weights will be internally normalized.
		/// </summary>
		public double Weight { get; set; } = 0;

		internal int ValueCount { get; set; }
	}
}
