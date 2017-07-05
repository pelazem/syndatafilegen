using System;
using System.Collections.Generic;
using System.Text;

namespace SynDataFileGen.Lib
{
	public class FileSpecConfig
	{
		/// <summary>
		/// Valid values: Avro, Delimited, FixedWidth, Json
		/// Other values will be ignored and replaced by Json.
		/// </summary>
		public string FileTypeName { get; set; }

		public int RecordsPerFileMin { get; set; }
		public int RecordsPerFileMax { get; set; }

		/// <summary>
		/// Output path specification relative to Generator.OutputFolderRoot.
		/// For date looping, this path should contain any of the following tokens: hh, dd, mm, yy, yyyy
		/// Example: "{yyyy}\{mm}\{dd}\{hh}.txt"
		/// Example with repeated tokens (also valid): {yyyy}\{yyyy}_{mm}_{dd}_{hh}.txt
		/// </summary>
		public string PathSpec { get; set; }

		/// <summary>
		/// For looping date/time file series, if the loop date/time should also be written into the output files, specify the field name to which to write the loop date/time.
		/// If a non-existent field name is specified, the loop date/time will not be written to the output files.
		/// </summary>
		public string PropertyNameForLoopDateTime { get; set; }

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
		/// Valid values: ASCII, UTF8, UTF16, UTF32
		/// Only used for text files. Ignored for non-text (binary) files.
		/// </summary>
		public string EncodingName
		{
			get { return this.Encoding.EncodingName ?? string.Empty; }
			set { this.Encoding = Util.GetEncoding(value); }
		}
		internal Encoding Encoding { get; set; }

		/// <summary>
		/// Character that will be used to pad fields in this file.
		/// Can be overridden at the field config level for individual fields, if needed.
		///  Only used for fixed-width files, ignored otherwise.
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
