using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class Generator
	{
		#region Variables

		private DateTime? _dateStart;
		private DateTime? _dateEnd;

		private List<ExpandoObject> _results = null;

		#endregion

		#region Properties

		public string OutputFolderRoot { get; private set; }

		/// <summary>
		/// Specify a valid date less than DateEnd to get date looping output. Leave null if time-series output is not needed.
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
		/// Specify a valid date greater than DateStart to get date looping output. Leave null if time-series output is not needed.
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

		public IFileSpec FileSpec { get; private set; }

		public IWriter Writer { get; private set; }

		public List<ExpandoObject> Results
		{
			get
			{
				if (_results == null)
					_results = new List<ExpandoObject>();

				return _results;
			}
			private set
			{
				_results = value;
			}
		}

		#endregion

		#region Constructors

		private Generator() { }

		public Generator
		(
			string outputFolderRoot,
			IFileSpec fileSpec,
			IWriter writer
		)
			: this(outputFolderRoot, null, null, fileSpec, writer)
		{ }


		public Generator
		(
			string outputFolderRoot,
			DateTime? dateStart,
			DateTime? dateEnd,
			IFileSpec fileSpec,
			IWriter writer
		)
		{
			this.OutputFolderRoot = outputFolderRoot;
			this.DateStart = dateStart;
			this.DateEnd = dateEnd;
			this.FileSpec = fileSpec;
			this.Writer = writer;
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="collectResults">If true, the Generator's Results property will contain all results for further usage in caller after Run() completes</param>
		public void Run(bool collectResults = false)
		{
			bool useDateLooping = (this.FileSpec.HasDateLooping && this.DateStart != null && this.DateEnd != null && this.DateStart.Value <= this.DateEnd.Value);

			if (!useDateLooping)
			{
				string uri = GetPath();
				var records = this.FileSpec.GetRecords();

				using (var stream = this.FileSpec.GetContentStream(records))
				{
					this.Writer.Write(uri, stream);
				}

				if (collectResults)
					this.Results.AddRange(records);
			}
			else
			{
				int loopCount;

				string dateLoopGranularity = GetDateLoopGranularity();

				// If collectResults true, pre-size the results list to the number of files to be written * max rows per file - this will over-allocate but we trim excess at end
				if (collectResults)
				{
					loopCount = GetLoopCount(dateLoopGranularity);
					this.Results = new List<ExpandoObject>(loopCount * this.FileSpec.RecordsPerFileMax.Value);
				}

				Func<DateTime, DateTime> func = GetDateLoopFunc(dateLoopGranularity);
				DateTime dateLoopStart = this.DateStart.Value;
				DateTime dateLoopEnd = func(dateLoopStart);

				while (dateLoopEnd <= this.DateEnd)
				{
					string uri = GetPath(dateLoopStart);
					var records = this.FileSpec.GetRecords(dateLoopStart, dateLoopEnd);

					using (var stream = this.FileSpec.GetContentStream(records))
					{
						this.Writer.Write(uri, stream);
					}

					if (collectResults)
						this.Results.AddRange(records);

					dateLoopStart = func(dateLoopStart);
					dateLoopEnd = func(dateLoopEnd);
				}

				if (collectResults)
					this.Results.TrimExcess();
			}
		}

		/// <summary>
		/// Writes file for passed-in list of externally generated items. Basically, use this method to write your own items instead of having them generated.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items"></param>
		public void Run(List<ExpandoObject> items)
		{
			string uri = GetPath();
			var stream = this.FileSpec.GetContentStream(items);

			this.Writer.Write(uri, stream);
		}

		private string GetDateLoopGranularity()
		{
			string result = string.Empty;

			if (this.FileSpec.PathSpec.Contains(Constants.SECOND))
				result = Constants.SECOND;
			else if (this.FileSpec.PathSpec.Contains(Constants.MINUTE))
				result = Constants.MINUTE;
			else if (this.FileSpec.PathSpec.Contains(Constants.HOUR))
				result = Constants.HOUR;
			else if (this.FileSpec.PathSpec.Contains(Constants.DAY))
				result = Constants.DAY;
			else if (this.FileSpec.PathSpec.Contains(Constants.MONTH))
				result = Constants.MONTH;
			else if (this.FileSpec.PathSpec.Contains(Constants.YEAR2) || this.FileSpec.PathSpec.Contains(Constants.YEAR4))
				result = Constants.YEAR4;

			return result;
		}

		private int GetLoopCount(string dateLoopGranularity)
		{
			int result = 1;

			if (this.DateStart == null || this.DateEnd == null || this.DateEnd < this.DateStart)
				return result;

			TimeSpan diff = this.DateEnd.Value.Subtract(this.DateStart.Value);

			if (dateLoopGranularity == Constants.SECOND)
				result = Converter.GetInt32(diff.TotalSeconds) + 1;
			else if (dateLoopGranularity == Constants.MINUTE)
				result = Converter.GetInt32(diff.TotalMinutes) + 1;
			else if (dateLoopGranularity == Constants.HOUR)
				result = Converter.GetInt32(diff.TotalHours) + 1;
			else if (dateLoopGranularity == Constants.DAY)
				result = Converter.GetInt32(diff.TotalDays) + 1;
			else if (dateLoopGranularity == Constants.MONTH)
				result = Converter.GetInt32(Math.Ceiling(diff.TotalDays / 30));
			else if (dateLoopGranularity == Constants.YEAR2 || dateLoopGranularity == Constants.YEAR4)
				result = Converter.GetInt32(Math.Ceiling(diff.TotalDays / 365));

			return result;
		}

		private Func<DateTime, DateTime> GetDateLoopFunc(string dateLoopGranularity)
		{
			Func<DateTime, DateTime> func = null;

			// We iterate through date/time tokens the path spec may contain, from smallest to largest, and the first one we find will dictate the increment of our date loop.
			if (dateLoopGranularity == Constants.SECOND)
				func = (d) => d.AddSeconds(1);
			else if (dateLoopGranularity == Constants.MINUTE)
				func = (d) => d.AddMinutes(1);
			else if (dateLoopGranularity == Constants.HOUR)
				func = (d) => d.AddHours(1);
			else if (dateLoopGranularity == Constants.DAY)
				func = (d) => d.AddDays(1);
			else if (dateLoopGranularity == Constants.MONTH)
				func = (d) => d.AddMonths(1);
			else if (dateLoopGranularity == Constants.YEAR2 || dateLoopGranularity == Constants.YEAR4)
				func = (d) => d.AddYears(1);

			return func;
		}

		#region Utility

		public string GetPath(DateTime? dateTime = null)
		{
			string path = Path.Combine(this.OutputFolderRoot, this.FileSpec.PathSpec);

			if (dateTime == null)
				dateTime = DateTime.UtcNow;
			else if (dateTime.Value.Kind != DateTimeKind.Utc)
				dateTime = dateTime.Value.ToUniversalTime();

			if (dateTime != null)
			{
				path = path
					.Replace(Constants.YEAR4, dateTime.Value.Year.ToString())
					.Replace(Constants.YEAR2, dateTime.Value.Year.ToString().Substring(2))
					.Replace(Constants.MONTH, Util.GetPadded(dateTime.Value.Month))
					.Replace(Constants.DAY, Util.GetPadded(dateTime.Value.Day))
					.Replace(Constants.HOUR, Util.GetPadded(dateTime.Value.Hour))
				;
			}

			if (string.IsNullOrWhiteSpace(Path.GetFileName(path)))
				path = Path.Combine(path, Constants.DEFAULT_FILE_NAME);

			if (!Path.HasExtension(path))
				path += Constants.DEFAULT_FILE_EXTENSION;

			return path;
		}

		#endregion
	}
}
