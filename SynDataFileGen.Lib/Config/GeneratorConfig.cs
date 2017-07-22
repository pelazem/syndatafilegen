using System;

namespace SynDataFileGen.Lib
{
	public class GeneratorConfig
	{
		public string OutputFolderRoot { get; set; }
		public DateTime? DateStart { get; set; }
		public DateTime? DateEnd { get; set; }
	}
}
