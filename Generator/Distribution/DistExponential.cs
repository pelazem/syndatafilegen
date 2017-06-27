using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pelazem.Common;

namespace Generator
{
	public class DistExponential : IDistribution
	{
		#region IDistribution implementation

		public double GetValue()
		{
			return RNG.GetExponential(this.Mean);
		}

		#endregion

		#region Properties

		public double Mean { get; private set; }

		#endregion

		#region Constructors

		public DistExponential()
			: this(1)
		{
		}

		public DistExponential(double mean)
		{
			this.Mean = mean;
		}

		#endregion
	}
}
