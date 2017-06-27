using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using pelazem.Common;

namespace Generator
{
	public class FileSpecDelimited<T> : FileSpecBase<T>
		where T : new()
	{
		#region Properties

		public bool IncludeHeaderRow { get; private set; }

		public string Delimiter { get; private set; }

		public string Encloser { get; private set; }

		public Encoding Encoding { get; } = Encoding.UTF8;

		#endregion

		#region Constructors

		private FileSpecDelimited() { }

		public FileSpecDelimited(bool includeHeaderRow, string delimiter, string encloser, List<IFieldSpec<T>> fieldSpecs, Encoding encoding, int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec)
			: base(recordsPerFileMin, recordsPerFileMax, pathSpec, fieldSpecs)
		{
			this.IncludeHeaderRow = includeHeaderRow;
			this.Delimiter = delimiter;
			this.Encloser = encloser;
			this.Encoding = encoding;
		}

		public FileSpecDelimited(bool includeHeaderRow, string delimiter, string encloser, List<IFieldSpec<T>> fieldSpecs, Encoding encoding, int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec, string propertyNameForLoopDateTime, DateTime? dateStart, DateTime? dateEnd)
			: base(recordsPerFileMin, recordsPerFileMax, pathSpec, propertyNameForLoopDateTime, dateStart, dateEnd, fieldSpecs)
		{
			this.IncludeHeaderRow = includeHeaderRow;
			this.Delimiter = delimiter;
			this.Encloser = encloser;
			this.Encoding = encoding;
		}

		#endregion

		#region IFileSpec implementation

		public override Stream GetFileContent(List<T> items)
		{
			var result = new MemoryStream();

			using (var interim = new MemoryStream())
			{
				using (var sw = new StreamWriter(interim, this.Encoding))
				{
					if (items != null && items.Count > 0)
					{
						if (this.IncludeHeaderRow)
							sw.WriteLine(GetHeaderRecord());

						foreach (T item in items)
							sw.WriteLine(GetRecord(item));

						sw.Flush();

						interim.Seek(0, SeekOrigin.Begin);

						interim.CopyTo(result);
					}
				}
			}

			return result;
		}

		#endregion

		#region Utility

		private string GetHeaderRecord()
		{
			return TypeHelper.GetPrimitiveProps(typeof(T)).Select(p => this.Encloser + p.Name + this.Encloser).GetDelimitedList(this.Delimiter, string.Empty);
		}

		private string GetRecord(T item)
		{
			List<string> result = new List<string>();

			foreach (IFieldSpec<T> fieldSpec in this.FieldSpecs)
			{
				object rawValue = fieldSpec.Prop.GetValueEx(item);
				string value;

				if (rawValue == null)
					value = this.Encloser + this.Encloser;
				else if (string.IsNullOrWhiteSpace(fieldSpec.FormatString))
					value = this.Encloser + rawValue.ToString() + this.Encloser;
				else
					value = this.Encloser + string.Format(fieldSpec.FormatString, rawValue) + this.Encloser;

				result.Add(value);
			}

			return result.GetDelimitedList(this.Delimiter, string.Empty, true);
		}

		#endregion
	}
}
