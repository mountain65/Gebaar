using System;
using System.Collections.Generic;

namespace Gebaar
{
	public class Medium
	{
        public Medium(string filmNaam)
        {
            this.FilmNaam = filmNaam;
        }

		public string FilmNaam {
			get;
		}
		
		public IList<string> Tekeningen {
			get;
		}
		
		public string PictogramNaam {
			get;
		}
		
		public IList<string> Fotos {
			get;
		}
		
		public IList<string> Kaarten {
			get;
		}
	}
}