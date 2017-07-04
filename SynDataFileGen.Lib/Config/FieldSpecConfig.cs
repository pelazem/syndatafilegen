using System;
using System.Collections.Generic;
using System.Text;

namespace SynDataFileGen.Lib
{
	public class FieldSpecConfig
	{
		/// <summary>
		/// Valid values: Categorical, ContinuousDateTime, ContinuousNumeric, CopyField, Dynamic, Idempotent
		/// </summary>
		public string FieldSpecTypeName { get; set; }

		/// <summary>
		/// Whether repeated values in the output should be prevented.
		/// Note that for categorical fields, if the number of results exceeds the number of categories, duplicate values will occur despite this setting.
		/// </summary>
		public bool EnforceUniqueValues { get; set; } = false;

		/// <summary>
		/// String to format output. Optional.
		/// </summary>
		public string FormatString { get; set; }

		/// <summary>
		/// Length of this field. Values will be padded or truncated (as needed) to this length.
		/// </summary>
		public int? FixedWidthLength { get; set; }

		/// <summary>
		/// Character that will be used to pad this field. Only used for fixed-width files.
		/// Typically a whitespace character like space (' ').
		/// If not specified/null, the default set on the file config will be used.
		/// </summary>
		public char? FixedWidthPaddingChar { get; set; } = null;

		/// <summary>
		/// Pad this field at start (i.e. right-justify field) or at end (i.e. left-justify field).
		/// Valid values: AtStart, AtEnd
		/// Only used for fixed-width files, ignored otherwise.
		/// </summary>
		public string FixedWidthAddPadding { get; set; }

		/// <summary>
		/// Truncate this field. when exceeding MaxLength, at start (i.e. chop off from the left) or at end (i.e. chop off from the right).
		/// Valid values: AtStart, AtEnd
		/// Only used for fixed-width files, ignored otherwise.
		/// </summary>
		public string FixedWidthTruncate { get; set; }

		#region Categorical

		/// <summary>
		/// Array of category values. Only used for categorical fields.
		/// </summary>
		public List<object> Categories { get; } = new List<object>();

		#endregion

		#region Continuous Date/Time

		/// <summary>
		/// Start date/time. Only used with continuous date/time fields.
		/// </summary>
		public DateTime DateStart { get; set; }

		/// <summary>
		/// End date/time. Only used with continuous date/time fields.
		/// </summary>
		public DateTime DateEnd { get; set; }

		#endregion

		#region Continuous Numeric

		public DistributionConfig NumericDistribution { get; set; } = null;

		/// <summary>
		/// Max digits after the decimal point. If not set, values output as is.
		/// Only used with continuous numeric fields. Ignored otherwise.
		/// </summary>
		public int? MaxDigitsAfterDecimalPoint { get; set; }

		#endregion

		#region CopyField

		public string FieldNameToCopy { get; set; }

		#endregion

		#region Dynamic

		/// <summary>
		/// (Advanced) A C# Func<object> expression.
		/// </summary>
		public string DynamicFunc { get; set; }

		#endregion
	}
}
