using System;
using System.Dynamic;
using System.IO;
using Newtonsoft.Json;

namespace SynDataFileGen.App
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("Usage: generator {run file path}");
				Console.WriteLine("Where {run file path} is the path to a JSON runfile.");
				Console.WriteLine("See example sampleRunFile.json in the same folder as the generator app.");
				Console.WriteLine("Press any key to exit.");
				Console.Read();
				return;
			}

			var config = ReadRunFile(args[0]);

			Console.Read();
		}

		private static ExpandoObject ReadRunFile(string path)
		{
			string runFileContent = File.ReadAllText(path);

			return JsonConvert.DeserializeObject<ExpandoObject>(runFileContent);
		}
	}
}