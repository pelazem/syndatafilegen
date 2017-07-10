using System;

namespace SynDataFileGen.Lib
{
	/// <summary>
	/// TODO determine if this class serves a purpose in addition to FieldSpecDynamic. May be retired.
	/// </summary>
	public class FieldSpecContinuousNonNumeric : FieldSpecBase
	{
		#region Properties

		#endregion

		#region Constructors

		private FieldSpecContinuousNonNumeric() { }

		public FieldSpecContinuousNonNumeric(string name, bool enforceUniqueValues, string formatString)
			: base(name, enforceUniqueValues, formatString)
		{
		}

		public FieldSpecContinuousNonNumeric(string name, bool enforceUniqueValues, string formatString, int? fixedWidthLength, Util.Location? fixedWidthAddPadding = Util.Location.AtStart, Util.Location? fixedWidthTruncate = Util.Location.AtEnd, char? fixedWidthPaddingChar = null)
			: base(name, enforceUniqueValues, formatString, fixedWidthLength, fixedWidthPaddingChar, fixedWidthAddPadding, fixedWidthTruncate)
		{
		}

		#endregion

		#region FieldSpecBase implementation

		protected override object GetValue()
		{
			return null;
		}

		#endregion
	}
}
