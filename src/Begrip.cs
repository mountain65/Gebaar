using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Gebaar
{
	public class Begrip : IComparable
	{
		private static readonly string docPath = ".";
		private static readonly string tekeningPath = Path.Combine(docPath, "Tekeningen");
		private static readonly string filmPath = Path.Combine(docPath, "Films");
		private static readonly string pictoPath = Path.Combine(docPath, "Pictogrammen");

        public static Begrip Parse(string begrip)
        {
            var parts = begrip.Split(";");
            return new Begrip
            {
                Naam = parts[0],
                Medium = new Medium(parts[1].Trim()),
                Themas = Thema.Parse(parts[6]),
            };
        }

        private static readonly string fotoPath = Path.Combine(docPath, "graphic");
		private static readonly string topoPath = Path.Combine(docPath, "Topografie");
		private static readonly string handvormPath = Path.Combine(docPath, "handvormen");
		
		public string Naam { get; private set; }
        public Medium Medium { get; private set; }

        public IEnumerable<string> Themas { get; private set; } = new List<string>();
		
		//public IList<Handvorm> Handvormen {
		//	get;
		//	set;
		//}
		
		//public static Begrip Deserialize(string begripDefinition)
		//{
		//	var defParts = begripDefinition.Split(';');

		//	if (defParts.Length != 8)
		//		throw new ArgumentException("Begripdefinitie heeft te weinig onderdelen:" + begripDefinition);
			
		//	string filmNaam = null;
		//	if (!string.IsNullOrEmpty(defParts[1]))
		//	{
		//		filmNaam = Path.Combine(filmPath, defParts[1]);
		//	}

		//	string pictogramNaam = null;
		//	if (!string.IsNullOrEmpty(defParts[3]))
		//	{
		//		pictogramNaam = Path.Combine(pictoPath, defParts[3]);
		//	}

		//	var begrip = new Begrip {
		//		Naam = defParts[0],
		//		Media = new Medium {
		//			FilmNaam = filmNaam,
		//			PictogramNaam = pictogramNaam
		//		}
		//	};

		//	begrip.Media.Tekeningen = SplitBegripPart(defParts[2], tekeningPath);
		//	begrip.Media.Fotos = SplitBegripPart(defParts[4], fotoPath);
		//	begrip.Media.Kaarten = SplitBegripPart(defParts[5], topoPath);
		//	begrip.Themas = SplitBegripPart(defParts[6], "");
			
		//	var handvormen = SplitBegripPart(defParts[7], handvormPath);
		//	begrip.Handvormen = new List<Handvorm>();
		//	begrip.Handvormen.Add(new Handvorm { Plaats = Plaats.Links, Volgorde = 1, Foto = handvormen[0] });
		//	begrip.Handvormen.Add(new Handvorm { Plaats = Plaats.Rechts, Volgorde = 1, Foto = handvormen[1] });
		//	begrip.Handvormen.Add(new Handvorm { Plaats = Plaats.Links, Volgorde = 2, Foto = handvormen[2] });
		//	begrip.Handvormen.Add(new Handvorm { Plaats = Plaats.Rechts, Volgorde = 2, Foto = handvormen[3] });
		//	return begrip;
		//}

		//public static string Serialize(Begrip begrip)
		//{
		//	StringBuilder serializedBegrip = new StringBuilder();
			
		//	// Naam
		//	serializedBegrip.Append(begrip.Naam);
		//	serializedBegrip.Append(";");
			
		//	// Film
		//	if (begrip.Media != null && !string.IsNullOrEmpty(begrip.Media.FilmNaam))
		//	{
		//		serializedBegrip.Append(Path.GetFileName(begrip.Media.FilmNaam));
		//	}
		//	serializedBegrip.Append(";");
			
		//	// Tekeningen
		//	if (begrip.Media != null && begrip.Media.Tekeningen != null)
		//	{
		//		foreach (var tekening in begrip.Media.Tekeningen) {
		//			serializedBegrip.Append(Path.GetFileName(tekening)).Append("/");
					
		//		}
		//	}
		//	serializedBegrip.Append(";");
			
		//	// Pictogram
		//	if (begrip.Media != null && !string.IsNullOrEmpty(begrip.Media.PictogramNaam))
		//	{
		//		serializedBegrip.Append(Path.GetFileName(begrip.Media.PictogramNaam));
		//	}
		//	serializedBegrip.Append(";");
			
		//	// Fotos
		//	if (begrip.Media != null && begrip.Media.Fotos != null)
		//	{
		//		foreach (var foto in begrip.Media.Fotos) {
		//			serializedBegrip.Append(Path.GetFileName(foto)).Append("/");
					
		//		}
		//	}
		//	serializedBegrip.Append(";");

		//	// Kaarten
		//	if (begrip.Media != null && begrip.Media.Kaarten != null)
		//	{
		//		foreach (var kaart in begrip.Media.Kaarten) {
		//			serializedBegrip.Append(Path.GetFileName(kaart)).Append("/");
					
		//		}
		//	}
		//	serializedBegrip.Append(";");

		//	// Themas
		//	if (begrip.Themas != null)
		//	{
		//		foreach (var thema in begrip.Themas) {
		//			serializedBegrip.Append(thema).Append("/");
		//		}
		//	}
		//	serializedBegrip.Append(";");
			
		//	// Handvormen
		//	if (begrip.Handvormen != null)
		//	{
		//		foreach (var handvorm in begrip.Handvormen) {
		//			serializedBegrip.Append(Path.GetFileName(handvorm.Foto)).Append("/");
		//		}
		//	}
			
		//	return serializedBegrip.ToString();
		//}
		
		//private static IList<string> SplitBegripPart(string line, string path)
		//{
		//	var list = new List<string>();
		//	if (!string.IsNullOrEmpty(line))
		//	{
		//		var parts = line.Split(new char[] {'/'});
		//		foreach (var part in parts) {
		//			if (!string.IsNullOrEmpty(part))
		//		    {
		//				list.Add(Path.Combine(path, part));
		//			}
		//		}
		//	}
			
		//	return list;
		//}
		
		#region IComparable implementation
		public int CompareTo (object obj)
		{
			Begrip b = obj as Begrip;
			if (b == null)
				return -1;
			
			return this.Naam.CompareTo(b.Naam);
		}

        public class Thema
        {
            public static IEnumerable<string> Parse(string thema)
            {
                return thema
                    .Trim()
                    .Split("/", StringSplitOptions.RemoveEmptyEntries)
                    .Select(t => t.Trim());
            }
        }
        #endregion
    }
}