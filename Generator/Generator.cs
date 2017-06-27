using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using pelazem.Common;

namespace Generator
{
	public class Generator<T>
		where T : new()
	{
		#region Constants

		// Tokens for path spec. May need to nuance this; currently these tokens assume fully qualified, e.g. 03.
		public const string YYYY = "{yyyy}";
		public const string YY = "{yy}";
		public const string MM = "{mm}";
		public const string DD = "{dd}";
		public const string HH = "{hh}";

		public const string DEFAULT_FILE_NAME = "generated";
		public const string DEFAULT_FILE_EXTENSION = ".txt";

		#endregion

		#region Variables

		// Local list of properties since we will check for loop date/time prop and remove it and do other mods that should not affect TypeInfo
		private List<PropertyInfo> _props = new List<PropertyInfo>();

		#endregion

		#region Properties

		public string OutputFolderRoot { get; private set; }

		public string PathSpec { get; private set; }

		public DateTime? DateStart { get; private set; }
		public DateTime? DateEnd { get; private set; }

		public IFileSpec<T> FileSpec { get; private set; }

		public string PropertyNameForLoopDateTime { get; private set; }

		public PropertyInfo PropertyForLoopDateTime { get; private set; }

		public List<IFieldSpec<T>> FieldSpecs { get; private set; }

		private DateTime? DateLoop { get; set; }

		#endregion

		#region Constructors

		private Generator() { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="outputFolderRoot">Root folder into which to write folder(s)/file(s). If not specified, AppData folder will be used.</param>
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
			string pathSpec,
			IFileSpec<T> fileSpec,
			List<IFieldSpec<T>> fieldSpecs = null,
			DateTime? dateStart = null,
			DateTime? dateEnd = null,
			string propertyNameForLoopDateTime = ""
		)
		{
			this.OutputFolderRoot = (string.IsNullOrWhiteSpace(outputFolderRoot) ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) : outputFolderRoot);
			this.PathSpec = pathSpec.Replace(@"/", @"\");
			this.FileSpec = fileSpec;
			this.FieldSpecs = fieldSpecs;
			this.DateStart = dateStart;
			this.DateEnd = dateEnd;
			this.PropertyNameForLoopDateTime = propertyNameForLoopDateTime;

			this.DateLoop = this.DateStart;
		}

		public Generator
		(
			string outputFolderRoot,
			string pathSpec,
			IFileSpec<T> fileSpec
		)
		{
			this.OutputFolderRoot = (string.IsNullOrWhiteSpace(outputFolderRoot) ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) : outputFolderRoot);
			this.PathSpec = pathSpec.Replace(@"/", @"\");
			this.FileSpec = fileSpec;
		}

		public Generator
		(
			string outputFolderRoot,
			string pathSpec,
			IFileSpec<T> fileSpec,
			List<IFieldSpec<T>> fieldSpecs
		)
		{
			this.OutputFolderRoot = (string.IsNullOrWhiteSpace(outputFolderRoot) ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) : outputFolderRoot);
			this.PathSpec = pathSpec.Replace(@"/", @"\");
			this.FileSpec = fileSpec;
			this.FieldSpecs = fieldSpecs;
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

			// Prep loop date/time property if one was specified
			SetPropertyForLoopDateTime();

			bool hasDateLooping = GetHasDateLooping();

			if (!hasDateLooping)
			{
				result.AddRange(GetItems());

				WriteFile(GetPath(), this.FileSpec.GetFileContent(result));
			}
			else
			{
				Func<DateTime> func = GetDateLoopFunc();

				while (this.DateLoop <= this.DateEnd)
				{
					List<T> items = GetItems();

					result.AddRange(items);

					WriteFile(GetPath(this.DateLoop.Value), this.FileSpec.GetFileContent(items));

					this.DateLoop = func();
				}
			}

			return result;
		}

		private void SetPropertyForLoopDateTime()
		{
			// Check whether there is a valid date/time property specified
			if (this.PropertyForLoopDateTime == null && !string.IsNullOrWhiteSpace(this.PropertyNameForLoopDateTime))
			{
				this.PropertyForLoopDateTime = TypeHelper.GetPrimitiveProps(typeof(T))
					.Where
					(p =>
						p.Name == this.PropertyNameForLoopDateTime
						&&
						p.CanWrite
						&&
						(p.PropertyType.Equals(TypeHelper.TypeDateTime) || p.PropertyType.Equals(TypeHelper.TypeDateTimeNullable))
					)
					.FirstOrDefault()
				;
			}
		}

		private bool GetHasDateLooping()
		{
			bool pathSpecHasDateLooping =
			(
				this.PathSpec.Contains(YYYY) ||
				this.PathSpec.Contains(YY) ||
				this.PathSpec.Contains(MM) ||
				this.PathSpec.Contains(DD) ||
				this.PathSpec.Contains(HH)
			);

			bool dateLoopingDatesSpecified = (this.DateStart != null && this.DateEnd != null && this.DateStart.Value <= this.DateEnd.Value);

			return pathSpecHasDateLooping && dateLoopingDatesSpecified;
		}

		private Func<DateTime> GetDateLoopFunc()
		{
			Func<DateTime> func = null;

			if (this.PathSpec.Contains(HH))
				func = () => this.DateLoop.Value.AddHours(1);
			else if (this.PathSpec.Contains(DD))
				func = () => this.DateLoop.Value.AddDays(1);
			else if (this.PathSpec.Contains(MM))
				func = () => this.DateLoop.Value.AddMonths(1);
			else if (this.PathSpec.Contains(YY) || this.PathSpec.Contains(YYYY))
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
				foreach (IFieldSpec<T> fieldSpec in this.FieldSpecs)
					fieldSpec.SetValue(item);

				// Set looping date if so specified
				if (this.PropertyForLoopDateTime != null)
					this.PropertyForLoopDateTime.SetValueEx(item, this.DateLoop);

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
			string path = Path.Combine(this.OutputFolderRoot, this.PathSpec);

			if (dateTime == null)
				dateTime = DateTime.UtcNow;

			if (dateTime != null)
			{
				path = path
					.Replace(YYYY, dateTime.Value.Year.ToString())
					.Replace(YY, dateTime.Value.Year.ToString().Substring(2))
					.Replace(MM, GetPadded(dateTime.Value.Month))
					.Replace(DD, GetPadded(dateTime.Value.Day))
					.Replace(HH, GetPadded(dateTime.Value.Hour))
				;
			}

			if (string.IsNullOrWhiteSpace(Path.GetFileName(path)))
				path = Path.Combine(path, DEFAULT_FILE_NAME);

			if (!Path.HasExtension(path))
				path += DEFAULT_FILE_EXTENSION;

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

		private string GetPadded(int someTwoDigitNumber)
		{
			return (someTwoDigitNumber < 10 ? "0" : string.Empty) + someTwoDigitNumber.ToString();
		}

		#endregion
	}
}
