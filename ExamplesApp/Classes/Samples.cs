using BogusLibrary.Classes;
using BogusLibrary.Generators;
using BogusLibrary.LanguageExtensions;
using BogusLibrary.Models;
using ExamplesApp.Classes.Configuration;
using Spectre.Console;
using System.Text.Json;

namespace ExamplesApp.Classes;


/// <summary>
/// Provides a collection of static methods demonstrating various examples and operations 
/// on data, including filtering, grouping, serialization, and formatted output using Spectre.Console.
/// </summary>
/// <remarks>
/// This class contains methods that showcase different functionalities, such as:
/// - Working with decimal numbers, including filtering and ordering.
/// - Displaying high-value products and products in specific categories.
/// - Generating and processing human data.
/// - Performing serialization and deserialization operations.
/// - Grouping and displaying data by specific criteria.
/// </remarks>
internal class Samples
{
    /// <summary>
    /// Demonstrates various operations on a collection of decimal numbers, including:
    /// ordering in descending order, filtering based on a range, and comparing filtered results.
    /// </summary>
    /// <remarks>
    /// This method utilizes Spectre.Console for formatted console output and performs the following:
    /// - Displays all decimal numbers in descending order.
    /// - Filters numbers within a specified range using LINQ and manual filtering.
    /// - Compares the results of LINQ filtering and manual filtering for equality.
    /// </remarks>
    public static void DecimalExamples()
    {

        SpectreConsoleHelpers.PrintCyan();

        var data = DataContainer.Instance.Decimals
            .OrderDescending()
            .ToArray();

        AnsiConsole.MarkupLine("[bold deeppink3]All elements in descending order:[/]");
        for (int index = 0; index < data.Length; index++)
        {
            AnsiConsole.MarkupLine($"[bold green]{index,-4}[/]: [yellow]{data[index]:F}[/]");
        }

        Console.WriteLine();

        AnsiConsole.MarkupLine("[bold deeppink3]IsBetween 300 and 600 using Where:[/]");
        var data1 = DataContainer.Instance.Decimals
            .Where(x => x.Between(300, 600))
            .OrderDescending()
            .ToArray();

        for (int index = 0; index < data1.Length; index++)
        {
            AnsiConsole.MarkupLine($"[bold green]{index,-4}[/]: [yellow]{data1[index]:F}[/]");
        }

        Console.WriteLine();

        AnsiConsole.MarkupLine("[bold deeppink3]IsBetween 300 and 600 using manual if:[/]");

        var data2 = DataContainer.Instance.Decimals
            .OrderDescending()
            .ToArray();

        List<decimal> filteredData = new List<decimal>();

        for (int index = 0; index < data2.Length; index++)
        {
            if (data2[index] >= 300 && data2[index] <= 600)
            {
                AnsiConsole.MarkupLine($"[bold green]{index,-4}[/]: [yellow]{data2[index]:F}[/]");
                filteredData.Add(data2[index]);
            }
        }

        Console.WriteLine();

        bool areEqual = data1.SequenceEqual(filteredData);
        AnsiConsole.MarkupLine($"[bold deeppink3]SequenceEqual Data1 and FilteredData are equal:[/] [bold]{areEqual.ToYesNo()}[/]");
        
    }

    /// <summary>
    /// Displays a list of high-value products with their names and unit prices.
    /// </summary>
    /// <remarks>
    /// This method generates a collection of products, filters them to include only those with a unit price greater than 100,
    /// and displays the filtered products in descending order of their unit prices. The output is formatted using Spectre.Console.
    /// </remarks>
    public static void DisplayHighValueProducts()
    {
        SpectreConsoleHelpers.PrintCyan();

        IOrderedEnumerable<Products> products = ProductGenerator.Create(15)
            .Where(x => x.UnitPrice > 100)
            .OrderByDescending(x => x.UnitPrice);

        foreach (var p in products)
        {
            AnsiConsole.MarkupLine($"[bold green]{p.ProductName,-25}[/][yellow]{p.UnitPrice:C}[/]");
        }
    }

    /// <summary>
    /// Displays a list of clothing products with their names and unit prices.
    /// </summary>
    /// <remarks>
    /// This method generates a collection of products, filters them to include only those in the "Clothing" category,
    /// and displays the filtered products with their names and unit prices formatted using Spectre.Console.
    /// </remarks>
    public static void DisplayClothingProducts()
    {
        SpectreConsoleHelpers.PrintCyan();

        List<Products> products = ProductGenerator.Create(10);

        List<Products> clothing = products
            .Where(p => p.Category.CategoryName == "Clothing")
            .ToList();

        foreach (var p in clothing)
        {
            AnsiConsole.MarkupLine($"[bold green]{p.ProductName,-25}[/][yellow]{p.UnitPrice:C}[/]");
        }

    }

