using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class Util
	{
		#region Enums

		public enum Location
		{
			AtStart,
			AtEnd
		}

		#endregion

		public static Encoding GetEncoding(string encodingName)
		{
			Encoding result;

			if (!string.IsNullOrWhiteSpace(encodingName))
			{
				switch (encodingName.ToLowerInvariant())
				{
					case ConfigValues.ENCODING_ASCII:
						result = Encoding.ASCII;
						break;
					case ConfigValues.ENCODING_UTF32:
						result = Encoding.UTF32;
						break;
					case ConfigValues.ENCODING_UTF8:
					default:
						result = Encoding.UTF8;
						break;
				}
			}
			else
				result = Encoding.UTF8;

			return result;
		}

		internal static string GetPadded(int someTwoDigitNumber)
		{
			return (someTwoDigitNumber < 10 ? "0" : string.Empty) + someTwoDigitNumber.ToString();
		}
	}
}
