namespace Applications;

using System.Text.Json;
using static Utilities.IO;
using static Utilities.Validators;

public class WaterCostCalculator
{
    public static void WaterCostMenu()
    {
        Menu(
            "Time-based or cycle-based?",
            ["Time-based", "Cycle-based"],
            [
                () => WaterCostTimeBased(),
                () => WaterCostCycleBased()
            ]
        );

    }

    public static decimal CostPerDay(
        decimal a, // volume per minute/cycle
        decimal b, // minutes/cycles per day
        decimal pricePer1000L)
    {
        return a * b * pricePer1000L / 1000;
    }

    private static void WaterCostTimeBased()
    {
        var (a, b) = (
            Input<decimal>("Enter flow rate (L/m): ", ValidateDecimal),
            Input<decimal>("Enter minutes per day: ", ValidateDecimal)
        );

        decimal costPerDay = CostPerDay(
            a, b, Input<decimal>("Enter price (£/1000L): ", ValidateDecimal)
        );

        Display(
            [
                $"Daily cost: {costPerDay:C}",
                $"Monthly cost: {costPerDay * 30:C}",
                $"Annual cost: {costPerDay * 365:C}"
            ],
            waitAfterWrite: true
        );
    }

    private static void WaterCostCycleBased()
    {
        var (a, b) = (
            Input<decimal>("Enter cycle volume (L/cycle): ", ValidateDecimal),
            Input<decimal>("Enter cycles per day: ", ValidateDecimal)
        );

        decimal costPerDay = CostPerDay(
            a, b, Input<decimal>("Enter price (£/1000L): ", ValidateDecimal)
        );

        Display(
            [
                $"Daily cost: {costPerDay:C}",
                $"Monthly cost: {costPerDay * 30:C}",
                $"Annual cost: {costPerDay * 365:C}"
            ],
            waitAfterWrite: true
        );
    }
}

public class HexConverter
{
    public static void HexMenu()
    {
        Menu(
            "Convert to decimal or hexadecimal?",
            [
                "From decimal to hexadecimal",
                "From hexadecimal to decimal"
            ],
            [FromDec, FromHex]
        );
    }

    private static void FromDec()
    {
        string hex = Input<string>("Enter denary number: ", ValidateDenary);

        Display($"Hex: {hex}", clearConsole: false, waitAfterWrite: true);
    }

    private static void FromHex()
    {
        int den = Input<int>("Enter hexadecimal number: ", ValidateHexadecimal);

        Display($"Denary: {den}", clearConsole: false, waitAfterWrite: true);
    }
}

public class Library
{
    public List<Book> Books { get; set; }

    public const string FileName = "library.json";

    public Library()
    {
        try
        {
            string jsonString = File.ReadAllText(FileName);

            Books = JsonSerializer.Deserialize<List<Book>>(jsonString)!;
        }
        catch
        {
            Books = [];
        }
    }

    public void Serialize()
    {
        string jsonString = JsonSerializer.Serialize(Books);

        File.WriteAllText(FileName, jsonString);
    }

    public class Book
    {
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public List<string> Genres { get; set; }

        public Book(string title, List<string> authors, List<string> genres)
        {
            Title = title;
            Authors = authors;
            Genres = genres;
        }

        public static string Format(Book book)
        {
            try
            {
                return $"{book.Title} by {string.Join(", ", book.Authors)}";
            }
            catch
            {
                return "";
            }
        }
    }

    public void LibraryMenu()
    {
        Menu(
            "Choose an operation",
            ["List all books", "Add new book"],
            [this.ListBooksMenu, this.AddBook]
        );
    }

    public void ListBooksMenu()
    {
        string[] descriptions = {
            "All books",
            "By title",
            "By author",
            "By genre"
        };

        Action[] actions = [
            ListAllBooks,
            ListBooksByTitle,
            ListBooksByAuthor,
            ListBooksByGenre,
        ];

        Menu("List books", descriptions, actions);
    }

    public void ListAllBooks()
    {
        Display(
            Books
            .Select(b => Book.Format(b))
            .ToArray(),
            waitAfterWrite: true
        );
    }

    public void ListBooksByTitle()
    {
        string title = Input("Search by title: ");

        Display(
            Books
            .Where(b => b.Title == title)
            .Select(b => Book.Format(b))
            .ToArray(),
            waitAfterWrite: true
        );
    }

    public void ListBooksByAuthor()
    {
        string author = Input("Search by author: ");

        Display(
            Books
            .Where(b => b.Authors.Contains(author))
            .Select(b => Book.Format(b))
            .ToArray(),
            waitAfterWrite: true
        );
    }

    public void ListBooksByGenre()
    {
        string genre = Input("Search by genre: ");

        Display(
            Books
            .Where(b => b.Genres.Contains(genre))
            .Select(b => Book.Format(b))
            .ToArray(),
            waitAfterWrite: true
        );
    }

    public void AddBook()
    {
        string title = Input("Enter title: ");

        List<string> authors = [Input("Enter author: ")];
        Menu(
            "Add another author?",
            ["Yes"], [() => authors.Add(Input("Enter author: "))],
            quitMessage: "No"
        );

        List<string> genres = [Input("Enter genre: ")];
        Menu(
            "Add another genre?",
            ["Yes"], [() => genres.Add(Input("Enter genre: "))],
            quitMessage: "No"
        );

        Books.Add(
            new Book(title, authors, genres)
        );
    }
}
