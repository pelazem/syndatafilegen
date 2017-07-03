using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class FileSpecFixedWidth<T> : FileSpecBase<T>
		where T : new()
	{
		#region Properties

		public bool IncludeHeaderRow { get; private set; }

		public string Delimiter { get; private set; }

		public string Encloser { get; private set; }

		public char PaddingCharacter { get; private set; }

		/// <summary>
		/// Pad a value that is shorter than Field length at the start or end. Defaults to end.
		/// </summary>
		public Util.Location AddPaddingAt { get; private set; }

		/// <summary>
		/// Truncate a value that is longer than Field length at the start or end. Defaults to end.
		/// </summary>
		public Util.Location TruncateTooLongAt { get; private set; }

		public Encoding Encoding { get; } = Encoding.UTF8;

		#endregion

		#region Constructors

		private FileSpecFixedWidth() { }

		public FileSpecFixedWidth(bool includeHeaderRow, string delimiter, string encloser, char paddingCharacter, Util.Location addPaddingAt, Util.Location truncateTooLongAt, List<IFieldSpec<T>> fieldSpecs, Encoding encoding, int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec)
			: base(recordsPerFileMin, recordsPerFileMax, pathSpec, fieldSpecs)
		{
			this.IncludeHeaderRow = includeHeaderRow;
			this.Delimiter = delimiter;
			this.Encloser = encloser;
			this.PaddingCharacter = (paddingCharacter.ToString().Trim().Length == 1 ? paddingCharacter : ' ');
			this.AddPaddingAt = addPaddingAt;
			this.TruncateTooLongAt = truncateTooLongAt;
			this.Encoding = encoding;
		}

		public FileSpecFixedWidth(bool includeHeaderRow, string delimiter, string encloser, char paddingCharacter, Util.Location addPaddingAt, Util.Location truncateTooLongAt, List<IFieldSpec<T>> fieldSpecs, Encoding encoding, int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec, string propertyNameForLoopDateTime, DateTime? dateStart, DateTime? dateEnd)
			: base(recordsPerFileMin, recordsPerFileMax, pathSpec, propertyNameForLoopDateTime, dateStart, dateEnd, fieldSpecs)
		{
			this.IncludeHeaderRow = includeHeaderRow;
			this.Delimiter = delimiter;
			this.Encloser = encloser;
			this.PaddingCharacter = (paddingCharacter.ToString().Trim().Length == 1 ? paddingCharacter : ' ');
			this.AddPaddingAt = addPaddingAt;
			this.TruncateTooLongAt = truncateTooLongAt;
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
							sw.WriteLine(GetHeaderRecord(items));

						foreach (T item in items)
							sw.WriteLine(GetRecord(item, items));

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

		private string GetHeaderRecord(List<T> items)
		{
			List<string> result = new List<string>();

			foreach (IFieldSpec<T> fieldSpec in this.FieldSpecs)
			{
				string propName = this.Encloser + fieldSpec.Prop.Name + this.Encloser;

				int fieldWidth = (fieldSpec.FixedWidthLength != null ? fieldSpec.FixedWidthLength.Value : Math.Max(propName.Length, items.Take(Math.Min(items.Count, 1000)).Select(i => fieldSpec.Prop.GetValueEx(i).ToString().Length).Max()));

				if (propName.Length == fieldWidth)
					result.Add(propName);
				else if (propName.Length < fieldWidth)
				{
					Util.Location? padAt = null;

					if (fieldSpec.FixedWidthAddPadding != null)
						padAt = fieldSpec.FixedWidthAddPadding.Value;
					else
						padAt = this.AddPaddingAt;

					switch (padAt.Value)
					{
						case Util.Location.AtStart:
							result.Add(propName.PadLeft(fieldWidth, (fieldSpec.FixedWidthPaddingChar ?? this.PaddingCharacter)));
							break;
						case Util.Location.AtEnd:
						default:
							result.Add(propName.PadRight(fieldWidth, (fieldSpec.FixedWidthPaddingChar ?? this.PaddingCharacter)));
							break;
					}
				}
				else if (propName.Length > fieldWidth)
				{
					Util.Location? truncateAt = null;

					if (fieldSpec.FixedWidthTruncate != null)
						truncateAt = fieldSpec.FixedWidthTruncate.Value;
					else
						truncateAt = this.TruncateTooLongAt;

					switch (truncateAt)
					{
						case Util.Location.AtStart:
							result.Add(propName.Substring(propName.Length - fieldWidth));
							break;
						case Util.Location.AtEnd:
						default:
							result.Add(propName.Substring(0, fieldWidth));
							break;
					}
				}
			}

			return result.GetDelimitedList(this.Delimiter, string.Empty, true);
		}

		private string GetRecord(T item, List<T> items)
		{
			List<string> result = new List<string>();

			foreach (IFieldSpec<T> fieldSpec in this.FieldSpecs)
			{
				int fullFieldWidth = (fieldSpec.FixedWidthLength != null ? fieldSpec.FixedWidthLength.Value : Math.Max((this.IncludeHeaderRow ? fieldSpec.Prop.Name.Length : 0), items.Take(Math.Min(items.Count, 1000)).Select(i => fieldSpec.Prop.GetValueEx(i).ToString().Length).Max()));
				int fieldWidthNetEncloser = Math.Max(0, fullFieldWidth - (2 * this.Encloser.Length));

				object rawValue = fieldSpec.Prop.GetValueEx(item);


				// First we get a formatted string
				string finalValue = GetString(rawValue, fieldSpec.FormatString);


				// Second we handle padding or truncation
				if (finalValue.Length < fieldWidthNetEncloser)
				{
					char paddingChar = fieldSpec.FixedWidthPaddingChar ?? this.PaddingCharacter;

					Util.Location? padAt = fieldSpec.FixedWidthAddPadding ?? this.AddPaddingAt;

					switch (padAt.Value)
					{
						case Util.Location.AtStart:
							// Edge Case
							// If we're left-padding with a non-whitespace character, and the value is a negative number: we need to move the - to the front so we get -000123.45 and not 000-123.45
							if (!char.IsWhiteSpace(paddingChar) && TypeUtil.IsNumeric(fieldSpec.Prop) && Converter.GetDouble(rawValue) < 0)
								finalValue = GetPaddedValueForNegativeNumber(finalValue, paddingChar, fieldWidthNetEncloser);
							else
								finalValue = finalValue.PadLeft(fieldWidthNetEncloser, paddingChar);
							break;
						case Util.Location.AtEnd:
						default:
							finalValue = finalValue.PadRight(fieldWidthNetEncloser, paddingChar);
							break;
					}
				}
				else if (finalValue.Length > fieldWidthNetEncloser)
				{
					Util.Location? truncateAt = fieldSpec.FixedWidthTruncate ?? this.TruncateTooLongAt;

					switch (truncateAt)
					{
						case Util.Location.AtStart:
							finalValue = finalValue.Substring(finalValue.Length - fieldWidthNetEncloser);
							break;
						case Util.Location.AtEnd:
						default:
							finalValue = finalValue.Substring(0, fieldWidthNetEncloser);
							break;
					}
				}

				// Last, we prepend and append encloser, if specified
				if (!string.IsNullOrWhiteSpace(this.Encloser))
					finalValue = this.Encloser + finalValue + this.Encloser;

				// Add to the output
				result.Add(finalValue);
			}

			return result.GetDelimitedList(this.Delimiter, string.Empty, true);
		}

		private string GetString(object rawValue, string formatString)
		{
			string result;

			if (rawValue != null)
			{
				if (string.IsNullOrWhiteSpace(formatString))
					result = rawValue.ToString();
				else
					result = string.Format(formatString, rawValue);
			}
			else
				result = string.Empty;

			return result;
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

		#endregion
	}
}
