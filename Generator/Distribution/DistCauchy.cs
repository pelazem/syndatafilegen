using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pelazem.Common;

namespace Generator
{
	public class DistCauchy : IDistribution
	{
		#region IDistribution implementation

		public double GetValue()
		{
			return RNG.GetCauchy(this.Median, this.Scale);
		}

		#endregion

		#region Properties

		public double Median { get; private set; }

		public double Scale { get; private set; }

		#endregion

		#region Constructors

		private DistCauchy() { }

		public DistCauchy(double median, double scale)
		{
			this.Median = median;
			this.Scale = scale;
		}

		#endregion
	}
}
