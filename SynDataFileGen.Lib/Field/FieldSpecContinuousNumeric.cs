using System;
using System.Reflection;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class FieldSpecContinuousNumeric<T> : FieldSpecBase<T>
		where T : new()

	{
		#region FieldSpecBase implementation

		public override void SetValue(T item)
		{
			double value = GetValue();

			Type propType = this.Prop.PropertyType;

			if (this.MaxDigitsAfterDecimalPoint != null)
				value = Math.Round(value, this.MaxDigitsAfterDecimalPoint.Value);

			if (propType.Equals(TypeUtil.TypeString) && !string.IsNullOrWhiteSpace(this.FormatString))
					this.Prop.SetValueEx(item, string.Format(this.FormatString, value));					
			else if (propType.Equals(TypeUtil.TypeDouble) || propType.Equals(TypeUtil.TypeDoubleNullable))
				this.Prop.SetValueEx(item, value);
			else if (propType.Equals(TypeUtil.TypeSingle) || propType.Equals(TypeUtil.TypeSingleNullable))
				this.Prop.SetValueEx(item, Converter.GetSingle(value));
			else if (propType.Equals(TypeUtil.TypeInt32) || propType.Equals(TypeUtil.TypeInt32Nullable))
				this.Prop.SetValueEx(item, Converter.GetInt32(value));
			else if (propType.Equals(TypeUtil.TypeInt64) || propType.Equals(TypeUtil.TypeInt64Nullable))
				this.Prop.SetValueEx(item, Converter.GetInt64(value));
			else if (propType.Equals(TypeUtil.TypeBool) || propType.Equals(TypeUtil.TypeBoolNullable))
				this.Prop.SetValueEx(item, Converter.GetBool(value));
			else
				this.Prop.SetValueEx(item, value);
		}

		#endregion

		#region Properties

		public IDistribution Distribution { get; private set; }

		public int? MaxDigitsAfterDecimalPoint { get; private set; }

		#endregion

		#region Constructors

		private FieldSpecContinuousNumeric() { }

		public FieldSpecContinuousNumeric(PropertyInfo prop, IDistribution distribution, int? maxDigitsAfterDecimalPoint, bool enforceUniqueValues, string formatString)
			: base(prop, enforceUniqueValues, formatString)
		{
			this.Distribution = distribution;

			if (maxDigitsAfterDecimalPoint != null)
				this.MaxDigitsAfterDecimalPoint = ((maxDigitsAfterDecimalPoint >= 0 && maxDigitsAfterDecimalPoint <= 5) ? maxDigitsAfterDecimalPoint : 2);
			else
				this.MaxDigitsAfterDecimalPoint = maxDigitsAfterDecimalPoint;
		}

		public FieldSpecContinuousNumeric(PropertyInfo prop, IDistribution distribution, int? maxDigitsAfterDecimalPoint, bool enforceUniqueValues, string formatString, int? fixedWidthLength, Util.Location? fixedWidthAddPadding = Util.Location.AtStart, Util.Location? fixedWidthTruncate = Util.Location.AtEnd, char? fixedWidthPaddingChar = null)
			: base(prop, enforceUniqueValues, formatString, fixedWidthLength, fixedWidthPaddingChar, fixedWidthAddPadding, fixedWidthTruncate)
		{
			this.Distribution = distribution;

			if (maxDigitsAfterDecimalPoint != null)
				this.MaxDigitsAfterDecimalPoint = ((maxDigitsAfterDecimalPoint >= 0 && maxDigitsAfterDecimalPoint <= 5) ? maxDigitsAfterDecimalPoint : 2);
			else
				this.MaxDigitsAfterDecimalPoint = maxDigitsAfterDecimalPoint;
		}

		#endregion

		private double GetValue()
		{
			double result = this.Distribution.GetValue();

			if (this.EnforceUniqueValues)
			{
				while (this.UniqueValues.ContainsKey(result))
					result = this.Distribution.GetValue();

				this.UniqueValues.Add(result, false);
			}

			return result;
		}
	}
}
