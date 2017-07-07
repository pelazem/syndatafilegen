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
			string baseFolder = @"..\SampleRunFiles\";

			Config config = ReadRunFile(Path.Combine(baseFolder, @"Delimited_DateRange\runFile.json"));

			//WriteRunFile(Path.Combine(baseFolder, @"FullTemplate\runFile.json"), GetRunFile_FullTemplate());
			//WriteRunFile(Path.Combine(baseFolder, @"Delimited_DateRange\runFile.json"), GetRunFile_Delimited_DateRange());
			//WriteRunFile(Path.Combine(baseFolder, @"FixedWidth_DateRange\runFile.json"), GetRunFile_FixedWidth_DateRange());

		}

		private static Config ReadRunFile(string path)
		{
			string content = File.ReadAllText(path);

			Config config = JsonConvert.DeserializeObject<Config>(content);

			return config;
		}

		private static void WriteRunFile(string path, string contents)
		{
			string folderPath = Path.GetDirectoryName(path);

			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			File.WriteAllText(path, contents);
		}

		private static string GetRunFile_FullTemplate()
		{
			Config config = new Config();

			FileSpecConfig fileSpecConfig = new FileSpecConfig();
			config.FileSpecs.Add(fileSpecConfig);

			FieldSpecConfig fieldSpecConfig = new FieldSpecConfig();
			fieldSpecConfig.NumericDistribution = new DistributionConfig();
			fieldSpecConfig.Categories.Add(new Category() { Value = "Category 1", Weight = 0.25 });
			fieldSpecConfig.Categories.Add(new Category() { Value = "Category 2", Weight = 0.75 });
			fileSpecConfig.FieldSpecs.Add(fieldSpecConfig);

			JsonSerializerSettings settings = new JsonSerializerSettings();
			settings.DefaultValueHandling = DefaultValueHandling.Include;
			return JsonConvert.SerializeObject(config, settings);
		}

		private static string GetRunFile_Delimited_DateRange()
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
				IncludeHeaderRow = true,
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


			FieldSpecConfig fieldSpecConfig2a = new FieldSpecConfig()
			{
				FieldType = ConfigValues.FIELDTYPE_CONTINUOUSNUMERIC,
				Name = "Qty",
				EnforceUniqueValues = false,
				FormatString = "{0:" + pelazem.util.Constants.FORMAT_NUM_0 + "}",
				MaxDigitsAfterDecimalPoint = 0,
				NumericDistribution = new DistributionConfig()
				{
					DistributionName = ConfigValues.DISTRIBUTION_NORMAL,
					Mean = 80,
					StandardDeviation = 35
				}
			};
			fileSpecConfig.FieldSpecs.Add(fieldSpecConfig2a);


			FieldSpecConfig fieldSpecConfig2b = new FieldSpecConfig()
			{
				FieldType = ConfigValues.FIELDTYPE_CONTINUOUSNUMERIC,
				Name = "UnitPrice",
				EnforceUniqueValues = false,
				FormatString = "{0:" + pelazem.util.Constants.FORMAT_CURRENCY + "}",
				MaxDigitsAfterDecimalPoint = 2,
				NumericDistribution = new DistributionConfig()
				{
					DistributionName = ConfigValues.DISTRIBUTION_UNIFORM,
					Min = 0.15,
					Max = 149.99
				}
			};
			fileSpecConfig.FieldSpecs.Add(fieldSpecConfig2b);


			FieldSpecConfig fieldSpecConfig3 = new FieldSpecConfig()
			{
				FieldType = ConfigValues.FIELDTYPE_CATEGORICAL,
				Name = "DayOfWeek",
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


			FieldSpecConfig fieldSpecConfig4 = new FieldSpecConfig()
			{
				FieldType = ConfigValues.FIELDTYPE_DYNAMIC,
				Name = "Guid",
				DynamicFunc = "() => Guid.NewGuid().ToString()"
			};
			fileSpecConfig.FieldSpecs.Add(fieldSpecConfig4);


			FieldSpecConfig fieldSpecConfig5 = new FieldSpecConfig()
			{
				FieldType = ConfigValues.FIELDTYPE_DYNAMIC,
				Name = "EventDateTime",
				DynamicFunc = null,
				FormatString = "{0:yyyy-MM-ddTHH:mm:ssK}"
			};
			fileSpecConfig.FieldSpecs.Add(fieldSpecConfig5);


			return JsonConvert.SerializeObject(config);
		}

		private static string GetRunFile_FixedWidth_DateRange()
		{
			Config config = new Config();


			// Generator
			config.Generator.OutputFolderRoot = @"d:\temp\";


			// File Spec
			FileSpecConfig fileSpecConfig = new FileSpecConfig()
			{
				FileType = "FixedWidth",
				RecordsPerFileMin = 300,
				RecordsPerFileMax = 400,
				PathSpec = @"{yyyy}-{mm}-{dd}-{hh}.txt",
				FieldNameForLoopDateTime = "EventDateTime",
				DateStart = new DateTime(2017, 1, 1),
				DateEnd = new DateTime(2017, 3, 31),
				IncludeHeaderRow = false,
				Delimiter = "|",
				Encloser = "'",
				EncodingName = "ASCII",
				FixedWidthPaddingChar = ' ',
				FixedWidthAddPadding = "AtStart",
				FixedWidthTruncate = "AtEnd"
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


			FieldSpecConfig fieldSpecConfig2a = new FieldSpecConfig()
			{
				FieldType = ConfigValues.FIELDTYPE_CONTINUOUSNUMERIC,
				Name = "Qty",
				EnforceUniqueValues = false,
				FormatString = "{0:" + pelazem.util.Constants.FORMAT_NUM_0 + "}",
				MaxDigitsAfterDecimalPoint = 0,
				NumericDistribution = new DistributionConfig()
				{
					DistributionName = ConfigValues.DISTRIBUTION_NORMAL,
					Mean = 800,
					StandardDeviation = 350
				}
			};
			fileSpecConfig.FieldSpecs.Add(fieldSpecConfig2a);


			FieldSpecConfig fieldSpecConfig2b = new FieldSpecConfig()
			{
				FieldType = ConfigValues.FIELDTYPE_CONTINUOUSNUMERIC,
				Name = "UnitPrice",
				EnforceUniqueValues = false,
				FormatString = "{0:" + pelazem.util.Constants.FORMAT_CURRENCY + "}",
				MaxDigitsAfterDecimalPoint = 2,
				NumericDistribution = new DistributionConfig()
				{
					DistributionName = ConfigValues.DISTRIBUTION_UNIFORM,
					Min = 123.45,
					Max = 180000.66
				}
			};
			fileSpecConfig.FieldSpecs.Add(fieldSpecConfig2b);


			FieldSpecConfig fieldSpecConfig3 = new FieldSpecConfig()
			{
				FieldType = ConfigValues.FIELDTYPE_CATEGORICAL,
				Name = "DayOfWeek",
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
					new Category { Value = "Gold", Weight = 0.01 },
					new Category {Value = "Plutonium", Weight = 0.1}
				}
			);
			fileSpecConfig.FieldSpecs.Add(fieldSpecConfig3);


			FieldSpecConfig fieldSpecConfig4 = new FieldSpecConfig()
			{
				FieldType = ConfigValues.FIELDTYPE_DYNAMIC,
				Name = "Guid",
				DynamicFunc = "() => Guid.NewGuid().ToString()"
			};
			fileSpecConfig.FieldSpecs.Add(fieldSpecConfig4);


			FieldSpecConfig fieldSpecConfig5 = new FieldSpecConfig()
			{
				FieldType = ConfigValues.FIELDTYPE_DYNAMIC,
				Name = "EventDateTime",
				DynamicFunc = null,
				FormatString = "{0:yyyy-MM-ddTHH:mm:ssK}"
			};
			fileSpecConfig.FieldSpecs.Add(fieldSpecConfig5);



			return JsonConvert.SerializeObject(config);
		}
	}
}