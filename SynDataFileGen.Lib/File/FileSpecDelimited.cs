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

		public FileSpecDelimited(bool includeHeaderRow, string delimiter, string encloser, List<IFieldSpec> fieldSpecs, Encoding encoding, int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec, string fieldNameForLoopDateTime)
			: base(recordsPerFileMin, recordsPerFileMax, pathSpec, fieldSpecs, fieldNameForLoopDateTime)
		{
			this.IncludeHeaderRecord = includeHeaderRow;
			this.Delimiter = delimiter;
			this.Encloser = encloser;
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
					if (this.IncludeHeaderRecord)
						sw.WriteLine(GetHeaderRecord());

					foreach (var record in records)
						sw.WriteLine(SerializeRecord(record));

					sw.Flush();

					interim.Seek(0, SeekOrigin.Begin);

					interim.CopyTo(result);
				}
			}

			return result;
		}

		#region Utility

		private string GetHeaderRecord()
		{
			List<string> fieldNames = new List<string>();

			if (!string.IsNullOrWhiteSpace(this.FieldNameForLoopDateTime))
				fieldNames.Add(this.FieldNameForLoopDateTime);

			fieldNames.AddRange(this.FieldSpecs.Select(f => f.Name));

			return fieldNames.Select(fn => this.Encloser + fn + this.Encloser).GetDelimitedList(this.Delimiter, string.Empty);
		}

		private string SerializeRecord(ExpandoObject record)
		{
			if (record is IDictionary<string, object> recordProperties)
				return recordProperties.Values.Select(v => this.Encloser + v.ToString() + this.Encloser).GetDelimitedList(this.Delimiter, string.Empty);
			else
				return string.Empty;
		}

		#endregion
	}
}
