using System;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class DistUniform : IDistribution
	{
		#region IDistribution implementation

		public double GetValue()
		{
			return RNG.GetUniform(this.Min, this.Max);
		}

		#endregion

		#region Properties

		public double Min { get; private set; }

		public double Max { get; private set; }

		#endregion

		#region Constructors

		public DistUniform()
			: this(0, 1)
		{
		}

		public DistUniform(double min, double max)
		{
			this.Min = min;
			this.Max = max;
		}

		#endregion
	}
}
