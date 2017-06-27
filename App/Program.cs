using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Generator;
using pelazem.Common;
using CCLF;

namespace FileGeneratorApp
{
	class Program
	{
		static void Main(string[] args)
		{
			int runOption = GetRunOption();

			if (runOption == 0)
				return;

			string runFilePath = GetRunFilePath(runOption);

			GeneratorConfig config = ReadRunFile(runFilePath);

			if (config == null)
			{
				Console.WriteLine("Could not read specified JSON run file! Please check file path, existence, and structure.");
				return;
			}

			switch (runOption)
			{
				case 1:
					DelimitedFileNoDateLooping(config);
					break;
				case 2:
					DelimitedFileWithDateLooping(config);
					break;
				case 3:
					FixedWidthFileNoDateLooping(config);
					break;
				case 6:
					JsonFileWithDateLooping(config);
					break;
				case 7:
					AvroFileNoDateLooping(config);
					break;
				case 8:
					AvroFileWithDateLooping(config);
					break;
				case 11:
					GenerateCCLFFiles(config);
					break;
			}
		}

		#region Utility

		private static int GetRunOption()
		{
			List<int> validOptions = new List<int>() { 0, 1, 2, 3, 6, 7, 8, 11 };

			Console.WriteLine("Please enter a number corresponding to one of the following options.");
			Console.WriteLine("(Customize the corresponding runfile first!)");
			Console.WriteLine("==============================");
			Console.WriteLine("0 - Quit without further action");
			Console.WriteLine("1 - Delimited - No Date Looping");
			Console.WriteLine("2 - Delimited - Date Looping");
			Console.WriteLine("3 - Fixed Width - No Date Looping");
			Console.WriteLine("6 - JSON - Date Looping");
			Console.WriteLine("7 - Avro - No Date Looping");
			Console.WriteLine("8 - Avro - Date Looping");
			Console.WriteLine("11 - CCLF File Set");
			Console.WriteLine();
			Console.Write("Please enter your selection and press Enter: ");

			string raw = Console.ReadLine();
			int result = Converter.GetInt32(raw);

			if (!validOptions.Contains(result))
				result = 0;

			return result;
		}

		private static string GetRunFilePath(int runOption)
		{
			string result = @"RunFiles\";

			switch (runOption)
			{
				case 1:
					result += "DelimitedFileNoDateLooping.json";
					break;
				case 2:
					result += "DelimitedFileWithDateLooping.json";
					break;
				case 3:
					result += "FixedWidthFileNoDateLooping.json";
					break;
				case 6:
					result += "JsonFileWithDateLooping.json";
					break;
				case 7:
					result += "AvroFileNoDateLooping.json";
					break;
				case 8:
					result += "AvroFileWithDateLooping.json";
					break;
				case 11:
					result += "CCLF.json";
					break;
				default:
					result = string.Empty;
					break;
			}

			return result;
		}

		private static GeneratorConfig ReadRunFile(string pathToRunFile)
		{
			if (!File.Exists(pathToRunFile))
				return null;

			GeneratorConfig config = JsonConvert.DeserializeObject<GeneratorConfig>(File.ReadAllText(pathToRunFile));

			return config;
		}

		#endregion

