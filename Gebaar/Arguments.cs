using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
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
					{ "l|list=", f => this.Filter = f },
				};

				options.Parse (_args);
			}
		}

		public string Filter {
			get;
			set;
		}

		public string Path {
			get;
			set;
		}
	}
}