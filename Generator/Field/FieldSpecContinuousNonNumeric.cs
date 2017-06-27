using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
	public class FieldSpecContinuousNonNumeric<T> : FieldSpecBase<T>
		where T : new()
	{
		#region FieldSpecBase implementation

		public override void SetValue(T item)
		{
			object value = GetValue();

			throw new NotImplementedException();

			// TODO what do we do here? A RegEx and magic? Hmmm....
		}

		#endregion

		#region Properties

		#endregion

		#region Constructors

		private FieldSpecContinuousNonNumeric() { }

		public FieldSpecContinuousNonNumeric(PropertyInfo prop, bool enforceUniqueValues, string formatString)
			: base(prop, enforceUniqueValues, formatString)
		{
		}

		public FieldSpecContinuousNonNumeric(PropertyInfo prop, bool enforceUniqueValues, string formatString, int? fixedWidthLength, Util.Location? fixedWidthAddPadding = Util.Location.AtStart, Util.Location? fixedWidthTruncate = Util.Location.AtEnd, char? fixedWidthPaddingChar = null)
			: base(prop, enforceUniqueValues, formatString, fixedWidthLength, fixedWidthPaddingChar, fixedWidthAddPadding, fixedWidthTruncate)
		{
		}

		#endregion

		private object GetValue()
		{
			return null;
		}
	}
}
