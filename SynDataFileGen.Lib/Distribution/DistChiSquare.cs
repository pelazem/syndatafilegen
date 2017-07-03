using System;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class DistChiSquare : IDistribution
	{
		#region IDistribution implementation

		public double GetValue()
		{
			return RNG.GetChiSquare(this.DegreesOfFreedom);
		}

		#endregion

		#region Properties

		public double DegreesOfFreedom { get; private set; }

		#endregion

		#region Constructors

		private DistChiSquare() { }

		public DistChiSquare(double degreesOfFreedom)
		{
			this.DegreesOfFreedom = degreesOfFreedom;
		}

		#endregion
	}
}
