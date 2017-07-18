using System;
using System.Collections.Generic;
using SynDataFileGen.Lib;
using pelazem.util;

namespace CCLF17.Lib
{
	public class CCLF2Specs
	{
		public static List<IFieldSpec> GetFieldSpecs
		(
			List<Category> CUR_CLM_UNIQ_ID,
			List<Category> BENE_HIC_NUM,
			List<Category> BENE_EQTBL_BIC_HICN_NUM,
			List<Category> PRVDR_OSCAR_NUM
		)
		{
			return new List<IFieldSpec>()
			{
				new FieldSpecCategorical(CCLFData.CUR_CLM_UNIQ_ID, CUR_CLM_UNIQ_ID, false, null, 13),
				new FieldSpecContinuousNumeric(CCLFData.CLM_LINE_NUM, new DistIncrementing(1, 1), 0, false, null, 10, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecCategorical(CCLFData.BENE_HIC_NUM, BENE_HIC_NUM, false, null, 11),
				new FieldSpecCategorical(CCLFData.CLM_TYPE_CD, CCLFData.LIST_CLM_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime(CCLFData.CLM_LINE_FROM_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime(CCLFData.CLM_LINE_THRU_DT, DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical(CCLFData.CLM_LINE_PROD_REV_CTR_CD, CCLFData.LIST_CLM_LINE_PROD_REV_CTR_CD, false, null, 4),
				new FieldSpecContinuousDateTime(CCLFData.CLM_LINE_INSTNL_REV_CTR_DT, DateTime.UtcNow.AddMonths(-4), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical(CCLFData.CLM_LINE_HCPCS_CD, CCLFData.LIST_HCPCS_CD, false, null, 5),
				new FieldSpecCategorical(CCLFData.BENE_EQTBL_BIC_HICN_NUM, BENE_EQTBL_BIC_HICN_NUM, false, null, 11),
				new FieldSpecCategorical(CCLFData.PRVDR_OSCAR_NUM, PRVDR_OSCAR_NUM, false, null, 6),
				new FieldSpecContinuousDateTime(CCLFData.CLM_FROM_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime(CCLFData.CLM_THRU_DT, DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousNumeric(CCLFData.CLM_LINE_SRVC_UNIT_QTY, new DistUniform(-999999999999.9999, 999999999999.9999), 4, false, null, 24, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecContinuousNumeric(CCLFData.CLM_LINE_CVRD_PD_AMT, new DistUniform(0, 99999999.99), 2, false, "{0:f2}", 17, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecCategorical(CCLFData.HCPCS_1_MDFR_CD, CCLFData.LIST_HCPCS_CPT_MOD_CD, false, null, 2),
				new FieldSpecCategorical(CCLFData.HCPCS_2_MDFR_CD, CCLFData.LIST_HCPCS_CPT_MOD_CD, false, null, 2),
				new FieldSpecCategorical(CCLFData.HCPCS_3_MDFR_CD, CCLFData.LIST_HCPCS_CPT_MOD_CD, false, null, 2),
				new FieldSpecCategorical(CCLFData.HCPCS_4_MDFR_CD, CCLFData.LIST_HCPCS_CPT_MOD_CD, false, null, 2),
				new FieldSpecCategorical(CCLFData.HCPCS_5_MDFR_CD, CCLFData.LIST_HCPCS_CPT_MOD_CD, false, null, 2)
			};
		}
	}
}
