using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using pelazem.Common;

namespace Generator
{
	public class FieldSpecContinuousNumeric<T> : FieldSpecBase<T>
	{
		#region FieldSpecBase implementation

		public override void SetValue(T item)
		{
			double value = GetValue();

			Type propType = this.Prop.PropertyType;

			if (this.MaxDigitsAfterDecimalPoint != null)
				value = Math.Round(value, this.MaxDigitsAfterDecimalPoint.Value);

			if (propType.Equals(TypeHelper.TypeString) && !string.IsNullOrWhiteSpace(this.FormatString))
					this.Prop.SetValueEx(item, string.Format(this.FormatString, value));					
			else if (propType.Equals(TypeHelper.TypeDouble) || propType.Equals(TypeHelper.TypeDoubleNullable))
				this.Prop.SetValueEx(item, value);
			else if (propType.Equals(TypeHelper.TypeSingle) || propType.Equals(TypeHelper.TypeSingleNullable))
				this.Prop.SetValueEx(item, Converter.GetSingle(value));
			else if (propType.Equals(TypeHelper.TypeInt32) || propType.Equals(TypeHelper.TypeInt32Nullable))
				this.Prop.SetValueEx(item, Converter.GetInt32(value));
			else if (propType.Equals(TypeHelper.TypeInt64) || propType.Equals(TypeHelper.TypeInt64Nullable))
				this.Prop.SetValueEx(item, Converter.GetInt64(value));
			else if (propType.Equals(TypeHelper.TypeBool) || propType.Equals(TypeHelper.TypeBoolNullable))
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

		public FieldSpecContinuousNumeric(PropertyInfo prop, IDistribution distribution, int? maxDigitsAfterDecimalPoint, bool enforceUniqueValues, string formatString, int? lengthIfFixedWidth, Util.Location? addPaddingAtIfFixedWidth = Util.Location.AtStart, Util.Location? truncateTooLongAtIfFixedWidth = Util.Location.AtEnd, char? paddingCharIfFixedWidth = null)
			: base(prop, enforceUniqueValues, formatString, lengthIfFixedWidth, addPaddingAtIfFixedWidth, truncateTooLongAtIfFixedWidth, paddingCharIfFixedWidth)
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
