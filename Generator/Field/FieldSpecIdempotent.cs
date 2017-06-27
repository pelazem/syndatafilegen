using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using pelazem.Common;

namespace Generator
{
	public class FieldSpecIdempotent<T> : FieldSpecBase<T>
	{
		#region FieldSpecBase implementation

		public override void SetValue(T item)
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

		public FieldSpecIdempotent(PropertyInfo prop, string formatString, int? lengthIfFixedWidth, Util.Location? addPaddingAtIfFixedWidth = Util.Location.AtStart, Util.Location? truncateTooLongAtIfFixedWidth = Util.Location.AtEnd, char? paddingCharIfFixedWidth = null)
			: base(prop, false, formatString, lengthIfFixedWidth, addPaddingAtIfFixedWidth, truncateTooLongAtIfFixedWidth, paddingCharIfFixedWidth)
		{
		}

		#endregion
	}
}
