using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Gebaar
{

	public class IniReader
	{
		string path;

		public IniReader (string path)
		{
			this.path = path;
		}

		public IEnumerable<string[]> Read (string fileName)
		{
			var lines = new List<string[]> ();
			foreach (var path in this.path.Split(':'))
				lines.AddRange (ReadOnePath (Path.Combine (path, fileName)));

			return lines.Distinct(new IniLineComparer());
		}

		private IEnumerable<string[]> ReadOnePath (string fullPath)
		{
            if (!File.Exists(fullPath))
                throw new ArgumentException($"Invalid path '{fullPath}'", nameof(fullPath));


            return File.ReadAllLines (fullPath)
                    //.SkipWhile (w => w.Trim () != "[Data]")
                    //.Skip (1)
                .Select (b => b.Split ('='));
		}
	}

}