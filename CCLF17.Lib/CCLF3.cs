using System;
using System.Collections.Generic;
using SynDataFileGen.Lib;
using pelazem.util;

namespace CCLF17.Lib
{
	public class CCLF3
	{
		public Int64 CUR_CLM_UNIQ_ID { get; set; } //
		public string BENE_HIC_NUM { get; set; } //
		public int CLM_TYPE_CD { get; set; }
		public Int64 CLM_VAL_SQNC_NUM { get; set; }
		public string CLM_PRCDR_CD { get; set; }
		public DateTime CLM_PRCDR_PRFRM_DT { get; set; }
		public string BENE_EQTBL_BIC_HICN_NUM { get; set; } //
		public string PRVDR_OSCAR_NUM { get; set; } //
		public DateTime CLM_FROM_DT { get; set; }
		public DateTime CLM_THRU_DT { get; set; }
		public string DGNS_PRCDR_ICD_IND { get; set; }
	}

	public class CCLF3Specs
	{
		public static List<IFieldSpec> GetFieldSpecs
		(
			List<Category>  CUR_CLM_UNIQ_ID,
			List<Category>  BENE_HIC_NUM,
			List<Category>  BENE_EQTBL_BIC_HICN_NUM,
			List<Category>  PRNCPL_DGNS_CD,
			List<Category>  PRVDR_OSCAR_NUM
		)
		{
			return new List<IFieldSpec>()
			{
				new FieldSpecCategorical(nameof(CCLF3.CUR_CLM_UNIQ_ID), CUR_CLM_UNIQ_ID, false, null, 13),
				new FieldSpecCategorical(nameof(CCLF3.BENE_HIC_NUM), BENE_HIC_NUM, false, null, 11),
				new FieldSpecCategorical(nameof(CCLF3.CLM_TYPE_CD), CCLFData.LIST_CLM_TYPE_CD, false, null, 2),
				new FieldSpecContinuousNumeric(nameof(CCLF3.CLM_VAL_SQNC_NUM), new DistIncrementing(0, 1), 0, false, null, 2),
				new FieldSpecCategorical(nameof(CCLF3.CLM_PRCDR_CD), PRNCPL_DGNS_CD, false, null, 7),
				new FieldSpecContinuousDateTime(nameof(CCLF3.CLM_PRCDR_PRFRM_DT), DateTime.UtcNow.AddMonths(-4), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical(nameof(CCLF3.BENE_EQTBL_BIC_HICN_NUM), BENE_EQTBL_BIC_HICN_NUM, false, null, 11),
				new FieldSpecCategorical(nameof(CCLF3.PRVDR_OSCAR_NUM), PRVDR_OSCAR_NUM, false, null, 6),
				new FieldSpecContinuousDateTime(nameof(CCLF3.CLM_FROM_DT), DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime(nameof(CCLF3.CLM_THRU_DT), DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecDynamic(nameof(CCLF3.DGNS_PRCDR_ICD_IND), () => "0", false, null, 1),
			};
		}
	}
}
