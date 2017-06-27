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
	public class CCLF8
	{
		public string BENE_HIC_NUM { get; set; }
		public string BENE_FIPS_STATE_CD { get; set; }
		public int BENE_FIPS_CNTY_CD { get; set; }
		public string BENE_ZIP_CD { get; set; }
		public DateTime BENE_DOB { get; set; }
		public int BENE_SEX_CD { get; set; }
		public int BENE_RACE_CD { get; set; }
		public int BENE_AGE { get; set; }
		public int BENE_MDCR_STUS_CD { get; set; }
		public string BENE_DUAL_STUS_CD { get; set; }
		public DateTime BENE_DEATH_DT { get; set; }
		public DateTime BENE_RNG_BGN_DT { get; set; }
		public DateTime BENE_RNG_END_DT { get; set; }
		public string BENE_1ST_NAME { get; set; }
		public string BENE_MIDL_NAME { get; set; }
		public string BENE_LAST_NAME { get; set; }
		public int BENE_ORGNL_ENTLMT_RSN_CD { get; set; }
		public string BENE_ENTLMT_BUYIN_IND { get; set; }
	}

	public class CCLF8Props
	{
		public static readonly PropertyInfo BENE_HIC_NUM = TypeHelper.GetProperty<CCLF8>(x => x.BENE_HIC_NUM);
		public static readonly PropertyInfo BENE_FIPS_STATE_CD = TypeHelper.GetProperty<CCLF8>(x => x.BENE_FIPS_STATE_CD);
		public static readonly PropertyInfo BENE_FIPS_CNTY_CD = TypeHelper.GetProperty<CCLF8>(x => x.BENE_FIPS_CNTY_CD);
		public static readonly PropertyInfo BENE_ZIP_CD = TypeHelper.GetProperty<CCLF8>(x => x.BENE_ZIP_CD);
		public static readonly PropertyInfo BENE_DOB = TypeHelper.GetProperty<CCLF8>(x => x.BENE_DOB);
		public static readonly PropertyInfo BENE_SEX_CD = TypeHelper.GetProperty<CCLF8>(x => x.BENE_SEX_CD);
		public static readonly PropertyInfo BENE_RACE_CD = TypeHelper.GetProperty<CCLF8>(x => x.BENE_RACE_CD);
		public static readonly PropertyInfo BENE_AGE = TypeHelper.GetProperty<CCLF8>(x => x.BENE_AGE);
		public static readonly PropertyInfo BENE_MDCR_STUS_CD = TypeHelper.GetProperty<CCLF8>(x => x.BENE_MDCR_STUS_CD);
		public static readonly PropertyInfo BENE_DUAL_STUS_CD = TypeHelper.GetProperty<CCLF8>(x => x.BENE_DUAL_STUS_CD);
		public static readonly PropertyInfo BENE_DEATH_DT = TypeHelper.GetProperty<CCLF8>(x => x.BENE_DEATH_DT);
		public static readonly PropertyInfo BENE_RNG_BGN_DT = TypeHelper.GetProperty<CCLF8>(x => x.BENE_RNG_BGN_DT);
		public static readonly PropertyInfo BENE_RNG_END_DT = TypeHelper.GetProperty<CCLF8>(x => x.BENE_RNG_END_DT);
		public static readonly PropertyInfo BENE_1ST_NAME = TypeHelper.GetProperty<CCLF8>(x => x.BENE_1ST_NAME);
		public static readonly PropertyInfo BENE_MIDL_NAME = TypeHelper.GetProperty<CCLF8>(x => x.BENE_MIDL_NAME);
		public static readonly PropertyInfo BENE_LAST_NAME = TypeHelper.GetProperty<CCLF8>(x => x.BENE_LAST_NAME);
		public static readonly PropertyInfo BENE_ORGNL_ENTLMT_RSN_CD = TypeHelper.GetProperty<CCLF8>(x => x.BENE_ORGNL_ENTLMT_RSN_CD);
		public static readonly PropertyInfo BENE_ENTLMT_BUYIN_IND = TypeHelper.GetProperty<CCLF8>(x => x.BENE_ENTLMT_BUYIN_IND);
	}

	public class CCLF8Specs
	{
		public static List<IFieldSpec<CCLF8>> GetFieldSpecs()
		{
			return new List<IFieldSpec<CCLF8>>()
			{
				new FieldSpecDynamic<CCLF8>(CCLF8Props.BENE_HIC_NUM, () => "HICN" + RNG.GetUniform(1000000, 9999999).ToString(), false, null, 11),
				new FieldSpecCategorical<CCLF8>(CCLF8Props.BENE_FIPS_STATE_CD, CCLFData.LIST_FIPS_STATE_CD, false, null, 2),
				new FieldSpecCategorical<CCLF8>(CCLF8Props.BENE_FIPS_CNTY_CD, CCLFData.LIST_BENE_FIPS_CNTY_CD, false, null, 3),
				new FieldSpecCategorical<CCLF8>(CCLF8Props.BENE_ZIP_CD, CCLFData.LIST_BENE_ZIP_CD, false, null, 5),
				new FieldSpecContinuousDateTime<CCLF8>(CCLF8Props.BENE_DOB, new DateTime(1910, 1, 1, 0, 0, 0, DateTimeKind.Utc), DateTime.UtcNow.AddDays(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical<CCLF8>(CCLF8Props.BENE_SEX_CD, CCLFData.LIST_BENE_SEX_CD, false, null, 1),
				new FieldSpecCategorical<CCLF8>(CCLF8Props.BENE_RACE_CD, CCLFData.LIST_BENE_RACE_CD, false, null, 1),
				new FieldSpecContinuousNumeric<CCLF8>(CCLF8Props.BENE_AGE, new DistNormal(40, 35), 0, false, null, 3),
				new FieldSpecCategorical<CCLF8>(CCLF8Props.BENE_MDCR_STUS_CD, CCLFData.LIST_BENE_MDCR_STUS_CD, false, null, 2),
				new FieldSpecCategorical<CCLF8>(CCLF8Props.BENE_DUAL_STUS_CD, CCLFData.LIST_BENE_DUAL_STUS_CD, false, null, 2),
				new FieldSpecContinuousDateTime<CCLF8>(CCLF8Props.BENE_DEATH_DT, new DateTime(2017, 1, 1, 0, 0, 0, DateTimeKind.Utc), DateTime.UtcNow.AddDays(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime<CCLF8>(CCLF8Props.BENE_RNG_BGN_DT, new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime<CCLF8>(CCLF8Props.BENE_RNG_END_DT, DateTime.UtcNow.AddYears(-2), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical<CCLF8>(CCLF8Props.BENE_1ST_NAME, CCLFData.LIST_BENE_1ST_NAME, false, null, 30),
				new FieldSpecCategorical<CCLF8>(CCLF8Props.BENE_MIDL_NAME, CCLFData.LIST_BENE_MIDL_NAME, false, null, 15),
				new FieldSpecCategorical<CCLF8>(CCLF8Props.BENE_LAST_NAME, CCLFData.LIST_BENE_LAST_NAME, false, null, 40),
				new FieldSpecCategorical<CCLF8>(CCLF8Props.BENE_ORGNL_ENTLMT_RSN_CD, CCLFData.LIST_BENE_ORGNL_ENTLMT_RSN_CD, false, null, 1),
				new FieldSpecCategorical<CCLF8>(CCLF8Props.BENE_ENTLMT_BUYIN_IND, CCLFData.LIST_BENE_ENTLMT_BUYIN_IND, false, null, 1)
			};
		}
	}
}
