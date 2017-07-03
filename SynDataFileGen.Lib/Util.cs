using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		#region Type Aliases for Avro Serializer

		/// <summary>
		/// Microsoft.Hadoop.Avro2 serializer has very limited list of supported primitive types. Their aliases are neither .NET nor C#-compliant.
		/// See https://github.com/dougmsft/microsoft-avro/blob/master/Microsoft.Hadoop.Avro/Schema/JsonSchemaBuilder.cs
		/// </summary>
		internal static readonly Dictionary<Type, string> AvroPrimitiveTypeAliases = new Dictionary<Type, string>
		{
			{ TypeUtil.TypeBool, "boolean" },
			{ TypeUtil.TypeInt32, "int" },
			{ TypeUtil.TypeInt64, "long" },
			{ TypeUtil.TypeSingle, "float" },
			{ TypeUtil.TypeDouble, "double" },
			{ TypeUtil.TypeString, "string" },
			{ typeof(Byte[]), "bytes" }
		};

		internal static string GetAvroPrimitiveTypeAlias(Type type)
		{
			if (type == null)
				return string.Empty;
			else
				return AvroPrimitiveTypeAliases.ContainsKey(type) ? AvroPrimitiveTypeAliases[type] : string.Empty;
		}

		#endregion

		public static Encoding GetEncoding(string encodingName)
		{
			Encoding result;

			if (!string.IsNullOrWhiteSpace(encodingName))
			{
				switch (encodingName)
				{
					case "ASCII":
						result = Encoding.ASCII;
						break;
					case "UTF32":
						result = Encoding.UTF32;
						break;
					case "UTF8":
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
