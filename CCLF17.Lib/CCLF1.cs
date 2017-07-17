using System;
using System.Collections.Generic;
using SynDataFileGen.Lib;
using pelazem.util;

namespace CCLF17.Lib
{
	public class CCLF1
	{
		public Int64 CUR_CLM_UNIQ_ID { get; set; }
		public string PRVDR_OSCAR_NUM { get; set; }
		public string BENE_HIC_NUM { get; set; }
		public int CLM_TYPE_CD { get; set; }
		public DateTime CLM_FROM_DT { get; set; }
		public DateTime CLM_THRU_DT { get; set; }
		public int CLM_BILL_FAC_TYPE_CD { get; set; }
		public string CLM_BILL_CLSFCTN_CD { get; set; }
		public string PRNCPL_DGNS_CD { get; set; }
		public string ADMTG_DGNS_CD { get; set; }
		public string CLM_MDCR_NPMT_RSN_CD { get; set; }
		public double CLM_PMT_AMT { get; set; }
		public string CLM_NCH_PRMRY_PYR_CD { get; set; }
		public string PRVDR_FAC_FIPS_ST_CD { get; set; }
		public string BENE_PTNT_STUS_CD { get; set; }
		public string DGNS_DRG_CD { get; set; }
		public string CLM_OP_SRVC_TYPE_CD { get; set; }
		public string FAC_PRVDR_NPI_NUM { get; set; }
		public string OPRTG_PRVDR_NPI_NUM { get; set; }
		public string ATNDG_PRVDR_NPI_NUM { get; set; }
		public string OTHR_PRVDR_NPI_NUM { get; set; }
		public string CLM_ADJSMT_TYPE_CD { get; set; }
		public DateTime CLM_EFCTV_DT { get; set; }
		public DateTime CLM_IDR_LD_DT { get; set; }
		public string BENE_EQTBL_BIC_HICN_NUM { get; set; }
		public string CLM_ADMSN_TYPE_CD { get; set; }
		public string CLM_ADMSN_SRC_CD { get; set; }
		public string CLM_BILL_FREQ_CD { get; set; }
		public string CLM_QUERY_CD { get; set; }
		public string DGNS_PRCDR_ICD_IND { get; set; }
	}

	public class CCLF1Specs
	{
		public static List<IFieldSpec> GetFieldSpecs(List<Category> BENE_HIC_NUM)
		{
			return new List<IFieldSpec>()
			{
				new FieldSpecContinuousNumeric(nameof(CCLF1.CUR_CLM_UNIQ_ID), new DistIncrementing(1000000, 1), 0, false, null, 13),
				new FieldSpecDynamic(nameof(CCLF1.PRVDR_OSCAR_NUM), () => RNG.GetUniform(100000, 999999).ToString(), false, null, 6),
				new FieldSpecCategorical(nameof(CCLF1.BENE_HIC_NUM), BENE_HIC_NUM, false, null, 11),
				new FieldSpecCategorical(nameof(CCLF1.CLM_TYPE_CD), CCLFData.LIST_CLM_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime(nameof(CCLF1.CLM_FROM_DT), DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime(nameof(CCLF1.CLM_THRU_DT), DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical(nameof(CCLF1.CLM_BILL_FAC_TYPE_CD), CCLFData.LIST_CLM_BILL_FAC_TYPE_CD,false, null, 1),
				new FieldSpecCategorical(nameof(CCLF1.CLM_BILL_CLSFCTN_CD), CCLFData.LIST_CLM_BILL_CLSFCTN_CD,false, null, 1),
				new FieldSpecDynamic(nameof(CCLF1.PRNCPL_DGNS_CD), () => CCLFGenerator.GetICD10Code(),false, null, 7),
				new FieldSpecDynamic(nameof(CCLF1.ADMTG_DGNS_CD), () => CCLFGenerator.GetICD10Code(),false, null, 7),
				new FieldSpecCategorical(nameof(CCLF1.CLM_MDCR_NPMT_RSN_CD), CCLFData.LIST_CLM_MDCR_NPMT_RSN_CD,false, null, 2),
				new FieldSpecContinuousNumeric(nameof(CCLF1.CLM_PMT_AMT), new DistUniform(0, 99999999.99), 2, false, "{0:f2}", 17, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecCategorical(nameof(CCLF1.CLM_NCH_PRMRY_PYR_CD), CCLFData.LIST_CLM_NCH_PRMRY_PYR_CD, false, null, 1),
				new FieldSpecCategorical(nameof(CCLF1.PRVDR_FAC_FIPS_ST_CD), CCLFData.LIST_FIPS_STATE_CD, false, null, 2),
				new FieldSpecCategorical(nameof(CCLF1.BENE_PTNT_STUS_CD), CCLFData.LIST_STUS_CD, false, null, 2),
				new FieldSpecCategorical(nameof(CCLF1.DGNS_DRG_CD), CCLFData.LIST_DGNS_DRG_CD, false, null, 4),
				new FieldSpecCategorical(nameof(CCLF1.CLM_OP_SRVC_TYPE_CD), CCLFData.LIST_CLM_OP_SRVC_TYPE_CD, false, null, 1),
				new FieldSpecDynamic(nameof(CCLF1.FAC_PRVDR_NPI_NUM), () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 10),
				new FieldSpecDynamic(nameof(CCLF1.OPRTG_PRVDR_NPI_NUM), () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 10),
				new FieldSpecDynamic(nameof(CCLF1.ATNDG_PRVDR_NPI_NUM), () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 10),
				new FieldSpecDynamic(nameof(CCLF1.OTHR_PRVDR_NPI_NUM), () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 10),
				new FieldSpecCategorical(nameof(CCLF1.CLM_ADJSMT_TYPE_CD), CCLFData.LIST_CLM_ADJSMT_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime(nameof(CCLF1.CLM_EFCTV_DT), DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime(nameof(CCLF1.CLM_IDR_LD_DT), DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecDynamic(nameof(CCLF1.BENE_EQTBL_BIC_HICN_NUM), () => "HICN" + RNG.GetUniform(1000000, 9999999).ToString(), false, null, 11),
				new FieldSpecCategorical(nameof(CCLF1.CLM_ADMSN_TYPE_CD), CCLFData.LIST_CLM_ADMSN_TYPE_CD, false, null, 2),
				new FieldSpecCategorical(nameof(CCLF1.CLM_BILL_FREQ_CD), CCLFData.LIST_CLM_BILL_FREQ_CD, false, null, 1),
				new FieldSpecCategorical(nameof(CCLF1.CLM_QUERY_CD), CCLFData.LIST_CLM_QUERY_CD, false, null, 1),
				new FieldSpecCategorical(nameof(CCLF1.DGNS_PRCDR_ICD_IND), CCLFData.LIST_DGNS_PRCDR_ICD_IND, false, null, 1)
			};
		}
	}
}
