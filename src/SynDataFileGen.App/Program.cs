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
#if DEBUG
			Console.WriteLine(Directory.GetCurrentDirectory());
#endif

			if (args.Length == 0 || args.Length > 1 || !File.Exists(args[0]))
			{
				Console.BackgroundColor = ConsoleColor.DarkBlue;
				Console.ForegroundColor = ConsoleColor.Yellow;

				Console.WriteLine("Usage: sdfg {run file path}");

				Console.ResetColor();

				Console.BackgroundColor = ConsoleColor.DarkBlue;
				Console.ForegroundColor = ConsoleColor.White;

				Console.WriteLine("{run file path} is a valid path to a JSON runfile.");
				Console.WriteLine("See sample runfiles at https://github.com/pelazem/syndatafilegen/tree/master/runfiles.");

				Console.ResetColor();

				return;
			}

			Stopwatch sw = null;

			Console.WriteLine("Starting...");
			sw = new Stopwatch();
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