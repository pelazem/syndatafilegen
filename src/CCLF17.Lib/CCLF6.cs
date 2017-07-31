using System;
using System.Collections.Generic;
using SynDataFileGen.Lib;
using pelazem.util;

namespace CCLF17.Lib
{
	public class CCLF6Specs
	{
		public static List<IFieldSpec> GetFieldSpecs
		(
			List<Category> CUR_CLM_UNIQ_ID,
			List<Category> BENE_HIC_NUM,
			List<Category> BENE_EQTBL_BIC_HICN_NUM
		)
		{
			return new List<IFieldSpec>()
			{
				new FieldSpecCategorical(CCLFData.CUR_CLM_UNIQ_ID, CUR_CLM_UNIQ_ID, false, null, 13),
				new FieldSpecContinuousNumeric(CCLFData.CLM_LINE_NUM, new DistIncrementing(1, 1), 0, false, null, 10, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecCategorical(CCLFData.BENE_HIC_NUM, BENE_HIC_NUM, false, null, 11),
				new FieldSpecCategorical(CCLFData.CLM_TYPE_CD, CCLFData.LIST_CCLF6_CLM_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime(CCLFData.CLM_FROM_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime(CCLFData.CLM_THRU_DT, DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical(CCLFData.CLM_FED_TYPE_SRVC_CD, CCLFData.LIST_CLM_FED_TYPE_SRVC_CD, false, null, 1),
				new FieldSpecCategorical(CCLFData.CLM_POS_CD, CCLFData.LIST_CLM_POS_CD, false, null, 2),
				new FieldSpecContinuousDateTime(CCLFData.CLM_LINE_FROM_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime(CCLFData.CLM_LINE_THRU_DT, DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical(CCLFData.CLM_LINE_HCPCS_CD, CCLFData.LIST_HCPCS_CD, false, null, 5),
				new FieldSpecContinuousNumeric(CCLFData.CLM_LINE_CVRD_PD_AMT, new DistUniform(0, 99999999.99), 2, false, "{0:f2}", 15, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecCategorical(CCLFData.CLM_PRMRY_PYR_CD, CCLFData.LIST_PRMRY_PYR_CD, false, null, 1),
				new FieldSpecDynamic(CCLFData.PAYTO_PRVDR_NPI_NUM, () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 10),
				new FieldSpecDynamic(CCLFData.ORDRG_PRVDR_NPI_NUM, () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 10),
				new FieldSpecCategorical(CCLFData.CLM_CARR_PMT_DNL_CD, CCLFData.LIST_CLM_CARR_PMT_DNL_CD, false, null, 2),
				new FieldSpecCategorical(CCLFData.CLM_PRCSG_IND_CD, CCLFData.LIST_CLM_PRCSG_IND_CD, false, null, 2),
				new FieldSpecCategorical(CCLFData.CLM_ADJSMT_TYPE_CD, CCLFData.LIST_CLM_ADJSMT_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime(CCLFData.CLM_EFCTV_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime(CCLFData.CLM_IDR_LD_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecDynamic(CCLFData.CLM_CNTL_NUM, () => Guid.NewGuid().ToString(), false, null, 40),
				new FieldSpecCategorical(CCLFData.BENE_EQTBL_BIC_HICN_NUM, BENE_EQTBL_BIC_HICN_NUM, false, null, 11),
				new FieldSpecContinuousNumeric(CCLFData.CLM_LINE_ALOWD_CHRG_AMT, new DistUniform(0, 999999999999.99), 2, false, "{0:f2}", 17, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecCategorical(CCLFData.CLM_DISP_CD, CCLFData.LIST_CLM_DISP_CD, false, null, 2)
			};
		}
	}
}
