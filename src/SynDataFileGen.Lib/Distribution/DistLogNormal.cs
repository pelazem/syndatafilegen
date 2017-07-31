using System;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class DistLogNormal : IDistribution
	{
		#region IDistribution implementation

		public double GetValue()
		{
			return RNG.GetLogNormal(this.Mu, this.Sigma);
		}

		#endregion

		#region Properties

		public double Mu { get; private set; }

		public double Sigma { get; private set; }

		#endregion

		#region Constructors

		private DistLogNormal() { }

		public DistLogNormal(double mu, double sigma)
		{
			this.Mu = mu;
			this.Sigma = sigma;
		}

		#endregion
	}
}
