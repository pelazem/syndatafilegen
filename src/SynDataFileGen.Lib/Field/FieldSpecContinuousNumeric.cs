﻿using System;
using System.Reflection;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class FieldSpecContinuousNumeric : FieldSpecBase
	{
		#region Properties

		public IDistribution Distribution { get; private set; }

		public int? MaxDigitsAfterDecimalPoint { get; private set; }

		#endregion

		#region Constructors

		private FieldSpecContinuousNumeric() { }

		public FieldSpecContinuousNumeric(string name, IDistribution distribution, int? maxDigitsAfterDecimalPoint, bool enforceUniqueValues, string formatString, double? percentChanceEmpty, string emptyValue)
			: base(name, enforceUniqueValues, formatString, percentChanceEmpty, emptyValue)
		{
			this.Distribution = distribution;

			if (maxDigitsAfterDecimalPoint != null)
				this.MaxDigitsAfterDecimalPoint = ((maxDigitsAfterDecimalPoint >= 0 && maxDigitsAfterDecimalPoint <= 5) ? maxDigitsAfterDecimalPoint : 2);
			else
				this.MaxDigitsAfterDecimalPoint = maxDigitsAfterDecimalPoint;
		}

		public FieldSpecContinuousNumeric(string name, IDistribution distribution, int? maxDigitsAfterDecimalPoint, bool enforceUniqueValues, string formatString, int? fixedWidthLength, Util.Location? fixedWidthAddPadding, Util.Location? fixedWidthTruncate, char? fixedWidthPaddingChar, double? percentChanceEmpty, string emptyValue)
			: base(name, enforceUniqueValues, formatString, percentChanceEmpty, emptyValue, fixedWidthLength, fixedWidthPaddingChar, fixedWidthAddPadding, fixedWidthTruncate)
		{
			this.Distribution = distribution;

			if (maxDigitsAfterDecimalPoint != null)
				this.MaxDigitsAfterDecimalPoint = ((maxDigitsAfterDecimalPoint >= 0 && maxDigitsAfterDecimalPoint <= 5) ? maxDigitsAfterDecimalPoint : 2);
			else
				this.MaxDigitsAfterDecimalPoint = maxDigitsAfterDecimalPoint;
		}

		#endregion

		#region FieldSpecBase implementation

		protected override void SetNextValueWorker()
		{
			double result = this.Distribution.GetValue();

			if (this.EnforceUniqueValues)
			{
				while (this.UniqueValues.ContainsKey(result))
					result = this.Distribution.GetValue();

				this.UniqueValues.Add(result, false);
			}

			if (this.MaxDigitsAfterDecimalPoint != null)
				result = Math.Round(result, this.MaxDigitsAfterDecimalPoint.Value);

			_value = result;
		}

		#endregion
	}
}
