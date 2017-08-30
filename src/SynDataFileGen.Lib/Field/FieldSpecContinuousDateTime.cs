using System;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class FieldSpecContinuousDateTime : FieldSpecBase
	{
		#region Properties

		public IDistribution Distribution { get; } = new DistUniform();

		public DateTime DateStart { get; set; }

		public DateTime DateEnd { get; set; }

		#endregion

		#region Constructors

		private FieldSpecContinuousDateTime() { }

		public FieldSpecContinuousDateTime(string name, DateTime dateStart, DateTime dateEnd, bool enforceUniqueValues, string formatString, double? percentChanceEmpty, string emptyValue)
			: base(name, enforceUniqueValues, formatString, percentChanceEmpty, emptyValue)
		{
			this.DateStart = dateStart;
			this.DateEnd = (dateEnd >= dateStart ? dateEnd : dateStart);
		}

		public FieldSpecContinuousDateTime(string name, DateTime dateStart, DateTime dateEnd, bool enforceUniqueValues, string formatString, int? fixedWidthLength, Util.Location? fixedWidthAddPadding = Util.Location.AtStart, Util.Location? fixedWidthTruncate = Util.Location.AtEnd, char? fixedWidthPaddingChar = null, double? percentChanceEmpty = null, string emptyValue = null)
			: base(name, enforceUniqueValues, formatString, percentChanceEmpty, emptyValue, fixedWidthLength, fixedWidthPaddingChar, fixedWidthAddPadding, fixedWidthTruncate)
		{
			this.DateStart = dateStart;
			this.DateEnd = (dateEnd >= dateStart ? dateEnd : dateStart);
		}

		#endregion

		#region FieldSpecBase implementation

		protected override void SetNextValueWorker()
		{
			if (this.DateStart == this.DateEnd)
			{
				_value = this.DateStart;
				return;
			}

			long diffTicks = this.DateEnd.Subtract(this.DateStart).Ticks;

			double value = this.Distribution.GetValue();

			if (this.EnforceUniqueValues)
			{
				while (this.UniqueValues.ContainsKey(value))
					value = this.Distribution.GetValue();

				this.UniqueValues.Add(value, false);
			}

			_value = this.DateStart.AddTicks(Converter.GetInt64(value * diffTicks));
		}

		#endregion
	}
}
