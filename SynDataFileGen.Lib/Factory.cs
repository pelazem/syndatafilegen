using System;
using System.Collections.Generic;
using System.Text;

namespace SynDataFileGen.Lib
{
	public static class Factory
	{
		public static List<Generator> Get(Config config)
		{
			List<Generator> result = new List<Generator>();

			foreach (FileSpecConfig fileSpecConfig in config.FileSpecs)
			{
				IFileSpec fileSpec = null;

				switch (fileSpecConfig.FileType)
				{
					case ConfigValues.FILETYPE_AVRO:
						fileSpec = GetFileSpecAvro(fileSpecConfig);
						break;
					case ConfigValues.FILETYPE_DELIMITED:

						break;
					case ConfigValues.FILETYPE_FIXEDWIDTH:

						break;
					case ConfigValues.FILETYPE_JSON:

						break;
				}


				Generator generator = new Generator(config.Generator.OutputFolderRoot, fileSpec);
			}

			return result;
		}

		private static IFileSpec GetFileSpecAvro(FileSpecConfig fileSpecConfig)
		{
			FileSpecAvro fileSpec = new FileSpecAvro(fileSpecConfig.RecordsPerFileMin, fileSpecConfig.RecordsPerFileMax, fileSpecConfig.PathSpec, fileSpecConfig.FieldNameForLoopDateTime, fileSpecConfig.DateStart, fileSpecConfig.DateEnd);

			fileSpec.FieldSpecs.AddRange(GetFieldSpecs(fileSpecConfig.FieldSpecs));

			return fileSpec;
		}

		private static List<IFieldSpec> GetFieldSpecs(List<FieldSpecConfig> fieldSpecConfigs)
		{
			List<IFieldSpec> result = new List<IFieldSpec>();



			return result;
		}
	}
}