using System;
using System.Collections.Generic;
using System.IO;
using SynDataFileGen.Lib;
using Newtonsoft.Json;
using pelazem.util;

namespace SynDataFileGen.RunFileUtil
{
	class Program
	{
		static void Main(string[] args)
		{
			//Config config = ReadRunFile();

			string outputFolder = @"..\SampleRunFiles\";
			if (!Directory.Exists(outputFolder))
				Directory.CreateDirectory(outputFolder);

			File.WriteAllText(Path.Combine(outputFolder, @"Delimited\runFile.json"), GetRunFile_Delimited());
			File.WriteAllText(Path.Combine(outputFolder, @"FixedWidth\runFile.json"), GetRunFile_FixedWidth());

		}

		private static Config ReadRunFile(string path)
		{
			string content = File.ReadAllText(path);

			Config config = JsonConvert.DeserializeObject<Config>(content);

			return config;
		}

		private static string GetRunFile_FullTemplate()
		{
			Config config = new Config();

			config.Generator.OutputFolderRoot = @"d:\temp\";

			FileSpecConfig fileSpecConfig = new FileSpecConfig()
			{
				FileType = "Delimited",
				RecordsPerFileMin = 100,
				RecordsPerFileMax = 200,
				PathSpec = @"{yyyy}\{mm}\{dd}\{hh}.txt",
				FieldNameForLoopDateTime = "EventDateTime",
				DateStart = new DateTime(2017, 1, 1),
				DateEnd = new DateTime(2017, 3, 31),
				IncludeHeaderRow = false,
				Delimiter = "|",
				Encloser = "'",
				EncodingName = "UTF8",

				//FixedWidthAddPadding = "AtStart",
				//FixedWidthPaddingChar = ' ',
				//FixedWidthTruncate = "AtEnd",
			};
			config.FileSpecs.Add(fileSpecConfig);

			FieldSpecConfig fieldSpecConfig1 = new FieldSpecConfig()
			{
				FieldType = "ContinuousNumeric",
				EnforceUniqueValues = false,
				FormatString = "{0:n}",
				MaxDigitsAfterDecimalPoint = 0,
				NumericDistribution = new DistributionConfig()
				{
					DistributionName = "Incrementing",
					Seed = 1000000,
					Increment = 1
				}
			};
			fileSpecConfig.FieldSpecs.Add(fieldSpecConfig1);

			FieldSpecConfig fieldSpecConfig2 = new FieldSpecConfig()
			{
				FieldType = "ContinuousNumeric",
				EnforceUniqueValues = false,
				FormatString = "{0:n}",
				MaxDigitsAfterDecimalPoint = 0,
				NumericDistribution = new DistributionConfig()
				{
					DistributionName = "Uniform",
					Min = 1000000,
					Max = 9999999
				}
			};
			fileSpecConfig.FieldSpecs.Add(fieldSpecConfig2);

			FieldSpecConfig fieldSpecConfig3 = new FieldSpecConfig()
			{
				FieldType = "Categorical",
				EnforceUniqueValues = false
			};
			fieldSpecConfig3.Categories.AddRange
			(
				new List<Category>()
				{
					new Category { Value = "Monday", Weight = 0 },
					new Category { Value = "Tuesday", Weight = 0 },
					new Category { Value = "Wednesday", Weight = 0 },
					new Category { Value = "Thursday", Weight = 0 },
					new Category { Value = "Friday", Weight = 0 },
					new Category { Value = "Saturday", Weight = 0 },
					new Category { Value = "Sunday", Weight = 0 }
				}
			);
			fileSpecConfig.FieldSpecs.Add(fieldSpecConfig3);

			return JsonConvert.SerializeObject(config);
		}

