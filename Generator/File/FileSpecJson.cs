using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using pelazem.Common;
using Newtonsoft.Json;

namespace Generator
{
	public class FileSpecJson<T> : IFileSpec<T>
	{
		#region Properties

		public Encoding Encoding { get; } = Encoding.UTF8;

		#endregion

		#region Constructors

		private FileSpecJson() { }

		public FileSpecJson(Encoding encoding, int? recordsPerFileMin = null, int? recordsPerFileMax = null)
		{
			this.Encoding = encoding;
			this.RecordsPerFileMin = recordsPerFileMin;
			this.RecordsPerFileMax = recordsPerFileMax;
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
