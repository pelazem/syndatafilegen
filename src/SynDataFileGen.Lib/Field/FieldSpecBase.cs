using System;
using System.Collections.Generic;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public abstract class FieldSpecBase : IFieldSpec
	{
		protected double? _percentChanceEmpty = 0;
		protected string _emptyValue = string.Empty;

		protected object _value = null;

		#region IFieldSpec implementation

		/// <summary>
		/// If header row output is specified, name will be written to the header row.
		/// </summary>
		public string Name { get; protected set; }

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
		/// Percent chance that a generated value will be empty.
		/// Passing null or negative will set this to zero.
		/// Passing greater than 100 will set this to 100.
		/// </summary>
		public double? PercentChanceEmpty
		{
			get { return _percentChanceEmpty; }
			protected set
			{
				if (value == null || value < 0)
					_percentChanceEmpty = 0;
				else if (value >= 100)
					_percentChanceEmpty = 100;
				else
					_percentChanceEmpty = value;
			}
		}

		/// <summary>
		/// If an empty value is generated (based on PercentChanceEmpty), the value to be written to the output.
		/// Generally empty string ("") is appropriate.
		/// </summary>
		public string EmptyValue
		{
			get { return _emptyValue; }
			protected set
			{
				if (value == null)
					_emptyValue = string.Empty;
				else
					_emptyValue = value;
			}
		}

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

		/// <summary>
		/// Generate a new value by calling SetNextValue()
		/// </summary>
		public object Value
		{
			get
			{
				return _value;
			}
		}

		/// <summary>
		/// Generate a new value by calling SetNextValue()
		/// </summary>
		public string ValueString
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(this.FormatString))
					return string.Format(this.FormatString, this.Value);
				else
					return (this.Value == null ? string.Empty : this.Value.ToString());
			}
		}

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
		public FieldSpecBase(string name, bool enforceUniqueValues, string formatString, double? percentChanceEmpty, string emptyValue)
		{
			this.Name = name;
			this.FormatString = formatString;
			this.EnforceUniqueValues = enforceUniqueValues;
			this.PercentChanceEmpty = percentChanceEmpty;
			this.EmptyValue = emptyValue;
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
		public FieldSpecBase(string name, bool enforceUniqueValues, string formatString, double? percentChanceEmpty, string emptyValue, int? fixedWidthLength, char? fixedWidthPaddingChar, Util.Location? fixedWidthAddPadding, Util.Location? fixedWidthTruncate)
			: this(name, enforceUniqueValues, formatString, percentChanceEmpty, emptyValue)
		{
			this.FixedWidthLength = fixedWidthLength;
			this.FixedWidthPaddingChar = fixedWidthPaddingChar;
			this.FixedWidthAddPadding = fixedWidthAddPadding;
			this.FixedWidthTruncate = fixedWidthTruncate;
		}

		#endregion

		public void SetNextValue()
		{
			bool itsEmpty = false;

			if (this.PercentChanceEmpty == 100)
				itsEmpty = true;
			else if (this.PercentChanceEmpty > 0)
				itsEmpty = (RNG.GetUniform(0, 100) <= this.PercentChanceEmpty);
			else
				itsEmpty = false;   // Just being explicit...

			if (itsEmpty)
				_value = this.EmptyValue;
			else
				SetNextValueWorker();
		}

		protected abstract void SetNextValueWorker();
	}
}