		private static string GetRunFile_Delimited()
		{
			Config config = new Config();


			// Generator
			config.Generator.OutputFolderRoot = @"d:\temp\";


			// File Spec
			FileSpecConfig fileSpecConfig = new FileSpecConfig()
			{
				FileType = "Delimited",
				RecordsPerFileMin = 100,
				RecordsPerFileMax = 200,
				PathSpec = @"{yyyy}\{mm}\{dd}\{hh}.txt",
				FieldNameForLoopDateTime = "EventDateTime",
				DateStart = new DateTime(2017, 1, 1),
				DateEnd = new DateTime(2017, 3, 31),
				IncludeHeaderRow = false,
				Delimiter = "|",
				Encloser = "'",
				EncodingName = "UTF8"
			};
			config.FileSpecs.Add(fileSpecConfig);


			// Field Specs
			FieldSpecConfig fieldSpecConfig1 = new FieldSpecConfig()
			{
				FieldType = ConfigValues.FIELDTYPE_CONTINUOUSNUMERIC,
				Name = "Id",
				EnforceUniqueValues = false,
				MaxDigitsAfterDecimalPoint = 0,
				NumericDistribution = new DistributionConfig()
				{
					DistributionName = ConfigValues.DISTRIBUTION_INCREMENTING,
					Seed = 1000000,
					Increment = 1
				}
			};
			fileSpecConfig.FieldSpecs.Add(fieldSpecConfig1);

			FieldSpecConfig fieldSpecConfig2 = new FieldSpecConfig()
			{
				FieldType = ConfigValues.FIELDTYPE_CONTINUOUSNUMERIC,
				Name = "Qty",
				EnforceUniqueValues = false,
				FormatString = "{0:" + pelazem.util.Constants.FORMAT_NUM_0 + "}",
				MaxDigitsAfterDecimalPoint = 0,
				NumericDistribution = new DistributionConfig()
				{
					DistributionName = ConfigValues.DISTRIBUTION_UNIFORM,
					Min = 1000000,
					Max = 9999999
				}
			};
			fileSpecConfig.FieldSpecs.Add(fieldSpecConfig2);

			FieldSpecConfig fieldSpecConfig3 = new FieldSpecConfig()
			{
				FieldType = ConfigValues.FIELDTYPE_CATEGORICAL,
				EnforceUniqueValues = false
			};
			fieldSpecConfig3.Categories.AddRange
			(
				new List<Category>()
				{
					new Category { Value = "Monday", Weight = 0 },
					new Category { Value = "Tuesday", Weight = 0 },
					new Category { Value = "Wednesday", Weight = 0 },
					new Category { Value = "Thursday", Weight = 0 },
					new Category { Value = "Friday", Weight = 0 },
					new Category { Value = "Saturday", Weight = 0 },
					new Category { Value = "Sunday", Weight = 0 }
				}
			);
			fileSpecConfig.FieldSpecs.Add(fieldSpecConfig3);

			return JsonConvert.SerializeObject(config);
		}

		private static string GetRunFile_FixedWidth()
		{
			Config config = new Config();

			config.Generator.OutputFolderRoot = @"d:\temp\";

			FileSpecConfig fileSpecConfig = new FileSpecConfig()
			{
				FileType = "FixedWidth",
				DateStart = new DateTime(2017, 1, 1),
				DateEnd = new DateTime(2017, 3, 31),
				EncodingName = "ASCII",
				FixedWidthAddPadding = "AtStart",
				FixedWidthPaddingChar = ' ',
				FixedWidthTruncate = "AtEnd",
				IncludeHeaderRow = false,
				PathSpec = @"{yyyy}-{mm}-{dd}-{hh}.txt",
				FieldNameForLoopDateTime = "EventDateTime",
				RecordsPerFileMin = 300,
				RecordsPerFileMax = 400
			};
			config.FileSpecs.Add(fileSpecConfig);

			FieldSpecConfig fieldSpecConfig1 = new FieldSpecConfig()
			{
				FieldType = "ContinuousNumeric",
				EnforceUniqueValues = false,
				FormatString = "{0:n}",
				MaxDigitsAfterDecimalPoint = 2,
				NumericDistribution = new DistributionConfig()
				{
					DistributionName = "Incrementing",
					Seed = 1000000,
					Increment = 1
				}
			};
			fileSpecConfig.FieldSpecs.Add(fieldSpecConfig1);

			FieldSpecConfig fieldSpecConfig2 = new FieldSpecConfig()
			{
				FieldType = "ContinuousNumeric",
				EnforceUniqueValues = false,
				FormatString = "{0:n}",
				MaxDigitsAfterDecimalPoint = 0,
				NumericDistribution = new DistributionConfig()
				{
					DistributionName = "Normal",
					Mean = 1000,
					StandardDeviation = 300
				}
			};
			fileSpecConfig.FieldSpecs.Add(fieldSpecConfig2);

			FieldSpecConfig fieldSpecConfig3 = new FieldSpecConfig()
			{
				FieldType = "Categorical",
				EnforceUniqueValues = false
			};
			fieldSpecConfig3.Categories.AddRange
			(
				new List<Category>()
				{
					new Category { Value = "Carbon", Weight = 6 },
					new Category { Value = "Nitrogen", Weight = 5 },
					new Category { Value = "Oxygen", Weight = 8 },
					new Category { Value = "Aluminum", Weight = 0.1 },
					new Category { Value = "Gold", Weight = 0.01 }
				}
			);
			fileSpecConfig.FieldSpecs.Add(fieldSpecConfig3);

			return JsonConvert.SerializeObject(config);
		}
	}
}