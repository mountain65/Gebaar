using System;
using NUnit.Framework;
using Gebaar;
using Shouldly;

namespace GebaarTests
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		public void NoArguments_SetDefaults ()
		{
			var arguments = new Arguments ();
			arguments.Parse ();
			arguments.Filter.ShouldBe (string.Empty);
			arguments.Path.ShouldBe (".");
		}
	
		[Test()]
		public void FilterSet_ParsedCorrectly ()
		{
			var arguments = new Arguments (new[] { "-l=aap" });
			arguments.Parse ();
			arguments.Filter.ShouldBe ("aap");
		}

	}
}

