using System;
using System.Collections.Generic;
using SynDataFileGen.Lib;
using pelazem.util;

namespace CCLF17.Lib
{
	public class CCLF5
	{
		public Int64 CUR_CLM_UNIQ_ID { get; set; }
		public Int64 CLM_LINE_NUM { get; set; }
		public string BENE_HIC_NUM { get; set; }
		public int CLM_TYPE_CD { get; set; }
		public DateTime CLM_FROM_DT { get; set; }
		public DateTime CLM_THRU_DT { get; set; }
		public string RNDRG_PRVDR_TYPE_CD { get; set; }
		public string RNDRG_PRVDR_FIPS_ST_CD { get; set; }
		public string CLM_PRVDR_SPCLTY_CD { get; set; }
		public string CLM_FED_TYPE_SRVC_CD { get; set; }
		public string CLM_POS_CD { get; set; }
		public DateTime CLM_LINE_FROM_DT { get; set; }
		public DateTime CLM_LINE_THRU_DT { get; set; }
		public string CLM_LINE_HCPCS_CD { get; set; }
		public string CLM_LINE_CVRD_PD_AMT { get; set; }
		public string CLM_LINE_PRMRY_PYR_CD { get; set; }
		public string CLM_LINE_DGNS_CD { get; set; }
		public string CLM_RNDRG_PRVDR_TAX_NUM { get; set; }
		public string RNDRG_PRVDR_NPI_NUM { get; set; }
		public string CLM_CARR_PMT_DNL_CD { get; set; }
		public string CLM_PRCSG_IND_CD { get; set; }
		public string CLM_ADJSMT_TYPE_CD { get; set; }
		public DateTime CLM_EFCTV_DT { get; set; }
		public DateTime CLM_IDR_LD_DT { get; set; }
		public string CLM_CNTL_NUM { get; set; }
		public string BENE_EQTBL_BIC_HICN_NUM { get; set; }
		public string CLM_LINE_ALOWD_CHRG_AMT { get; set; }
		public string CLM_LINE_SRVC_UNIT_QTY { get; set; }
		public string HCPCS_1_MDFR_CD { get; set; }
		public string HCPCS_2_MDFR_CD { get; set; }
		public string HCPCS_3_MDFR_CD { get; set; }
		public string HCPCS_4_MDFR_CD { get; set; }
		public string HCPCS_5_MDFR_CD { get; set; }
		public string CLM_DISP_CD { get; set; }
		public string CLM_DGNS_1_CD { get; set; }
		public string CLM_DGNS_2_CD { get; set; }
		public string CLM_DGNS_3_CD { get; set; }
		public string CLM_DGNS_4_CD { get; set; }
		public string CLM_DGNS_5_CD { get; set; }
		public string CLM_DGNS_6_CD { get; set; }
		public string CLM_DGNS_7_CD { get; set; }
		public string CLM_DGNS_8_CD { get; set; }
		public string DGNS_PRCDR_ICD_IND { get; set; }
	}

