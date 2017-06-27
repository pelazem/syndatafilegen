using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pelazem.Common;

namespace Generator
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
