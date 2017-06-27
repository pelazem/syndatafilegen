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

	public class CCLF6Props
	{
		public static readonly PropertyInfo CUR_CLM_UNIQ_ID = TypeHelper.GetProperty<CCLF6>(x => x.CUR_CLM_UNIQ_ID);
		public static readonly PropertyInfo CLM_LINE_NUM = TypeHelper.GetProperty<CCLF6>(x => x.CLM_LINE_NUM);
		public static readonly PropertyInfo BENE_HIC_NUM = TypeHelper.GetProperty<CCLF6>(x => x.BENE_HIC_NUM);
		public static readonly PropertyInfo CLM_TYPE_CD = TypeHelper.GetProperty<CCLF6>(x => x.CLM_TYPE_CD);
		public static readonly PropertyInfo CLM_FROM_DT = TypeHelper.GetProperty<CCLF6>(x => x.CLM_FROM_DT);
		public static readonly PropertyInfo CLM_THRU_DT = TypeHelper.GetProperty<CCLF6>(x => x.CLM_THRU_DT);
		public static readonly PropertyInfo CLM_FED_TYPE_SRVC_CD = TypeHelper.GetProperty<CCLF6>(x => x.CLM_FED_TYPE_SRVC_CD);
		public static readonly PropertyInfo CLM_POS_CD = TypeHelper.GetProperty<CCLF6>(x => x.CLM_POS_CD);
		public static readonly PropertyInfo CLM_LINE_FROM_DT = TypeHelper.GetProperty<CCLF6>(x => x.CLM_LINE_FROM_DT);
		public static readonly PropertyInfo CLM_LINE_THRU_DT = TypeHelper.GetProperty<CCLF6>(x => x.CLM_LINE_THRU_DT);
		public static readonly PropertyInfo CLM_LINE_HCPCS_CD = TypeHelper.GetProperty<CCLF6>(x => x.CLM_LINE_HCPCS_CD);
		public static readonly PropertyInfo CLM_LINE_CVRD_PD_AMT = TypeHelper.GetProperty<CCLF6>(x => x.CLM_LINE_CVRD_PD_AMT);
		public static readonly PropertyInfo CLM_PRMRY_PYR_CD = TypeHelper.GetProperty<CCLF6>(x => x.CLM_PRMRY_PYR_CD);
		public static readonly PropertyInfo PAYTO_PRVDR_NPI_NUM = TypeHelper.GetProperty<CCLF6>(x => x.PAYTO_PRVDR_NPI_NUM);
		public static readonly PropertyInfo ORDRG_PRVDR_NPI_NUM = TypeHelper.GetProperty<CCLF6>(x => x.ORDRG_PRVDR_NPI_NUM);
		public static readonly PropertyInfo CLM_CARR_PMT_DNL_CD = TypeHelper.GetProperty<CCLF6>(x => x.CLM_CARR_PMT_DNL_CD);
		public static readonly PropertyInfo CLM_PRCSG_IND_CD = TypeHelper.GetProperty<CCLF6>(x => x.CLM_PRCSG_IND_CD);
		public static readonly PropertyInfo CLM_ADJSMT_TYPE_CD = TypeHelper.GetProperty<CCLF6>(x => x.CLM_ADJSMT_TYPE_CD);
		public static readonly PropertyInfo CLM_EFCTV_DT = TypeHelper.GetProperty<CCLF6>(x => x.CLM_EFCTV_DT);
		public static readonly PropertyInfo CLM_IDR_LD_DT = TypeHelper.GetProperty<CCLF6>(x => x.CLM_IDR_LD_DT);
		public static readonly PropertyInfo CLM_CNTL_NUM = TypeHelper.GetProperty<CCLF6>(x => x.CLM_CNTL_NUM);
		public static readonly PropertyInfo BENE_EQTBL_BIC_HICN_NUM = TypeHelper.GetProperty<CCLF6>(x => x.BENE_EQTBL_BIC_HICN_NUM);
		public static readonly PropertyInfo CLM_LINE_ALOWD_CHRG_AMT = TypeHelper.GetProperty<CCLF6>(x => x.CLM_LINE_ALOWD_CHRG_AMT);
		public static readonly PropertyInfo CLM_DISP_CD = TypeHelper.GetProperty<CCLF6>(x => x.CLM_DISP_CD);
	}

	public class CCLF6Specs
	{
		public static List<IFieldSpec<CCLF6>> GetFieldSpecs
		(
			List<Category> CUR_CLM_UNIQ_ID,
			List<Category> BENE_HIC_NUM,
			List<Category> BENE_EQTBL_BIC_HICN_NUM
		)
		{
			return new List<IFieldSpec<CCLF6>>()
			{
				new FieldSpecCategorical<CCLF6>(CCLF6Props.CUR_CLM_UNIQ_ID, CUR_CLM_UNIQ_ID, false, null, 13),
				new FieldSpecContinuousNumeric<CCLF6>(CCLF6Props.CLM_LINE_NUM, new DistIncrementing(1, 1), 0, false, null, 10, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecCategorical<CCLF6>(CCLF6Props.BENE_HIC_NUM, BENE_HIC_NUM, false, null, 11),
				new FieldSpecCategorical<CCLF6>(CCLF6Props.CLM_TYPE_CD, CCLFData.LIST_CCLF6_CLM_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime<CCLF6>(CCLF6Props.CLM_FROM_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime<CCLF6>(CCLF6Props.CLM_THRU_DT, DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical<CCLF6>(CCLF6Props.CLM_FED_TYPE_SRVC_CD, CCLFData.LIST_CLM_FED_TYPE_SRVC_CD, false, null, 1),
				new FieldSpecCategorical<CCLF6>(CCLF6Props.CLM_POS_CD, CCLFData.LIST_CLM_POS_CD, false, null, 2),
				new FieldSpecContinuousDateTime<CCLF6>(CCLF6Props.CLM_LINE_FROM_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime<CCLF6>(CCLF6Props.CLM_LINE_THRU_DT, DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical<CCLF6>(CCLF6Props.CLM_LINE_HCPCS_CD, CCLFData.LIST_HCPCS_CD, false, null, 5),
				new FieldSpecContinuousNumeric<CCLF6>(CCLF6Props.CLM_LINE_CVRD_PD_AMT, new DistUniform(0, 99999999.99), 2, false, "{0:f2}", 15, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecCategorical<CCLF6>(CCLF6Props.CLM_PRMRY_PYR_CD, CCLFData.LIST_PRMRY_PYR_CD, false, null, 1),
				new FieldSpecDynamic<CCLF6>(CCLF6Props.PAYTO_PRVDR_NPI_NUM, () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 10),
				new FieldSpecDynamic<CCLF6>(CCLF6Props.ORDRG_PRVDR_NPI_NUM, () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 10),
				new FieldSpecCategorical<CCLF6>(CCLF6Props.CLM_CARR_PMT_DNL_CD, CCLFData.LIST_CLM_CARR_PMT_DNL_CD, false, null, 2),
				new FieldSpecCategorical<CCLF6>(CCLF6Props.CLM_PRCSG_IND_CD, CCLFData.LIST_CLM_PRCSG_IND_CD, false, null, 2),
				new FieldSpecCategorical<CCLF6>(CCLF6Props.CLM_ADJSMT_TYPE_CD, CCLFData.LIST_CLM_ADJSMT_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime<CCLF6>(CCLF6Props.CLM_EFCTV_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime<CCLF6>(CCLF6Props.CLM_IDR_LD_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecDynamic<CCLF6>(CCLF6Props.CLM_CNTL_NUM, () => Guid.NewGuid().ToString(), false, null, 40),
				new FieldSpecCategorical<CCLF6>(CCLF6Props.BENE_EQTBL_BIC_HICN_NUM, BENE_EQTBL_BIC_HICN_NUM, false, null, 11),
				new FieldSpecContinuousNumeric<CCLF6>(CCLF6Props.CLM_LINE_ALOWD_CHRG_AMT, new DistUniform(0, 999999999999.99), 2, false, "{0:f2}", 17, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecCategorical<CCLF6>(CCLF6Props.CLM_DISP_CD, CCLFData.LIST_CLM_DISP_CD, false, null, 2)
			};
		}
	}
}
