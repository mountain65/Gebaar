using System.IO;
using System.Linq;
using Gebaar;
using Xunit;
using Shouldly;
using System;

public class IniReaderTest
{
    [Fact]
    public void NonExistingFile_Throws()
    {
        var rdr = new IniReader(@".");

        Should.Throw<ArgumentException>(() => rdr.Read("empty.ini"));
    }

    [Fact]
    public void EmptyFile_ReturnsEmptyArray()
    {
        File.WriteAllLines(@".\empty.ini", new string[] { });
        var rdr = new IniReader(@".");

        rdr.Read("empty.ini").Count().ShouldBe(0);
        File.Delete(@".\empty.ini");
    }

    [Fact]
    public void FileWithValidBegrippen_ReturnsBegrippen()
    {
        var rdr = new BegrippenReader(new[] {
"AARDRIJKSKUNDE; 42500.mpg; ; ; ; ; Topografie / ; 37 - l.bmp / 37 - r.bmp / 1 - l.bmp / 44 - r.bmp",
"ACHT plaatsnaam; 2147428194.mpg; ; ; ; 8340.jpg / 7657.jpg /; Nederland: plaatsnamen / Provincie Noord - Brabant /; 1 - l.bmp / 61 - r.bmp / 1 - l.bmp / 1 - r.bmp",
"ACHTERHOEK; 2147429544.mpg; ; ; ; 8254.jpg /; Provincie Gelderland/; 1 - l.bmp / 22 - r.bmp / 1 - l.bmp / 1 - r.bmp",
        });

        rdr.Read().Count().ShouldBe(3);
        var begrip = rdr.Read().First();
        begrip.Naam.ShouldBe("AARDRIJKSKUNDE");
        begrip.Medium.FilmNaam.ShouldBe("42500.mpg");
        begrip.Themas.Count().ShouldBe(1);
        begrip.Themas.First().ShouldBe("Topografie");

        begrip = rdr.Read().Skip(1).First();
        begrip.Naam.ShouldBe("ACHT plaatsnaam");
        begrip.Medium.FilmNaam.ShouldBe("2147428194.mpg");
        begrip.Themas.Count().ShouldBe(2);
        begrip.Themas.First().ShouldBe("Nederland: plaatsnamen");
        begrip.Themas.Last().ShouldBe("Nederland: Provincie Noord - Brabant");
    }
}
