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
	public class CCLF3
	{
		public Int64 CUR_CLM_UNIQ_ID { get; set; } //
		public string BENE_HIC_NUM { get; set; } //
		public int CLM_TYPE_CD { get; set; }
		public Int64 CLM_VAL_SQNC_NUM { get; set; }
		public string CLM_PRCDR_CD { get; set; }
		public DateTime CLM_PRCDR_PRFRM_DT { get; set; }
		public string BENE_EQTBL_BIC_HICN_NUM { get; set; } //
		public string PRVDR_OSCAR_NUM { get; set; } //
		public DateTime CLM_FROM_DT { get; set; }
		public DateTime CLM_THRU_DT { get; set; }
		public string DGNS_PRCDR_ICD_IND { get; set; }
	}

	public class CCLF3Props
	{
		public static readonly PropertyInfo CUR_CLM_UNIQ_ID = TypeHelper.GetProperty<CCLF3>(x => x.CUR_CLM_UNIQ_ID);
		public static readonly PropertyInfo BENE_HIC_NUM = TypeHelper.GetProperty<CCLF3>(x => x.BENE_HIC_NUM);
		public static readonly PropertyInfo CLM_TYPE_CD = TypeHelper.GetProperty<CCLF3>(x => x.CLM_TYPE_CD);
		public static readonly PropertyInfo CLM_VAL_SQNC_NUM = TypeHelper.GetProperty<CCLF3>(x => x.CLM_VAL_SQNC_NUM);
		public static readonly PropertyInfo CLM_PRCDR_CD = TypeHelper.GetProperty<CCLF3>(x => x.CLM_PRCDR_CD);
		public static readonly PropertyInfo CLM_PRCDR_PRFRM_DT = TypeHelper.GetProperty<CCLF3>(x => x.CLM_PRCDR_PRFRM_DT);
		public static readonly PropertyInfo BENE_EQTBL_BIC_HICN_NUM = TypeHelper.GetProperty<CCLF3>(x => x.BENE_EQTBL_BIC_HICN_NUM);
		public static readonly PropertyInfo PRVDR_OSCAR_NUM = TypeHelper.GetProperty<CCLF3>(x => x.PRVDR_OSCAR_NUM);
		public static readonly PropertyInfo CLM_FROM_DT = TypeHelper.GetProperty<CCLF3>(x => x.CLM_FROM_DT);
		public static readonly PropertyInfo CLM_THRU_DT = TypeHelper.GetProperty<CCLF3>(x => x.CLM_THRU_DT);
		public static readonly PropertyInfo DGNS_PRCDR_ICD_IND = TypeHelper.GetProperty<CCLF3>(x => x.DGNS_PRCDR_ICD_IND);
	}

	public class CCLF3Specs
	{
		public static List<IFieldSpec<CCLF3>> GetFieldSpecs
		(
			List<Category>  CUR_CLM_UNIQ_ID,
			List<Category>  BENE_HIC_NUM,
			List<Category>  BENE_EQTBL_BIC_HICN_NUM,
			List<Category>  PRNCPL_DGNS_CD,
			List<Category>  PRVDR_OSCAR_NUM
		)
		{
			return new List<IFieldSpec<CCLF3>>()
			{
				new FieldSpecCategorical<CCLF3>(CCLF3Props.CUR_CLM_UNIQ_ID, CUR_CLM_UNIQ_ID, false, null, 13),
				new FieldSpecCategorical<CCLF3>(CCLF3Props.BENE_HIC_NUM, BENE_HIC_NUM, false, null, 11),
				new FieldSpecCategorical<CCLF3>(CCLF3Props.CLM_TYPE_CD, CCLFData.LIST_CLM_TYPE_CD, false, null, 2),
				new FieldSpecContinuousNumeric<CCLF3>(CCLF3Props.CLM_VAL_SQNC_NUM, new DistIncrementing(0, 1), 0, false, null, 2),
				new FieldSpecCategorical<CCLF3>(CCLF3Props.CLM_PRCDR_CD, PRNCPL_DGNS_CD, false, null, 7),
				new FieldSpecContinuousDateTime<CCLF3>(CCLF3Props.CLM_PRCDR_PRFRM_DT, DateTime.UtcNow.AddMonths(-4), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical<CCLF3>(CCLF3Props.BENE_EQTBL_BIC_HICN_NUM, BENE_EQTBL_BIC_HICN_NUM, false, null, 11),
				new FieldSpecCategorical<CCLF3>(CCLF3Props.PRVDR_OSCAR_NUM, PRVDR_OSCAR_NUM, false, null, 6),
				new FieldSpecContinuousDateTime<CCLF3>(CCLF3Props.CLM_FROM_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime<CCLF3>(CCLF3Props.CLM_THRU_DT, DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecDynamic<CCLF3>(CCLF3Props.DGNS_PRCDR_ICD_IND, () => "0", false, null, 1),
			};
		}
	}
}
