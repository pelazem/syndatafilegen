using System;

namespace SynDataFileGen.Lib
{
	public struct Constants
	{
		// Tokens for path spec. May need to nuance this; currently these tokens assume fully qualified, e.g. 03.
		public const string YYYY = "{yyyy}";
		public const string YY = "{yy}";
		public const string MM = "{mm}";
		public const string DD = "{dd}";
		public const string HH = "{hh}";

		public const string DEFAULT_FILE_NAME = "generated";
		public const string DEFAULT_FILE_EXTENSION = ".txt";
	}
}
