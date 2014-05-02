using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Mono.Options;

namespace Gebaar
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var arguments = new Arguments (args);
			arguments.Parse ();

			var iniReader = new IniReader(arguments.Path);

			var begrippen = iniReader.Read("begrip_ok.ini").ToDictionary(l => l[1].Split (';').First (), l => l[0]);
			var gebaren = iniReader.Read("gebaar_ok.ini").ToDictionary(l => l[1].Split(';').First (), l => l[1]);
			var films = iniReader.Read("film_ok.ini").ToDictionary(l => l[0], l => l[1]);

			if (arguments.List) {
				DisplayList (begrippen, arguments.Filter);
			}

			var begripToFind = arguments.Name;
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

				Process.Start("/Applications/QuickTime Player.app", Path.Combine(arguments.Path, "films", films[filmNr] + ".mpg"));
			}
		}


		static void DisplayList (IEnumerable<KeyValuePair<string,string>> begrippen, string filter)
		{
			var names = begrippen;
			if (!string.IsNullOrEmpty(filter))
				names = begrippen.Where (b => b.Key.StartsWith (filter));

			foreach (var name in names.OrderBy (b => b.Key))
				Console.WriteLine (name.Key);
		}
	}


}