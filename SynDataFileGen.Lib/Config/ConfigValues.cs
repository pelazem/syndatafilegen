using System;
using System.Collections.Generic;
using System.Text;

namespace SynDataFileGen.Lib
{
	public static class ConfigValues
	{
		#region Constants

		public const string ENCODING_ASCII = "ascii";
		public const string ENCODING_UTF8 = "utf8";
		public const string ENCODING_UTF32 = "utf32";

		public const string FIELDTYPE_CATEGORICAL = "categorical";
		public const string FIELDTYPE_CONTINUOUSDATETIME = "continuousdatetime";
		public const string FIELDTYPE_CONTINUOUSNUMERIC = "continuousnumeric";
		public const string FIELDTYPE_COPYFIELD = "copyfield";
		public const string FIELDTYPE_DYNAMIC = "dynamic";
		public const string FIELDTYPE_IDEMPOTENT = "idempotent";

		public const string FILETYPE_AVRO = "avro";
		public const string FILETYPE_DELIMITED = "delimited";
		public const string FILETYPE_FIXEDWIDTH = "fixedwidth";
		public const string FILETYPE_JSON = "json";

		public const string LOCATION_ATSTART = "atstart";
		public const string LOCATION_ATEND = "atend";

		#endregion

		#region Lists

		internal static List<string> ValidEncodingNames = new List<string>() { ENCODING_ASCII, ENCODING_UTF8, ENCODING_UTF32 };
		internal static List<string> ValidFieldTypes = new List<string>() { FIELDTYPE_CATEGORICAL, FIELDTYPE_CONTINUOUSDATETIME, FIELDTYPE_CONTINUOUSNUMERIC, FIELDTYPE_COPYFIELD, FIELDTYPE_DYNAMIC, FIELDTYPE_IDEMPOTENT };
		internal static List<string> ValidFileTypes = new List<string>() { FILETYPE_AVRO, FILETYPE_DELIMITED, FILETYPE_FIXEDWIDTH, FILETYPE_JSON };
		internal static List<string> ValidLocations = new List<string>() { LOCATION_ATSTART, LOCATION_ATEND };

		#endregion
	}
}
