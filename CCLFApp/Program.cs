using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCLF;
using Generator;

namespace CCLFApp
{
	class Program
	{
		static void Main(string[] args)
		{
			CCLFGenerator gen = new CCLFGenerator();

			gen.Run();
		}
	}
}
