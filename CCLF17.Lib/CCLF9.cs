using System;
using System.Collections.Generic;
using SynDataFileGen.Lib;
using pelazem.util;

namespace CCLF17.Lib
{
	public class CCLF9
	{
		public string CRNT_HIC_NUM { get; set; }
		public string PRVS_HIC_NUM { get; set; }
		public DateTime PRVS_HICN_EFCTV_DT { get; set; }
		public DateTime PRVS_HICN_OBSLT_DT { get; set; }
		public string BENE_RRB_NUM { get; set; }
	}

	public class CCLF9Specs
	{
		public static List<IFieldSpec> GetFieldSpecs
		(
			List<Category> BENE_HIC_NUM
		)
		{
			return new List<IFieldSpec>()
			{
				new FieldSpecCategorical(nameof(CCLF9.CRNT_HIC_NUM), BENE_HIC_NUM, false, null, 11),
				new FieldSpecDynamic(nameof(CCLF9.PRVS_HIC_NUM), () => "HICN" + RNG.GetUniform(1000000, 9999999).ToString(), false, null, 11),
				new FieldSpecContinuousDateTime(nameof(CCLF9.PRVS_HICN_EFCTV_DT), DateTime.UtcNow.AddYears(-10), DateTime.UtcNow.AddMonths(-6), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime(nameof(CCLF9.PRVS_HICN_OBSLT_DT), DateTime.UtcNow.AddMonths(-6), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecDynamic(nameof(CCLF9.PRVS_HIC_NUM), () => "RRB" + RNG.GetUniform(1000000, 999999999).ToString(), false, null, 12)
			};
		}
	}
}
