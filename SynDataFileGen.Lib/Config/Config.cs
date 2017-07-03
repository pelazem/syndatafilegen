using System;
using System.Collections.Generic;
using System.Text;

namespace SynDataFileGen.Lib
{
	public class Config
	{
		public GeneratorConfig Generator { get; } = new GeneratorConfig();

		public FileSpecConfig FileSpec { get; } = new FileSpecConfig();

		public FieldSpecsListConfig Fields { get; } = new FieldSpecsListConfig();
	}
}
