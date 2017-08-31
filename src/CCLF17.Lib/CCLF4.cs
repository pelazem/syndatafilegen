using System;
using System.Collections.Generic;
using SynDataFileGen.Lib;
using pelazem.util;

namespace CCLF17.Lib
{
	public class CCLF4Specs
	{
		public static List<IFieldSpec> GetFieldSpecs
		(
			List<Category> CUR_CLM_UNIQ_ID,
			List<Category> BENE_HIC_NUM,
			List<Category> BENE_EQTBL_BIC_HICN_NUM,
			List<Category> PRNCPL_DGNS_CD,
			List<Category> PRVDR_OSCAR_NUM
		)
		{
			return new List<IFieldSpec>()
			{
				new FieldSpecCategorical(CCLFData.CUR_CLM_UNIQ_ID, CUR_CLM_UNIQ_ID, false, null, 13, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.BENE_HIC_NUM, BENE_HIC_NUM, false, null, 11, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.CLM_TYPE_CD, CCLFData.LIST_CLM_TYPE_CD, false, null, 2, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.CLM_PROD_TYPE_CD, CCLFData.LIST_CLM_PROD_TYPE_CD, false, null, 1, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecContinuousNumeric(CCLFData.CLM_VAL_SQNC_NUM, new DistUniform(0, 99), 0, false, null, 2, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.CLM_DGNS_CD, PRNCPL_DGNS_CD, false, null, 7, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.BENE_EQTBL_BIC_HICN_NUM, BENE_EQTBL_BIC_HICN_NUM, false, null, 11, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.PRVDR_OSCAR_NUM, PRVDR_OSCAR_NUM, false, null, 6, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecContinuousDateTime(CCLFData.CLM_FROM_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecContinuousDateTime(CCLFData.CLM_THRU_DT, DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.CLM_POA_IND, CCLFData.LIST_CLM_POA_IND, false, null, 7, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecDynamic(CCLFData.DGNS_PRCDR_ICD_IND, () => "0", false, null, 1, Util.Location.AtStart, Util.Location.AtEnd, null, null, null)
			};
		}
	}
}
