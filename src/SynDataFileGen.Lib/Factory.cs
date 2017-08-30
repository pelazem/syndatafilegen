using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Scripting;

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

				switch (fileSpecConfig.FileType.ToLowerInvariant())
				{
					case ConfigValues.FILETYPE_ARFF:
						fileSpec = GetFileSpecArff(fileSpecConfig);
						break;
					case ConfigValues.FILETYPE_AVRO:
						fileSpec = GetFileSpecAvro(fileSpecConfig);
						break;
					case ConfigValues.FILETYPE_DELIMITED:
						fileSpec = GetFileSpecDelimited(fileSpecConfig);
						break;
					case ConfigValues.FILETYPE_FIXEDWIDTH:
						fileSpec = GetFileSpecFixedWidth(fileSpecConfig);
						break;
					case ConfigValues.FILETYPE_JSON:
						fileSpec = GetFileSpecJson(fileSpecConfig);
						break;
				}

				Generator generator = new Generator(config.Generator.OutputFolderRoot, config.Generator.DateStart, config.Generator.DateEnd, fileSpec, new WriterLocalFile());

				result.Add(generator);
			}

			return result;
		}

		private static IFileSpec GetFileSpecArff(FileSpecConfig fileSpecConfig)
		{
			return new FileSpecArff
			(
				fileSpecConfig.RecordSetName,
				GetFieldSpecs(fileSpecConfig.FieldSpecs),
				Util.GetEncoding(fileSpecConfig.EncodingName),
				fileSpecConfig.RecordsPerFileMin,
				fileSpecConfig.RecordsPerFileMax,
				fileSpecConfig.PathSpec,
				fileSpecConfig.FieldNameForLoopDateTime
			);
		}

		private static IFileSpec GetFileSpecAvro(FileSpecConfig fileSpecConfig)
		{
			return new FileSpecAvro
			(
				fileSpecConfig.RecordSetName,
				GetFieldSpecs(fileSpecConfig.FieldSpecs),
				fileSpecConfig.RecordsPerFileMin,
				fileSpecConfig.RecordsPerFileMax,
				fileSpecConfig.PathSpec,
				fileSpecConfig.FieldNameForLoopDateTime
			);
		}

		private static IFileSpec GetFileSpecDelimited(FileSpecConfig fileSpecConfig)
		{
			return new FileSpecDelimited
			(
				fileSpecConfig.IncludeHeaderRow,
				fileSpecConfig.Delimiter,
				fileSpecConfig.Encloser,
				GetFieldSpecs(fileSpecConfig.FieldSpecs),
				Util.GetEncoding(fileSpecConfig.EncodingName),
				fileSpecConfig.RecordsPerFileMin,
				fileSpecConfig.RecordsPerFileMax,
				fileSpecConfig.PathSpec,
				fileSpecConfig.FieldNameForLoopDateTime
			);
		}

		private static IFileSpec GetFileSpecFixedWidth(FileSpecConfig fileSpecConfig)
		{
			return new FileSpecFixedWidth
			(
				fileSpecConfig.IncludeHeaderRow,
				fileSpecConfig.Delimiter,
				fileSpecConfig.Encloser,
				(fileSpecConfig.FixedWidthPaddingChar != null ? fileSpecConfig.FixedWidthPaddingChar.Value : ConfigValues.DEFAULT_PADDING_CHAR),
				GetLocation(fileSpecConfig.FixedWidthAddPadding),
				GetLocation(fileSpecConfig.FixedWidthTruncate),
				GetFieldSpecs(fileSpecConfig.FieldSpecs),
				Util.GetEncoding(fileSpecConfig.EncodingName),
				fileSpecConfig.RecordsPerFileMin,
				fileSpecConfig.RecordsPerFileMax,
				fileSpecConfig.PathSpec,
				fileSpecConfig.FieldNameForLoopDateTime
			);
		}

		private static IFileSpec GetFileSpecJson(FileSpecConfig fileSpecConfig)
		{
			return new FileSpecJson
			(
				GetFieldSpecs(fileSpecConfig.FieldSpecs),
				Util.GetEncoding(fileSpecConfig.EncodingName),
				fileSpecConfig.RecordsPerFileMin,
				fileSpecConfig.RecordsPerFileMax,
				fileSpecConfig.PathSpec,
				fileSpecConfig.FieldNameForLoopDateTime
			);
		}

		private static List<IFieldSpec> GetFieldSpecs(List<FieldSpecConfig> fieldSpecConfigs)
		{
			List<IFieldSpec> result = new List<IFieldSpec>();

			foreach (FieldSpecConfig fieldSpecConfig in fieldSpecConfigs)
			{
				IFieldSpec fieldSpec = null;

				switch (fieldSpecConfig.FieldType)
				{
					case ConfigValues.FIELDTYPE_CATEGORICAL:
						fieldSpec = new FieldSpecCategorical(fieldSpecConfig.Name, fieldSpecConfig.Categories, fieldSpecConfig.EnforceUniqueValues, fieldSpecConfig.FormatString, fieldSpecConfig.FixedWidthLength, GetLocation(fieldSpecConfig.FixedWidthAddPadding), GetLocation(fieldSpecConfig.FixedWidthTruncate), fieldSpecConfig.FixedWidthPaddingChar, fieldSpecConfig.PercentChanceEmpty, fieldSpecConfig.EmptyValue);
						break;
					case ConfigValues.FIELDTYPE_CONTINUOUSDATETIME:
						fieldSpec = new FieldSpecContinuousDateTime(fieldSpecConfig.Name, fieldSpecConfig.DateStart, fieldSpecConfig.DateEnd, fieldSpecConfig.EnforceUniqueValues, fieldSpecConfig.FormatString, fieldSpecConfig.FixedWidthLength, GetLocation(fieldSpecConfig.FixedWidthAddPadding), GetLocation(fieldSpecConfig.FixedWidthTruncate), fieldSpecConfig.FixedWidthPaddingChar, fieldSpecConfig.PercentChanceEmpty, fieldSpecConfig.EmptyValue);
						break;
					case ConfigValues.FIELDTYPE_CONTINUOUSNUMERIC:
						fieldSpec = new FieldSpecContinuousNumeric(fieldSpecConfig.Name, GetDistribution(fieldSpecConfig.NumericDistribution), fieldSpecConfig.MaxDigitsAfterDecimalPoint, fieldSpecConfig.EnforceUniqueValues, fieldSpecConfig.FormatString, fieldSpecConfig.FixedWidthLength, GetLocation(fieldSpecConfig.FixedWidthAddPadding), GetLocation(fieldSpecConfig.FixedWidthTruncate), fieldSpecConfig.FixedWidthPaddingChar, fieldSpecConfig.PercentChanceEmpty, fieldSpecConfig.EmptyValue);
						break;
					case ConfigValues.FIELDTYPE_DYNAMIC:
						fieldSpec = new FieldSpecDynamic(fieldSpecConfig.Name, GetFunky(fieldSpecConfig.DynamicFunc), fieldSpecConfig.EnforceUniqueValues, fieldSpecConfig.FormatString, fieldSpecConfig.FixedWidthLength, GetLocation(fieldSpecConfig.FixedWidthAddPadding), GetLocation(fieldSpecConfig.FixedWidthTruncate), fieldSpecConfig.FixedWidthPaddingChar, fieldSpecConfig.PercentChanceEmpty, fieldSpecConfig.EmptyValue);
						break;
				}

				if (fieldSpec != null)
					result.Add(fieldSpec);
			}

			return result;
		}

		private static Util.Location GetLocation(string configLocation)
		{
			if (configLocation.ToLowerInvariant() == ConfigValues.LOCATION_ATSTART)
				return Util.Location.AtStart;
			else if (configLocation.ToLowerInvariant() == ConfigValues.LOCATION_ATEND)
				return Util.Location.AtEnd;
			else
				return Util.Location.AtStart;
		}

		private static IDistribution GetDistribution(DistributionConfig config)
		{
			IDistribution result = null;

			switch (config.DistributionName)
			{
				case ConfigValues.DISTRIBUTION_BETA:
					result = new DistBeta(config.A, config.B);
					break;
				case ConfigValues.DISTRIBUTION_CAUCHY:
					result = new DistCauchy(config.Median, config.Scale);
					break;
				case ConfigValues.DISTRIBUTION_CHISQUARE:
					result = new DistChiSquare(config.DegreesOfFreedom);
					break;
				case ConfigValues.DISTRIBUTION_EXPONENTIAL:
					result = new DistExponential(config.Mean);
					break;
				case ConfigValues.DISTRIBUTION_GAMMA:
					result = new DistGamma(config.Shape, config.Scale);
					break;
				case ConfigValues.DISTRIBUTION_INCREMENTING:
					result = new DistIncrementing(config.Seed, config.Increment);
					break;
				case ConfigValues.DISTRIBUTION_INVERSEGAMMA:
					result = new DistInverseGamma(config.Shape, config.Scale);
					break;
				case ConfigValues.DISTRIBUTION_LAPLACE:
					result = new DistLaplace(config.Mean, config.Scale);
					break;
				case ConfigValues.DISTRIBUTION_LOGNORMAL:
					result = new DistLogNormal(config.Mu, config.Sigma);
					break;
				case ConfigValues.DISTRIBUTION_NORMAL:
					result = new DistNormal(config.Mean, config.StandardDeviation);
					break;
				case ConfigValues.DISTRIBUTION_STUDENTT:
					result = new DistStudentT(config.DegreesOfFreedom);
					break;
				case ConfigValues.DISTRIBUTION_UNIFORM:
					result = new DistUniform(config.Min, config.Max);
					break;
				case ConfigValues.DISTRIBUTION_WEIBULL:
					result = new DistWeibull(config.Shape, config.Scale);
					break;
			}

			return result;
		}

		private static Func<object> GetFunky(string funkyText)
		{
			if (string.IsNullOrWhiteSpace(funkyText))
				return null;

			Func<object> result = null;

			try
			{
				result = CSharpScript.EvaluateAsync<Func<object>>(funkyText).Result;
			}
			catch (Exception ex)
			{
				result = null;
				// TODO log exception
				throw ex;
			}

			return result;
		}
	}
}