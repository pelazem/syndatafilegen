using System;
using System.Reflection;

namespace SynDataFileGen.Lib
{
	public class FieldSpecIdempotent : FieldSpecBase
	{
		#region FieldSpecBase implementation

		public override void SetValue(object item)
		{
			// no op as we will just want to read the already-set value on the property - i.e. this is for pre-loaded items
		}

		#endregion

		#region Properties

		#endregion

		#region Constructors

		private FieldSpecIdempotent() { }

		public FieldSpecIdempotent(PropertyInfo prop, string formatString)
			: base(prop, false, formatString)
		{
		}

		public FieldSpecIdempotent(PropertyInfo prop, string formatString, int? fixedWidthLength, Util.Location? fixedWidthAddPadding = Util.Location.AtStart, Util.Location? fixedWidthTruncate = Util.Location.AtEnd, char? fixedWidthPaddingChar = null)
			: base(prop, false, formatString, fixedWidthLength, fixedWidthPaddingChar, fixedWidthAddPadding, fixedWidthTruncate)
		{
		}

		#endregion
	}
}
