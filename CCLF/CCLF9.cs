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
	public class CCLF9
	{
		public string CRNT_HIC_NUM { get; set; }
		public string PRVS_HIC_NUM { get; set; }
		public DateTime PRVS_HICN_EFCTV_DT { get; set; }
		public DateTime PRVS_HICN_OBSLT_DT { get; set; }
		public string BENE_RRB_NUM { get; set; }
	}

	public class CCLF9Props
	{
		public static readonly PropertyInfo CRNT_HIC_NUM = TypeHelper.GetProperty<CCLF9>(x => x.CRNT_HIC_NUM);
		public static readonly PropertyInfo PRVS_HIC_NUM = TypeHelper.GetProperty<CCLF9>(x => x.PRVS_HIC_NUM);
		public static readonly PropertyInfo PRVS_HICN_EFCTV_DT = TypeHelper.GetProperty<CCLF9>(x => x.PRVS_HICN_EFCTV_DT);
		public static readonly PropertyInfo PRVS_HICN_OBSLT_DT = TypeHelper.GetProperty<CCLF9>(x => x.PRVS_HICN_OBSLT_DT);
		public static readonly PropertyInfo BENE_RRB_NUM = TypeHelper.GetProperty<CCLF9>(x => x.BENE_RRB_NUM);
	}

	public class CCLF9Specs
	{
		public static List<IFieldSpec<CCLF9>> GetFieldSpecs
		(
			List<Category> BENE_HIC_NUM
		)
		{
			return new List<IFieldSpec<CCLF9>>()
			{
				new FieldSpecCategorical<CCLF9>(CCLF9Props.CRNT_HIC_NUM, BENE_HIC_NUM, false, null, 11),
				new FieldSpecDynamic<CCLF9>(CCLF9Props.PRVS_HIC_NUM, () => "HICN" + RNG.GetUniform(1000000, 9999999).ToString(), false, null, 11),
				new FieldSpecContinuousDateTime<CCLF9>(CCLF9Props.PRVS_HICN_EFCTV_DT, DateTime.UtcNow.AddYears(-10), DateTime.UtcNow.AddMonths(-6), false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecContinuousDateTime<CCLF9>(CCLF9Props.PRVS_HICN_OBSLT_DT, DateTime.UtcNow.AddMonths(-6), DateTime.UtcNow, false, "{0:yyyy-MM-dd}", 10),
				new FieldSpecDynamic<CCLF9>(CCLF9Props.PRVS_HIC_NUM, () => "RRB" + RNG.GetUniform(1000000, 999999999).ToString(), false, null, 12)
			};
		}
	}
}
