using System;

namespace SynDataFileGen.Lib
{
	public class DistIncrementing : IDistribution
	{
		#region IDistribution implementation

		public double GetValue()
		{
			double result = this.CurrentValue;
			this.CurrentValue += this.Increment;
			return result;
		}

		#endregion

		#region Properties

		public double Seed { get; private set; }

		public double Increment { get; private set; }

		private double CurrentValue { get; set; }

		#endregion

		#region Constructors

		public DistIncrementing()
			: this(1, 1)
		{
		}

		public DistIncrementing(double seed, double increment)
		{
			this.Seed = seed;
			this.Increment = increment;

			this.CurrentValue = this.Seed;
		}

		#endregion
	}
}
