using System;
using System.Reflection;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class FieldSpecCopyField<T> : FieldSpecBase<T>
		where T : new()
	{
		#region FieldSpecBase implementation

		public override void SetValue(T item)
		{
			if (this.PropToCopy == null)
				return;

			if (this.Prop.PropertyType.Equals(TypeUtil.TypeString) && !string.IsNullOrWhiteSpace(this.FormatString))
				this.Prop.SetValueEx(item, string.Format(this.FormatString, GetValue(item)));
			else
				this.Prop.SetValueEx(item, GetValue(item));
		}

		#endregion

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

		private object GetValue(T item)
		{
			return this.PropToCopy.GetValueEx(item);
		}
	}
}
