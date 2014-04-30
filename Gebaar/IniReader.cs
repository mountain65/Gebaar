using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Mono.Options;

namespace Gebaar
{

	public class IniReader
	{
		string path;

		public IniReader (string path)
		{
			this.path = path;
		}

		public IEnumerable<string[]> Read(string fileName)
		{
			return File.ReadAllLines(Path.Combine(this.path, fileName)).SkipWhile (w => w.Trim () != "[Data]").Skip (1).Select (b => b.Split ('='));
		}
	}

}