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

	public class CCLFBProps
	{
		public static readonly PropertyInfo CUR_CLM_UNIQ_ID = TypeHelper.GetProperty<CCLFB>(x => x.CUR_CLM_UNIQ_ID);
		public static readonly PropertyInfo CLM_LINE_NUM = TypeHelper.GetProperty<CCLFB>(x => x.CLM_LINE_NUM);
		public static readonly PropertyInfo BENE_HIC_NUM = TypeHelper.GetProperty<CCLFB>(x => x.BENE_HIC_NUM);
		public static readonly PropertyInfo CLM_TYPE_CD = TypeHelper.GetProperty<CCLFB>(x => x.CLM_TYPE_CD);
		public static readonly PropertyInfo CLM_LINE_NGACO_PBPMT_SW = TypeHelper.GetProperty<CCLFB>(x => x.CLM_LINE_NGACO_PBPMT_SW);
		public static readonly PropertyInfo CLM_LINE_NGACO_PDSCHRG_HCBS_SW = TypeHelper.GetProperty<CCLFB>(x => x.CLM_LINE_NGACO_PDSCHRG_HCBS_SW);
		public static readonly PropertyInfo CLM_LINE_NGACO_SNF_WVR_SW = TypeHelper.GetProperty<CCLFB>(x => x.CLM_LINE_NGACO_SNF_WVR_SW);
		public static readonly PropertyInfo CLM_LINE_NGACO_TLHLTH_SW = TypeHelper.GetProperty<CCLFB>(x => x.CLM_LINE_NGACO_TLHLTH_SW);
		public static readonly PropertyInfo CLM_LINE_NGACO_CPTATN_SW = TypeHelper.GetProperty<CCLFB>(x => x.CLM_LINE_NGACO_CPTATN_SW);
		public static readonly PropertyInfo CLM_DEMO_1ST_NUM = TypeHelper.GetProperty<CCLFB>(x => x.CLM_DEMO_1ST_NUM);
		public static readonly PropertyInfo CLM_DEMO_2ND_NUM = TypeHelper.GetProperty<CCLFB>(x => x.CLM_DEMO_2ND_NUM);
		public static readonly PropertyInfo CLM_DEMO_3RD_NUM = TypeHelper.GetProperty<CCLFB>(x => x.CLM_DEMO_3RD_NUM);
		public static readonly PropertyInfo CLM_DEMO_4TH_NUM = TypeHelper.GetProperty<CCLFB>(x => x.CLM_DEMO_4TH_NUM);
		public static readonly PropertyInfo CLM_DEMO_5TH_NUM = TypeHelper.GetProperty<CCLFB>(x => x.CLM_DEMO_5TH_NUM);
		public static readonly PropertyInfo CLM_PBP_INCLSN_AMT = TypeHelper.GetProperty<CCLFB>(x => x.CLM_PBP_INCLSN_AMT);
		public static readonly PropertyInfo CLM_PBP_RDCTN_AMT = TypeHelper.GetProperty<CCLFB>(x => x.CLM_PBP_RDCTN_AMT);

	}

	public class CCLFBSpecs
	{
		public static List<IFieldSpec<CCLFB>> GetFieldSpecs()
		{
			return new List<IFieldSpec<CCLFB>>()
			{
				new FieldSpecContinuousNumeric<CCLFB>(CCLFBProps.CUR_CLM_UNIQ_ID, new DistIncrementing(1000000, 1), 0, false, null, 13),
				new FieldSpecContinuousNumeric<CCLFB>(CCLFBProps.CLM_LINE_NUM, new DistIncrementing(1, 1), 0, false, null, 10, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecDynamic<CCLFB>(CCLFBProps.BENE_HIC_NUM, () => "HICN" + RNG.GetUniform(1000000, 9999999).ToString(), false, null, 11),
				new FieldSpecCategorical<CCLFB>(CCLFBProps.CLM_TYPE_CD, CCLFData.LIST_CLM_TYPE_CD, false, null, 2),
				new FieldSpecCategorical<CCLFB>(CCLFBProps.CLM_LINE_NGACO_PBPMT_SW, CCLFData.LIST_YN, false, null, 1),
				new FieldSpecCategorical<CCLFB>(CCLFBProps.CLM_LINE_NGACO_PDSCHRG_HCBS_SW, CCLFData.LIST_YN, false, null, 1),
				new FieldSpecCategorical<CCLFB>(CCLFBProps.CLM_LINE_NGACO_SNF_WVR_SW, CCLFData.LIST_YN, false, null, 1),
				new FieldSpecCategorical<CCLFB>(CCLFBProps.CLM_LINE_NGACO_TLHLTH_SW, CCLFData.LIST_YN, false, null, 1),
				new FieldSpecCategorical<CCLFB>(CCLFBProps.CLM_LINE_NGACO_CPTATN_SW, CCLFData.LIST_YN, false, null, 1),
				new FieldSpecDynamic<CCLFB>(CCLFBProps.CLM_DEMO_1ST_NUM, () => "N1", false, null, 2),
				new FieldSpecDynamic<CCLFB>(CCLFBProps.CLM_DEMO_2ND_NUM, () => "N2", false, null, 2),
				new FieldSpecDynamic<CCLFB>(CCLFBProps.CLM_DEMO_3RD_NUM, () => "N3", false, null, 2),
				new FieldSpecDynamic<CCLFB>(CCLFBProps.CLM_DEMO_4TH_NUM, () => "N4", false, null, 2),
				new FieldSpecDynamic<CCLFB>(CCLFBProps.CLM_DEMO_5TH_NUM, () => "N5", false, null, 2),
				new FieldSpecContinuousNumeric<CCLFB>(CCLFBProps.CLM_PBP_INCLSN_AMT, new DistUniform(-999999.99, 999999.99), 2, false, "{0:f2}", 19, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecContinuousNumeric<CCLFB>(CCLFBProps.CLM_PBP_RDCTN_AMT, new DistUniform(-999999.99, 999999.99), 2, false, "{0:f2}", 19, Util.Location.AtStart, Util.Location.AtEnd, '0')
			};
		}

	}
}
