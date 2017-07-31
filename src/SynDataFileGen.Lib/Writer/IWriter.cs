using System;
using System.IO;

namespace SynDataFileGen.Lib
{
	public interface IWriter
	{
		void Write(string uri, Stream contents);
	}
}
