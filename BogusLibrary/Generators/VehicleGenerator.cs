using Bogus;
using static Bogus.Randomizer;
using BogusLibrary.Models;


namespace BogusLibrary.Generators;
public class VehicleGenerator
{
    /// <summary>
    /// Generates a list of <see cref="Vehicle"/> instances with optionally randomized data.
    /// </summary>
    /// <param name="count">The number of <see cref="Vehicle"/> instances to generate.</param>
    /// <param name="random">
    /// If set to <c>true</c>, the data will be randomized. If <c>false</c>, a fixed seed is used for deterministic results.
    /// </param>
    /// <returns>A list of <see cref="Vehicle"/> instances.</returns>
    public static List<Vehicle> Create(int count, bool random = false)
    {

        if (!random)
        {
            Seed = new Random(338);
        }
        
        var id = 1;
        
        Faker<Vehicle> faker = new Faker<Vehicle>()
            .RuleFor(c => c.Id, f => id++)
            .RuleFor(v => v.Manufacturer, f => f.Vehicle.Manufacturer())
            .RuleFor(v => v.Model, (f, v) => f.Vehicle.Model())
            .RuleFor(v => v.Year, f => f.Random.Int(1995, DateTime.Now.Year))
            .RuleFor(v => v.Vin, f => f.Vehicle.Vin())
            .RuleFor(v => v.Type, f => f.Vehicle.Type());

        return faker.Generate(count);
    }

    /// <summary>
    /// Generates a single <see cref="Vehicle"/> instance with optionally randomized data.
    /// </summary>
    /// <param name="random">
    /// If set to <c>true</c>, the data will be randomized. If <c>false</c>, a fixed seed is used for deterministic results.
    /// </param>
    /// <returns>
    /// A single instance of the <see cref="Vehicle"/> class.
    /// </returns>
    public static Vehicle CreateOne(bool random = false)
        => Create(1, random).FirstOrDefault()!;
}
