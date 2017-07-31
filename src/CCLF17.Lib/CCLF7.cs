using System;
using System.Collections.Generic;
using SynDataFileGen.Lib;
using pelazem.util;

namespace CCLF17.Lib
{
	public class CCLF7Specs
	{
		public static List<IFieldSpec> GetFieldSpecs
		(
			List<Category> CUR_CLM_UNIQ_ID,
			List<Category> BENE_HIC_NUM
		)
		{
			return new List<IFieldSpec>()
			{
				new FieldSpecCategorical(CCLFData.CUR_CLM_UNIQ_ID, CUR_CLM_UNIQ_ID, false, null, 13),
				new FieldSpecCategorical(CCLFData.BENE_HIC_NUM, BENE_HIC_NUM, false, null, 11),
				new FieldSpecDynamic(CCLFData.CLM_LINE_NDC_CD, () => RNG.GetUniform(1000000000, 99999999999).ToString(), false, null, 11),
				new FieldSpecCategorical(CCLFData.CLM_TYPE_CD, CCLFData.LIST_CCLF7_CLM_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime(CCLFData.CLM_LINE_FROM_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical(CCLFData.PRVDR_SRVC_ID_QLFYR_CD, CCLFData.LIST_PRVDR_SRVC_ID_QLFYR_CD, false, null, 2),
				new FieldSpecDynamic(CCLFData.CLM_SRVC_PRVDR_GNRC_ID_NUM, () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 20),
				new FieldSpecCategorical(CCLFData.CLM_DSPNSNG_STUS_CD, CCLFData.LIST_CLM_DSPNSNG_STUS_CD, false, null, 1),
				new FieldSpecCategorical(CCLFData.CLM_DAW_PROD_SLCTN_CD, CCLFData.LIST_CLM_DAW_PROD_SLCTN_CD, false, null, 1),
				new FieldSpecContinuousNumeric(CCLFData.CLM_LINE_SRVC_UNIT_QTY, new DistUniform(0, 999999999999.9999), 4, false, null, 24),
				new FieldSpecContinuousNumeric(CCLFData.CLM_LINE_DAYS_SUPLY_QTY, new DistUniform(0, 999999999), 0, false, null, 9),
				new FieldSpecCategorical(CCLFData.PRVDR_PRSBNG_ID_QLFYR_CD, CCLFData.LIST_PRVDR_PRSBNG_ID_QLFYR_CD, false, null, 2),
				new FieldSpecDynamic(CCLFData.CLM_PRSBNG_PRVDR_GNRC_ID_NUM, () => RNG.GetUniform(1000000000, 999999999999999).ToString(), false, null, 20),
				new FieldSpecContinuousNumeric(CCLFData.CLM_LINE_BENE_PMT_AMT, new DistUniform(-9999999.99, 99999999.99), 2, false, "{0:f2}", 13, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecCategorical(CCLFData.CLM_ADJSMT_TYPE_CD, CCLFData.LIST_CLM_ADJSMT_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime(CCLFData.CLM_EFCTV_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime(CCLFData.CLM_IDR_LD_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecDynamic(CCLFData.CLM_LINE_RX_SRVC_RFRNC_NUM, () => RNG.GetUniform(1000000000, 999999999999), false, null, 12),
				new FieldSpecDynamic(CCLFData.CLM_LINE_RX_FILL_NUM, () => RNG.GetUniform(10000, 999999999).ToString(), false, null, 9)
			};
		}
	}
}
