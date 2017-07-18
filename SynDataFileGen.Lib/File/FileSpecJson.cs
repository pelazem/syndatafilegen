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

		public FileSpecJson(List<IFieldSpec> fieldSpecs, Encoding encoding, int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec, string fieldNameForLoopDateTime, DateTime? dateStart, DateTime? dateEnd)
			: base(recordsPerFileMin, recordsPerFileMax, pathSpec, fieldSpecs, fieldNameForLoopDateTime, dateStart, dateEnd)
		{
			this.Encoding = encoding;
		}

		#endregion

		public override Stream GetContentStream(List<ExpandoObject> records)
		{
			int numOfItems = Converter.GetInt32(RNG.GetUniform(this.RecordsPerFileMin ?? 0, this.RecordsPerFileMax ?? 0));

			var result = new MemoryStream();

			using (var interim = new MemoryStream())
			{
				using (var sw = new StreamWriter(interim, this.Encoding))
				{
					JArray jsonRecords = new JArray();

					foreach (var record in records)
					{
						IDictionary<string, object> recordProperties = record as IDictionary<string, object>;
						JObject jsonRecord = new JObject();     // JSON output

						foreach (KeyValuePair<string, object> recordKVP in recordProperties)
							jsonRecord.Add(recordKVP.Key, recordKVP.Value.ToString());

						jsonRecords.Add(jsonRecord);
					}

					sw.Write(jsonRecords.ToString());

					sw.Flush();

					interim.Seek(0, SeekOrigin.Begin);

					interim.CopyTo(result);
				}
			}

			return result;
		}
	}
}