		private static void DelimitedFileNoDateLooping(GeneratorConfig config)
		{
			// ////////////////////////////////////////
			// Field specs
			List<Category> productIds = Enumerable.Range(1, 20).Select(a => new Category() { Value = a }).ToList();

			List<Category> userIds = Enumerable.Range(1000001, 30).Select(a => new Category() { Value = a }).ToList();

			List<Category> loyalties = new List<Category>()
			{
				new Category() {Value = false, Weight = 0.9},
				new Category() {Value = true, Weight = 0.1}
			};

			List<IFieldSpec<RecordWithDate>> fieldSpecs = new List<IFieldSpec<RecordWithDate>>
			{
				new FieldSpecDynamic<RecordWithDate>(RecordWithDateProps.DateTime, () => DateTime.UtcNow, false, "{0:yyyy-MM-ddTHH:mm:ssK}"),
				new FieldSpecDynamic<RecordWithDate>(RecordWithDateProps.Name, () => Guid.NewGuid().ToString(), false, null),
				new FieldSpecContinuousNumeric<RecordWithDate>(RecordWithDateProps.UnitPrice, new DistUniform(2.22, 145.67), 2, false, "{0:c}"),
				new FieldSpecContinuousNumeric<RecordWithDate>(RecordWithDateProps.Quantity, new DistNormal(14, 5), 2, false, "{0:n}"),
				new FieldSpecCategorical<RecordWithDate>(RecordWithDateProps.ProductId, productIds, false, null),
				new FieldSpecCategorical<RecordWithDate>(RecordWithDateProps.UserId, userIds, false, null),
				new FieldSpecCategorical<RecordWithDate>(RecordWithDateProps.IsLoyaltyMember, loyalties, false, null)
			};
			// ////////////////////////////////////////

			FileSpecDelimited<RecordWithDate> fileSpec = new FileSpecDelimited<RecordWithDate>(config.IncludeHeaderRow, config.Delimiter, config.Encloser, fieldSpecs, config.Encoding, config.RecordsPerFileMin, config.RecordsPerFileMax);

			Generator<RecordWithDate> generator = new Generator<RecordWithDate>
			(
				config.OutputFolderRoot,
				config.PathSpec,
				fileSpec,
				fieldSpecs,
				null,
				null,
				null
			);

			var items = generator.Run();
		}

		private static void DelimitedFileWithDateLooping(GeneratorConfig config)
		{
			// ////////////////////////////////////////
			// Field specs
			List<Category> productIds = Enumerable.Range(1, 20).Select(a => new Category() { Value = a }).ToList();

			List<Category> userIds = Enumerable.Range(1000001, 30).Select(a => new Category() { Value = a }).ToList();

			List<Category> loyalties = new List<Category>()
			{
				new Category() {Value = false, Weight = 0.9},
				new Category() {Value = true, Weight = 0.1}
			};

			List<IFieldSpec<RecordWithDate>> fieldSpecs = new List<IFieldSpec<RecordWithDate>>
			{
				new FieldSpecDynamic<RecordWithDate>(RecordWithDateProps.DateTime, null, false, "{0:yyyy-MM-ddTHH:mm:ssK}"),
				new FieldSpecDynamic<RecordWithDate>(RecordWithDateProps.Name, () => Guid.NewGuid().ToString(), false, null),
				new FieldSpecContinuousNumeric<RecordWithDate>(RecordWithDateProps.UnitPrice, new DistUniform(2.22, 145.67), 2, false, "{0:c}"),
				new FieldSpecContinuousNumeric<RecordWithDate>(RecordWithDateProps.Quantity, new DistNormal(14, 5), 2, false, "{0:n}"),
				new FieldSpecCategorical<RecordWithDate>(RecordWithDateProps.ProductId, productIds, false, null),
				new FieldSpecCategorical<RecordWithDate>(RecordWithDateProps.UserId, userIds, false, null),
				new FieldSpecCategorical<RecordWithDate>(RecordWithDateProps.IsLoyaltyMember, loyalties, false, null)
			};
			// ////////////////////////////////////////

			FileSpecDelimited<RecordWithDate> fileSpec = new FileSpecDelimited<RecordWithDate>(config.IncludeHeaderRow, config.Delimiter, config.Encloser, fieldSpecs, config.Encoding, config.RecordsPerFileMin, config.RecordsPerFileMax);

			Generator<RecordWithDate> generator = new Generator<RecordWithDate>
			(
				config.OutputFolderRoot,
				config.PathSpec,
				fileSpec,
				fieldSpecs,
				config.DateStart,
				config.DateEnd,
				nameof(RecordWithDate.DateTime)
			);

			generator.Run();
		}

