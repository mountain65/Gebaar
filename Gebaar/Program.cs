using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace Gebaar
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var path = ".";
//			var path = "/Users/mountain/Public/GebarenDVDs/Sexualiteit";
			var iniReader = new IniReader(path);

			var begrippen = iniReader.Read("begrip_ok.ini").ToDictionary(l => l[1].Split (';').First (), l => l[0]);
			var gebaren = iniReader.Read("gebaar_ok.ini").ToDictionary(l => l[1].Split(';').First (), l => l[1]);
			var films = iniReader.Read("film_ok.ini").ToDictionary(l => l[0], l => l[1]);

			var begripToFind = args [0].ToUpper();
			if (begripToFind == "-LIST") {
				DisplayList (begrippen, args);
			}

			var matches = begrippen.Where (b => b.Key.StartsWith (begripToFind));
			if (matches.Count() == 0) {
				Console.WriteLine ("Begrip {0} niet gevonden", begripToFind);
				return;
			}

			foreach (var match in matches) {

				var begripNr = begrippen [match.Key];

				if (!gebaren.ContainsKey (begripNr)) {
					Console.WriteLine ("Gebaar {0} niet gevonden bij begrip {1}", begripNr, begripToFind);
					return;
				}

				var filmNr = gebaren [begripNr].Split (';').Last ();

				Process.Start("/Applications/QuickTime Player.app", Path.Combine(path, "films", films[filmNr] + ".mpg"));
			}
		}


		static void DisplayList (IDictionary<string,string> begrippen, string[] args)
		{
			foreach (var begrip in begrippen.OrderBy (b => b.Key))
				Console.WriteLine (begrip.Key);
		}
	}

	class IniReader
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