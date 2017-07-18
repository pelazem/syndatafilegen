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
				this.FileSpec.GetRecords(GetPath(), this.DateLoop);
			else
			{
				Func<DateTime> func = GetDateLoopFunc();

				while (this.DateLoop <= this.FileSpec.DateEnd)
				{
					this.FileSpec.GetRecords(GetPath(), this.DateLoop);

					this.DateLoop = func();
				}
			}
		}

		public List<T> Run<T>()
			where T : new()
		{
			List<ExpandoObject> rawResults = new List<ExpandoObject>();

			if (!this.FileSpec.HasDateLooping)
				rawResults.AddRange(this.FileSpec.GetRecords(GetPath(), this.DateLoop));
			else
			{
				Func<DateTime> func = GetDateLoopFunc();

				while (this.DateLoop <= this.FileSpec.DateEnd)
				{
					rawResults.AddRange(this.FileSpec.GetRecords(GetPath(), this.DateLoop));

					this.DateLoop = func();
				}
			}

			return GetResults<T>(rawResults);
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

		private List<T> GetResults<T>(List<ExpandoObject> rawResults)
			where T : new()
		{
			List<T> genericResults = new List<T>();

			if (rawResults.Count == 0)
				return genericResults;

			// Properties of the passed-in generic type
			List<PropertyInfo> props = TypeUtil.GetPrimitiveProps(typeof(T));

			// Keys - i.e. field names which correspond to property names - of results
			ICollection<string> fieldNames = (rawResults.First() as IDictionary<string, object>).Keys;

			// Map result field names to matching properties
			Dictionary<string, PropertyInfo> fieldsAndThePropsToWriteTo = new Dictionary<string, PropertyInfo>();

			foreach (string fieldName in fieldNames)
			{
				PropertyInfo prop = props.SingleOrDefault(p => p.Name.ToLowerInvariant() == fieldName.ToLowerInvariant());

				if (prop != null)
					fieldsAndThePropsToWriteTo.Add(fieldName, prop);
			}

			foreach (IDictionary<string, object> expandoResult in rawResults.Select(r => r as IDictionary<string, object>))
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

		#endregion
	}
}
