using System;
using System.Collections.Generic;
using System.Text;

namespace SynDataFileGen.Lib
{
	public class FileSpecConfig
	{
		private string _fileType = string.Empty;

		/// <summary>
		/// Valid values: Avro, Delimited, FixedWidth, Json
		/// Other values will be ignored and replaced by Json.
		/// </summary>
		public string FileType
		{
			get { return _fileType; }
			set
			{
				if (ConfigValues.ValidFileTypes.Contains(value.ToLowerInvariant()))
					_fileType = value;
				else
					_fileType = ConfigValues.FILETYPE_JSON;
			}
		}

		public int RecordsPerFileMin { get; set; }
		public int RecordsPerFileMax { get; set; }

		public string PathSpec { get; set; }

		public string FieldNameForLoopDateTime { get; set; }

		public bool IncludeHeaderRow { get; set; }

		public string Delimiter { get; set; }

		public string Encloser { get; set; }

		public string EncodingName { get; set; }

		public char? FixedWidthPaddingChar { get; set; } = null;

		public string FixedWidthAddPadding { get; set; }

		public string FixedWidthTruncate { get; set; }

		public List<FieldSpecConfig> FieldSpecs { get; } = new List<FieldSpecConfig>();
	}
}