	public class CCLF5Specs
	{
		public static List<IFieldSpec> GetFieldSpecs
		(
			List<Category> CUR_CLM_UNIQ_ID,
			List<Category> BENE_HIC_NUM,
			List<Category> BENE_EQTBL_BIC_HICN_NUM,
			List<Category> PRNCPL_DGNS_CD
		)
		{
			return new List<IFieldSpec>()
			{
				new FieldSpecCategorical(nameof(CCLF5.CUR_CLM_UNIQ_ID), CUR_CLM_UNIQ_ID, false, null, 13),
				new FieldSpecContinuousNumeric(nameof(CCLF5.CLM_LINE_NUM), new DistIncrementing(1, 1), 0, false, null, 10, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecCategorical(nameof(CCLF5.BENE_HIC_NUM), BENE_HIC_NUM, false, null, 11),
				new FieldSpecCategorical(nameof(CCLF5.CLM_TYPE_CD), CCLFData.LIST_CCLF5_CLM_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime(nameof(CCLF5.CLM_FROM_DT), DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime(nameof(CCLF5.CLM_THRU_DT), DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical(nameof(CCLF5.RNDRG_PRVDR_TYPE_CD), CCLFData.LIST_RNDRG_PRVDR_TYPE_CD, false, null, 3),
				new FieldSpecCategorical(nameof(CCLF5.RNDRG_PRVDR_FIPS_ST_CD), CCLFData.LIST_FIPS_STATE_CD, false, null, 2),
				new FieldSpecCategorical(nameof(CCLF5.CLM_PRVDR_SPCLTY_CD), CCLFData.LIST_CLM_PRVDR_SPCLTY_CD, false, null, 2),
				new FieldSpecCategorical(nameof(CCLF5.CLM_FED_TYPE_SRVC_CD), CCLFData.LIST_CLM_FED_TYPE_SRVC_CD, false, null, 1),
				new FieldSpecCategorical(nameof(CCLF5.CLM_POS_CD), CCLFData.LIST_CLM_POS_CD, false, null, 2),
				new FieldSpecContinuousDateTime(nameof(CCLF5.CLM_LINE_FROM_DT), DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime(nameof(CCLF5.CLM_LINE_THRU_DT), DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical(nameof(CCLF5.CLM_LINE_HCPCS_CD), CCLFData.LIST_HCPCS_CD, false, null, 5),
				new FieldSpecContinuousNumeric(nameof(CCLF5.CLM_LINE_CVRD_PD_AMT), new DistUniform(0, 99999999.99), 2, false, "{0:f2}", 15, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecCategorical(nameof(CCLF5.CLM_LINE_PRMRY_PYR_CD), CCLFData.LIST_PRMRY_PYR_CD, false, null, 1),
				new FieldSpecCategorical(nameof(CCLF5.CLM_LINE_DGNS_CD), PRNCPL_DGNS_CD, false, null, 7),
				new FieldSpecDynamic(nameof(CCLF5.CLM_RNDRG_PRVDR_TAX_NUM), () => RNG.GetUniform(100000000, 9999999999).ToString(), false, null, 10),
				new FieldSpecDynamic(nameof(CCLF5.RNDRG_PRVDR_NPI_NUM), () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 10),
				new FieldSpecCategorical(nameof(CCLF5.CLM_CARR_PMT_DNL_CD), CCLFData.LIST_CLM_CARR_PMT_DNL_CD, false, null, 2),
				new FieldSpecCategorical(nameof(CCLF5.CLM_PRCSG_IND_CD), CCLFData.LIST_CLM_PRCSG_IND_CD, false, null, 2),
				new FieldSpecCategorical(nameof(CCLF5.CLM_ADJSMT_TYPE_CD), CCLFData.LIST_CLM_ADJSMT_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime(nameof(CCLF5.CLM_EFCTV_DT), DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime(nameof(CCLF5.CLM_IDR_LD_DT), DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecDynamic(nameof(CCLF5.CLM_CNTL_NUM), () => Guid.NewGuid().ToString(), false, null, 40),
				new FieldSpecCategorical(nameof(CCLF5.BENE_EQTBL_BIC_HICN_NUM), BENE_EQTBL_BIC_HICN_NUM, false, null, 11),
				new FieldSpecContinuousNumeric(nameof(CCLF5.CLM_LINE_ALOWD_CHRG_AMT), new DistUniform(0, 999999999999.99), 2, false, "{0:f2}", 17, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecContinuousNumeric(nameof(CCLF5.CLM_LINE_SRVC_UNIT_QTY), new DistUniform(0, 999999999999.9999), 4, false, null, 24),
				new FieldSpecCategorical(nameof(CCLF5.HCPCS_1_MDFR_CD), CCLFData.LIST_HCPCS_CPT_MOD_CD, false, null, 2),
				new FieldSpecCategorical(nameof(CCLF5.HCPCS_2_MDFR_CD), CCLFData.LIST_HCPCS_CPT_MOD_CD, false, null, 2),
				new FieldSpecCategorical(nameof(CCLF5.HCPCS_3_MDFR_CD), CCLFData.LIST_HCPCS_CPT_MOD_CD, false, null, 2),
				new FieldSpecCategorical(nameof(CCLF5.HCPCS_4_MDFR_CD), CCLFData.LIST_HCPCS_CPT_MOD_CD, false, null, 2),
				new FieldSpecCategorical(nameof(CCLF5.HCPCS_5_MDFR_CD), CCLFData.LIST_HCPCS_CPT_MOD_CD, false, null, 2),
				new FieldSpecCategorical(nameof(CCLF5.CLM_DISP_CD), CCLFData.LIST_CLM_DISP_CD, false, null, 2),
				new FieldSpecDynamic(nameof(CCLF5.CLM_DGNS_1_CD), () => CCLFGenerator.GetICD10Code(), false, null, 7),
				new FieldSpecDynamic(nameof(CCLF5.CLM_DGNS_2_CD), () => CCLFGenerator.GetICD10Code(), false, null, 7),
				new FieldSpecDynamic(nameof(CCLF5.CLM_DGNS_3_CD), () => CCLFGenerator.GetICD10Code(), false, null, 7),
				new FieldSpecDynamic(nameof(CCLF5.CLM_DGNS_4_CD), () => CCLFGenerator.GetICD10Code(), false, null, 7),
				new FieldSpecDynamic(nameof(CCLF5.CLM_DGNS_5_CD), () => CCLFGenerator.GetICD10Code(), false, null, 7),
				new FieldSpecDynamic(nameof(CCLF5.CLM_DGNS_6_CD), () => CCLFGenerator.GetICD10Code(), false, null, 7),
				new FieldSpecDynamic(nameof(CCLF5.CLM_DGNS_7_CD), () => CCLFGenerator.GetICD10Code(), false, null, 7),
				new FieldSpecDynamic(nameof(CCLF5.CLM_DGNS_8_CD), () => CCLFGenerator.GetICD10Code(), false, null, 7),
				new FieldSpecDynamic(nameof(CCLF5.DGNS_PRCDR_ICD_IND), () => "0", false, null, 1)
			};
		}
	}
}
