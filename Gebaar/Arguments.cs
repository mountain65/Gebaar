using System;
using Mono.Options;

namespace Gebaar
{

	public class Arguments
	{
		string[] _args;

		public Arguments (string[] args = null)
		{
			_args = args;
			this.Filter = "";
			this.Path = ".";
		}

		public void Parse()
		{
			if (_args != null)
			{
				var options = new OptionSet () {
					{ "l|list", "Show a list of all signs, possibly filtered by the argument", f => { this.List = true; this.Filter = f; }},
					{ "p|path=", "Specify path to DVD", p => this.Path = p },
					{ "<>", n => this.Name = n },
				};

				try {
					options.Parse (_args);
				}
				catch (OptionException oe) {
					Console.Write ("Gebaar.exe:");
					Console.WriteLine (oe.Message);
					options.WriteOptionDescriptions (Console.Out);
				}
			}
		}

		public bool List {
			get;
			set;
		}

		public string Filter {
			get;
			set;
		}

		public string Path {
			get;
			set;
		}

		public string Name {
			get;
			set;
		}
	}
}