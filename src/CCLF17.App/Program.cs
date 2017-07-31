using System;
using System.IO;
using CCLF17.Lib;
using Newtonsoft.Json;

namespace CCLF17.App
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length == 0 || args.Length > 1)
			{
				Console.WriteLine("Usage: generator [path to runfile]");
				Console.WriteLine("[path to runfile] is the path to a JSON runfile with config values. See the runfile_cclfgenerator sample in the project's git repo.");
				Console.WriteLine("Press any key to exit.");
				Console.Read();
				return;
			}

			string runFilePath = args[0];

			if (!File.Exists(runFilePath))
			{
				Console.WriteLine("Ummm.... that run file doesn't exist or cannot be read. Try again, OK?");
				Console.WriteLine("Press any key to exit.");
				Console.Read();
			}

			CCLFConfig config = ReadRunFile(runFilePath);

			try
			{
				if (!Directory.Exists(config.OutputFolder))
					Directory.CreateDirectory(config.OutputFolder);
			}
			catch
			{
				Console.WriteLine("Well, that didn't work... The output folder you specified doesn't exist and cannot be created. Try again, OK?");
				Console.WriteLine("Press any key to exit.");
				Console.Read();
			}

			new CCLFGenerator(config).Run();
		}

		private static CCLFConfig ReadRunFile(string path)
		{
			string runFileContent = File.ReadAllText(path);

			return JsonConvert.DeserializeObject<CCLFConfig>(runFileContent);
		}

	}
}