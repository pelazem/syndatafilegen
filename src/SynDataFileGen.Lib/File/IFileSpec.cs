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

		IEnumerable<ExpandoObject> GetRecords();
		IEnumerable<ExpandoObject> GetRecords(DateTime dateStart, DateTime dateEnd);

		Stream GetContentStream(IEnumerable<ExpandoObject> records);
	}
}
