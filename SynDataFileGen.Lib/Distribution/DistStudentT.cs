using System;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class DistStudentT : IDistribution
	{
		#region IDistribution implementation

		public double GetValue()
		{
			return RNG.GetStudentT(this.DegreesOfFreedom);
		}

		#endregion

		#region Properties

		public double DegreesOfFreedom { get; private set; }

		#endregion

		#region Constructors

		private DistStudentT() { }

		public DistStudentT(double degreesOfFreedom)
		{
			this.DegreesOfFreedom = degreesOfFreedom;
		}

		#endregion
	}
}
