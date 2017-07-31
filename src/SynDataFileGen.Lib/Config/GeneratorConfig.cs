using System;

namespace SynDataFileGen.Lib
{
	public class GeneratorConfig
	{
		private DateTime? _dateStart;
		private DateTime? _dateEnd;

		public string OutputFolderRoot { get; set; }

		public DateTime? DateStart
		{
			get
			{
				return _dateStart;
			}
			set
			{
				if (value == null)
					_dateStart = value;
				else if (value.Value.Kind == DateTimeKind.Unspecified)
					_dateStart = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
				else
					_dateStart = value;
			}
		}

		public DateTime? DateEnd
		{
			get
			{
				return _dateEnd;
			}
			set
			{
				if (value == null)
					_dateEnd = value;
				else if (value.Value.Kind == DateTimeKind.Unspecified)
					_dateEnd = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
				else
					_dateEnd = value;
			}
		}
	}
}
