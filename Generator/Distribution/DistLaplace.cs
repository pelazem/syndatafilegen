using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pelazem.Common;

namespace Generator
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
