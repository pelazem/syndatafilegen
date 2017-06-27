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
	public class CCLF1
	{
		public Int64 CUR_CLM_UNIQ_ID { get; set; }
		public string PRVDR_OSCAR_NUM { get; set; }
		public string BENE_HIC_NUM { get; set; }
		public int CLM_TYPE_CD { get; set; }
		public DateTime CLM_FROM_DT { get; set; }
		public DateTime CLM_THRU_DT { get; set; }
		public int CLM_BILL_FAC_TYPE_CD { get; set; }
		public string CLM_BILL_CLSFCTN_CD { get; set; }
		public string PRNCPL_DGNS_CD { get; set; }
		public string ADMTG_DGNS_CD { get; set; }
		public string CLM_MDCR_NPMT_RSN_CD { get; set; }
		public double CLM_PMT_AMT { get; set; }
		public string CLM_NCH_PRMRY_PYR_CD { get; set; }
		public string PRVDR_FAC_FIPS_ST_CD { get; set; }
		public string BENE_PTNT_STUS_CD { get; set; }
		public string DGNS_DRG_CD { get; set; }
		public string CLM_OP_SRVC_TYPE_CD { get; set; }
		public string FAC_PRVDR_NPI_NUM { get; set; }
		public string OPRTG_PRVDR_NPI_NUM { get; set; }
		public string ATNDG_PRVDR_NPI_NUM { get; set; }
		public string OTHR_PRVDR_NPI_NUM { get; set; }
		public string CLM_ADJSMT_TYPE_CD { get; set; }
		public DateTime CLM_EFCTV_DT { get; set; }
		public DateTime CLM_IDR_LD_DT { get; set; }
		public string BENE_EQTBL_BIC_HICN_NUM { get; set; }
		public string CLM_ADMSN_TYPE_CD { get; set; }
		public string CLM_ADMSN_SRC_CD { get; set; }
		public string CLM_BILL_FREQ_CD { get; set; }
		public string CLM_QUERY_CD { get; set; }
		public string DGNS_PRCDR_ICD_IND { get; set; }
	}

	public class CCLF1Props
	{
		public static readonly PropertyInfo CUR_CLM_UNIQ_ID = TypeHelper.GetProperty<CCLF1>(x => x.CUR_CLM_UNIQ_ID);
		public static readonly PropertyInfo PRVDR_OSCAR_NUM = TypeHelper.GetProperty<CCLF1>(x => x.PRVDR_OSCAR_NUM);
		public static readonly PropertyInfo BENE_HIC_NUM = TypeHelper.GetProperty<CCLF1>(x => x.BENE_HIC_NUM);
		public static readonly PropertyInfo CLM_TYPE_CD = TypeHelper.GetProperty<CCLF1>(x => x.CLM_TYPE_CD);
		public static readonly PropertyInfo CLM_FROM_DT = TypeHelper.GetProperty<CCLF1>(x => x.CLM_FROM_DT);
		public static readonly PropertyInfo CLM_THRU_DT = TypeHelper.GetProperty<CCLF1>(x => x.CLM_THRU_DT);
		public static readonly PropertyInfo CLM_BILL_FAC_TYPE_CD = TypeHelper.GetProperty<CCLF1>(x => x.CLM_BILL_FAC_TYPE_CD);
		public static readonly PropertyInfo CLM_BILL_CLSFCTN_CD = TypeHelper.GetProperty<CCLF1>(x => x.CLM_BILL_CLSFCTN_CD);
		public static readonly PropertyInfo PRNCPL_DGNS_CD = TypeHelper.GetProperty<CCLF1>(x => x.PRNCPL_DGNS_CD);
		public static readonly PropertyInfo ADMTG_DGNS_CD = TypeHelper.GetProperty<CCLF1>(x => x.ADMTG_DGNS_CD);
		public static readonly PropertyInfo CLM_MDCR_NPMT_RSN_CD = TypeHelper.GetProperty<CCLF1>(x => x.CLM_MDCR_NPMT_RSN_CD);
		public static readonly PropertyInfo CLM_PMT_AMT = TypeHelper.GetProperty<CCLF1>(x => x.CLM_PMT_AMT);
		public static readonly PropertyInfo CLM_NCH_PRMRY_PYR_CD = TypeHelper.GetProperty<CCLF1>(x => x.CLM_NCH_PRMRY_PYR_CD);
		public static readonly PropertyInfo PRVDR_FAC_FIPS_ST_CD = TypeHelper.GetProperty<CCLF1>(x => x.PRVDR_FAC_FIPS_ST_CD);
		public static readonly PropertyInfo BENE_PTNT_STUS_CD = TypeHelper.GetProperty<CCLF1>(x => x.BENE_PTNT_STUS_CD);
		public static readonly PropertyInfo DGNS_DRG_CD = TypeHelper.GetProperty<CCLF1>(x => x.DGNS_DRG_CD);
		public static readonly PropertyInfo CLM_OP_SRVC_TYPE_CD = TypeHelper.GetProperty<CCLF1>(x => x.CLM_OP_SRVC_TYPE_CD);
		public static readonly PropertyInfo FAC_PRVDR_NPI_NUM = TypeHelper.GetProperty<CCLF1>(x => x.FAC_PRVDR_NPI_NUM);
		public static readonly PropertyInfo OPRTG_PRVDR_NPI_NUM = TypeHelper.GetProperty<CCLF1>(x => x.OPRTG_PRVDR_NPI_NUM);
		public static readonly PropertyInfo ATNDG_PRVDR_NPI_NUM = TypeHelper.GetProperty<CCLF1>(x => x.ATNDG_PRVDR_NPI_NUM);
		public static readonly PropertyInfo OTHR_PRVDR_NPI_NUM = TypeHelper.GetProperty<CCLF1>(x => x.OTHR_PRVDR_NPI_NUM);
		public static readonly PropertyInfo CLM_ADJSMT_TYPE_CD = TypeHelper.GetProperty<CCLF1>(x => x.CLM_ADJSMT_TYPE_CD);
		public static readonly PropertyInfo CLM_EFCTV_DT = TypeHelper.GetProperty<CCLF1>(x => x.CLM_EFCTV_DT);
		public static readonly PropertyInfo CLM_IDR_LD_DT = TypeHelper.GetProperty<CCLF1>(x => x.CLM_IDR_LD_DT);
		public static readonly PropertyInfo BENE_EQTBL_BIC_HICN_NUM = TypeHelper.GetProperty<CCLF1>(x => x.BENE_EQTBL_BIC_HICN_NUM);
		public static readonly PropertyInfo CLM_ADMSN_TYPE_CD = TypeHelper.GetProperty<CCLF1>(x => x.CLM_ADMSN_TYPE_CD);
		public static readonly PropertyInfo CLM_ADMSN_SRC_CD = TypeHelper.GetProperty<CCLF1>(x => x.CLM_ADMSN_SRC_CD);
		public static readonly PropertyInfo CLM_BILL_FREQ_CD = TypeHelper.GetProperty<CCLF1>(x => x.CLM_BILL_FREQ_CD);
		public static readonly PropertyInfo CLM_QUERY_CD = TypeHelper.GetProperty<CCLF1>(x => x.CLM_QUERY_CD);
		public static readonly PropertyInfo DGNS_PRCDR_ICD_IND = TypeHelper.GetProperty<CCLF1>(x => x.DGNS_PRCDR_ICD_IND);
	}

	public class CCLF1Specs
	{
		public static List<IFieldSpec<CCLF1>> GetFieldSpecs(List<Category> BENE_HIC_NUM)
		{
			return new List<IFieldSpec<CCLF1>>()
			{
				new FieldSpecContinuousNumeric<CCLF1>(CCLF1Props.CUR_CLM_UNIQ_ID, new DistIncrementing(1000000, 1), 0, false, null, 13),
				new FieldSpecDynamic<CCLF1>(CCLF1Props.PRVDR_OSCAR_NUM, () => RNG.GetUniform(100000, 999999).ToString(), false, null, 6),
				new FieldSpecCategorical<CCLF1>(CCLF1Props.BENE_HIC_NUM, BENE_HIC_NUM, false, null, 11),
				new FieldSpecCategorical<CCLF1>(CCLF1Props.CLM_TYPE_CD, CCLFData.LIST_CLM_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime<CCLF1>(CCLF1Props.CLM_FROM_DT, DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime<CCLF1>(CCLF1Props.CLM_THRU_DT, DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical<CCLF1>(CCLF1Props.CLM_BILL_FAC_TYPE_CD, CCLFData.LIST_CLM_BILL_FAC_TYPE_CD,false, null, 1),
				new FieldSpecCategorical<CCLF1>(CCLF1Props.CLM_BILL_CLSFCTN_CD, CCLFData.LIST_CLM_BILL_CLSFCTN_CD,false, null, 1),
				new FieldSpecDynamic<CCLF1>(CCLF1Props.PRNCPL_DGNS_CD, () => CCLFGenerator.GetICD10Code(),false, null, 7),
				new FieldSpecDynamic<CCLF1>(CCLF1Props.ADMTG_DGNS_CD, () => CCLFGenerator.GetICD10Code(),false, null, 7),
				new FieldSpecCategorical<CCLF1>(CCLF1Props.CLM_MDCR_NPMT_RSN_CD, CCLFData.LIST_CLM_MDCR_NPMT_RSN_CD,false, null, 2),
				new FieldSpecContinuousNumeric<CCLF1>(CCLF1Props.CLM_PMT_AMT, new DistUniform(0, 99999999.99), 2, false, "{0:f2}", 17, Util.Location.AtStart, Util.Location.AtEnd, '0'),
				new FieldSpecCategorical<CCLF1>(CCLF1Props.CLM_NCH_PRMRY_PYR_CD, CCLFData.LIST_CLM_NCH_PRMRY_PYR_CD, false, null, 1),
				new FieldSpecCategorical<CCLF1>(CCLF1Props.PRVDR_FAC_FIPS_ST_CD, CCLFData.LIST_FIPS_STATE_CD, false, null, 2),
				new FieldSpecCategorical<CCLF1>(CCLF1Props.BENE_PTNT_STUS_CD, CCLFData.LIST_STUS_CD, false, null, 2),
				new FieldSpecCategorical<CCLF1>(CCLF1Props.DGNS_DRG_CD, CCLFData.LIST_DGNS_DRG_CD, false, null, 4),
				new FieldSpecCategorical<CCLF1>(CCLF1Props.CLM_OP_SRVC_TYPE_CD, CCLFData.LIST_CLM_OP_SRVC_TYPE_CD, false, null, 1),
				new FieldSpecDynamic<CCLF1>(CCLF1Props.FAC_PRVDR_NPI_NUM, () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 10),
				new FieldSpecDynamic<CCLF1>(CCLF1Props.OPRTG_PRVDR_NPI_NUM, () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 10),
				new FieldSpecDynamic<CCLF1>(CCLF1Props.ATNDG_PRVDR_NPI_NUM, () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 10),
				new FieldSpecDynamic<CCLF1>(CCLF1Props.OTHR_PRVDR_NPI_NUM, () => RNG.GetUniform(1000000000, 9999999999).ToString(), false, null, 10),
				new FieldSpecCategorical<CCLF1>(CCLF1Props.CLM_ADJSMT_TYPE_CD, CCLFData.LIST_CLM_ADJSMT_TYPE_CD, false, null, 2),
				new FieldSpecContinuousDateTime<CCLF1>(CCLF1Props.CLM_EFCTV_DT, DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime<CCLF1>(CCLF1Props.CLM_IDR_LD_DT, DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecDynamic<CCLF1>(CCLF1Props.BENE_EQTBL_BIC_HICN_NUM, () => "HICN" + RNG.GetUniform(1000000, 9999999).ToString(), false, null, 11),
				new FieldSpecCategorical<CCLF1>(CCLF1Props.CLM_ADMSN_TYPE_CD, CCLFData.LIST_CLM_ADMSN_TYPE_CD, false, null, 2),
				new FieldSpecCategorical<CCLF1>(CCLF1Props.CLM_BILL_FREQ_CD, CCLFData.LIST_CLM_BILL_FREQ_CD, false, null, 1),
				new FieldSpecCategorical<CCLF1>(CCLF1Props.CLM_QUERY_CD, CCLFData.LIST_CLM_QUERY_CD, false, null, 1),
				new FieldSpecCategorical<CCLF1>(CCLF1Props.DGNS_PRCDR_ICD_IND, CCLFData.LIST_DGNS_PRCDR_ICD_IND, false, null, 1)
			};
		}
	}
}
