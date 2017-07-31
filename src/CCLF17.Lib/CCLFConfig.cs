using System;

namespace CCLF17.Lib
{
	public class CCLFConfig
	{
		public string OutputFolder { get; set; }
		public int RecordsPerFileMin { get; set; }
		public int RecordsPerFileMax { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }
	}
}