		private static void FixedWidthFileNoDateLooping(GeneratorConfig config)
		{
			// ////////////////////////////////////////
			// Field specs
			List<Category> productIds = Enumerable.Range(1, 20).Select(a => new Category() { Value = a }).ToList();

			List<Category> userIds = Enumerable.Range(1000001, 30).Select(a => new Category() { Value = a }).ToList();

			List<Category> loyalties = new List<Category>()
			{
				new Category() {Value = false, Weight = 0.9},
				new Category() {Value = true, Weight = 0.1}
			};

			List<IFieldSpec<RecordWithDate>> fieldSpecs = new List<IFieldSpec<RecordWithDate>>
			{
				new FieldSpecDynamic<RecordWithDate>(RecordWithDateProps.DateTime, null, false, "{0:yyyy-MM-ddTHH:mm:ssK}", 30, Util.Location.AtStart, Util.Location.AtEnd, '*'),
				new FieldSpecDynamic<RecordWithDate>(RecordWithDateProps.Name, () => Guid.NewGuid().ToString(), false, null, 36),
				new FieldSpecContinuousNumeric<RecordWithDate>(RecordWithDateProps.UnitPrice, new DistUniform(2.22, 145.67), 2, false, "{0:c}", 12),
				new FieldSpecContinuousNumeric<RecordWithDate>(RecordWithDateProps.Quantity, new DistNormal(14, 5), 2, false, "{0:n}", 12),
				new FieldSpecCategorical<RecordWithDate>(RecordWithDateProps.ProductId, productIds, false, null, 10),
				new FieldSpecCategorical<RecordWithDate>(RecordWithDateProps.UserId, userIds, false, null, 20),
				new FieldSpecCategorical<RecordWithDate>(RecordWithDateProps.IsLoyaltyMember, loyalties, false, null, 20)
			};

			// ////////////////////////////////////////

			FileSpecFixedWidth<RecordWithDate> fileSpec = new FileSpecFixedWidth<RecordWithDate>(config.IncludeHeaderRow, config.Delimiter, config.Encloser, config.PaddingCharacter, Util.Location.AtStart, Util.Location.AtEnd, fieldSpecs, config.Encoding, config.RecordsPerFileMin, config.RecordsPerFileMax);

			Generator<RecordWithDate> generator = new Generator<RecordWithDate>
			(
				config.OutputFolderRoot,
				config.PathSpec,
				fileSpec,
				fieldSpecs,
				null,
				null,
				null
			);

			generator.Run();
		}

		private static void JsonFileWithDateLooping(GeneratorConfig config)
		{
			// ////////////////////////////////////////
			// Field specs
			List<Category> productIds = Enumerable.Range(1, 20).Select(a => new Category() { Value = a }).ToList();

			List<Category> userIds = Enumerable.Range(1000001, 30).Select(a => new Category() { Value = a }).ToList();

			List<Category> loyalties = new List<Category>()
			{
				new Category() {Value = false, Weight = 0.9},
				new Category() {Value = true, Weight = 0.1}
			};

			List<IFieldSpec<RecordWithDate>> fieldSpecs = new List<IFieldSpec<RecordWithDate>>
			{
				new FieldSpecDynamic<RecordWithDate>(RecordWithDateProps.DateTime, null, false, "{0:yyyy-MM-ddTHH:mm:ssK}"),
				new FieldSpecDynamic<RecordWithDate>(RecordWithDateProps.Name, () => Guid.NewGuid().ToString(), false, null),
				new FieldSpecContinuousNumeric<RecordWithDate>(RecordWithDateProps.UnitPrice, new DistUniform(2.22, 145.67), 2, false, "{0:c}"),
				new FieldSpecContinuousNumeric<RecordWithDate>(RecordWithDateProps.Quantity, new DistNormal(14, 5), 2, false, "{0:n}"),
				new FieldSpecCategorical<RecordWithDate>(RecordWithDateProps.ProductId, productIds, false, null),
				new FieldSpecCategorical<RecordWithDate>(RecordWithDateProps.UserId, userIds, false, null),
				new FieldSpecCategorical<RecordWithDate>(RecordWithDateProps.IsLoyaltyMember, loyalties, false, null)
			};

			// ////////////////////////////////////////

			FileSpecJson<RecordWithDate> fileSpec = new FileSpecJson<RecordWithDate>(config.Encoding, config.RecordsPerFileMin, config.RecordsPerFileMax);

			Generator<RecordWithDate> generator = new Generator<RecordWithDate>
			(
				config.OutputFolderRoot,
				config.PathSpec,
				fileSpec,
				fieldSpecs,
				config.DateStart,
				config.DateEnd,
				nameof(RecordWithDate.DateTime)
			);

			generator.Run();
		}

