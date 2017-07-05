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
			Config config = ReadRunFile();
		}

		private static Config ReadRunFile()
		{
			string path = "test.json";
			string content = File.ReadAllText(path);

			Config config = JsonConvert.DeserializeObject<Config>(content);

			return config;
		}

		private static void MakeJson()
		{
			Config config = new Config();

			config.Generator.OutputFolderRoot = @"d:\temp\";

			FileSpecConfig fileSpecConfig1 = new FileSpecConfig()
			{
				DateStart = new DateTime(2017, 1, 1),
				DateEnd = new DateTime(2017, 3, 31),
				Delimiter = "|",
				Encloser = "'",
				EncodingName = "UTF8",
				FileTypeName = "Delimited",
				//FixedWidthAddPadding = "AtStart",
				//FixedWidthPaddingChar = ' ',
				//FixedWidthTruncate = "AtEnd",
				IncludeHeaderRow = false,
				PathSpec = @"{yyyy}\{mm}\{dd}\{hh}.txt",
				PropertyNameForLoopDateTime = "EventDateTime",
				RecordsPerFileMin = 100,
				RecordsPerFileMax = 200
			};
			config.FileSpecs.Add(fileSpecConfig1);

			FieldSpecConfig fieldSpecConfig11 = new FieldSpecConfig()
			{
				FieldSpecTypeName = "ContinuousNumeric",
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
			fileSpecConfig1.FieldSpecs.Add(fieldSpecConfig11);

			FieldSpecConfig fieldSpecConfig12 = new FieldSpecConfig()
			{
				FieldSpecTypeName = "ContinuousNumeric",
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
			fileSpecConfig1.FieldSpecs.Add(fieldSpecConfig12);

			FieldSpecConfig fieldSpecConfig13 = new FieldSpecConfig()
			{
				FieldSpecTypeName = "Categorical",
				EnforceUniqueValues = false
			};
			fieldSpecConfig13.Categories.AddRange
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
			fileSpecConfig1.FieldSpecs.Add(fieldSpecConfig13);



			FileSpecConfig fileSpecConfig2 = new FileSpecConfig()
			{
				//fileSpecConfig2.DateStart = new DateTime(2017, 1, 1);
				//fileSpecConfig2.DateEnd = new DateTime(2017, 3, 31);
				//fileSpecConfig2.Delimiter = "|";
				//fileSpecConfig2.Encloser = "'";
				EncodingName = "ASCII",
				FileTypeName = "FixedWidth",
				FixedWidthAddPadding = "AtStart",
				FixedWidthPaddingChar = ' ',
				FixedWidthTruncate = "AtEnd",
				IncludeHeaderRow = false,
				PathSpec = @"{yyyy}\{mm}\{dd}\{hh}.txt",
				//PropertyNameForLoopDateTime = "EventDateTime",
				RecordsPerFileMin = 300,
				RecordsPerFileMax = 400
			};
			config.FileSpecs.Add(fileSpecConfig2);

			FieldSpecConfig fieldSpecConfig21 = new FieldSpecConfig()
			{
				FieldSpecTypeName = "ContinuousNumeric",
				EnforceUniqueValues = false,
				FormatString = "{0:c}",
				//MaxDigitsAfterDecimalPoint = 0,
				NumericDistribution = new DistributionConfig()
				{
					DistributionName = "Incrementing",
					Seed = 1000000,
					Increment = 1
				}
			};
			fileSpecConfig2.FieldSpecs.Add(fieldSpecConfig21);

			FieldSpecConfig fieldSpecConfig22 = new FieldSpecConfig()
			{
				FieldSpecTypeName = "ContinuousNumeric",
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
			fileSpecConfig2.FieldSpecs.Add(fieldSpecConfig22);

			FieldSpecConfig fieldSpecConfig23 = new FieldSpecConfig()
			{
				FieldSpecTypeName = "Categorical",
				EnforceUniqueValues = false
			};
			fieldSpecConfig23.Categories.AddRange
			(
				new List<Category>()
				{
					new Category { Value = "Carbon", Weight = 1 },
					new Category { Value = "Nitrogen", Weight = 7 },
					new Category { Value = "Oxygen", Weight = 2 },
					new Category { Value = "Aluminum", Weight = 0.1 },
					new Category { Value = "Gold", Weight = 0.01 }
				}
			);
			fileSpecConfig2.FieldSpecs.Add(fieldSpecConfig23);


			string json = JsonConvert.SerializeObject(config);
		}
	}
}