using System;
using System.Collections.Generic;
using SynDataFileGen.Lib;
using pelazem.util;

namespace CCLF17.Lib
{
	public class CCLF9Specs
	{
		public static List<IFieldSpec> GetFieldSpecs
		(
			List<Category> BENE_HIC_NUM
		)
		{
			return new List<IFieldSpec>()
			{
				new FieldSpecCategorical(CCLFData.CRNT_HIC_NUM, BENE_HIC_NUM, false, null, 11, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecDynamic(CCLFData.PRVS_HIC_NUM, () => "HICN" + RNG.GetUniform(1000000, 9999999).ToString(), false, null, 11, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecContinuousDateTime(CCLFData.PRVS_HICN_EFCTV_DT, DateTime.UtcNow.AddYears(-10), DateTime.UtcNow.AddMonths(-6), false, "{0:yyyy-MM-dd}", 10, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecContinuousDateTime(CCLFData.PRVS_HICN_OBSLT_DT, DateTime.UtcNow.AddMonths(-6), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecDynamic(CCLFData.PRVS_HIC_NUM, () => "RRB" + RNG.GetUniform(1000000, 999999999).ToString(), false, null, 12, Util.Location.AtStart, Util.Location.AtEnd, null, null, null)
			};
		}
	}
}
