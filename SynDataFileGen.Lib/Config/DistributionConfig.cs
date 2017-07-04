using System;
using System.Collections.Generic;
using System.Text;

namespace SynDataFileGen.Lib
{
	public class DistributionConfig
	{
		/// <summary>
		/// Numeric distribution to use to generate values for this field.
		/// Valid values: Beta, Cauchy, ChiSquare, Exponential, Gamma, Incrementing, InverseGamma, Laplace, LogNormal, Normal, StudentT, Uniform, Weibull
		/// If any other value is specified, Uniform will be used.
		/// Used only with Continuous Numeric fields. Ignored otherwise.
		/// </summary>
		public string DistributionName { get; set; }

		/// <summary>
		/// Used with Beta
		/// </summary>
		public double A { get; set; }

		/// <summary>
		/// Used with Beta
		/// </summary>
		public double B { get; set; }

		/// <summary>
		/// Used with Cauchy
		/// </summary>
		public double Median { get; set; }

		/// <summary>
		/// Used with Cauchy, Gamma, InverseGamma, Laplace, Weibull
		/// </summary>
		public double Scale { get; set; }

		/// <summary>
		/// Used with ChiSquare, StudentT
		/// </summary>
		public double DegreesOfFreedom { get; set; }

		/// <summary>
		/// Used with Exponential, Laplace, Normal
		/// </summary>
		public double Mean { get; set; }

		/// <summary>
		/// Used with Gamma, InverseGamma, Weibull
		/// </summary>
		public double Shape { get; set; }

		/// <summary>
		/// Used with Incrementing
		/// </summary>
		public double Seed { get; set; }

		/// <summary>
		/// Used with Incrementing
		/// </summary>
		public double Increment { get; set; }

		/// <summary>
		/// Used with LogNormal
		/// </summary>
		public double Mu { get; set; }

		/// <summary>
		/// Used with LogNormal
		/// </summary>
		public double Sigma { get; set; }

		/// <summary>
		/// Used with Normal
		/// </summary>
		public double StandardDeviation { get; set; }

		/// <summary>
		/// Used with Uniform
		/// </summary>
		public double Min { get; set; }

		/// <summary>
		/// Used with Uniform
		/// </summary>
		public double Max { get; set; }
	}
}
