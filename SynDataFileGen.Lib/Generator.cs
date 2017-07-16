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

		private DateTime? DateLoop { get; set; }

		public List<ExpandoObject> Results { get; } = new List<ExpandoObject>();

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
			if (!this.FileSpec.HasDateLooping)
			{
				WriteFile(GetPath(), this.FileSpec.GetFileContent(this.DateLoop));

				this.Results.AddRange(this.FileSpec.Results);
			}
			else
			{
				Func<DateTime> func = GetDateLoopFunc();

				while (this.DateLoop <= this.FileSpec.DateEnd)
				{
					WriteFile(GetPath(this.DateLoop), this.FileSpec.GetFileContent(this.DateLoop));

					this.Results.AddRange(this.FileSpec.Results);

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

		public List<T> GetResults<T>()
			where T : new()
		{
			List<T> genericResults = new List<T>();

			if (this.Results.Count == 0)
				return genericResults;

			// Properties of the passed-in generic type
			List<PropertyInfo> props = TypeUtil.GetPrimitiveProps(typeof(T));

			// Keys - i.e. field names which correspond to property names - of results
			ICollection<string> fieldNames = (this.Results.First() as IDictionary<string, object>).Keys;

			// Map result field names to matching properties
			Dictionary<string, PropertyInfo> fieldsAndThePropsToWriteTo = new Dictionary<string, PropertyInfo>();

			foreach (string fieldName in fieldNames)
			{
				PropertyInfo prop = props.SingleOrDefault(p => p.Name.ToLowerInvariant() == fieldName.ToLowerInvariant());

				if (prop != null)
					fieldsAndThePropsToWriteTo.Add(fieldName, prop);
			}

			foreach (IDictionary<string, object> expandoResult in this.Results.Select(r => r as IDictionary<string, object>))
			{
				T genericInstance = new T();

				foreach (KeyValuePair<string, PropertyInfo> fieldAndProp in fieldsAndThePropsToWriteTo)
				{
					string fieldName = fieldAndProp.Key;
					PropertyInfo prop = fieldAndProp.Value;
					prop.SetValueEx(genericInstance, expandoResult[fieldName]);
				}

				genericResults.Add(genericInstance);
			}

			return genericResults;
		}
	}
}
