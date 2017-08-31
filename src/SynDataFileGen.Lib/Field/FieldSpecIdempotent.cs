using System;

namespace SynDataFileGen.Lib
{
	public class FieldSpecIdempotent : FieldSpecBase
	{
		#region Properties

		public new object Value { get; set; }

		#endregion

		#region Constructors

		private FieldSpecIdempotent() { }

		public FieldSpecIdempotent(string name, bool enforceUniqueValues, string formatString, double? percentChanceEmpty, string emptyValue)
			: base(name, enforceUniqueValues, formatString, percentChanceEmpty, emptyValue)
		{
			
		}

		public FieldSpecIdempotent(string name, bool enforceUniqueValues, string formatString, int? fixedWidthLength, Util.Location? fixedWidthAddPadding, Util.Location? fixedWidthTruncate, char? fixedWidthPaddingChar, double? percentChanceEmpty, string emptyValue)
			: base(name, enforceUniqueValues, formatString, percentChanceEmpty, emptyValue, fixedWidthLength, fixedWidthPaddingChar, fixedWidthAddPadding, fixedWidthTruncate)
		{

		}

		#endregion

		#region FieldSpecBase implementation

		protected override void SetNextValueWorker()
		{
			return;
		}

		#endregion
	}
}
