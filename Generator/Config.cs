using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Generator
{
	public class GeneratorConfig
	{
		public bool IncludeHeaderRow { get; set; }
		public string Delimiter { get; set; }
		public string Encloser { get; set; }
		public char PaddingCharacter { get; set; }
		public Util.Location PadAt { get; set; }
		public Util.Location TruncateAt { get; set; }
		public string OutputFolderRoot { get; set; }
		public string PathSpec { get; set; }
		public int RecordsPerFileMin { get; set; }
		public int RecordsPerFileMax { get; set; }
		public DateTime? DateStart { get; set; }
		public DateTime? DateEnd { get; set; }



		private string _encodingName = string.Empty;

		public string EncodingName
		{
			get { return _encodingName; }
			set
			{
				_encodingName = value;

				this.Encoding = GetEncoding(_encodingName);
			}
		}

		public Encoding Encoding { get; set; }

		private Encoding GetEncoding(string value)
		{
			Encoding result;

			if (!string.IsNullOrWhiteSpace(value))
			{
				switch (value)
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
	}
}
