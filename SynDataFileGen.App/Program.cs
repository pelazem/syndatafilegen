using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SynDataFileGen.Lib;

namespace SynDataFileGen.App
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length == 0 || (args.Length == 1 && args[0].ToLowerInvariant() == "-i") || args.Length > 2)
			{
				Console.WriteLine("Usage: generator [-i] {run file path}");
				Console.WriteLine("If -i is specified, some output will be written to the console and the app will wait for user input at end. Otherwise, app runs silently and exits when done.");
				Console.WriteLine("{run file path} is the path to a JSON runfile.");
				Console.WriteLine("See example sampleRunFile.json in the same folder as the generator app.");
				Console.WriteLine("Press any key to exit.");
				Console.Read();
				return;
			}

			bool interactive = (args.FirstOrDefault(a => a.ToLowerInvariant() == "-i") != null);

			Stopwatch sw = null;

			if (interactive)
			{
				Console.WriteLine("Starting...");
				sw = new Stopwatch();
				sw.Start();
			}

			Config config = ReadRunFile(args.FirstOrDefault(a => a.ToLowerInvariant() != "-i"));

			List<Generator> generators = Factory.Get(config);

			generators.ForEach(g => g.Run());

			if (interactive)
			{
				sw.Stop();
				Console.WriteLine("Completed. Elapsed time: " + sw.Elapsed.ToString());
				Console.Read();
			}
		}

		private static Config ReadRunFile(string path)
		{
			string runFileContent = File.ReadAllText(path);

			return JsonConvert.DeserializeObject<Config>(runFileContent);
		}
	}
}