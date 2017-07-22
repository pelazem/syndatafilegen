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

		List<IFieldSpec> FieldSpecs { get; }

		List<ExpandoObject> GetRecords();
		List<ExpandoObject> GetRecords(DateTime dateStart, DateTime dateEnd);

		Stream GetContentStream(List<ExpandoObject> records);
	}
}
