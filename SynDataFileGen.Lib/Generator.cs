using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class Generator<T>
		where T : new()
	{
		#region Properties

		public string OutputFolderRoot { get; private set; }

		public IFileSpec<T> FileSpec { get; private set; }

		private DateTime? DateLoop { get; set; }

		#endregion

		#region Constructors

		private Generator() { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="outputFolderRoot">Root folder into which to write folder(s)/file(s).</param>
		/// <param name="pathSpec">Folder/file specification. Can use the following tokens: {yyyy},{yy},{mm},{dd},{hh} in folder path and/or filename. Other tokens will be ignored. If none specified or if specified path does not contain any date tokens, no date looping will be used.</param>
		/// <param name="fileEncoding"></param>
		/// <param name="recordsPerFileMin"></param>
		/// <param name="recordsPerFileMax"></param>
		/// <param name="fileSpec"></param>
		/// <param name="fieldSpecs"></param>
		/// <param name="dateStart">Start date for looping output. Param pathSpec must also contain date tokens.</param>
		/// <param name="dateEnd">End date for looping output. Param pathSpec must also contain date tokens.</param>
		/// <param name="propertyNameForLoopDateTime">If generated records should contain the loop date/time, specify a property name to have the loop date/time written to.</param>
		public Generator
		(
			string outputFolderRoot,
			IFileSpec<T> fileSpec
		)
		{
			this.OutputFolderRoot = outputFolderRoot;
			this.FileSpec = fileSpec;

			this.DateLoop = this.FileSpec.DateStart;
		}

		#endregion

		/// <summary>
		/// Pass in your own items instead of having the generator generate them - i.e. use this as a glorified serializer
		/// </summary>
		/// <param name="items"></param>
		/// <returns></returns>
		public IEnumerable<T> Run(List<T> items)
		{
			if (items != null && items.Count() > 0)
				WriteFile(GetPath(), this.FileSpec.GetFileContent(items));

			return items;
		}

		/// <summary>
		/// Generator generates items and returns them
		/// </summary>
		/// <returns></returns>
		public IEnumerable<T> Run()
		{
			List<T> result = new List<T>();

			if (!this.FileSpec.HasDateLooping)
			{
				result.AddRange(GetItems());

				WriteFile(GetPath(), this.FileSpec.GetFileContent(result));
			}
			else
			{
				Func<DateTime> func = GetDateLoopFunc();

				while (this.DateLoop <= this.FileSpec.DateEnd)
				{
					List<T> items = GetItems();

					result.AddRange(items);

					WriteFile(GetPath(this.DateLoop.Value), this.FileSpec.GetFileContent(items));

					this.DateLoop = func();
				}
			}

			return result;
		}

		private Func<DateTime> GetDateLoopFunc()
		{
			Func<DateTime> func = null;

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

		/// <summary>
		/// Using the supplied column specs and min/max records per file, builds and returns a list of items to be written to file.
		/// </summary>
		/// <returns></returns>
		private List<T> GetItems()
		{
			int numOfItems = Converter.GetInt32(RNG.GetUniform(this.FileSpec.RecordsPerFileMin ?? 0, this.FileSpec.RecordsPerFileMax ?? 0));

			List<T> result = new List<T>(numOfItems);

			for (int i = 1; i <= numOfItems; i++)
			{
				// Get a new item instance
				T item = new T();

				// Set values per the supplied field specifications
				foreach (IFieldSpec<T> fieldSpec in this.FileSpec.FieldSpecs)
					fieldSpec.SetValue(item);

				// Set looping date if so specified
				if (this.FileSpec.PropertyForLoopDateTime != null)
					this.FileSpec.PropertyForLoopDateTime.SetValueEx(item, this.DateLoop);

				result.Add(item);
			}

			return result;
		}

		#region Utility

		public string GetPath()
		{
			return this.GetPath(null);
		}

		public string GetPath(DateTime? dateTime)
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
