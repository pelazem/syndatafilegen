using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SynDataFileGen.Lib
{
	public class Generator
	{
		#region Properties

		public string OutputFolderRoot { get; private set; }

		public IFileSpec FileSpec { get; private set; }

		private DateTime? DateLoop { get; set; }

		#endregion

		#region Constructors

		private Generator() { }

		public Generator
		(
			string outputFolderRoot,
			IFileSpec fileSpec
		)
		{
			this.OutputFolderRoot = outputFolderRoot;
			this.FileSpec = fileSpec;

			this.DateLoop = this.FileSpec.DateStart;
		}

		#endregion

		public void Run()
		{
			WriteFile(GetPath(), this.FileSpec.GetFileContent(this.DateLoop));

			if (!this.FileSpec.HasDateLooping)
			{
				WriteFile(GetPath(), this.FileSpec.GetFileContent(this.DateLoop));
			}
			else
			{
				Func<DateTime> func = GetDateLoopFunc();

				while (this.DateLoop <= this.FileSpec.DateEnd)
				{
					WriteFile(GetPath(this.DateLoop), this.FileSpec.GetFileContent(this.DateLoop));

					this.DateLoop = func();
				}
			}
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

		public void WriteFile(string fullFilePath, Stream contents)
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

		#endregion
	}
}
