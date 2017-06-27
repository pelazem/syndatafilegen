using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Generator;
using pelazem.Common;

namespace CCLF
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

	public class CCLFAProps
	{
		public static readonly PropertyInfo CUR_CLM_UNIQ_ID = TypeHelper.GetProperty<CCLFA>(x => x.CUR_CLM_UNIQ_ID);
		public static readonly PropertyInfo BENE_HIC_NUM = TypeHelper.GetProperty<CCLFA>(x => x.BENE_HIC_NUM);
		public static readonly PropertyInfo CLM_TYPE_CD = TypeHelper.GetProperty<CCLFA>(x => x.CLM_TYPE_CD);
		public static readonly PropertyInfo CLM_ACTV_CARE_FROM_DT = TypeHelper.GetProperty<CCLFA>(x => x.CLM_ACTV_CARE_FROM_DT);
		public static readonly PropertyInfo CLM_NGACO_PBPMT_SW = TypeHelper.GetProperty<CCLFA>(x => x.CLM_NGACO_PBPMT_SW);
		public static readonly PropertyInfo CLM_NGACO_PDSCHRG_HCBS_SW = TypeHelper.GetProperty<CCLFA>(x => x.CLM_NGACO_PDSCHRG_HCBS_SW);
		public static readonly PropertyInfo CLM_NGACO_SNF_WVR_SW = TypeHelper.GetProperty<CCLFA>(x => x.CLM_NGACO_SNF_WVR_SW);
		public static readonly PropertyInfo CLM_NGACO_TLHLTH_SW = TypeHelper.GetProperty<CCLFA>(x => x.CLM_NGACO_TLHLTH_SW);
		public static readonly PropertyInfo CLM_NGACO_CPTATN_SW = TypeHelper.GetProperty<CCLFA>(x => x.CLM_NGACO_CPTATN_SW);
		public static readonly PropertyInfo CLM_DEMO_1ST_NUM = TypeHelper.GetProperty<CCLFA>(x => x.CLM_DEMO_1ST_NUM);
		public static readonly PropertyInfo CLM_DEMO_2ND_NUM = TypeHelper.GetProperty<CCLFA>(x => x.CLM_DEMO_2ND_NUM);
		public static readonly PropertyInfo CLM_DEMO_3RD_NUM = TypeHelper.GetProperty<CCLFA>(x => x.CLM_DEMO_3RD_NUM);
		public static readonly PropertyInfo CLM_DEMO_4TH_NUM = TypeHelper.GetProperty<CCLFA>(x => x.CLM_DEMO_4TH_NUM);
		public static readonly PropertyInfo CLM_DEMO_5TH_NUM = TypeHelper.GetProperty<CCLFA>(x => x.CLM_DEMO_5TH_NUM);
		public static readonly PropertyInfo CLM_PBP_INCLSN_AMT = TypeHelper.GetProperty<CCLFA>(x => x.CLM_PBP_INCLSN_AMT);
		public static readonly PropertyInfo CLM_PBP_RDCTN_AMT = TypeHelper.GetProperty<CCLFA>(x => x.CLM_PBP_RDCTN_AMT);
	}

	public class CCLFASpecs
	{
		public static List<IFieldSpec<CCLFA>> GetFieldSpecs(List<Category> CUR_CLM_UNIQ_ID, DateTime dateStartClaimAdmission, DateTime dateEndClaimAdmission)
		{
			return new List<IFieldSpec<CCLFA>>()
			{
				new FieldSpecCategorical<CCLFA>(CCLFAProps.CUR_CLM_UNIQ_ID, CUR_CLM_UNIQ_ID, false, null, 13),
				new FieldSpecDynamic<CCLFA>(CCLFAProps.BENE_HIC_NUM, () => "HICN" + RNG.GetUniform(1000000, 9999999).ToString(), false, null, 11),
				new FieldSpecCategorical<CCLFA>(CCLFAProps.CLM_TYPE_CD, CCLFData.LIST_CLM_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime<CCLFA>(CCLFAProps.CLM_ACTV_CARE_FROM_DT, dateStartClaimAdmission, dateEndClaimAdmission, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical<CCLFA>(CCLFAProps.CLM_NGACO_PBPMT_SW, CCLFData.LIST_YN, false, null, 1),
				new FieldSpecCategorical<CCLFA>(CCLFAProps.CLM_NGACO_PDSCHRG_HCBS_SW, CCLFData.LIST_YN, false, null, 1),
				new FieldSpecCategorical<CCLFA>(CCLFAProps.CLM_NGACO_SNF_WVR_SW, CCLFData.LIST_YN, false, null, 1),
				new FieldSpecCategorical<CCLFA>(CCLFAProps.CLM_NGACO_TLHLTH_SW, CCLFData.LIST_YN, false, null, 1),
				new FieldSpecCategorical<CCLFA>(CCLFAProps.CLM_NGACO_CPTATN_SW, CCLFData.LIST_YN, false, null, 1),
				new FieldSpecDynamic<CCLFA>(CCLFAProps.CLM_DEMO_1ST_NUM, () => "N1", false, null, 2),
				new FieldSpecDynamic<CCLFA>(CCLFAProps.CLM_DEMO_2ND_NUM, () => "N2", false, null, 2),
				new FieldSpecDynamic<CCLFA>(CCLFAProps.CLM_DEMO_3RD_NUM, () => "N3", false, null, 2),
				new FieldSpecDynamic<CCLFA>(CCLFAProps.CLM_DEMO_4TH_NUM, () => "N4", false, null, 2),
				new FieldSpecDynamic<CCLFA>(CCLFAProps.CLM_DEMO_5TH_NUM, () => "N5", false, null, 2),
				new FieldSpecContinuousNumeric<CCLFA>(CCLFAProps.CLM_PBP_INCLSN_AMT, new DistUniform(-999999.99, 999999.99), 2, false, "{0:f2}", 19, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecContinuousNumeric<CCLFA>(CCLFAProps.CLM_PBP_RDCTN_AMT, new DistUniform(-999999.99, 999999.99), 2, false, "{0:f2}", 19, Util.Location.AtStart, Util.Location.AtEnd, '0')
			};
		}
	}
}
