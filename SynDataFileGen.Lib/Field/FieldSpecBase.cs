using System;
using System.Collections.Generic;
using System.Reflection;

namespace SynDataFileGen.Lib
{
	public abstract class FieldSpecBase<T> : IFieldSpec<T>
		where T : new()
	{
		#region IFieldSpec implementation

		/// <summary>
		/// If header row output is specified, property name will be written to the header row.
		/// </summary>
		public PropertyInfo Prop { get; protected set; }

		/// <summary>
		/// Whether repeated values in the output should be prevented.
		/// Note that for categorical fields, if the number of results exceeds the number of categories, duplicate values will occur despite this setting.
		/// </summary>
		public bool EnforceUniqueValues { get; protected set; } = false;

		/// <summary>
		/// String to format output. Optional.
		/// </summary>
		public string FormatString { get; protected set; }

		/// <summary>
		/// Length of this field in fixed-width files; ignored otherwise. Values will be padded or truncated (as needed) to this length.
		/// </summary>
		public int? FixedWidthLength { get; protected set; }

		/// <summary>
		/// Character that will be used to pad this field. Only used for fixed-width files; ignored otherwise.
		/// Typically a whitespace character like space (' ').
		/// If not specified/null, the default set on the file config will be used.
		/// </summary>
		public char? FixedWidthPaddingChar { get; private set; } = null;

		/// <summary>
		/// Pad this field at start (i.e. right-justify field) or at end (i.e. left-justify field).
		/// Defaults to file-level setting if not set here.
		/// Only used for fixed-width files, ignored otherwise.
		/// </summary>
		public Util.Location? FixedWidthAddPadding { get; protected set; } = null;

		/// <summary>
		/// Truncate this field. when exceeding FixedWidthLength, at start (i.e. chop off from the left) or at end (i.e. chop off from the right).
		/// Defaults to file-level setting if not set here.
		/// Only used for fixed-width files, ignored otherwise.
		/// </summary>
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