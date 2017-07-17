using System;
using System.Collections.Generic;
using SynDataFileGen.Lib;
using pelazem.util;

namespace CCLF17.Lib
{
	public class CCLF0
	{
		public string File_Type { get; set; }
		public string File_Name { get; set; }
		public string Number_Of_Records { get; set; }
		public string Length_Of_Record { get; set; }
	}

	public class CCLF0Specs
	{
		public static List<IFieldSpec> GetFieldSpecs()
		{
			return new List<IFieldSpec>()
			{
				new FieldSpecIdempotent(nameof(CCLF0.File_Type), false, null, 7),
				new FieldSpecIdempotent(nameof(CCLF0.File_Name), false, null, 43),
				new FieldSpecIdempotent(nameof(CCLF0.Number_Of_Records), false, null, 11),
				new FieldSpecIdempotent(nameof(CCLF0.Length_Of_Record), false, null, 5)
			};
		}
	}
}