    /// <summary>
    /// Displays a list of humans born between the years 1950 and 1980, inclusive.
    /// </summary>
    /// <remarks>
    /// This method generates a collection of humans, filters them based on their birth year,
    /// and displays their first name, last name, and birth year. The output is formatted using Spectre.Console.
    /// </remarks>
    public static void HumansBornBetween1950And1980()
    {
        SpectreConsoleHelpers.PrintCyan();

        var humans = HumanGenerator.Create(15);

        foreach (var h in humans)
        {
            var year = h.BirthDay!.Value.Year;
            if (year is >= 1950 and <= 1980)
            {
                AnsiConsole.MarkupLine($"[bold green]{h.FirstName,-15}{h.LastName,-15}[/] born in [yellow]{year}[/]");
            }
        }
    }

    /// <summary>
    /// Displays a list of humans along with their social security numbers.
    /// </summary>
    /// <remarks>
    /// This method generates a collection of humans, iterates through the list, 
    /// and displays each human's first name, last name, and social security number.
    /// The output is formatted using Spectre.Console utilities.
    /// </remarks>
    public static void SocialSecurityProperty()
    {

        SpectreConsoleHelpers.PrintCyan();

        var humans = HumanGenerator.Create(25, true);
        foreach (var h in humans)
        {
            //Console.WriteLine($"{h.FirstName,-10} {h.LastName,-15}{h.SocialSecurityNumber.MaskSsn()}");
            Console.WriteLine($"{h.FirstName,-10} {h.LastName,-15}{h.SocialSecurityNumber}");
        }
    }

    /// <summary>
    /// Generates a JSON file containing a collection of humans, then deserializes the JSON back into a list of human objects.
    /// </summary>
    /// <remarks>
    /// This method performs the following operations:
    /// - Generates a JSON representation of a collection of humans and writes it to a file.
    /// - Reads the JSON file and deserializes its content into a list of <see cref="Human"/> objects.
    /// - Outputs the file path to the console for inspection.
    /// </remarks>
    public static void GenerateAndDeserialize()
    {
        SpectreConsoleHelpers.PrintCyan();

        var fileName = "Json\\humans.json";
        File.WriteAllText(fileName, JsonGenerator.HumansAsJson(10));

        // Set a breakpoint on the following line to inspect the 'humans' variable
        var humans = JsonSerializer.Deserialize<List<Human>>(File.ReadAllText(fileName));
        AnsiConsole.MarkupLine($"[bold green]See {fileName}[/]");

    }

    /// <summary>
    /// Groups a collection of humans by their gender and displays the grouped data in a formatted manner.
    /// </summary>
    /// <remarks>
    /// This method generates a collection of humans, groups them by their gender, and orders each group by the last name of the individuals.
    /// The output is displayed using Spectre.Console with the following details:
    /// - Gender of the group.
    /// - First name, last name, and age of each individual in the group.
    /// - If the individual was born today, it is explicitly noted.
    /// </remarks>
    public static void GroupAndDisplayHumansByGender()
    {

        SpectreConsoleHelpers.PrintCyan();

        var humans = HumanGenerator.Create(25);

        var result = humans
            .GroupBy(h => h.Gender)
            .Select(g => new
            {
                Gender = g.Key,
                People = g.OrderBy(h => h.LastName).ToList()
            })
            .ToList();


        foreach (var group in result)
        {
            AnsiConsole.MarkupLine($"[bold yellow]Gender: {group.Gender}[/]");

            foreach (var person in group.People)
            {
                var age = person.BirthDate.GetAge();

                if (age == 0)
                {
                    AnsiConsole.MarkupLine($"  {person.FirstName,-10} {person.LastName,-15}[cyan]Born today[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"  {person.FirstName,-10} {person.LastName,-15}{age}");
                }

            }

            Console.WriteLine();
        }
    }

    /// <summary>
    /// Displays a list of vehicles with their details, including ID, manufacturer, model, and year.
    /// </summary>
    /// <remarks>
    /// This method retrieves the vehicle data from the singleton <see cref="BogusLibrary.Classes.DataContainer"/> instance
    /// and outputs it in a formatted table using <see cref="Spectre.Console.AnsiConsole"/>.
    /// </remarks>
    public static void VehicleSample()
    {
        SpectreConsoleHelpers.PrintCyan();
        
        var vehicles = DataContainer.Instance.Vehicles;
        
        foreach (var v in vehicles)
            AnsiConsole.MarkupLine($"{v.Id, -4}{v.Manufacturer, -15} {v.Model, -15} {v.Year}");

    }
}
