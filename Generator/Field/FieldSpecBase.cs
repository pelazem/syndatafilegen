using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using pelazem.Common;

namespace Generator
{
	public abstract class FieldSpecBase<T> : IFieldSpec<T>
	{
		#region IFieldSpec implementation

		public PropertyInfo Prop { get; protected set; }

		public bool EnforceUniqueValues { get; protected set; } = false;

		public string FormatString { get; protected set; }

		public int? LengthIfFixedWidth { get; protected set; }

		public Util.Location? AddPaddingAtIfFixedWidth { get; protected set; } = null;

		public Util.Location? TruncateTooLongAtIfFixedWidth { get; protected set; } = null;

		public char? PaddingCharIfFixedWidth { get; private set; } = null;

		public abstract void SetValue(T item);

		#endregion

		#region Properties

		public SortedDictionary<object, bool> UniqueValues { get; }

		#endregion

		#region Constructors

		protected FieldSpecBase() { }

		/// <summary>
		/// Constructor for non-fixed-width files
		/// </summary>
		/// <param name="prop"></param>
		/// <param name="formatString"></param>
		/// <param name="enforceUniqueValues"></param>
		public FieldSpecBase(PropertyInfo prop, bool enforceUniqueValues, string formatString)
		{
			this.Prop = prop;
			this.FormatString = formatString;
			this.EnforceUniqueValues = enforceUniqueValues;
		}

		/// <summary>
		/// Constructor for fixed-width files
		/// </summary>
		/// <param name="prop"></param>
		/// <param name="formatString"></param>
		/// <param name="paddingCharIfFixedWidth"></param>
		/// <param name="lengthIfFixedWidth"></param>
		/// <param name="addPaddingAtIfFixedWidth"></param>
		/// <param name="truncateTooLongAtIfFixedWidth"></param>
		/// <param name="enforceUniqueValues"></param>
		public FieldSpecBase(PropertyInfo prop, bool enforceUniqueValues, string formatString, int? lengthIfFixedWidth, Util.Location? addPaddingAtIfFixedWidth, Util.Location? truncateTooLongAtIfFixedWidth, char? paddingCharIfFixedWidth)
			: this(prop, enforceUniqueValues, formatString)
		{
			this.LengthIfFixedWidth = lengthIfFixedWidth;
			this.AddPaddingAtIfFixedWidth = addPaddingAtIfFixedWidth;
			this.TruncateTooLongAtIfFixedWidth = truncateTooLongAtIfFixedWidth;
			this.PaddingCharIfFixedWidth = paddingCharIfFixedWidth;
		}

		#endregion
	}
}
