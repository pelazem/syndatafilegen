using System;
using System.Collections.Generic;
using System.Text;

namespace SynDataFileGen.Lib
{
	public class FieldSpecConfig
	{
		private string _fieldType = string.Empty;
		private string _fixedWidthAddPadding = string.Empty;
		private string _fixedWidthTruncate = string.Empty;

		/// <summary>
		/// Valid values: Categorical, ContinuousDateTime, ContinuousNumeric, CopyField, Dynamic, Idempotent. Anything else will be ignored and ContinuousNumeric will be used instead.
		/// </summary>
		public string FieldType
		{
			get { return _fieldType; }
			set
			{
				if (ConfigValues.ValidFieldTypes.Contains(value.ToLowerInvariant()))
					_fieldType = value;
				else
					_fieldType = ConfigValues.FIELDTYPE_CONTINUOUSNUMERIC;
			}
		}

		/// <summary>
		/// Field name. If header row output is specified, this will be written to the header row.
		/// </summary>
		public string Name { get; set; }

		public bool EnforceUniqueValues { get; set; } = false;

		public string FormatString { get; set; }

		public int? FixedWidthLength { get; set; }

		public char? FixedWidthPaddingChar { get; set; } = null;

		public string FixedWidthAddPadding
		{
			get { return _fixedWidthAddPadding; }
			set
			{
				if (ConfigValues.ValidLocations.Contains(value.ToLowerInvariant()))
					_fixedWidthAddPadding = value;
				else
					_fixedWidthAddPadding = ConfigValues.LOCATION_ATSTART;
			}
		}

		public string FixedWidthTruncate
		{
			get { return _fixedWidthTruncate; }
		set
			{
				if (ConfigValues.ValidLocations.Contains(value.ToLowerInvariant()))
					_fixedWidthTruncate = value;
				else
					_fixedWidthTruncate = ConfigValues.LOCATION_ATEND;
			}
		}

		#region Categorical

		/// <summary>
		/// List of category values. Only used for categorical fields.
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
