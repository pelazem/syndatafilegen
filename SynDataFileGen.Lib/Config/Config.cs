using System;
using System.Collections.Generic;
using System.Text;

namespace SynDataFileGen.Lib
{
	public class Config
	{
		public GeneratorConfig Generator { get; } = new GeneratorConfig();

		public List<FileSpecConfig> FileSpecs { get; } = new List<FileSpecConfig>();
	}
}
