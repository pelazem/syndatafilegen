using System;
using System.Collections.Generic;
using SynDataFileGen.Lib;
using pelazem.util;

namespace CCLF17.Lib
{
	public class CCLF1Specs
	{
		public static List<IFieldSpec> GetFieldSpecs(List<Category> BENE_HIC_NUM)
		{
			return new List<IFieldSpec>()
			{
				new FieldSpecContinuousNumeric(CCLFData.CUR_CLM_UNIQ_ID, new DistIncrementing(1000000, 1), 0, false, null, 13, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecDynamic(CCLFData.PRVDR_OSCAR_NUM, () => RNG.GetUniform(100000, 999999).ToString(), false, null, 6, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.BENE_HIC_NUM, BENE_HIC_NUM, false, null, 11, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.CLM_TYPE_CD, CCLFData.LIST_CLM_TYPE_CD, false, null, 2, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecContinuousDateTime(CCLFData.CLM_FROM_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecContinuousDateTime(CCLFData.CLM_THRU_DT, DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.CLM_BILL_FAC_TYPE_CD, CCLFData.LIST_CLM_BILL_FAC_TYPE_CD,false, null, 1, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.CLM_BILL_CLSFCTN_CD, CCLFData.LIST_CLM_BILL_CLSFCTN_CD,false, null, 1, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecDynamic(CCLFData.PRNCPL_DGNS_CD, () => CCLFGenerator.GetICD10Code(),false, null, 7, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecDynamic(CCLFData.ADMTG_DGNS_CD, () => CCLFGenerator.GetICD10Code(),false, null, 7, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.CLM_MDCR_NPMT_RSN_CD, CCLFData.LIST_CLM_MDCR_NPMT_RSN_CD,false, null, 2, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecContinuousNumeric(CCLFData.CLM_PMT_AMT, new DistUniform(0, 99999999.99), 2, false, "{0:f2}", 17, Util.Location.AtStart, Util.Location.AtEnd, '0', null, null),
				new FieldSpecCategorical(CCLFData.CLM_NCH_PRMRY_PYR_CD, CCLFData.LIST_CLM_NCH_PRMRY_PYR_CD, false, null, 1, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.PRVDR_FAC_FIPS_ST_CD, CCLFData.LIST_FIPS_STATE_CD, false, null, 2, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.BENE_PTNT_STUS_CD, CCLFData.LIST_STUS_CD, false, null, 2, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.DGNS_DRG_CD, CCLFData.LIST_DGNS_DRG_CD, false, null, 4, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.CLM_OP_SRVC_TYPE_CD, CCLFData.LIST_CLM_OP_SRVC_TYPE_CD, false, null, 1, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecDynamic(CCLFData.FAC_PRVDR_NPI_NUM, () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 10, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecDynamic(CCLFData.OPRTG_PRVDR_NPI_NUM, () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 10, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecDynamic(CCLFData.ATNDG_PRVDR_NPI_NUM, () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 10, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecDynamic(CCLFData.OTHR_PRVDR_NPI_NUM, () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 10, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.CLM_ADJSMT_TYPE_CD, CCLFData.LIST_CLM_ADJSMT_TYPE_CD, false, null, 2, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecContinuousDateTime(CCLFData.CLM_EFCTV_DT, DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecContinuousDateTime(CCLFData.CLM_IDR_LD_DT, DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecDynamic(CCLFData.BENE_EQTBL_BIC_HICN_NUM, () => "HICN" + RNG.GetUniform(1000000, 9999999).ToString(), false, null, 11, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.CLM_ADMSN_TYPE_CD, CCLFData.LIST_CLM_ADMSN_TYPE_CD, false, null, 2, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.CLM_BILL_FREQ_CD, CCLFData.LIST_CLM_BILL_FREQ_CD, false, null, 1, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.CLM_QUERY_CD, CCLFData.LIST_CLM_QUERY_CD, false, null, 1, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.DGNS_PRCDR_ICD_IND, CCLFData.LIST_DGNS_PRCDR_ICD_IND, false, null, 1, Util.Location.AtStart, Util.Location.AtEnd, null, null, null)
			};
		}
	}
}
