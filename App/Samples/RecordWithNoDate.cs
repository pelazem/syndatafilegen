using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using pelazem.Common;

namespace FileGeneratorApp
{
	public class RecordWithNoDate
	{
		public string Name { get; set; }

		public double UnitPrice { get; set; }

		public double Quantity { get; set; }

		public int ProductId { get; set; }

		public int UserId { get; set; }

		public bool IsLoyaltyMember { get; set; }
	}

	public class RecordWithNoDateProps
	{
		public static readonly PropertyInfo Name = TypeHelper.GetProperty<RecordWithNoDate>(x => x.Name);
		public static readonly PropertyInfo UnitPrice = TypeHelper.GetProperty<RecordWithNoDate>(x => x.UnitPrice);
		public static readonly PropertyInfo Quantity = TypeHelper.GetProperty<RecordWithNoDate>(x => x.Quantity);
		public static readonly PropertyInfo ProductId = TypeHelper.GetProperty<RecordWithNoDate>(x => x.ProductId);
		public static readonly PropertyInfo UserId = TypeHelper.GetProperty<RecordWithNoDate>(x => x.UserId);
		public static readonly PropertyInfo IsLoyaltyMember = TypeHelper.GetProperty<RecordWithNoDate>(x => x.IsLoyaltyMember);
	}
}
