using System;

namespace SynDataFileGen.Lib
{
	public class FieldSpecIdempotent : FieldSpecBase
	{
		#region FieldSpecBase implementation

		protected override object GetValue()
		{
			return this.Value;
		}

		#endregion

		#region Properties

		public new object Value { get; set; }

		#endregion

		#region Constructors

		private FieldSpecIdempotent() { }

		public FieldSpecIdempotent(string name, bool enforceUniqueValues, string formatString)
			: base(name, enforceUniqueValues, formatString)
		{
			
		}

		public FieldSpecIdempotent(string name, bool enforceUniqueValues, string formatString, int? fixedWidthLength, Util.Location? fixedWidthAddPadding = Util.Location.AtStart, Util.Location? fixedWidthTruncate = Util.Location.AtEnd, char? fixedWidthPaddingChar = null)
			: base(name, enforceUniqueValues, formatString, fixedWidthLength, fixedWidthPaddingChar, fixedWidthAddPadding, fixedWidthTruncate)
		{

		}

		#endregion
	}
}
