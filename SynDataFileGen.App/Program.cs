using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using Newtonsoft.Json;
using SynDataFileGen.Lib;

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

			Console.WriteLine("Starting...");

			Stopwatch sw = new Stopwatch();

			sw.Start();

			Config config = ReadRunFile(args[0]);

			List<Generator> generators = Factory.Get(config);

			generators.ForEach(g => g.Run());

			sw.Stop();

			Console.WriteLine("Completed. Elapsed time: " + sw.Elapsed.ToString());
		}

		private static Config ReadRunFile(string path)
		{
			string runFileContent = File.ReadAllText(path);

			return JsonConvert.DeserializeObject<Config>(runFileContent);
		}
	}
}