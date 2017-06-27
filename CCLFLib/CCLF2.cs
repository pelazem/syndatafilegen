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
	public class CCLF2
	{
		public Int64 CUR_CLM_UNIQ_ID { get; set; } //
		public Int64 CLM_LINE_NUM { get; set; }
		public string BENE_HIC_NUM { get; set; } //
		public int CLM_TYPE_CD { get; set; }
		public DateTime CLM_LINE_FROM_DT { get; set; }
		public DateTime CLM_LINE_THRU_DT { get; set; }
		public string CLM_LINE_PROD_REV_CTR_CD { get; set; }
		public DateTime CLM_LINE_INSTNL_REV_CTR_DT { get; set; }
		public string CLM_LINE_HCPCS_CD { get; set; }
		public string BENE_EQTBL_BIC_HICN_NUM { get; set; } //
		public string PRVDR_OSCAR_NUM { get; set; }
		public DateTime CLM_FROM_DT { get; set; }
		public DateTime CLM_THRU_DT { get; set; }
		public double CLM_LINE_SRVC_UNIT_QTY { get; set; }
		public double CLM_LINE_CVRD_PD_AMT { get; set; }
		public string HCPCS_1_MDFR_CD { get; set; }
		public string HCPCS_2_MDFR_CD { get; set; }
		public string HCPCS_3_MDFR_CD { get; set; }
		public string HCPCS_4_MDFR_CD { get; set; }
		public string HCPCS_5_MDFR_CD { get; set; }
	}

	public class CCLF2Props
	{
		public static readonly PropertyInfo CUR_CLM_UNIQ_ID = TypeHelper.GetProperty<CCLF2>(x => x.CUR_CLM_UNIQ_ID);
		public static readonly PropertyInfo CLM_LINE_NUM = TypeHelper.GetProperty<CCLF2>(x => x.CLM_LINE_NUM);
		public static readonly PropertyInfo BENE_HIC_NUM = TypeHelper.GetProperty<CCLF2>(x => x.BENE_HIC_NUM);
		public static readonly PropertyInfo CLM_TYPE_CD = TypeHelper.GetProperty<CCLF2>(x => x.CLM_TYPE_CD);
		public static readonly PropertyInfo CLM_LINE_FROM_DT = TypeHelper.GetProperty<CCLF2>(x => x.CLM_LINE_FROM_DT);
		public static readonly PropertyInfo CLM_LINE_THRU_DT = TypeHelper.GetProperty<CCLF2>(x => x.CLM_LINE_THRU_DT);
		public static readonly PropertyInfo CLM_LINE_PROD_REV_CTR_CD = TypeHelper.GetProperty<CCLF2>(x => x.CLM_LINE_PROD_REV_CTR_CD);
		public static readonly PropertyInfo CLM_LINE_INSTNL_REV_CTR_DT = TypeHelper.GetProperty<CCLF2>(x => x.CLM_LINE_INSTNL_REV_CTR_DT);
		public static readonly PropertyInfo CLM_LINE_HCPCS_CD = TypeHelper.GetProperty<CCLF2>(x => x.CLM_LINE_HCPCS_CD);
		public static readonly PropertyInfo BENE_EQTBL_BIC_HICN_NUM = TypeHelper.GetProperty<CCLF2>(x => x.BENE_EQTBL_BIC_HICN_NUM);
		public static readonly PropertyInfo PRVDR_OSCAR_NUM = TypeHelper.GetProperty<CCLF2>(x => x.PRVDR_OSCAR_NUM);
		public static readonly PropertyInfo CLM_FROM_DT = TypeHelper.GetProperty<CCLF2>(x => x.CLM_FROM_DT);
		public static readonly PropertyInfo CLM_THRU_DT = TypeHelper.GetProperty<CCLF2>(x => x.CLM_THRU_DT);
		public static readonly PropertyInfo CLM_LINE_SRVC_UNIT_QTY = TypeHelper.GetProperty<CCLF2>(x => x.CLM_LINE_SRVC_UNIT_QTY);
		public static readonly PropertyInfo CLM_LINE_CVRD_PD_AMT = TypeHelper.GetProperty<CCLF2>(x => x.CLM_LINE_CVRD_PD_AMT);
		public static readonly PropertyInfo HCPCS_1_MDFR_CD = TypeHelper.GetProperty<CCLF2>(x => x.HCPCS_1_MDFR_CD);
		public static readonly PropertyInfo HCPCS_2_MDFR_CD = TypeHelper.GetProperty<CCLF2>(x => x.HCPCS_2_MDFR_CD);
		public static readonly PropertyInfo HCPCS_3_MDFR_CD = TypeHelper.GetProperty<CCLF2>(x => x.HCPCS_3_MDFR_CD);
		public static readonly PropertyInfo HCPCS_4_MDFR_CD = TypeHelper.GetProperty<CCLF2>(x => x.HCPCS_4_MDFR_CD);
		public static readonly PropertyInfo HCPCS_5_MDFR_CD = TypeHelper.GetProperty<CCLF2>(x => x.HCPCS_5_MDFR_CD);
	}

	public class CCLF2Specs
	{
		public static List<IFieldSpec<CCLF2>> GetFieldSpecs
		(
			List<Category> CUR_CLM_UNIQ_ID,
			List<Category> BENE_HIC_NUM,
			List<Category> BENE_EQTBL_BIC_HICN_NUM,
			List<Category> PRVDR_OSCAR_NUM
		)
		{
			return new List<IFieldSpec<CCLF2>>()
			{
				new FieldSpecCategorical<CCLF2>(CCLF2Props.CUR_CLM_UNIQ_ID, CUR_CLM_UNIQ_ID, false, null, 13),
				new FieldSpecContinuousNumeric<CCLF2>(CCLF2Props.CLM_LINE_NUM, new DistIncrementing(1, 1), 0, false, null, 10, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecCategorical<CCLF2>(CCLF2Props.BENE_HIC_NUM, BENE_HIC_NUM, false, null, 11),
				new FieldSpecCategorical<CCLF2>(CCLF2Props.CLM_TYPE_CD, CCLFData.LIST_CLM_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime<CCLF2>(CCLF2Props.CLM_LINE_FROM_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime<CCLF2>(CCLF2Props.CLM_LINE_THRU_DT, DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical<CCLF2>(CCLF2Props.CLM_LINE_PROD_REV_CTR_CD, CCLFData.LIST_CLM_LINE_PROD_REV_CTR_CD, false, null, 4),
				new FieldSpecContinuousDateTime<CCLF2>(CCLF2Props.CLM_LINE_INSTNL_REV_CTR_DT, DateTime.UtcNow.AddMonths(-4), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical<CCLF2>(CCLF2Props.CLM_LINE_HCPCS_CD, CCLFData.LIST_HCPCS_CD, false, null, 5),
				new FieldSpecCategorical<CCLF2>(CCLF2Props.BENE_EQTBL_BIC_HICN_NUM, BENE_EQTBL_BIC_HICN_NUM, false, null, 11),
				new FieldSpecCategorical<CCLF2>(CCLF2Props.PRVDR_OSCAR_NUM, PRVDR_OSCAR_NUM, false, null, 6),
				new FieldSpecContinuousDateTime<CCLF2>(CCLF2Props.CLM_FROM_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime<CCLF2>(CCLF2Props.CLM_THRU_DT, DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousNumeric<CCLF2>(CCLF2Props.CLM_LINE_SRVC_UNIT_QTY, new DistUniform(-999999999999.9999, 999999999999.9999), 4, false, null, 24, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecContinuousNumeric<CCLF2>(CCLF2Props.CLM_LINE_CVRD_PD_AMT, new DistUniform(0, 99999999.99), 2, false, "{0:f2}", 17, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecCategorical<CCLF2>(CCLF2Props.HCPCS_1_MDFR_CD, CCLFData.LIST_HCPCS_CPT_MOD_CD, false, null, 2),
				new FieldSpecCategorical<CCLF2>(CCLF2Props.HCPCS_2_MDFR_CD, CCLFData.LIST_HCPCS_CPT_MOD_CD, false, null, 2),
				new FieldSpecCategorical<CCLF2>(CCLF2Props.HCPCS_3_MDFR_CD, CCLFData.LIST_HCPCS_CPT_MOD_CD, false, null, 2),
				new FieldSpecCategorical<CCLF2>(CCLF2Props.HCPCS_4_MDFR_CD, CCLFData.LIST_HCPCS_CPT_MOD_CD, false, null, 2),
				new FieldSpecCategorical<CCLF2>(CCLF2Props.HCPCS_5_MDFR_CD, CCLFData.LIST_HCPCS_CPT_MOD_CD, false, null, 2)
			};
		}
	}
}
