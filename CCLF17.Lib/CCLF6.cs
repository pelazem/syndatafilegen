using System;
using System.Collections.Generic;
using SynDataFileGen.Lib;
using pelazem.util;

namespace CCLF17.Lib
{
	public class CCLF6
	{
		public Int64 CUR_CLM_UNIQ_ID { get; set; }
		public Int64 CLM_LINE_NUM { get; set; }
		public string BENE_HIC_NUM { get; set; }
		public int CLM_TYPE_CD { get; set; }
		public DateTime CLM_FROM_DT { get; set; }
		public DateTime CLM_THRU_DT { get; set; }
		public string CLM_FED_TYPE_SRVC_CD { get; set; }
		public string CLM_POS_CD { get; set; }
		public DateTime CLM_LINE_FROM_DT { get; set; }
		public DateTime CLM_LINE_THRU_DT { get; set; }
		public string CLM_LINE_HCPCS_CD { get; set; }
		public double CLM_LINE_CVRD_PD_AMT { get; set; }
		public string CLM_PRMRY_PYR_CD { get; set; }
		public string PAYTO_PRVDR_NPI_NUM { get; set; }
		public string ORDRG_PRVDR_NPI_NUM { get; set; }
		public string CLM_CARR_PMT_DNL_CD { get; set; }
		public string CLM_PRCSG_IND_CD { get; set; }
		public string CLM_ADJSMT_TYPE_CD { get; set; }
		public DateTime CLM_EFCTV_DT { get; set; }
		public DateTime CLM_IDR_LD_DT { get; set; }
		public string CLM_CNTL_NUM { get; set; }
		public string BENE_EQTBL_BIC_HICN_NUM { get; set; }
		public double CLM_LINE_ALOWD_CHRG_AMT { get; set; }
		public string CLM_DISP_CD { get; set; }
	}

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
				new FieldSpecCategorical(nameof(CCLF6.CUR_CLM_UNIQ_ID), CUR_CLM_UNIQ_ID, false, null, 13),
				new FieldSpecContinuousNumeric(nameof(CCLF6.CLM_LINE_NUM), new DistIncrementing(1, 1), 0, false, null, 10, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecCategorical(nameof(CCLF6.BENE_HIC_NUM), BENE_HIC_NUM, false, null, 11),
				new FieldSpecCategorical(nameof(CCLF6.CLM_TYPE_CD), CCLFData.LIST_CCLF6_CLM_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime(nameof(CCLF6.CLM_FROM_DT), DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime(nameof(CCLF6.CLM_THRU_DT), DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical(nameof(CCLF6.CLM_FED_TYPE_SRVC_CD), CCLFData.LIST_CLM_FED_TYPE_SRVC_CD, false, null, 1),
				new FieldSpecCategorical(nameof(CCLF6.CLM_POS_CD), CCLFData.LIST_CLM_POS_CD, false, null, 2),
				new FieldSpecContinuousDateTime(nameof(CCLF6.CLM_LINE_FROM_DT), DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime(nameof(CCLF6.CLM_LINE_THRU_DT), DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical(nameof(CCLF6.CLM_LINE_HCPCS_CD), CCLFData.LIST_HCPCS_CD, false, null, 5),
				new FieldSpecContinuousNumeric(nameof(CCLF6.CLM_LINE_CVRD_PD_AMT), new DistUniform(0, 99999999.99), 2, false, "{0:f2}", 15, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecCategorical(nameof(CCLF6.CLM_PRMRY_PYR_CD), CCLFData.LIST_PRMRY_PYR_CD, false, null, 1),
				new FieldSpecDynamic(nameof(CCLF6.PAYTO_PRVDR_NPI_NUM), () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 10),
				new FieldSpecDynamic(nameof(CCLF6.ORDRG_PRVDR_NPI_NUM), () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 10),
				new FieldSpecCategorical(nameof(CCLF6.CLM_CARR_PMT_DNL_CD), CCLFData.LIST_CLM_CARR_PMT_DNL_CD, false, null, 2),
				new FieldSpecCategorical(nameof(CCLF6.CLM_PRCSG_IND_CD), CCLFData.LIST_CLM_PRCSG_IND_CD, false, null, 2),
				new FieldSpecCategorical(nameof(CCLF6.CLM_ADJSMT_TYPE_CD), CCLFData.LIST_CLM_ADJSMT_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime(nameof(CCLF6.CLM_EFCTV_DT), DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime(nameof(CCLF6.CLM_IDR_LD_DT), DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecDynamic(nameof(CCLF6.CLM_CNTL_NUM), () => Guid.NewGuid().ToString(), false, null, 40),
				new FieldSpecCategorical(nameof(CCLF6.BENE_EQTBL_BIC_HICN_NUM), BENE_EQTBL_BIC_HICN_NUM, false, null, 11),
				new FieldSpecContinuousNumeric(nameof(CCLF6.CLM_LINE_ALOWD_CHRG_AMT), new DistUniform(0, 999999999999.99), 2, false, "{0:f2}", 17, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecCategorical(nameof(CCLF6.CLM_DISP_CD), CCLFData.LIST_CLM_DISP_CD, false, null, 2)
			};
		}
	}
}
