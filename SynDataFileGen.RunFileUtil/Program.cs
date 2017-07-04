using System;
using System.Collections.Generic;
using SynDataFileGen.Lib;
using Newtonsoft.Json;

namespace SynDataFileGen.RunFileUtil
{
	class Program
	{
		static void Main(string[] args)
		{
			Config config = new Config();

			config.Generator.OutputFolderRoot = @"d:\temp\";

			config.FileSpec.DateStart = new DateTime(2017, 1, 1);
			config.FileSpec.DateEnd = new DateTime(2017, 3, 31);
			config.FileSpec.Delimiter = "|";
			config.FileSpec.Encloser = "'";
			config.FileSpec.EncodingName = "UTF8";
			config.FileSpec.FileTypeName = "Delimited";
			config.FileSpec.FixedWidthAddPadding = "AtStart";
			config.FileSpec.FixedWidthPaddingChar = ' ';
			config.FileSpec.FixedWidthTruncate = "AtEnd";
			config.FileSpec.IncludeHeaderRow = false;
			config.FileSpec.PathSpec = @"{yyyy}\{mm}\{dd}\{hh}.txt";
			config.FileSpec.PropertyNameForLoopDateTime = "EventDateTime";
			config.FileSpec.RecordsPerFileMin = 100;
			config.FileSpec.RecordsPerFileMax = 200;


			FieldSpecConfig config1 = new FieldSpecConfig();
			config1.FieldSpecTypeName = "ContinuousNumeric";
			config1.EnforceUniqueValues = false;
			config1.FormatString = "{0:n}";
			config1.MaxDigitsAfterDecimalPoint = 0;
			config1.NumericDistribution = new DistributionConfig();
			config1.NumericDistribution.DistributionName = "Incrementing";
			config1.NumericDistribution.Seed = 1000000;
			config1.NumericDistribution.Increment = 1;
			config.FieldSpecs.Add(config1);

			FieldSpecConfig config2 = new FieldSpecConfig();
			config2.FieldSpecTypeName = "ContinuousNumeric";
			config2.EnforceUniqueValues = false;
			config2.FormatString = "{0:n}";
			config2.MaxDigitsAfterDecimalPoint = 0;
			config2.NumericDistribution = new DistributionConfig();
			config2.NumericDistribution.DistributionName = "Uniform";
			config2.NumericDistribution.Min = 1000000;
			config2.NumericDistribution.Max = 9999999;
			config.FieldSpecs.Add(config2);

			FieldSpecConfig config3 = new FieldSpecConfig();
			config3.FieldSpecTypeName = "Categorical";
			config3.EnforceUniqueValues = false;
			config3.Categories.AddRange
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
			config.FieldSpecs.Add(config3);



			string json = JsonConvert.SerializeObject(config);
		}
	}
}