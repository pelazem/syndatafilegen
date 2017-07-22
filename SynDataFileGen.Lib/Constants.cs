using System;

namespace SynDataFileGen.Lib
{
	public struct Constants
	{
		// Tokens for path spec. May need to nuance this; currently these tokens assume fully qualified, e.g. 03.
		public const string YEAR4 = "{yyyy}";
		public const string YEAR2 = "{yy}";
		public const string MONTH = "{MM}";
		public const string DAY = "{dd}";
		public const string HOUR = "{hh}";
		public const string MINUTE = "{mm}";
		public const string SECOND = "{ss}";

		public const string DEFAULT_FILE_NAME = "generated";
		public const string DEFAULT_FILE_EXTENSION = ".txt";
	}
}