		private static void AvroFileNoDateLooping(GeneratorConfig config)
		{
			// ////////////////////////////////////////
			// Field specs
			List<Category> productIds = Enumerable.Range(1, 20).Select(a => new Category() { Value = a }).ToList();

			List<Category> userIds = Enumerable.Range(1000001, 30).Select(a => new Category() { Value = a }).ToList();

			List<Category> loyalties = new List<Category>()
			{
				new Category() {Value = false, Weight = 0.9},
				new Category() {Value = true, Weight = 0.1}
			};

			List<IFieldSpec<RecordWithNoDate>> fieldSpecs = new List<IFieldSpec<RecordWithNoDate>>
			{
				new FieldSpecDynamic<RecordWithNoDate>(RecordWithNoDateProps.Name, () => Guid.NewGuid().ToString(), false, null),
				new FieldSpecContinuousNumeric<RecordWithNoDate>(RecordWithNoDateProps.UnitPrice, new DistUniform(2.22, 145.67), 2, false, "{0:c}"),
				new FieldSpecContinuousNumeric<RecordWithNoDate>(RecordWithNoDateProps.Quantity, new DistNormal(14, 5), 2, false, "{0:n}"),
				new FieldSpecCategorical<RecordWithNoDate>(RecordWithNoDateProps.ProductId, productIds, false, null),
				new FieldSpecCategorical<RecordWithNoDate>(RecordWithNoDateProps.UserId, userIds, false, null),
				new FieldSpecCategorical<RecordWithNoDate>(RecordWithNoDateProps.IsLoyaltyMember, loyalties, false, null)
			};

			// ////////////////////////////////////////

			FileSpecAvro<RecordWithNoDate> fileSpec = new FileSpecAvro<RecordWithNoDate>(config.RecordsPerFileMin, config.RecordsPerFileMax);

			Generator<RecordWithNoDate> generator = new Generator<RecordWithNoDate>
			(
				config.OutputFolderRoot,
				config.PathSpec,
				fileSpec,
				fieldSpecs,
				null,
				null,
				null
			);

			generator.Run();
		}

		private static void AvroFileWithDateLooping(GeneratorConfig config)
		{
			// ////////////////////////////////////////
			// Field specs
			List<Category> productIds = Enumerable.Range(1, 20).Select(a => new Category() { Value = a }).ToList();

			List<Category> userIds = Enumerable.Range(1000001, 30).Select(a => new Category() { Value = a }).ToList();

			List<Category> loyalties = new List<Category>()
			{
				new Category() {Value = false, Weight = 0.9},
				new Category() {Value = true, Weight = 0.1}
			};

			List<IFieldSpec<RecordWithNoDate>> fieldSpecs = new List<IFieldSpec<RecordWithNoDate>>
			{
				new FieldSpecDynamic<RecordWithNoDate>(RecordWithNoDateProps.Name, () => Guid.NewGuid().ToString(), false, null),
				new FieldSpecContinuousNumeric<RecordWithNoDate>(RecordWithNoDateProps.UnitPrice, new DistUniform(2.22, 145.67), 2, false, "{0:c}"),
				new FieldSpecContinuousNumeric<RecordWithNoDate>(RecordWithNoDateProps.Quantity, new DistNormal(14, 5), 2, false, "{0:n}"),
				new FieldSpecCategorical<RecordWithNoDate>(RecordWithNoDateProps.ProductId, productIds, false, null),
				new FieldSpecCategorical<RecordWithNoDate>(RecordWithNoDateProps.UserId, userIds, false, null),
				new FieldSpecCategorical<RecordWithNoDate>(RecordWithNoDateProps.IsLoyaltyMember, loyalties, false, null)
			};

			// ////////////////////////////////////////

			FileSpecAvro<RecordWithNoDate> fileSpec = new FileSpecAvro<RecordWithNoDate>(config.RecordsPerFileMin, config.RecordsPerFileMax);

			Generator<RecordWithNoDate> generator = new Generator<RecordWithNoDate>
			(
				config.OutputFolderRoot,
				config.PathSpec,
				fileSpec,
				fieldSpecs,
				config.DateStart,
				config.DateEnd,
				null
			);

			generator.Run();
		}

		private static void GenerateCCLFFiles(GeneratorConfig config)
		{
			CCLFGenerator gen = new CCLFGenerator(config);

			gen.Run();
		}
	}
}
