using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace SynDataFileGen.Lib
{
	public class FileSpecJson<T> : FileSpecBase<T>
		where T : new()
	{
		#region Properties

		public Encoding Encoding { get; } = Encoding.UTF8;

		#endregion

		#region Constructors

		private FileSpecJson() { }

		public FileSpecJson(Encoding encoding, int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec)
			: base(recordsPerFileMin, recordsPerFileMax, pathSpec)
		{
			this.Encoding = encoding;
		}

		public FileSpecJson(Encoding encoding, int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec, string propertyNameForLoopDateTime, DateTime? dateStart, DateTime? dateEnd)
			: base(recordsPerFileMin, recordsPerFileMax, pathSpec, propertyNameForLoopDateTime, dateStart, dateEnd)
		{
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
						JsonSerializerSettings settings = new JsonSerializerSettings();
						settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
						settings.Formatting = Formatting.Indented;

						sw.Write(JsonConvert.SerializeObject(items, settings));

						sw.Flush();

						interim.Seek(0, SeekOrigin.Begin);

						interim.CopyTo(result);
					}
				}
			}

			return result;
		}

		#endregion
	}
}
