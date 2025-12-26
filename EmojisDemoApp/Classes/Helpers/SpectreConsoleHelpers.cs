using Spectre.Console;
using System.Runtime.CompilerServices;

namespace EmojisDemoApp.Classes.Helpers;
public static class SpectreConsoleHelpers
{
    /// <summary>
    /// Sets and displays a styled window title in the console using Spectre.Console.
    /// </summary>
    /// <param name="text">
    /// The title text to display. Defaults to "Home screen" if no value is provided.
    /// </param>
    /// <remarks>
    /// This method uses a custom <see cref="Pill"/> element with the <see cref="PillType.Info"/> style
    /// to render the title in a visually appealing format.
    /// </remarks>
    public static void WindowTitle(string text = "Home screen")
    {

        AnsiConsole.Write(new Table()
            .Border(TableBorder.None)
            .Alignment(Justify.Center)
            .AddColumn("")
            .AddRow(new Pill(text, PillType.Info)));

        Console.WriteLine();
    }
    public static void ExitPrompt()
    {
        Console.CursorVisible = false;
        Console.WriteLine();
        
        AnsiConsole.Write(new Table()
            .Border(TableBorder.None)
            .Alignment(Justify.Center)
            .AddColumn("").AddColumn("")
            .AddRow(new Pill("Press any key to exit...", PillType.Info), new Text("")));
        
        Console.ReadLine();
    }



    public static void PrintCyan([CallerMemberName] string? methodName = null)
    {
        AnsiConsole.MarkupLine($"[cyan]{methodName}[/]");
        Console.WriteLine();
    }

    public static void LineSeparator()
    {
        AnsiConsole.Write(new Rule().RuleStyle(Style.Parse("grey")).Centered());
    }

    /// <summary>
    /// Spectre.Console  Add [ to [ and ] to ] so Children[0].Name changes to Children[[0]].Name
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public static string ConsoleEscape(this string sender)
        => Markup.Escape(sender);

    /// <summary>
    /// Spectre.Console Removes markup from the specified string.
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public static string ConsoleRemove(this string sender)
        => Markup.Remove(sender);
}