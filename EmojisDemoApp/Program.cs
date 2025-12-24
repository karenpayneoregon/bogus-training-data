using BogusLibrary.Generators;
using BogusLibrary.Models;
using EmojisDemoApp.Classes.Helpers;
using Spectre.Console;

namespace EmojisDemoApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        var humans = HumanGenerator.Create(25);
        var table = CreateTable();

        foreach (var human in humans)
        {
            table.AddRow(
                human.Id.ToString(),
                human.FirstName,
                human.LastName,
                human.Gender == Gender.Female ? "[hotpink2]:female_sign:[/]" : "[blue]:male_sign:[/]",
                human.BirthDate.ToString("yyyy-MM-dd")
            );
        }

        AnsiConsole.Write(table);
        SpectreConsoleHelpers.ExitPrompt();
    }

    private static Table CreateTable()
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.LightCyan1)
            .Alignment(Justify.Center)
            .Title("[cyan]:person_standing: Humans[/]");

        table.AddColumn("[cyan]Id[/]");
        table.AddColumn("[cyan]First Name[/]");
        table.AddColumn("[cyan]Last Name[/]");
        table.AddColumn("[cyan]Gender[/]");
        table.AddColumn("[cyan]Birth Date[/]");

        return table;
    }
}
