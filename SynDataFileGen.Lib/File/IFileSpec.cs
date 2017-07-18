using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;

namespace SynDataFileGen.Lib
{
	public interface IFileSpec
	{
		int? RecordsPerFileMin { get; }
		int? RecordsPerFileMax { get; }

		string PathSpec { get; }

		bool HasDateLooping { get; }

		string FieldNameForLoopDateTime { get; }

		DateTime? DateStart { get; }
		DateTime? DateEnd { get; }

		List<IFieldSpec> FieldSpecs { get; }

		List<ExpandoObject> GetRecords(DateTime? dateLoop = null);
		Stream GetContentStream(List<ExpandoObject> records);
		Stream GetContentStream<T>(List<T> records);
	}
}
