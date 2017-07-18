using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public abstract class FileSpecBase : IFileSpec
	{
		#region Variables

		private DateTime? _dateStart;
		private DateTime? _dateEnd;

		#endregion

		#region Constructors

		protected FileSpecBase() { }

		public FileSpecBase(int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec, IEnumerable<IFieldSpec> fieldSpecs, string fieldNameForLoopDateTime, DateTime? dateStart, DateTime? dateEnd)
		{
			this.RecordsPerFileMin = recordsPerFileMin;
			this.RecordsPerFileMax = recordsPerFileMax;
			this.PathSpec = pathSpec.Replace(@"/", @"\");

			this.FieldSpecs.AddRange(fieldSpecs);

			this.FieldNameForLoopDateTime = fieldNameForLoopDateTime;
			this.DateStart = dateStart;
			this.DateEnd = dateEnd;
		}

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

		/// <summary>
		/// Gets a list of records with generated values. This list can then be translated into a Stream with GetFileContent.
		/// </summary>
		/// <param name="dateLoop"></param>
		/// <returns></returns>
		public List<ExpandoObject> GetRecords(DateTime? dateLoop = null)
		{
			int numOfItems = Converter.GetInt32(RNG.GetUniform(this.RecordsPerFileMin ?? 0, this.RecordsPerFileMax ?? 0));

			List<ExpandoObject> result = new List<ExpandoObject>(numOfItems);

			for (int i = 1; i <= numOfItems; i++)
				result.Add(GetRecord(dateLoop));

			return result;
		}

		/// <summary>
		/// Generates a Stream from the list of dynamic objects.
		/// </summary>
		/// <param name="records"></param>
		/// <returns></returns>
		public abstract Stream GetContentStream(List<ExpandoObject> records);

		/// <summary>
		/// Generates a Stream from the list of objects. The objects are serialized by writing key-value pairs for their primitive properties and values.
		/// </summary>
		/// <param name="records"></param>
		/// <returns></returns>
		public Stream GetContentStream<T>(List<T> records)
		{
			List<ExpandoObject> exoRecords = records.Select(r => GetRecord<T>(r)).ToList();

			return GetContentStream(exoRecords);
		}

		#endregion

		/// <summary>
		/// Gets a record based on FieldSpec names and generated values.
		/// </summary>
		/// <param name="dateLoop"></param>
		/// <returns></returns>
		protected virtual ExpandoObject GetRecord(DateTime? dateLoop = null)
		{
			ExpandoObject record = new ExpandoObject();

			IDictionary<string, object> recordProperties = record as IDictionary<string, object>;

			if (!string.IsNullOrWhiteSpace(this.FieldNameForLoopDateTime) && dateLoop != null)
				recordProperties[this.FieldNameForLoopDateTime] = string.Format("{0:" + pelazem.util.Constants.FORMAT_DATETIME_UNIVERSAL + "}", dateLoop);

			foreach (IFieldSpec fieldSpec in this.FieldSpecs)
				recordProperties[fieldSpec.Name] = fieldSpec.Value;

			return record;
		}

		/// <summary>
		/// Gets a record to write to file based on passed-in type. FieldSpecs and the date loop field are ignored since the presumption is that the type, i.e. T, and the item are externally specified/instantiated.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		/// <returns></returns>
		protected virtual ExpandoObject GetRecord<T>(T item)
		{
			ExpandoObject record = new ExpandoObject();

			IDictionary<string, object> recordProperties = record as IDictionary<string, object>;

			var props = TypeUtil.GetPrimitiveProps(typeof(T));

			foreach (PropertyInfo prop in props)
				recordProperties[prop.Name] = prop.GetValueEx(item);

			return record;
		}
	}
}
