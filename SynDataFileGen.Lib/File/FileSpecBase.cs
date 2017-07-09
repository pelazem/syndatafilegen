using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public abstract class FileSpecBase<T> : IFileSpec<T>
		where T : new()
	{
		#region Variables

		private string _propertyNameForLoopDateTime = string.Empty;

		private DateTime? _dateStart;
		private DateTime? _dateEnd;

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
		public string PropertyNameForLoopDateTime
		{
			get { return _propertyNameForLoopDateTime; }

			protected set
			{
				_propertyNameForLoopDateTime = value;

				if (!string.IsNullOrWhiteSpace(_propertyNameForLoopDateTime))
					SetPropertyForLoopDateTime();
			}
		}

		public PropertyInfo PropertyForLoopDateTime { get; protected set; }

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


		public abstract Stream GetFileContent(List<T> items);

		public List<IFieldSpec<T>> FieldSpecs { get; } = new List<IFieldSpec<T>>();

		#endregion

		#region Constructors

		protected FileSpecBase() { }

		public FileSpecBase(int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec, IEnumerable<IFieldSpec<T>> fieldSpecs = null)
		{
			this.RecordsPerFileMin = recordsPerFileMin;
			this.RecordsPerFileMax = recordsPerFileMax;
			this.PathSpec = pathSpec.Replace(@"/", @"\");

			this.FieldSpecs.AddRange(fieldSpecs);
		}

		public FileSpecBase(int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec, string propertyNameForLoopDateTime, DateTime? dateStart, DateTime? dateEnd, IEnumerable<IFieldSpec<T>> fieldSpecs = null)
		{
			this.RecordsPerFileMin = recordsPerFileMin;
			this.RecordsPerFileMax = recordsPerFileMax;
			this.PathSpec = pathSpec.Replace(@"/", @"\");

			this.PropertyNameForLoopDateTime = propertyNameForLoopDateTime;
			this.DateStart = dateStart;
			this.DateEnd = dateEnd;

			this.FieldSpecs.AddRange(fieldSpecs);
		}

		#endregion

		#region Utility

		private void SetPropertyForLoopDateTime()
		{
			// Check whether there is a valid date/time property specified
			if (this.PropertyForLoopDateTime == null && !string.IsNullOrWhiteSpace(this.PropertyNameForLoopDateTime))
			{
				this.PropertyForLoopDateTime = TypeUtil.GetPrimitiveProps(typeof(T))
					.Where
					(p =>
						p.Name == this.PropertyNameForLoopDateTime
						&&
						p.CanWrite
						&&
						(p.PropertyType.Equals(TypeUtil.TypeDateTime) || p.PropertyType.Equals(TypeUtil.TypeDateTimeNullable))
					)
					.FirstOrDefault()
				;
			}
		}

		#endregion
	}
}
