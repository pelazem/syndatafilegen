using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class FileSpecJson : FileSpecBase
	{
		#region Properties

		public Encoding Encoding { get; } = Encoding.UTF8;

		#endregion

		#region Constructors

		private FileSpecJson() { }

		public FileSpecJson(List<IFieldSpec> fieldSpecs, Encoding encoding, int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec)
			: base(recordsPerFileMin, recordsPerFileMax, pathSpec, fieldSpecs)
		{
			this.Encoding = encoding;
		}

		public FileSpecJson(List<IFieldSpec> fieldSpecs, Encoding encoding, int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec, string fieldNameForLoopDateTime, DateTime? dateStart, DateTime? dateEnd)
			: base(recordsPerFileMin, recordsPerFileMax, pathSpec, fieldNameForLoopDateTime, dateStart, dateEnd, fieldSpecs)
		{
			this.Encoding = encoding;
		}

		#endregion

		#region IFileSpec implementation

		public override Stream GetFileContent(DateTime? dateLoop = null)
		{
			int numOfItems = Converter.GetInt32(RNG.GetUniform(this.RecordsPerFileMin ?? 0, this.RecordsPerFileMax ?? 0));

			var result = new MemoryStream();

			using (var interim = new MemoryStream())
			{
				using (var sw = new StreamWriter(interim, this.Encoding))
				{
					JArray records = new JArray();

					for (int i = 1; i <= numOfItems; i++)
					{
						JObject jsonRecord = new JObject();		// JSON output
						dynamic record = new ExpandoObject();   // Add to .Results list
						IDictionary<string, object> recordProperties = record as IDictionary<string, object>;

						if (!string.IsNullOrWhiteSpace(this.FieldNameForLoopDateTime) && dateLoop != null)
						{
							string dateString = string.Format("{0:" + pelazem.util.Constants.FORMAT_DATETIME_UNIVERSAL + "}", dateLoop);
							jsonRecord.Add(this.FieldNameForLoopDateTime, dateString);
							recordProperties[this.FieldNameForLoopDateTime] = dateString;
						}

						foreach (var fieldSpec in this.FieldSpecs)
						{
							object value = fieldSpec.Value;
							jsonRecord.Add(fieldSpec.Name, fieldSpec.Value.ToString());
							recordProperties[fieldSpec.Name] = value;
						}

						records.Add(jsonRecord);
						this.Results.Add(record);
					}

					sw.Write(records.ToString());

					sw.Flush();

					interim.Seek(0, SeekOrigin.Begin);

					interim.CopyTo(result);
				}
			}

			return result;
		}

		#endregion
	}
}
