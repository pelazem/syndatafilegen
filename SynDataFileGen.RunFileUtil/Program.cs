using System;
using System.Collections.Generic;
using System.IO;
using SynDataFileGen.Lib;
using Newtonsoft.Json;

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

			File.WriteAllText(Path.Combine(outputFolder, "delimited.json"), GetRunFile_Delimited());
			File.WriteAllText(Path.Combine(outputFolder, "fixedWidth.json"), GetRunFile_Delimited());

		}

		private static Config ReadRunFile(string path)
		{
			string content = File.ReadAllText(path);

			Config config = JsonConvert.DeserializeObject<Config>(content);

			return config;
		}

		private static string GetRunFile_Delimited()
		{
			Config config = new Config();

			config.Generator.OutputFolderRoot = @"d:\temp\";

			FileSpecConfig fileSpecConfig = new FileSpecConfig()
			{
				DateStart = new DateTime(2017, 1, 1),
				DateEnd = new DateTime(2017, 3, 31),
				Delimiter = "|",
				Encloser = "'",
				EncodingName = "UTF8",
				FileType = "Delimited",
				//FixedWidthAddPadding = "AtStart",
				//FixedWidthPaddingChar = ' ',
				//FixedWidthTruncate = "AtEnd",
				IncludeHeaderRow = false,
				PathSpec = @"{yyyy}\{mm}\{dd}\{hh}.txt",
				PropertyNameForLoopDateTime = "EventDateTime",
				RecordsPerFileMin = 100,
				RecordsPerFileMax = 200
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
				PropertyNameForLoopDateTime = "EventDateTime",
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