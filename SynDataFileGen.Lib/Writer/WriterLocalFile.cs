using System;
using System.Collections.Generic;
using System.IO;

namespace SynDataFileGen.Lib
{
	public class WriterLocalFile : IWriter
	{
		public async void Write(string uri, Stream contents)
		{
			{
				if (File.Exists(uri))
					File.Delete(uri);

				string fullFolderPath = Path.GetDirectoryName(uri);

				if (!Directory.Exists(fullFolderPath))
					Directory.CreateDirectory(fullFolderPath);

				try
				{
					using (FileStream fs = File.Create(uri))
					{
						contents.Seek(0, SeekOrigin.Begin);
						await contents.CopyToAsync(fs);
					}
				}
				catch (Exception ex)
				{
				}
			}
		}
	}
}
