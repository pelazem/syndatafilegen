using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SynDataFileGen.Lib
{
	public static class ConfigValues
	{
		#region Constants

		public const char DEFAULT_PADDING_CHAR = ' ';

		public const string DISTRIBUTION_BETA = "beta";
		public const string DISTRIBUTION_CAUCHY = "cauchy";
		public const string DISTRIBUTION_CHISQUARE = "chisquare";
		public const string DISTRIBUTION_EXPONENTIAL = "exponential";
		public const string DISTRIBUTION_GAMMA = "gamma";
		public const string DISTRIBUTION_INCREMENTING = "incrementing";
		public const string DISTRIBUTION_INVERSEGAMMA = "inversegamma";
		public const string DISTRIBUTION_LAPLACE = "laplace";
		public const string DISTRIBUTION_LOGNORMAL = "lognormal";
		public const string DISTRIBUTION_NORMAL = "normal";
		public const string DISTRIBUTION_STUDENTT = "studentt";
		public const string DISTRIBUTION_UNIFORM = "uniform";
		public const string DISTRIBUTION_WEIBULL = "weibull";

		public const string ENCODING_ASCII = "ascii";
		public const string ENCODING_UTF8 = "utf8";
		public const string ENCODING_UTF32 = "utf32";

		public const string FIELDTYPE_CATEGORICAL = "categorical";
		public const string FIELDTYPE_CONTINUOUSDATETIME = "continuousdatetime";
		// public const string FIELDTYPE_CONTINUOUSNONNUMERIC = "continuousnonnumeric";
		public const string FIELDTYPE_CONTINUOUSNUMERIC = "continuousnumeric";
		// public const string FIELDTYPE_COPYFIELD = "copyfield";
		public const string FIELDTYPE_DYNAMIC = "dynamic";
		// public const string FIELDTYPE_IDEMPOTENT = "idempotent";

		public const string FILETYPE_AVRO = "avro";
		public const string FILETYPE_DELIMITED = "delimited";
		public const string FILETYPE_FIXEDWIDTH = "fixedwidth";
		public const string FILETYPE_JSON = "json";

		public const string LOCATION_ATSTART = "atstart";
		public const string LOCATION_ATEND = "atend";

		#endregion

		#region Lists

		internal static List<string> ValidDistributionNames = new List<string>() { DISTRIBUTION_BETA, DISTRIBUTION_CAUCHY, DISTRIBUTION_CHISQUARE, DISTRIBUTION_EXPONENTIAL, DISTRIBUTION_GAMMA, DISTRIBUTION_INCREMENTING, DISTRIBUTION_INVERSEGAMMA, DISTRIBUTION_LAPLACE, DISTRIBUTION_LOGNORMAL, DISTRIBUTION_NORMAL, DISTRIBUTION_STUDENTT, DISTRIBUTION_UNIFORM, DISTRIBUTION_WEIBULL };
		internal static List<string> ValidEncodingNames = new List<string>() { ENCODING_ASCII, ENCODING_UTF8, ENCODING_UTF32 };
		internal static List<string> ValidFieldTypes = new List<string>() { FIELDTYPE_CATEGORICAL, FIELDTYPE_CONTINUOUSDATETIME, FIELDTYPE_CONTINUOUSNUMERIC, FIELDTYPE_DYNAMIC };
		internal static List<string> ValidFileTypes = new List<string>() { FILETYPE_AVRO, FILETYPE_DELIMITED, FILETYPE_FIXEDWIDTH, FILETYPE_JSON };
		internal static List<string> ValidLocations = new List<string>() { LOCATION_ATSTART, LOCATION_ATEND };

		#endregion
	}
}
