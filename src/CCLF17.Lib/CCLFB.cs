using System;
using System.Collections.Generic;
using SynDataFileGen.Lib;
using pelazem.util;

namespace CCLF17.Lib
{
	public class CCLFBSpecs
	{
		public static List<IFieldSpec> GetFieldSpecs()
		{
			return new List<IFieldSpec>()
			{
				new FieldSpecContinuousNumeric(CCLFData.CUR_CLM_UNIQ_ID, new DistIncrementing(1000000, 1), 0, false, null, 13, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecContinuousNumeric(CCLFData.CLM_LINE_NUM, new DistIncrementing(1, 1), 0, false, null, 10, Util.Location.AtStart, Util.Location.AtEnd, '0', null, null),
				new FieldSpecDynamic(CCLFData.BENE_HIC_NUM, () => "HICN" + RNG.GetUniform(1000000, 9999999).ToString(), false, null, 11, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.CLM_TYPE_CD, CCLFData.LIST_CLM_TYPE_CD, false, null, 2, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.CLM_LINE_NGACO_PBPMT_SW, CCLFData.LIST_YN, false, null, 1, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.CLM_LINE_NGACO_PDSCHRG_HCBS_SW, CCLFData.LIST_YN, false, null, 1, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.CLM_LINE_NGACO_SNF_WVR_SW, CCLFData.LIST_YN, false, null, 1, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.CLM_LINE_NGACO_TLHLTH_SW, CCLFData.LIST_YN, false, null, 1, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecCategorical(CCLFData.CLM_LINE_NGACO_CPTATN_SW, CCLFData.LIST_YN, false, null, 1, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecDynamic(CCLFData.CLM_DEMO_1ST_NUM, () => "N1", false, null, 2, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecDynamic(CCLFData.CLM_DEMO_2ND_NUM, () => "N2", false, null, 2, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecDynamic(CCLFData.CLM_DEMO_3RD_NUM, () => "N3", false, null, 2, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecDynamic(CCLFData.CLM_DEMO_4TH_NUM, () => "N4", false, null, 2, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecDynamic(CCLFData.CLM_DEMO_5TH_NUM, () => "N5", false, null, 2, Util.Location.AtStart, Util.Location.AtEnd, null, null, null),
				new FieldSpecContinuousNumeric(CCLFData.CLM_PBP_INCLSN_AMT, new DistUniform(-999999.99, 999999.99), 2, false, "{0:f2}", 19, Util.Location.AtStart, Util.Location.AtEnd, '0', null, null),
				new FieldSpecContinuousNumeric(CCLFData.CLM_PBP_RDCTN_AMT, new DistUniform(-999999.99, 999999.99), 2, false, "{0:f2}", 19, Util.Location.AtStart, Util.Location.AtEnd, '0', null, null)
			};
		}

	}
}
