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
	public class CCLF7
	{
		public Int64 CUR_CLM_UNIQ_ID { get; set; }
		public string BENE_HIC_NUM { get; set; }
		public string CLM_LINE_NDC_CD { get; set; }
		public string CLM_TYPE_CD { get; set; }
		public DateTime CLM_LINE_FROM_DT { get; set; }
		public string PRVDR_SRVC_ID_QLFYR_CD { get; set; }
		public string CLM_SRVC_PRVDR_GNRC_ID_NUM { get; set; }
		public string CLM_DSPNSNG_STUS_CD { get; set; }
		public string CLM_DAW_PROD_SLCTN_CD { get; set; }
		public double CLM_LINE_SRVC_UNIT_QTY { get; set; }
		public Int64 CLM_LINE_DAYS_SUPLY_QTY { get; set; }
		public string PRVDR_PRSBNG_ID_QLFYR_CD { get; set; }
		public string CLM_PRSBNG_PRVDR_GNRC_ID_NUM { get; set; }
		public double CLM_LINE_BENE_PMT_AMT { get; set; }
		public string CLM_ADJSMT_TYPE_CD { get; set; }
		public DateTime CLM_EFCTV_DT { get; set; }
		public DateTime CLM_IDR_LD_DT { get; set; }
		public Int64 CLM_LINE_RX_SRVC_RFRNC_NUM { get; set; }
		public string CLM_LINE_RX_FILL_NUM { get; set; }
	}

	public class CCLF7Props
	{
		public static readonly PropertyInfo CUR_CLM_UNIQ_ID = TypeHelper.GetProperty<CCLF7>(x => x.CUR_CLM_UNIQ_ID);
		public static readonly PropertyInfo BENE_HIC_NUM = TypeHelper.GetProperty<CCLF7>(x => x.BENE_HIC_NUM);
		public static readonly PropertyInfo CLM_LINE_NDC_CD = TypeHelper.GetProperty<CCLF7>(x => x.CLM_LINE_NDC_CD);
		public static readonly PropertyInfo CLM_TYPE_CD = TypeHelper.GetProperty<CCLF7>(x => x.CLM_TYPE_CD);
		public static readonly PropertyInfo CLM_LINE_FROM_DT = TypeHelper.GetProperty<CCLF7>(x => x.CLM_LINE_FROM_DT);
		public static readonly PropertyInfo PRVDR_SRVC_ID_QLFYR_CD = TypeHelper.GetProperty<CCLF7>(x => x.PRVDR_SRVC_ID_QLFYR_CD);
		public static readonly PropertyInfo CLM_SRVC_PRVDR_GNRC_ID_NUM = TypeHelper.GetProperty<CCLF7>(x => x.CLM_SRVC_PRVDR_GNRC_ID_NUM);
		public static readonly PropertyInfo CLM_DSPNSNG_STUS_CD = TypeHelper.GetProperty<CCLF7>(x => x.CLM_DSPNSNG_STUS_CD);
		public static readonly PropertyInfo CLM_DAW_PROD_SLCTN_CD = TypeHelper.GetProperty<CCLF7>(x => x.CLM_DAW_PROD_SLCTN_CD);
		public static readonly PropertyInfo CLM_LINE_SRVC_UNIT_QTY = TypeHelper.GetProperty<CCLF7>(x => x.CLM_LINE_SRVC_UNIT_QTY);
		public static readonly PropertyInfo CLM_LINE_DAYS_SUPLY_QTY = TypeHelper.GetProperty<CCLF7>(x => x.CLM_LINE_DAYS_SUPLY_QTY);
		public static readonly PropertyInfo PRVDR_PRSBNG_ID_QLFYR_CD = TypeHelper.GetProperty<CCLF7>(x => x.PRVDR_PRSBNG_ID_QLFYR_CD);
		public static readonly PropertyInfo CLM_PRSBNG_PRVDR_GNRC_ID_NUM = TypeHelper.GetProperty<CCLF7>(x => x.CLM_PRSBNG_PRVDR_GNRC_ID_NUM);
		public static readonly PropertyInfo CLM_LINE_BENE_PMT_AMT = TypeHelper.GetProperty<CCLF7>(x => x.CLM_LINE_BENE_PMT_AMT);
		public static readonly PropertyInfo CLM_ADJSMT_TYPE_CD = TypeHelper.GetProperty<CCLF7>(x => x.CLM_ADJSMT_TYPE_CD);
		public static readonly PropertyInfo CLM_EFCTV_DT = TypeHelper.GetProperty<CCLF7>(x => x.CLM_EFCTV_DT);
		public static readonly PropertyInfo CLM_IDR_LD_DT = TypeHelper.GetProperty<CCLF7>(x => x.CLM_IDR_LD_DT);
		public static readonly PropertyInfo CLM_LINE_RX_SRVC_RFRNC_NUM = TypeHelper.GetProperty<CCLF7>(x => x.CLM_LINE_RX_SRVC_RFRNC_NUM);
		public static readonly PropertyInfo CLM_LINE_RX_FILL_NUM = TypeHelper.GetProperty<CCLF7>(x => x.CLM_LINE_RX_FILL_NUM);
	}

	public class CCLF7Specs
	{
		public static List<IFieldSpec<CCLF7>> GetFieldSpecs
		(
			List<Category> CUR_CLM_UNIQ_ID,
			List<Category> BENE_HIC_NUM
		)
		{
			return new List<IFieldSpec<CCLF7>>()
			{
				new FieldSpecCategorical<CCLF7>(CCLF7Props.CUR_CLM_UNIQ_ID, CUR_CLM_UNIQ_ID, false, null, 13),
				new FieldSpecCategorical<CCLF7>(CCLF7Props.BENE_HIC_NUM, BENE_HIC_NUM, false, null, 11),
				new FieldSpecDynamic<CCLF7>(CCLF7Props.CLM_LINE_NDC_CD, () => RNG.GetUniform(1000000000, 99999999999).ToString(), false, null, 11),
				new FieldSpecCategorical<CCLF7>(CCLF7Props.CLM_TYPE_CD, CCLFData.LIST_CCLF7_CLM_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime<CCLF7>(CCLF7Props.CLM_LINE_FROM_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical<CCLF7>(CCLF7Props.PRVDR_SRVC_ID_QLFYR_CD, CCLFData.LIST_PRVDR_SRVC_ID_QLFYR_CD, false, null, 2),
				new FieldSpecDynamic<CCLF7>(CCLF7Props.CLM_SRVC_PRVDR_GNRC_ID_NUM, () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 20),
				new FieldSpecCategorical<CCLF7>(CCLF7Props.CLM_DSPNSNG_STUS_CD, CCLFData.LIST_CLM_DSPNSNG_STUS_CD, false, null, 1),
				new FieldSpecCategorical<CCLF7>(CCLF7Props.CLM_DAW_PROD_SLCTN_CD, CCLFData.LIST_CLM_DAW_PROD_SLCTN_CD, false, null, 1),
				new FieldSpecContinuousNumeric<CCLF7>(CCLF7Props.CLM_LINE_SRVC_UNIT_QTY, new DistUniform(0, 999999999999.9999), 4, false, null, 24),
				new FieldSpecContinuousNumeric<CCLF7>(CCLF7Props.CLM_LINE_DAYS_SUPLY_QTY, new DistUniform(0, 999999999), 0, false, null, 9),
				new FieldSpecCategorical<CCLF7>(CCLF7Props.PRVDR_PRSBNG_ID_QLFYR_CD, CCLFData.LIST_PRVDR_PRSBNG_ID_QLFYR_CD, false, null, 2),
				new FieldSpecDynamic<CCLF7>(CCLF7Props.CLM_PRSBNG_PRVDR_GNRC_ID_NUM, () => RNG.GetUniform(1000000000, 999999999999999).ToString(), false, null, 20),
				new FieldSpecContinuousNumeric<CCLF7>(CCLF7Props.CLM_LINE_BENE_PMT_AMT, new DistUniform(-9999999.99, 99999999.99), 2, false, "{0:f2}", 13, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecCategorical<CCLF7>(CCLF7Props.CLM_ADJSMT_TYPE_CD, CCLFData.LIST_CLM_ADJSMT_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime<CCLF7>(CCLF7Props.CLM_EFCTV_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime<CCLF7>(CCLF7Props.CLM_IDR_LD_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecDynamic<CCLF7>(CCLF7Props.CLM_LINE_RX_SRVC_RFRNC_NUM, () => RNG.GetUniform(1000000000, 999999999999), false, null, 12),
				new FieldSpecDynamic<CCLF7>(CCLF7Props.CLM_LINE_RX_FILL_NUM, () => RNG.GetUniform(10000, 999999999).ToString(), false, null, 9)
			};
		}
	}
}
