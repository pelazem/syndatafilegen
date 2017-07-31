using System;
using System.Collections.Generic;
using SynDataFileGen.Lib;
using pelazem.util;

namespace CCLF17.Lib
{
	public class CCLF0Specs
	{
		public static List<IFieldSpec> GetFieldSpecs()
		{
			return new List<IFieldSpec>()
			{
				new FieldSpecIdempotent(CCLFData.File_Type, false, null, 7),
				new FieldSpecIdempotent(CCLFData.File_Name, false, null, 43),
				new FieldSpecIdempotent(CCLFData.Number_Of_Records, false, null, 11),
				new FieldSpecIdempotent(CCLFData.Length_Of_Record, false, null, 5)
			};
		}
	}
}
