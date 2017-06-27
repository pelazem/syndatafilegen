using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
	public interface IFileSpec<T>
	{
		int? RecordsPerFileMin { get; }

		int? RecordsPerFileMax { get; }

		Stream GetFileContent(List<T> items);
	}
}
