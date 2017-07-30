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
					_results = new List<ExpandoObject>();	// TODO size this list based on RecordsPerFileMax and calc max num of files

				return _results;
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
				var stream = this.FileSpec.GetContentStream(records);

				this.Writer.Write(uri, stream);

				if (collectResults)
					this.Results.AddRange(records);
			}
			else
			{
				DateTime dateLoopStart = this.DateStart.Value;
				Func<DateTime, DateTime> func = GetDateLoopFunc();
				DateTime dateLoopEnd = func(dateLoopStart);

				while (dateLoopEnd <= this.DateEnd)
				{
					string uri = GetPath(dateLoopStart);
					var records = this.FileSpec.GetRecords(dateLoopStart, dateLoopEnd);
					var stream = this.FileSpec.GetContentStream(records);

					this.Writer.Write(uri, stream);

					if (collectResults)
						this.Results.AddRange(records);

					dateLoopStart = func(dateLoopStart);
					dateLoopEnd = func(dateLoopEnd);
				}
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

		private Func<DateTime, DateTime> GetDateLoopFunc()
		{
			Func<DateTime, DateTime> func = null;

			// We iterate through date/time tokens the path spec may contain, from smallest to largest, and the first one we find will dictate the increment of our date loop.
			if (this.FileSpec.PathSpec.Contains(Constants.SECOND))
				func = (d) => d.AddSeconds(1);
			else if (this.FileSpec.PathSpec.Contains(Constants.MINUTE))
				func = (d) => d.AddMinutes(1);
			else if (this.FileSpec.PathSpec.Contains(Constants.HOUR))
				func = (d) => d.AddHours(1);
			else if (this.FileSpec.PathSpec.Contains(Constants.DAY))
				func = (d) => d.AddDays(1);
			else if (this.FileSpec.PathSpec.Contains(Constants.MONTH))
				func = (d) => d.AddMonths(1);
			else if (this.FileSpec.PathSpec.Contains(Constants.YEAR2) || this.FileSpec.PathSpec.Contains(Constants.YEAR4))
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
