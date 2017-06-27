using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using pelazem.Common;

namespace Generator
{
	public class FileSpecDelimited<T> : IFileSpec<T>
	{
		#region Properties

		public bool IncludeHeaderRow { get; private set; }

		public string Delimiter { get; private set; }

		public string Encloser { get; private set; }

		public List<IFieldSpec<T>> FieldSpecs { get; private set; }

		public Encoding Encoding { get; } = Encoding.UTF8;

		#endregion

		#region Constructors

		private FileSpecDelimited() { }

		public FileSpecDelimited(bool includeHeaderRow, string delimiter, string encloser, List<IFieldSpec<T>> fieldSpecs, Encoding encoding, int? recordsPerFileMin = null, int? recordsPerFileMax = null)
		{
			this.IncludeHeaderRow = includeHeaderRow;
			this.Delimiter = delimiter;
			this.Encloser = encloser;
			this.Encoding = encoding;
			this.RecordsPerFileMin = recordsPerFileMin;
			this.RecordsPerFileMax = recordsPerFileMax;
			this.FieldSpecs = fieldSpecs;
		}

		#endregion

		#region IFileSpec implementation

		public int? RecordsPerFileMin { get; private set; }

		public int? RecordsPerFileMax { get; private set; }

		public Stream GetFileContent(List<T> items)
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
