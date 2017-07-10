using System;
using System.Collections.Generic;
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

		Stream GetFileContent(DateTime? dateLoop = null);
	}
}
