using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pelazem.Common;

namespace Generator
{
	public class DistBeta : IDistribution
	{
		#region IDistribution implementation

		public double GetValue()
		{
			return RNG.GetBeta(this.A, this.B);
		}

		#endregion

		#region Properties

		public double A { get; private set; }

		public double B { get; private set; }

		#endregion

		#region Constructors

		private DistBeta() { }

		public DistBeta(double a, double b)
		{
			this.A = a;
			this.B = b;
		}

		#endregion
	}
}
