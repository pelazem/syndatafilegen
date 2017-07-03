using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SynDataFileGen.Lib
{
	public interface IFileSpec<T>
		where T : new()
	{
		int? RecordsPerFileMin { get; }

		int? RecordsPerFileMax { get; }

		string PathSpec { get; }


		bool HasDateLooping { get; }
		PropertyInfo PropertyForLoopDateTime { get; }
		DateTime? DateStart { get; }
		DateTime? DateEnd { get; }


		List<IFieldSpec<T>> FieldSpecs { get; }

		Stream GetFileContent(List<T> items);
	}
}
