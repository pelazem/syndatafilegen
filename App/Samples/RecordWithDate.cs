using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using pelazem.Common;

namespace FileGeneratorApp
{
	public class RecordWithDate
	{
		public DateTime DateTime { get; set; }

		public string Name { get; set; }

		public double UnitPrice { get; set; } 

		public double Quantity { get; set; }

		public int ProductId { get; set; }

		public int UserId { get; set; }

		public bool IsLoyaltyMember { get; set; }
	}

	public class RecordWithDateProps
	{
		public static readonly PropertyInfo DateTime = TypeHelper.GetProperty<RecordWithDate>(x => x.DateTime);
		public static readonly PropertyInfo Name = TypeHelper.GetProperty<RecordWithDate>(x => x.Name);
		public static readonly PropertyInfo UnitPrice = TypeHelper.GetProperty<RecordWithDate>(x => x.UnitPrice);
		public static readonly PropertyInfo Quantity = TypeHelper.GetProperty<RecordWithDate>(x => x.Quantity);
		public static readonly PropertyInfo ProductId = TypeHelper.GetProperty<RecordWithDate>(x => x.ProductId);
		public static readonly PropertyInfo UserId = TypeHelper.GetProperty<RecordWithDate>(x => x.UserId);
		public static readonly PropertyInfo IsLoyaltyMember = TypeHelper.GetProperty<RecordWithDate>(x => x.IsLoyaltyMember);
	}
}
