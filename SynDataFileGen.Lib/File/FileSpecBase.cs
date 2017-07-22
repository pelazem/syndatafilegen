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
		#region Constructors

		protected FileSpecBase() { }

		public FileSpecBase(int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec, IEnumerable<IFieldSpec> fieldSpecs, string fieldNameForLoopDateTime)
		{
			this.RecordsPerFileMin = recordsPerFileMin;
			this.RecordsPerFileMax = recordsPerFileMax;
			this.PathSpec = pathSpec.Replace(@"/", @"\");

			this.FieldSpecs.AddRange(fieldSpecs);

			this.FieldNameForLoopDateTime = fieldNameForLoopDateTime;
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
					this.PathSpec.Contains(Constants.YEAR4) ||
					this.PathSpec.Contains(Constants.YEAR2) ||
					this.PathSpec.Contains(Constants.MONTH) ||
					this.PathSpec.Contains(Constants.DAY) ||
					this.PathSpec.Contains(Constants.HOUR) ||
					this.PathSpec.Contains(Constants.MINUTE) ||
					this.PathSpec.Contains(Constants.SECOND)
				);

				//bool dateLoopingDatesSpecified = (this.DateStart != null && this.DateEnd != null && this.DateStart.Value <= this.DateEnd.Value);

				return pathSpecHasDateLooping; // && dateLoopingDatesSpecified;
			}
		}

		/// <summary>
		/// For looping date/time file series, if the loop date/time should also be written into the output files, specify the field name to which to write the loop date/time.
		/// If a non-existent field name is specified, the loop date/time will not be written to the output files.
		/// </summary>
		public string FieldNameForLoopDateTime { get; protected set; }

		public List<IFieldSpec> FieldSpecs { get; } = new List<IFieldSpec>();

		/// <summary>
		/// Gets a list of records with generated values. This list can then be translated into a Stream with GetFileContent.
		/// </summary>
		/// <returns></returns>
		public List<ExpandoObject> GetRecords()
		{
			int numOfItems = Converter.GetInt32(RNG.GetUniform(this.RecordsPerFileMin ?? 0, this.RecordsPerFileMax ?? 0));

			List<ExpandoObject> result = new List<ExpandoObject>(numOfItems);

			for (int i = 1; i <= numOfItems; i++)
				result.Add(GetRecord());

			return result;
		}

		/// <summary>
		/// Gets a list of records with generated values. This list can then be translated into a Stream with GetFileContent.
		/// </summary>
		/// <returns></returns>
		public List<ExpandoObject> GetRecords(DateTime dateStart, DateTime dateEnd)
		{
			int numOfItems = Converter.GetInt32(RNG.GetUniform(this.RecordsPerFileMin ?? 0, this.RecordsPerFileMax ?? 0));

			long ticksDelta = dateEnd.Ticks - dateStart.Ticks;
			long ticksPerItem = ticksDelta / numOfItems;
			DateTime dateLoop = dateStart;

			List<ExpandoObject> result = new List<ExpandoObject>(numOfItems);

			for (int i = 1; i <= numOfItems; i++)
			{
				result.Add(GetRecord(dateLoop));

				double ticksFactor = RNG.GetUniform(0, 1.999999);
				dateLoop = dateLoop.AddTicks(Converter.GetInt64(ticksPerItem * ticksFactor));
			}

			return result;
		}


		/// <summary>
		/// Generates a Stream from the list of dynamic objects.
		/// </summary>
		/// <param name="records"></param>
		/// <returns></returns>
		public abstract Stream GetContentStream(List<ExpandoObject> records);

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
	}
}
