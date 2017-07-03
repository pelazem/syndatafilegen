using System;
using System.Collections.Generic;
using System.Reflection;

namespace SynDataFileGen.Lib
{
	public abstract class FieldSpecBase<T> : IFieldSpec<T>
		where T : new()
	{
		#region IFieldSpec implementation

		public PropertyInfo Prop { get; protected set; }

		public bool EnforceUniqueValues { get; protected set; } = false;

		public string FormatString { get; protected set; }


		public int? FixedWidthLength { get; protected set; }

		public char? FixedWidthPaddingChar { get; private set; } = null;

		public Util.Location? FixedWidthAddPadding { get; protected set; } = null;

		public Util.Location? FixedWidthTruncate { get; protected set; } = null;


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
		/// <param name="fixedWidthLength"></param>
		/// <param name="fixedWidthPaddingChar"></param>
		/// <param name="fixedWidthAddPadding"></param>
		/// <param name="fixedWidthTruncate"></param>
		/// <param name="enforceUniqueValues"></param>
		public FieldSpecBase(PropertyInfo prop, bool enforceUniqueValues, string formatString, int? fixedWidthLength, char? fixedWidthPaddingChar, Util.Location? fixedWidthAddPadding, Util.Location? fixedWidthTruncate)
			: this(prop, enforceUniqueValues, formatString)
		{
			this.FixedWidthLength = fixedWidthLength;
			this.FixedWidthPaddingChar = fixedWidthPaddingChar;
			this.FixedWidthAddPadding = fixedWidthAddPadding;
			this.FixedWidthTruncate = fixedWidthTruncate;
		}

		#endregion
	}
}