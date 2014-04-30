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
		string[] args;

		OptionSet options = new OptionSet () {
			{ "-list", f => this.Filter = f },
		};

		public static Arguments Parse(string[] args)
		{
			options.Parse (args);
		}

		public string Filter {
			get;
			set;
		}
	}
}