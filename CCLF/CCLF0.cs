using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Generator;
using pelazem.Common;

namespace CCLF
{
	public class CCLF0
	{
		public string File_Type { get; set; }
		public string File_Name { get; set; }
		public string Number_Of_Records { get; set; }
		public string Length_Of_Record { get; set; }
	}

	public class CCLF0Props
	{
		public static readonly PropertyInfo File_Type = TypeHelper.GetProperty<CCLF0>(x => x.File_Type);
		public static readonly PropertyInfo File_Name = TypeHelper.GetProperty<CCLF0>(x => x.File_Name);
		public static readonly PropertyInfo Number_Of_Records = TypeHelper.GetProperty<CCLF0>(x => x.Number_Of_Records);
		public static readonly PropertyInfo Length_Of_Record = TypeHelper.GetProperty<CCLF0>(x => x.Length_Of_Record);
	}

	public class CCLF0Specs
	{
		public static List<IFieldSpec<CCLF0>> GetFieldSpecs()
		{
			return new List<IFieldSpec<CCLF0>>()
			{
				new FieldSpecIdempotent<CCLF0>(CCLF0Props.File_Type, null, 7),
				new FieldSpecIdempotent<CCLF0>(CCLF0Props.File_Name, null, 43),
				new FieldSpecIdempotent<CCLF0>(CCLF0Props.Number_Of_Records, null, 11),
				new FieldSpecIdempotent<CCLF0>(CCLF0Props.Length_Of_Record, null, 5)
			};
		}
	}
}
