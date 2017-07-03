using System;
using System.Reflection;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class FieldSpecContinuousDateTime<T> : FieldSpecBase<T>
		where T : new()
	{
		#region FieldSpecBase implementation

		public override void SetValue(T item)
		{
			if (this.Prop.PropertyType.Equals(TypeUtil.TypeString) && !string.IsNullOrWhiteSpace(this.FormatString))
				this.Prop.SetValueEx(item, string.Format(this.FormatString, GetValue()));
			else
				this.Prop.SetValueEx(item, GetValue());
		}

		#endregion

		#region Properties

		public IDistribution Distribution { get; } = new DistUniform();

		public DateTime DateStart { get; set; }

		public DateTime DateEnd { get; set; }

		#endregion

		#region Constructors

		private FieldSpecContinuousDateTime() { }

		public FieldSpecContinuousDateTime(PropertyInfo prop, DateTime dateStart, DateTime dateEnd, bool enforceUniqueValues, string formatString)
			: base(prop, enforceUniqueValues, formatString)
		{
			this.DateStart = dateStart;
			this.DateEnd = (dateEnd >= dateStart ? dateEnd : dateStart);
		}

		public FieldSpecContinuousDateTime(PropertyInfo prop, DateTime dateStart, DateTime dateEnd, bool enforceUniqueValues, string formatString, int? fixedWidthLength, Util.Location? fixedWidthAddPadding = Util.Location.AtStart, Util.Location? fixedWidthTruncate = Util.Location.AtEnd, char? fixedWidthPaddingChar = null)
			: base(prop, enforceUniqueValues, formatString, fixedWidthLength, fixedWidthPaddingChar, fixedWidthAddPadding, fixedWidthTruncate)
		{
			this.DateStart = dateStart;
			this.DateEnd = (dateEnd >= dateStart ? dateEnd : dateStart);
		}

		#endregion

		private DateTime GetValue()
		{
			if (this.DateStart == this.DateEnd)
				return this.DateStart;

			long diffTicks = this.DateEnd.Subtract(this.DateStart).Ticks;

			double value = this.Distribution.GetValue();

			if (this.EnforceUniqueValues)
			{
				while (this.UniqueValues.ContainsKey(value))
					value = this.Distribution.GetValue();

				this.UniqueValues.Add(value, false);
			}

			return this.DateStart.AddTicks(Converter.GetInt64(value * diffTicks));
		}
	}
}
