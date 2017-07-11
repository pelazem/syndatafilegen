using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class FileSpecDelimited : FileSpecBase
	{
		#region Properties

		/// <summary>
		/// Specify whether to include a header row with field names. Only used for delimited or fixed-width file types; ignored otherwise.
		/// Note that if field name lengths exceed field max lengths (in fixed-width files), field names may be truncated.
		/// </summary>
		public bool IncludeHeaderRecord { get; private set; }

		/// <summary>
		/// String value to separate fields (columns) in fixed-width or delimited files.
		/// If omitted, fields will be immediately adjacent. This property will be ignored for file formats other than fixed-width or delimited.
		/// </summary>
		public string Delimiter { get; private set; }

		/// <summary>
		/// String value to enclose fields (columns) in fixed-width or delimited files.
		/// This property will be ignored for file formats other than fixed-width or delimited.
		/// </summary>
		public string Encloser { get; private set; }

		public Encoding Encoding { get; } = Encoding.UTF8;

		#endregion

		#region Constructors

		private FileSpecDelimited() { }

		public FileSpecDelimited(bool includeHeaderRow, string delimiter, string encloser, List<IFieldSpec> fieldSpecs, Encoding encoding, int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec)
			: base(recordsPerFileMin, recordsPerFileMax, pathSpec, fieldSpecs)
		{
			this.IncludeHeaderRecord = includeHeaderRow;
			this.Delimiter = delimiter;
			this.Encloser = encloser;
			this.Encoding = encoding;
		}

		public FileSpecDelimited(bool includeHeaderRow, string delimiter, string encloser, List<IFieldSpec> fieldSpecs, Encoding encoding, int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec, string fieldNameForLoopDateTime, DateTime? dateStart, DateTime? dateEnd)
			: base(recordsPerFileMin, recordsPerFileMax, pathSpec, fieldNameForLoopDateTime, dateStart, dateEnd, fieldSpecs)
		{
			this.IncludeHeaderRecord = includeHeaderRow;
			this.Delimiter = delimiter;
			this.Encloser = encloser;
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
					if (this.IncludeHeaderRecord)
						sw.WriteLine(GetHeaderRecord());

					for (int i = 1; i <= numOfItems; i++)
					{
						var record = GetRecord(dateLoop);

						this.Results.Add(record);

						sw.WriteLine(SerializeRecord(record));
					}

					sw.Flush();

					interim.Seek(0, SeekOrigin.Begin);

					interim.CopyTo(result);
				}
			}

			return result;
		}

		#endregion

		#region Utility

		private string GetHeaderRecord()
		{
			List<string> fieldNames = new List<string>();

			if (!string.IsNullOrWhiteSpace(this.FieldNameForLoopDateTime))
				fieldNames.Add(this.FieldNameForLoopDateTime);

			fieldNames.AddRange(this.FieldSpecs.Select(f => f.Name));

			return fieldNames.Select(fn => this.Encloser + fn + this.Encloser).GetDelimitedList(this.Delimiter, string.Empty);
		}

		private dynamic GetRecord(DateTime? dateLoop = null)
		{
			dynamic record = new ExpandoObject();
			IDictionary<string, object> recordProperties = record as IDictionary<string, object>;

			if (!string.IsNullOrWhiteSpace(this.FieldNameForLoopDateTime) && dateLoop != null)
				recordProperties[this.FieldNameForLoopDateTime] = string.Format("{0:" + pelazem.util.Constants.FORMAT_DATETIME_UNIVERSAL + "}", dateLoop);

			foreach (IFieldSpec fieldSpec in this.FieldSpecs)
				recordProperties[fieldSpec.Name] = fieldSpec.Value;

			return record;
		}

		private string SerializeRecord(ExpandoObject record)
		{
			IDictionary<string, object> recordProperties = record as IDictionary<string, object>;

			if (recordProperties != null)
				return recordProperties.Values.Select(v => this.Encloser + v.ToString() + this.Encloser).GetDelimitedList(this.Delimiter, string.Empty);
			else
				return string.Empty;
		}

		#endregion
	}
}
