using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pelazem.Common;

namespace Generator
{
	public class DistNormal : IDistribution
	{
		#region IDistribution implementation

		public double GetValue()
		{
			return RNG.GetNormal(this.Mean, this.StandardDeviation);
		}

		#endregion

		#region Properties

		public double Mean { get; private set; }

		public double StandardDeviation { get; private set; }

		#endregion

		#region Constructors

		public DistNormal()
			: this(0, 1)
		{
		}

		public DistNormal(double mean, double standardDeviation)
		{
			this.Mean = mean;
			this.StandardDeviation = standardDeviation;
		}

		#endregion
	}
}
