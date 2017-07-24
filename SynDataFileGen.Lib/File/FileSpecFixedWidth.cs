using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class FileSpecFixedWidth : FileSpecBase
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

		/// <summary>
		/// Character that will be used to pad fields in this file.
		/// Can be overridden at the field config level for individual fields, if needed.
		///  Only used for fixed-width files, ignored otherwise.
		///  If not specified, space is used.
		/// </summary>
		public char PaddingCharacter { get; private set; }

		/// <summary>
		/// Pad fields in this file at start (i.e. right-justify field) or at end (i.e. left-justify field).
		/// Can be overridden at the field config level for individual fields, if needed.
		/// Only used for fixed-width files, ignored otherwise.
		/// </summary>
		public Util.Location AddPadding { get; private set; } = Util.Location.AtStart;

		/// <summary>
		/// Truncate fields in this file, when exceeding field max length, at start (i.e. chop off from the left) or at end (i.e. chop off from the right).
		/// Can be overridden at the field config level for individual fields, if needed.
		/// Only used for fixed-width files, ignored otherwise.
		/// </summary>
		public Util.Location Truncate { get; private set; } = Util.Location.AtEnd;

		public Encoding Encoding { get; } = Encoding.UTF8;

		#endregion

		#region Constructors

		private FileSpecFixedWidth() { }

		public FileSpecFixedWidth(bool includeHeaderRow, string delimiter, string encloser, char paddingCharacter, Util.Location addPaddingAt, Util.Location truncateTooLongAt, List<IFieldSpec> fieldSpecs, Encoding encoding, int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec, string fieldNameForLoopDateTime)
			: base(recordsPerFileMin, recordsPerFileMax, pathSpec, fieldSpecs, fieldNameForLoopDateTime)
		{
			this.IncludeHeaderRecord = includeHeaderRow;
			this.Delimiter = delimiter;
			this.Encloser = encloser;
			this.PaddingCharacter = (paddingCharacter.ToString().Trim().Length == 1 ? paddingCharacter : ' ');
			this.AddPadding = addPaddingAt;
			this.Truncate = truncateTooLongAt;
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

		protected override ExpandoObject GetRecord(DateTime? dateLoop = null)
		{
			ExpandoObject record = new ExpandoObject();
			IDictionary<string, object> recordProperties = record as IDictionary<string, object>;

			// Loop date/time
			if (!string.IsNullOrWhiteSpace(this.FieldNameForLoopDateTime))
			{
				// 30 is a magic number; a full date/time in ISO format with timezone + a little padding
				int fieldSize = Math.Max(30, this.FieldNameForLoopDateTime.Length);

				if (dateLoop != null)
				{
					string raw = string.Format("{0:" + pelazem.util.Constants.FORMAT_DATETIME_UNIVERSAL + "}", dateLoop);

					switch (this.AddPadding)
					{
						case Util.Location.AtStart:
							recordProperties[this.FieldNameForLoopDateTime] = raw.PadLeft(fieldSize, this.PaddingCharacter);
							break;
						case Util.Location.AtEnd:
						default:
							recordProperties[this.FieldNameForLoopDateTime] = raw.PadRight(fieldSize, this.PaddingCharacter);
							break;
					}
				}
				else
					recordProperties[this.FieldNameForLoopDateTime] = " ".PadRight(fieldSize, this.PaddingCharacter);
			}

			// Actual field values
			foreach (IFieldSpec fieldSpec in this.FieldSpecs)
			{
				fieldSpec.SetNextValue();

				int fullFieldWidth = (fieldSpec.FixedWidthLength != null ? fieldSpec.FixedWidthLength.Value : fieldSpec.Name.Length);
				int fieldWidthNetEncloser = Math.Max(0, fullFieldWidth - (2 * this.Encloser.Length));

				string valueString = fieldSpec.ValueString;

				// Handle padding or truncation
				if (valueString.Length < fieldWidthNetEncloser)
				{
					char paddingChar = fieldSpec.FixedWidthPaddingChar ?? this.PaddingCharacter;

					Util.Location? padAt = fieldSpec.FixedWidthAddPadding ?? this.AddPadding;

					switch (padAt.Value)
					{
						case Util.Location.AtStart:
							// Edge Case
							// If we're left-padding with a non-whitespace character, and the value is a negative number: we need to move the - to the front so we get -000123.45 and not 000-123.45
							if (!char.IsWhiteSpace(paddingChar) && Converter.GetDouble(fieldSpec.Value) < 0)
								valueString = GetPaddedValueForNegativeNumber(valueString, paddingChar, fieldWidthNetEncloser);
							else
								valueString = valueString.PadLeft(fieldWidthNetEncloser, paddingChar);
							break;
						case Util.Location.AtEnd:
						default:
							valueString = valueString.PadRight(fieldWidthNetEncloser, paddingChar);
							break;
					}
				}
				else if (valueString.Length > fieldWidthNetEncloser)
				{
					Util.Location? truncateAt = fieldSpec.FixedWidthTruncate ?? this.Truncate;

					switch (truncateAt)
					{
						case Util.Location.AtStart:
							valueString = valueString.Substring(valueString.Length - fieldWidthNetEncloser);
							break;
						case Util.Location.AtEnd:
						default:
							valueString = valueString.Substring(0, fieldWidthNetEncloser);
							break;
					}
				}

				// Add to the output
				recordProperties[fieldSpec.Name] = valueString;
			}

			return record;
		}

		private string GetHeaderRecord()
		{
			List<string> result = new List<string>();

			// Loop date/time
			if (!string.IsNullOrWhiteSpace(this.FieldNameForLoopDateTime))
			{
				// 30 is a magic number; a full date/time in ISO format with timezone + a little padding
				int fieldSize = Math.Max(30, this.FieldNameForLoopDateTime.Length);

				if (this.FieldNameForLoopDateTime.Length < fieldSize)
				{
					switch (this.AddPadding)
					{
						case Util.Location.AtStart:
							result.Add(this.FieldNameForLoopDateTime.PadLeft(fieldSize, this.PaddingCharacter));
							break;
						case Util.Location.AtEnd:
						default:
							result.Add(this.FieldNameForLoopDateTime.PadRight(fieldSize, this.PaddingCharacter));
							break;
					}
				}
			}

			foreach (IFieldSpec fieldSpec in this.FieldSpecs)
			{
				string fieldName = this.Encloser + fieldSpec.Name + this.Encloser;

				int fieldWidth = (fieldSpec.FixedWidthLength != null ? fieldSpec.FixedWidthLength.Value : fieldName.Length);

				if (fieldName.Length == fieldWidth)
					result.Add(fieldName);
				else if (fieldName.Length < fieldWidth)
				{
					Util.Location? padAt = null;

					if (fieldSpec.FixedWidthAddPadding != null)
						padAt = fieldSpec.FixedWidthAddPadding.Value;
					else
						padAt = this.AddPadding;

					switch (padAt.Value)
					{
						case Util.Location.AtStart:
							result.Add(fieldName.PadLeft(fieldWidth, (fieldSpec.FixedWidthPaddingChar ?? this.PaddingCharacter)));
							break;
						case Util.Location.AtEnd:
						default:
							result.Add(fieldName.PadRight(fieldWidth, (fieldSpec.FixedWidthPaddingChar ?? this.PaddingCharacter)));
							break;
					}
				}
				else if (fieldName.Length > fieldWidth)
				{
					Util.Location? truncateAt = null;

					if (fieldSpec.FixedWidthTruncate != null)
						truncateAt = fieldSpec.FixedWidthTruncate.Value;
					else
						truncateAt = this.Truncate;

					switch (truncateAt)
					{
						case Util.Location.AtStart:
							result.Add(fieldName.Substring(fieldName.Length - fieldWidth));
							break;
						case Util.Location.AtEnd:
						default:
							result.Add(fieldName.Substring(0, fieldWidth));
							break;
					}
				}
			}


			return result.GetDelimitedList(this.Delimiter, string.Empty, true);
		}

		private string SerializeRecord(ExpandoObject record)
		{
			if (record is IDictionary<string, object> recordProperties)
				return recordProperties.Values.Select(v => this.Encloser + v.ToString() + this.Encloser).GetDelimitedList(this.Delimiter, string.Empty);
			else
				return string.Empty;
		}

		private string GetPaddedValueForNegativeNumber(string rawValue, char paddingChar, int finalLength)
		{
			NumberFormatInfo numberFormat = new CultureInfo(CultureInfo.CurrentCulture.Name).NumberFormat;
			int indexOfFirstCurrencyDecimalSeparator = rawValue.IndexOfAny(numberFormat.CurrencyDecimalSeparator.ToCharArray());
			int indexOfFirstNumberDecimalSeparator = rawValue.IndexOfAny(numberFormat.NumberDecimalSeparator.ToCharArray());
			int indexOfFirstPercentDecimalSeparator = rawValue.IndexOfAny(numberFormat.PercentDecimalSeparator.ToCharArray());
			int indexOfFirstDecimalSeparator = Math.Min(Math.Min(indexOfFirstCurrencyDecimalSeparator, indexOfFirstNumberDecimalSeparator), indexOfFirstPercentDecimalSeparator);

			int indexOfFirstDigit = Math.Max(0, rawValue.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }));

			int putPaddingHere;

			if (indexOfFirstDigit < 0 && indexOfFirstDecimalSeparator < 0)
				putPaddingHere = 0;
			else if (indexOfFirstDigit >= 0 && indexOfFirstDecimalSeparator < 0)
				putPaddingHere = indexOfFirstDigit;
			else if (indexOfFirstDigit < 0 && indexOfFirstDecimalSeparator >= 0)
				putPaddingHere = indexOfFirstDecimalSeparator;
			else
				putPaddingHere = Math.Min(indexOfFirstDigit, indexOfFirstDecimalSeparator);

			int numOfPads = finalLength - rawValue.Length;
			string pad = string.Empty.PadLeft(numOfPads, paddingChar);

			string final = rawValue.Substring(0, putPaddingHere) + pad + rawValue.Substring(putPaddingHere);

			return final;
		}
	}
}
