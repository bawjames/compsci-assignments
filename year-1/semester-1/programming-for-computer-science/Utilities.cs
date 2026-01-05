namespace Utilities;

using static Validators;

public class Validators
{
    /// <summary>
    /// Delegate <c>Validator</c> converts string to T, returning a boolean indicating if the conversion was successful.
    /// </summary>
    public delegate bool Validator<T>(string s, out T result);

    public static bool ValidateString(string s, out string result)
    {
        if (!string.IsNullOrWhiteSpace(s))
        {
            result = s;
            return true;
        }

        IO.Display("Input cannot be empty or whitespace.", waitAfterWrite: true);

        result = "";
        return false;
    }

    public static bool ValidateInt(string s, out int result)
    {
        if (int.TryParse(s, out result))
            return true;

        IO.Display($"Please enter an integer.", waitAfterWrite: true);

        return false;
    }

    public static bool ValidateDecimal(string s, out decimal result)
    {
        if (decimal.TryParse(s, out result))
            return true;

        IO.Display($"Please enter a decimal.", waitAfterWrite: true);

        return false;
    }

    public static bool ValidateHexadecimal(string s, out int result)
    {
        if (int.TryParse(s, System.Globalization.NumberStyles.HexNumber, null, out result))
            return true;

        IO.Display($"Please enter a hexadecimal number.", waitAfterWrite: true);

        return false;
    }

    public static bool ValidateDenary(string s, out string result)
    {
        if (int.TryParse(s, null, out int i))
        {
            result = i.ToString("X");
            return true;
        }

        IO.Display($"Please enter a denary number.", waitAfterWrite: true);

        result = "";
        return false;
    }
}

public class IO
{
    public static void Display(
        string[] lines,
        bool clearConsole = true,
        bool waitAfterWrite = false)
    {
        if (clearConsole) Console.Clear();

        Console.Write(string.Join('\n', lines));

        if (waitAfterWrite) {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
    }

    public static void Display(
        string line,
        bool clearConsole = true,
        bool waitAfterWrite = false)
    {
        Display([line], clearConsole, waitAfterWrite);
    }

    public static T Input<T>(string prompt, Validator<T> Validate)
    {
        while (true)
        {
            Display(prompt);

            if (Console.ReadLine() is string s && Validate(s, out T x))
                return x;
        }
    }

    public static string Input(string prompt)
    {
        return Input<string>(prompt, ValidateString);
    }

    public static void Menu(
        string prompt,
        string[] descriptions,
        Action[] actions,
        string quitMessage = "Back")
    {
        if (descriptions.Length > actions.Length) descriptions = descriptions[0..actions.Length];

        char q = char.ToLower(quitMessage[0]);

        while (true)
        {
            Display(
                descriptions
                .Select((d, i) => $"{i}) {d}")
                .Append($"{q}) {quitMessage}\n")
                .Prepend(prompt)
                .ToArray()
            );

            if (MenuInput() is Action app) app();
            else return;
        }

        Action? MenuInput()
        {
            bool noErr = true;

            while (true) switch (Console.ReadKey(true).KeyChar)
            {
                case 'q':
                case var ch when ch == q:
                    return null;

                case var input when int.TryParse(input.ToString(), out int i) && i < actions.Length:
                    return actions[i];

                case var _ when noErr:
                    Console.WriteLine($"Invalid input. Try entering a number [0-{Math.Min(actions.Length - 1, 9)}].");

                    noErr = false;
                    break;
            }
        }
    }
}
