using System;
using System.Collections.Generic;
using System.Text;

namespace SynDataFileGen.Lib
{
	public class FileSpecConfig
	{
		private string _fileType = string.Empty;
		private string _encodingName = string.Empty;

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

		/// <summary>
		/// Output path specification relative to Generator.OutputFolderRoot.
		/// For date looping, this path should contain any of the following tokens: hh, dd, mm, yy, yyyy
		/// Example: "{yyyy}\{mm}\{dd}\{hh}.txt"
		/// Example with repeated tokens (also valid): {yyyy}\{yyyy}_{mm}_{dd}_{hh}.txt
		/// Can also be an explicit path and file name if no date looping will be used.
		/// Example: c:\temp\output\myFile.txt
		/// </summary>
		public string PathSpec { get; set; }

		/// <summary>
		/// For looping date/time file series, if the loop date/time should also be written into the output files, specify the field name to which to write the loop date/time.
		/// If a non-existent field name is specified, the loop date/time will not be written to the output files.
		/// </summary>
		public string FieldNameForLoopDateTime { get; set; }

		/// <summary>
		/// Specify a valid date less than DateEnd to get date looping output. Leave null to output a single file.
		/// </summary>
		public DateTime? DateStart { get; set; }

		/// <summary>
		/// Specify a valid date greater than DateStart to get date looping output. Leave null to output a single file.
		/// </summary>
		public DateTime? DateEnd { get; set; }

		/// <summary>
		/// Specify whether to include a header row with field names. Only used for delimited or fixed-width file types; ignored otherwise.
		/// Note that if field name lengths exceed field max lengths (in fixed-width files), field names may be truncated.
		/// </summary>
		public bool IncludeHeaderRow { get; set; }

		/// <summary>
		/// String value to separate fields (columns) in fixed-width or delimited files.
		/// If omitted, fields will be immediately adjacent. This property will be ignored for file formats other than fixed-width or delimited.
		/// </summary>
		public string Delimiter { get; set; }

		/// <summary>
		/// String value to enclose fields (columns) in fixed-width or delimited files.
		/// This property will be ignored for file formats other than fixed-width or delimited.
		/// </summary>
		public string Encloser { get; set; }

		/// <summary>
		/// Valid values: ASCII, UTF8, UTF32. Anything else will be ignored and UTF8 will be used by default.
		/// Only used for text files. Ignored for non-text (binary) files.
		/// </summary>
		public string EncodingName
		{
			get { return _encodingName; }
			set
			{
				if (ConfigValues.ValidEncodingNames.Contains(value.ToLowerInvariant()))
					_encodingName = value;
				else
					_encodingName = ConfigValues.ENCODING_UTF8;

				this.Encoding = Util.GetEncoding(_encodingName);
			}
		}
		internal Encoding Encoding { get; set; }

		/// <summary>
		/// Character that will be used to pad fields in this file.
		/// Can be overridden at the field config level for individual fields, if needed.
		///  Only used for fixed-width files, ignored otherwise.
		///  If not specified, space is used.
		/// </summary>
		public char? FixedWidthPaddingChar { get; set; } = null;

		/// <summary>
		/// Pad fields in this file at start (i.e. right-justify field) or at end (i.e. left-justify field).
		/// Valid values: AtStart, AtEnd
		/// Can be overridden at the field config level for individual fields, if needed.
		/// Only used for fixed-width files, ignored otherwise.
		/// </summary>
		public string FixedWidthAddPadding { get; set; }

		/// <summary>
		/// Truncate fields in this file, when exceeding field max length, at start (i.e. chop off from the left) or at end (i.e. chop off from the right).
		/// Valid values: AtStart, AtEnd
		/// Can be overridden at the field config level for individual fields, if needed.
		/// Only used for fixed-width files, ignored otherwise.
		/// </summary>
		public string FixedWidthTruncate { get; set; }

		public List<FieldSpecConfig> FieldSpecs { get; } = new List<FieldSpecConfig>();
	}
}
