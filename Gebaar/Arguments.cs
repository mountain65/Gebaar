using System;
using System.Configuration;

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
			if (_args.Length == 0 || _args[0] == "-list") {
				this.List = true;
				if (_args.Length == 2 && !string.IsNullOrEmpty (_args [1]))
					this.Filter = _args [1].ToUpper();
			}

			if (_args.Length == 1)
				this.Name = _args [0].ToUpper ();

			if (ConfigurationManager.AppSettings != null)
				this.Path = ConfigurationManager.AppSettings ["path"];
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