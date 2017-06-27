using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pelazem.Common;

namespace Generator
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
