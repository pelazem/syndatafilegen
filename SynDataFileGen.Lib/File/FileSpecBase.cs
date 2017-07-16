using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public abstract class FileSpecBase : IFileSpec
	{
		#region Variables

		private DateTime? _dateStart;
		private DateTime? _dateEnd;

		#endregion

		#region IFileSpec implementation

		public int? RecordsPerFileMin { get; protected set; }

		public int? RecordsPerFileMax { get; protected set; }

		/// <summary>
		/// Output path specification relative to Generator.OutputFolderRoot.
		/// For date looping, this path should contain any of the following tokens: hh, dd, mm, yy, yyyy
		/// Example: "{yyyy}\{mm}\{dd}\{hh}.txt"
		/// Example with repeated tokens (also valid): {yyyy}\{yyyy}_{mm}_{dd}_{hh}.txt
		/// Can also be an explicit path and file name if no date looping will be used.
		/// Example: c:\temp\output\myFile.txt
		/// </summary>
		public string PathSpec { get; protected set; }


		public bool HasDateLooping
		{
			get
			{
				bool pathSpecHasDateLooping =
				(
					this.PathSpec.Contains(Constants.YYYY) ||
					this.PathSpec.Contains(Constants.YY) ||
					this.PathSpec.Contains(Constants.MM) ||
					this.PathSpec.Contains(Constants.DD) ||
					this.PathSpec.Contains(Constants.HH)
				);

				bool dateLoopingDatesSpecified = (this.DateStart != null && this.DateEnd != null && this.DateStart.Value <= this.DateEnd.Value);

				return pathSpecHasDateLooping && dateLoopingDatesSpecified;
			}
		}

		/// <summary>
		/// For looping date/time file series, if the loop date/time should also be written into the output files, specify the field name to which to write the loop date/time.
		/// If a non-existent field name is specified, the loop date/time will not be written to the output files.
		/// </summary>
		public string FieldNameForLoopDateTime { get; protected set; }

		/// <summary>
		/// Specify a valid date less than DateEnd to get date looping output. Leave null to output a single file.
		/// </summary>
		public DateTime? DateStart
		{
			get { return _dateStart; }
			protected set
			{
				if (value == null)
					_dateStart = value;
				else
				{
					if (value.Value.Kind == DateTimeKind.Utc)
						_dateStart = value;
					else
						_dateStart = value.Value.ToUniversalTime();
				}
			}
		}

		/// <summary>
		/// Specify a valid date greater than DateStart to get date looping output. Leave null to output a single file.
		/// </summary>
		public DateTime? DateEnd
		{
			get { return _dateEnd; }
			protected set
			{
				if (value == null)
					_dateEnd = value;
				else
				{
					if (value.Value.Kind == DateTimeKind.Utc)
						_dateEnd = value;
					else
						_dateEnd = value.Value.ToUniversalTime();
				}
			}
		}

		public List<IFieldSpec> FieldSpecs { get; } = new List<IFieldSpec>();

		public List<ExpandoObject> GenerateFile(string fullFilePath, DateTime? dateLoop = null)
		{
			int numOfItems = Converter.GetInt32(RNG.GetUniform(this.RecordsPerFileMin ?? 0, this.RecordsPerFileMax ?? 0));

			List<ExpandoObject> result = new List<ExpandoObject>(numOfItems);

			for (int i = 1; i <= numOfItems; i++)
				result.Add(GetRecord(dateLoop));

			WriteFile(fullFilePath, GetFileContent(result));

			return result;
		}

		#endregion

		#region Constructors

		protected FileSpecBase() { }

		public FileSpecBase(int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec, IEnumerable<IFieldSpec> fieldSpecs = null)
		{
			this.RecordsPerFileMin = recordsPerFileMin;
			this.RecordsPerFileMax = recordsPerFileMax;
			this.PathSpec = pathSpec.Replace(@"/", @"\");

			this.FieldSpecs.AddRange(fieldSpecs);
		}

		public FileSpecBase(int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec, string fieldNameForLoopDateTime, DateTime? dateStart, DateTime? dateEnd, IEnumerable<IFieldSpec> fieldSpecs = null)
		{
			this.RecordsPerFileMin = recordsPerFileMin;
			this.RecordsPerFileMax = recordsPerFileMax;
			this.PathSpec = pathSpec.Replace(@"/", @"\");

			this.FieldNameForLoopDateTime = fieldNameForLoopDateTime;
			this.DateStart = dateStart;
			this.DateEnd = dateEnd;

			this.FieldSpecs.AddRange(fieldSpecs);
		}

		#endregion

		protected virtual dynamic GetRecord(DateTime? dateLoop = null)
		{
			dynamic record = new ExpandoObject();

			IDictionary<string, object> recordProperties = record as IDictionary<string, object>;

			if (!string.IsNullOrWhiteSpace(this.FieldNameForLoopDateTime) && dateLoop != null)
				recordProperties[this.FieldNameForLoopDateTime] = string.Format("{0:" + pelazem.util.Constants.FORMAT_DATETIME_UNIVERSAL + "}", dateLoop);

			foreach (IFieldSpec fieldSpec in this.FieldSpecs)
				recordProperties[fieldSpec.Name] = fieldSpec.Value;

			return record;
		}

		protected abstract Stream GetFileContent(List<ExpandoObject> records);

		protected virtual void WriteFile(string fullFilePath, Stream contents)
		{
			if (File.Exists(fullFilePath))
				File.Delete(fullFilePath);

			string fullFolderPath = Path.GetDirectoryName(fullFilePath);

			if (!Directory.Exists(fullFolderPath))
				Directory.CreateDirectory(fullFolderPath);

			try
			{
				using (FileStream fs = File.Create(fullFilePath))
				{
					contents.Seek(0, SeekOrigin.Begin);
					contents.CopyTo(fs);
				}
			}
			catch (Exception ex)
			{
			}
		}
	}
}
