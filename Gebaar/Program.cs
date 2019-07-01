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
			var arguments = new Arguments (args);
			arguments.Parse ();

			var iniReader = new IniReader(arguments.Path);

			var begrippen = ReadBegrippen (iniReader);

			if (arguments.List) {
				DisplayList (begrippen, arguments.Filter);
				return;
			}

			var begripToFind = arguments.Name;
			var matches = begrippen.Where (b => b.Key.StartsWith (begripToFind));
			if (!matches.Any ())
			{
				Console.WriteLine ("Begrip {0} niet gevonden", begripToFind);
				return;
			}

			var gebaren = ReadGebaren (iniReader);

			var films = ReadFilms(iniReader);

			foreach (var match in matches) {
				ShowOneBegrip (begrippen[match.Key], begripToFind, gebaren, films, arguments.Path);
			}
		}

		static Dictionary<string,string> ReadBegrippen(IniReader reader)
		{
			return reader.Read("begrip_ok.ini").ToDictionary(l => l[1].Split (';').First (), l => l[0]);
		}

		static Dictionary<string,string> ReadGebaren(IniReader reader)
		{
			return reader.Read("gebaar_ok.ini").ToDictionary(l => l[1].Split(';').First (), l => l[1]);
		}

		static Dictionary<string,string> ReadFilms(IniReader reader)
		{
			return reader.Read("film_ok.ini").ToDictionary(l => l[0], l => l[1]);
		}

		static void ShowOneBegrip (string begripNr, string begripToFind, Dictionary<string,string> gebaren, Dictionary<string,string> films, string paths)
		{
			if (!gebaren.ContainsKey (begripNr))
			{
				Console.WriteLine ("Gebaar {0} niet gevonden bij begrip {1}", begripNr, begripToFind);
				return;
			}

			var filmNr = gebaren [begripNr].Split (';').Last ();
			var fileName = "";
			foreach (var path in paths.Split (':'))
			{
				fileName = Path.Combine (path, "films", films [filmNr] + ".mpg");
				if (File.Exists (fileName))
					break;
			}
			Process.Start ("/Applications/QuickTime Player.app", fileName);
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

	public class IniLineComparer : IEqualityComparer<string[]>
	{
		#region IEqualityComparer implementation
		public bool Equals (string[] x, string[] y)
		{
			return x [1].Split (';').First ().Equals (y [1].Split (';').First ());
		}

		public int GetHashCode (string[] obj)
		{
			return obj [1].Split (';').First ().GetHashCode ();
		}
		#endregion
	}


}