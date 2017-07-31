using System;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class DistLaplace : IDistribution
	{
		#region IDistribution implementation

		public double GetValue()
		{
			return RNG.GetLaplace(this.Mean, this.Scale);
		}

		#endregion

		#region Properties

		public double Mean { get; private set; }

		public double Scale { get; private set; }

		#endregion

		#region Constructors

		private DistLaplace() { }

		public DistLaplace(double mean, double scale)
		{
			this.Mean = mean;
			this.Scale = scale;
		}

		#endregion
	}
}
