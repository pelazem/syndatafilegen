using System;
using System.Reflection;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class FieldSpecCopyField : FieldSpecBase
	{
		#region Properties

		public PropertyInfo PropToCopy { get; private set; }

		#endregion

		#region Constructors

		private FieldSpecCopyField() { }

		public FieldSpecCopyField(PropertyInfo prop, PropertyInfo propWhoseValueToCopy, string formatString)
			: base(prop, false, formatString)
		{
			this.PropToCopy = propWhoseValueToCopy;
		}

		public FieldSpecCopyField(PropertyInfo prop, PropertyInfo propWhoseValueToCopy, string formatString, int? fixedWidthLength, Util.Location? fixedWidthAddPadding = Util.Location.AtStart, Util.Location? fixedWidthTruncate = Util.Location.AtEnd, char? fixedWidthPaddingChar = null)
			: base(prop, false, formatString, fixedWidthLength, fixedWidthPaddingChar, fixedWidthAddPadding, fixedWidthTruncate)
		{
			this.PropToCopy = propWhoseValueToCopy;
		}

		#endregion

		#region FieldSpecBase implementation

		protected override object GetValue()
		{
			return this.PropToCopy.GetValueEx(item);
		}

		#endregion
	}
}
