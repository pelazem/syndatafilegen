using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class FileSpecArff : FileSpecBase
	{
		#region Constants

		private const string RELATION = "@RELATION ";
		private const string ATTRIB = "@ATTRIBUTE ";
		private const string DATA = "@DATA";

		private const string FMT_DATE = " date ";
		private const string FMT_NUMERIC = " numeric ";
		private const string FMT_STRING = " string ";

		private const string DELIM = ",";

		#endregion

		#region Properties

		public Encoding Encoding { get; } = Encoding.UTF8;

		public string RecordSetName { get; private set; } = string.Empty;

		#endregion

		#region Constructors

		private FileSpecArff() { }

		public FileSpecArff(string recordSetName, List<IFieldSpec> fieldSpecs, Encoding encoding, int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec, string fieldNameForLoopDateTime)
			: base(recordsPerFileMin, recordsPerFileMax, pathSpec, fieldSpecs, fieldNameForLoopDateTime)
		{
			this.RecordSetName = recordSetName;
			this.Encoding = encoding;
		}

		#endregion

		public override Stream GetContentStream(List<ExpandoObject> records)
		{
			var result = new MemoryStream();

			using (var interim = new MemoryStream())
			{
				using (var sw = new StreamWriter(interim, this.Encoding))
				{
					sw.Write(GetHeaderRecord());

					foreach (var record in records)
						sw.WriteLine(SerializeRecord(record));

					sw.Flush();

					interim.Seek(0, SeekOrigin.Begin);

					interim.CopyTo(result);
				}
			}

			return result;
		}

		protected override ExpandoObject GetRecord(DateTime? dateLoop = null)
		{
			ExpandoObject record = new ExpandoObject();

			IDictionary<string, object> recordProperties = record as IDictionary<string, object>;

			if (!string.IsNullOrWhiteSpace(this.FieldNameForLoopDateTime) && dateLoop != null)
				recordProperties[this.FieldNameForLoopDateTime] = "\"" + string.Format("{0:" + pelazem.util.Constants.FORMAT_DATETIME_UNIVERSAL + "}", dateLoop) + "\"";

			foreach (IFieldSpec fieldSpec in this.FieldSpecs)
			{
				fieldSpec.SetNextValue();

				string type = GetDataType(fieldSpec);

				if (type == FMT_NUMERIC)
					recordProperties[fieldSpec.Name] = fieldSpec.ValueString;
				else if (type == FMT_DATE)
					recordProperties[fieldSpec.Name] = "\"" + fieldSpec.ValueString + "\"";
				else
					recordProperties[fieldSpec.Name] = "\"" + fieldSpec.ValueString.Replace("\"", "\\\"") + "\"";
			}

			return record;
		}

		#region Utility

		private string GetHeaderRecord()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine("% 1. Title: '" + this.RecordSetName + "'");
			sb.AppendLine();

			sb.AppendLine(RELATION + this.RecordSetName);

			if (!string.IsNullOrWhiteSpace(this.FieldNameForLoopDateTime))
				sb.AppendLine(ATTRIB + this.FieldNameForLoopDateTime + FMT_DATE + pelazem.util.Constants.FORMAT_DATETIME_UNIVERSAL);

			foreach (IFieldSpec fieldSpec in this.FieldSpecs)
			{
				// We set a value so we can get fieldSpec type from it
				fieldSpec.SetNextValue();

				sb.AppendLine(ATTRIB + fieldSpec.Name + GetDataType(fieldSpec));
			}

			sb.AppendLine();

			sb.AppendLine(DATA);

			return sb.ToString();
		}

		private string GetDataType(IFieldSpec fieldSpec)
		{
			Type type = fieldSpec.Value.GetType();

			if (TypeUtil.IsNumeric(type))
				return FMT_NUMERIC;
			else if (type.Equals(TypeUtil.TypeDateTime) || type.Equals(TypeUtil.TypeDateTimeNullable))
				return FMT_DATE;
			else
				return FMT_STRING;
		}

		private string SerializeRecord(ExpandoObject record)
		{
			if (record is IDictionary<string, object> recordProperties)
				return recordProperties.Values.GetDelimitedList(DELIM, string.Empty);
			else
				return string.Empty;
		}

		#endregion
	}
}
