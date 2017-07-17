using System;
using System.Collections.Generic;
using SynDataFileGen.Lib;
using pelazem.util;

namespace CCLF17.Lib
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

	public class CCLF8Specs
	{
		public static List<IFieldSpec> GetFieldSpecs()
		{
			return new List<IFieldSpec>()
			{
				new FieldSpecDynamic(nameof(CCLF8.BENE_HIC_NUM), () => "HICN" + RNG.GetUniform(1000000, 9999999).ToString(), false, null, 11),
				new FieldSpecCategorical(nameof(CCLF8.BENE_FIPS_STATE_CD), CCLFData.LIST_FIPS_STATE_CD, false, null, 2),
				new FieldSpecCategorical(nameof(CCLF8.BENE_FIPS_CNTY_CD), CCLFData.LIST_BENE_FIPS_CNTY_CD, false, null, 3),
				new FieldSpecCategorical(nameof(CCLF8.BENE_ZIP_CD), CCLFData.LIST_BENE_ZIP_CD, false, null, 5),
				new FieldSpecContinuousDateTime(nameof(CCLF8.BENE_DOB), new DateTime(1910, 1, 1, 0, 0, 0, DateTimeKind.Utc), DateTime.UtcNow.AddDays(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical(nameof(CCLF8.BENE_SEX_CD), CCLFData.LIST_BENE_SEX_CD, false, null, 1),
				new FieldSpecCategorical(nameof(CCLF8.BENE_RACE_CD), CCLFData.LIST_BENE_RACE_CD, false, null, 1),
				new FieldSpecContinuousNumeric(nameof(CCLF8.BENE_AGE), new DistNormal(40, 35), 0, false, null, 3),
				new FieldSpecCategorical(nameof(CCLF8.BENE_MDCR_STUS_CD), CCLFData.LIST_BENE_MDCR_STUS_CD, false, null, 2),
				new FieldSpecCategorical(nameof(CCLF8.BENE_DUAL_STUS_CD), CCLFData.LIST_BENE_DUAL_STUS_CD, false, null, 2),
				new FieldSpecContinuousDateTime(nameof(CCLF8.BENE_DEATH_DT), new DateTime(2017, 1, 1, 0, 0, 0, DateTimeKind.Utc), DateTime.UtcNow.AddDays(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime(nameof(CCLF8.BENE_RNG_BGN_DT), new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), DateTime.UtcNow.AddMonths(-1), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime(nameof(CCLF8.BENE_RNG_END_DT), DateTime.UtcNow.AddYears(-2), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecCategorical(nameof(CCLF8.BENE_1ST_NAME), CCLFData.LIST_BENE_1ST_NAME, false, null, 30),
				new FieldSpecCategorical(nameof(CCLF8.BENE_MIDL_NAME), CCLFData.LIST_BENE_MIDL_NAME, false, null, 15),
				new FieldSpecCategorical(nameof(CCLF8.BENE_LAST_NAME), CCLFData.LIST_BENE_LAST_NAME, false, null, 40),
				new FieldSpecCategorical(nameof(CCLF8.BENE_ORGNL_ENTLMT_RSN_CD), CCLFData.LIST_BENE_ORGNL_ENTLMT_RSN_CD, false, null, 1),
				new FieldSpecCategorical(nameof(CCLF8.BENE_ENTLMT_BUYIN_IND), CCLFData.LIST_BENE_ENTLMT_BUYIN_IND, false, null, 1)
			};
		}
	}

}
