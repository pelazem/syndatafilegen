using System;
using System.Collections.Generic;
using SynDataFileGen.Lib;
using pelazem.util;

namespace CCLF17.Lib
{
	public class CCLFB
	{
		public Int64 CUR_CLM_UNIQ_ID { get; set; }
		public Int64 CLM_LINE_NUM { get; set; }
		public string BENE_HIC_NUM { get; set; }
		public int CLM_TYPE_CD { get; set; }
		public string CLM_LINE_NGACO_PBPMT_SW { get; set; }
		public string CLM_LINE_NGACO_PDSCHRG_HCBS_SW { get; set; }
		public string CLM_LINE_NGACO_SNF_WVR_SW { get; set; }
		public string CLM_LINE_NGACO_TLHLTH_SW { get; set; }
		public string CLM_LINE_NGACO_CPTATN_SW { get; set; }
		public string CLM_DEMO_1ST_NUM { get; set; }
		public string CLM_DEMO_2ND_NUM { get; set; }
		public string CLM_DEMO_3RD_NUM { get; set; }
		public string CLM_DEMO_4TH_NUM { get; set; }
		public string CLM_DEMO_5TH_NUM { get; set; }
		public double CLM_PBP_INCLSN_AMT { get; set; }
		public double CLM_PBP_RDCTN_AMT { get; set; }
	}

	public class CCLFBSpecs
	{
		public static List<IFieldSpec> GetFieldSpecs()
		{
			return new List<IFieldSpec>()
			{
				new FieldSpecContinuousNumeric(nameof(CCLFB.CUR_CLM_UNIQ_ID), new DistIncrementing(1000000, 1), 0, false, null, 13),
				new FieldSpecContinuousNumeric(nameof(CCLFB.CLM_LINE_NUM), new DistIncrementing(1, 1), 0, false, null, 10, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecDynamic(nameof(CCLFB.BENE_HIC_NUM), () => "HICN" + RNG.GetUniform(1000000, 9999999).ToString(), false, null, 11),
				new FieldSpecCategorical(nameof(CCLFB.CLM_TYPE_CD), CCLFData.LIST_CLM_TYPE_CD, false, null, 2),
				new FieldSpecCategorical(nameof(CCLFB.CLM_LINE_NGACO_PBPMT_SW), CCLFData.LIST_YN, false, null, 1),
				new FieldSpecCategorical(nameof(CCLFB.CLM_LINE_NGACO_PDSCHRG_HCBS_SW), CCLFData.LIST_YN, false, null, 1),
				new FieldSpecCategorical(nameof(CCLFB.CLM_LINE_NGACO_SNF_WVR_SW), CCLFData.LIST_YN, false, null, 1),
				new FieldSpecCategorical(nameof(CCLFB.CLM_LINE_NGACO_TLHLTH_SW), CCLFData.LIST_YN, false, null, 1),
				new FieldSpecCategorical(nameof(CCLFB.CLM_LINE_NGACO_CPTATN_SW), CCLFData.LIST_YN, false, null, 1),
				new FieldSpecDynamic(nameof(CCLFB.CLM_DEMO_1ST_NUM), () => "N1", false, null, 2),
				new FieldSpecDynamic(nameof(CCLFB.CLM_DEMO_2ND_NUM), () => "N2", false, null, 2),
				new FieldSpecDynamic(nameof(CCLFB.CLM_DEMO_3RD_NUM), () => "N3", false, null, 2),
				new FieldSpecDynamic(nameof(CCLFB.CLM_DEMO_4TH_NUM), () => "N4", false, null, 2),
				new FieldSpecDynamic(nameof(CCLFB.CLM_DEMO_5TH_NUM), () => "N5", false, null, 2),
				new FieldSpecContinuousNumeric(nameof(CCLFB.CLM_PBP_INCLSN_AMT), new DistUniform(-999999.99, 999999.99), 2, false, "{0:f2}", 19, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecContinuousNumeric(nameof(CCLFB.CLM_PBP_RDCTN_AMT), new DistUniform(-999999.99, 999999.99), 2, false, "{0:f2}", 19, Util.Location.AtStart, Util.Location.AtEnd, '0')
			};
		}

	}
}
