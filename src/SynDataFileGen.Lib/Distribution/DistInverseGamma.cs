using System;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class DistInverseGamma : IDistribution
	{
		#region IDistribution implementation

		public double GetValue()
		{
			return RNG.GetInverseGamma(this.Shape, this.Scale);
		}

		#endregion

		#region Properties

		public double Shape { get; private set; }

		public double Scale { get; private set; }

		#endregion

		#region Constructors

		private DistInverseGamma() { }

		public DistInverseGamma(double shape, double scale)
		{
			this.Shape = shape;
			this.Scale = scale;
		}

		#endregion
	}
}
