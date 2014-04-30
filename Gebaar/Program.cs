using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace Gebaar
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var path = ".";

			var begrippen = File.ReadAllLines (Path.Combine(path, "begrip_ok.ini")).SkipWhile(w => w.Trim () != "[Data]").Skip (1).Select (b => b.Split('=')).ToDictionary(l => l[1].Split (';').First (), l => l[0]);

			var begripToFind = args [0].ToUpper();
			if (begripToFind == "-ALL") {
				foreach(var begrip in begrippen.OrderBy(b => b.Key))
					Console.WriteLine(begrip.Key);
				return;
			}

			if (!begrippen.ContainsKey (begripToFind)) {
				Console.WriteLine ("Begrip {0} niet gevonden", begripToFind);
				return;
			}

			var gebaren = File.ReadAllLines (Path.Combine(path, "gebaar_ok.ini")).SkipWhile(w => w != "[Data]").Skip (1).Select (b => b.Split('=')).ToDictionary(l => l[1].Split(';').First (), l => l[1]);
			var begripNr = begrippen [begripToFind];

			if (!gebaren.ContainsKey (begripNr)) {
				Console.WriteLine ("Gebaar {0} niet gevonden bij begrip {1}", begripNr, begripToFind);
				return;
			}

			var filmNr = gebaren [begripNr].Split (';').Last ();
			var films = File.ReadAllLines (Path.Combine(path, "film_ok.ini")).SkipWhile(w => w != "[Data]").Skip (1).Select (b => b.Split('=')).ToDictionary(l => l[0], l => l[1]);

			Console.WriteLine ("Filmbestand={0}", Path.Combine(path, "films", films[filmNr] + ".mpg"));

			Process.Start("/Applications/QuickTime Player.app", Path.Combine(path, "films", films[filmNr] + ".mpg"));
		}
	}
}
