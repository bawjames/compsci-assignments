namespace Assignment;

using static Utilities.IO;
using Applications;

public class Program
{
    public static void Main(string[] args)
    {
        Library library = new Library();

        string[] descs = {
            "Household Water Usage & Cost Calculator",
            "Library Catalogue",
            "Hexadecimal Converter"
        };

        Action[] acts = {
            WaterCostCalculator.WaterCostMenu,
            library.LibraryMenu,
            HexConverter.HexMenu
        };

        Menu("Choose an application:", descs, acts, "Quit");

        library.Serialize();
    }
}
