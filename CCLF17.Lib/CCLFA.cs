using System;
using System.Collections.Generic;
using SynDataFileGen.Lib;
using pelazem.util;

namespace CCLF17.Lib
{
	public class CCLFA
	{
		public Int64 CUR_CLM_UNIQ_ID { get; set; }
		public string BENE_HIC_NUM { get; set; }
		public int CLM_TYPE_CD { get; set; }
		public DateTime CLM_ACTV_CARE_FROM_DT { get; set; }
		public string CLM_NGACO_PBPMT_SW { get; set; }
		public string CLM_NGACO_PDSCHRG_HCBS_SW { get; set; }
		public string CLM_NGACO_SNF_WVR_SW { get; set; }
		public string CLM_NGACO_TLHLTH_SW { get; set; }
		public string CLM_NGACO_CPTATN_SW { get; set; }
		public string CLM_DEMO_1ST_NUM { get; set; }
		public string CLM_DEMO_2ND_NUM { get; set; }
		public string CLM_DEMO_3RD_NUM { get; set; }
		public string CLM_DEMO_4TH_NUM { get; set; }
		public string CLM_DEMO_5TH_NUM { get; set; }
		public double CLM_PBP_INCLSN_AMT { get; set; }
		public double CLM_PBP_RDCTN_AMT { get; set; }
	}

	public class CCLFASpecs
	{
		public static List<IFieldSpec> GetFieldSpecs(List<Category> CUR_CLM_UNIQ_ID, DateTime dateStartClaimAdmission, DateTime dateEndClaimAdmission)
		{
			return new List<IFieldSpec>()
			{
				new FieldSpecCategorical(nameof(CCLFA.CUR_CLM_UNIQ_ID), CUR_CLM_UNIQ_ID, false, null, 13),
				new FieldSpecDynamic(nameof(CCLFA.BENE_HIC_NUM), () => "HICN" + RNG.GetUniform(1000000, 9999999).ToString(), false, null, 11),
				new FieldSpecCategorical(nameof(CCLFA.CLM_TYPE_CD), CCLFData.LIST_CLM_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime(nameof(CCLFA.CLM_ACTV_CARE_FROM_DT), dateStartClaimAdmission, dateEndClaimAdmission, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical(nameof(CCLFA.CLM_NGACO_PBPMT_SW), CCLFData.LIST_YN, false, null, 1),
				new FieldSpecCategorical(nameof(CCLFA.CLM_NGACO_PDSCHRG_HCBS_SW), CCLFData.LIST_YN, false, null, 1),
				new FieldSpecCategorical(nameof(CCLFA.CLM_NGACO_SNF_WVR_SW), CCLFData.LIST_YN, false, null, 1),
				new FieldSpecCategorical(nameof(CCLFA.CLM_NGACO_TLHLTH_SW), CCLFData.LIST_YN, false, null, 1),
				new FieldSpecCategorical(nameof(CCLFA.CLM_NGACO_CPTATN_SW), CCLFData.LIST_YN, false, null, 1),
				new FieldSpecDynamic(nameof(CCLFA.CLM_DEMO_1ST_NUM), () => "N1", false, null, 2),
				new FieldSpecDynamic(nameof(CCLFA.CLM_DEMO_2ND_NUM), () => "N2", false, null, 2),
				new FieldSpecDynamic(nameof(CCLFA.CLM_DEMO_3RD_NUM), () => "N3", false, null, 2),
				new FieldSpecDynamic(nameof(CCLFA.CLM_DEMO_4TH_NUM), () => "N4", false, null, 2),
				new FieldSpecDynamic(nameof(CCLFA.CLM_DEMO_5TH_NUM), () => "N5", false, null, 2),
				new FieldSpecContinuousNumeric(nameof(CCLFA.CLM_PBP_INCLSN_AMT), new DistUniform(-999999.99, 999999.99), 2, false, "{0:f2}", 19, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecContinuousNumeric(nameof(CCLFA.CLM_PBP_RDCTN_AMT), new DistUniform(-999999.99, 999999.99), 2, false, "{0:f2}", 19, Util.Location.AtStart, Util.Location.AtEnd, '0')
			};
		}
	}
}
