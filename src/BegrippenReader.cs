using System.Linq;
using System.Collections.Generic;

namespace Gebaar
{

    public class BegrippenReader
    {
        readonly string[] begrippen;

		public BegrippenReader(string[] begrippen)
		{
			this.begrippen = begrippen;
		}

		public IEnumerable<Begrip> Read ()
		{
            return 
                this.begrippen
                    .Select(b => Begrip.Parse(b));
		}
	}
}