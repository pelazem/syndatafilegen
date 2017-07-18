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
		#region Properties

		public string OutputFolderRoot { get; private set; }

		public IFileSpec FileSpec { get; private set; }

		public IWriter Writer { get; private set; }

		private DateTime? DateLoop { get; set; }

		#endregion

		#region Constructors

		private Generator() { }

		public Generator
		(
			string outputFolderRoot,
			IFileSpec fileSpec,
			IWriter writer
		)
		{
			this.OutputFolderRoot = outputFolderRoot;
			this.FileSpec = fileSpec;
			this.Writer = writer;
			this.DateLoop = this.FileSpec.DateStart;
		}

		#endregion

		public List<ExpandoObject> Run()
		{
			List<ExpandoObject> results = new List<ExpandoObject>();

			if (!this.FileSpec.HasDateLooping)
			{
				string uri = GetPath();
				var records = this.FileSpec.GetRecords();
				var stream = this.FileSpec.GetContentStream(records);

				this.Writer.Write(uri, stream);

				results.AddRange(records);
			}
			else
			{
				Func<DateTime> func = GetDateLoopFunc();

				while (this.DateLoop <= this.FileSpec.DateEnd)
				{
					string uri = GetPath(this.DateLoop);
					var records = this.FileSpec.GetRecords(this.DateLoop);
					var stream = this.FileSpec.GetContentStream(records);

					this.Writer.Write(uri, stream);

					results.AddRange(records);

					this.DateLoop = func();
				}
			}

			return results;
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

		private Func<DateTime> GetDateLoopFunc()
		{
			Func<DateTime> func = null;

			// We iterate through date/time tokens the path spec may contain, from smallest to largest, and the first one we find will dictate the increment of our date loop.
			if (this.FileSpec.PathSpec.Contains(Constants.HH))
				func = () => this.DateLoop.Value.AddHours(1);
			else if (this.FileSpec.PathSpec.Contains(Constants.DD))
				func = () => this.DateLoop.Value.AddDays(1);
			else if (this.FileSpec.PathSpec.Contains(Constants.MM))
				func = () => this.DateLoop.Value.AddMonths(1);
			else if (this.FileSpec.PathSpec.Contains(Constants.YY) || this.FileSpec.PathSpec.Contains(Constants.YYYY))
				func = () => this.DateLoop.Value.AddYears(1);

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
					.Replace(Constants.YYYY, dateTime.Value.Year.ToString())
					.Replace(Constants.YY, dateTime.Value.Year.ToString().Substring(2))
					.Replace(Constants.MM, Util.GetPadded(dateTime.Value.Month))
					.Replace(Constants.DD, Util.GetPadded(dateTime.Value.Day))
					.Replace(Constants.HH, Util.GetPadded(dateTime.Value.Hour))
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
