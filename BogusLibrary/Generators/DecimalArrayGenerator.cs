using Bogus;
using static Bogus.Randomizer;
namespace BogusLibrary.Generators;

public class DecimalArrayGenerator
{
    private static readonly Faker _faker = new();

    
    /// <summary>
    /// Generates an array of random decimal numbers within a specified range.
    /// </summary>
    /// <param name="count">The number of decimal numbers to generate. Must be non-negative.</param>
    /// <param name="min">The minimum value of the range.</param>
    /// <param name="max">The maximum value of the range. Must be greater than or equal to <paramref name="min"/>.</param>
    /// <param name="decimals">
    /// The number of decimal places to round each generated number to. 
    /// Must be between 0 and 28, inclusive. Defaults to 2.
    /// </param>
    /// <returns>An array of randomly generated decimal numbers.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="count"/> is negative or <paramref name="decimals"/> is outside the range of 0 to 28.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="min"/> is greater than <paramref name="max"/>.
    /// </exception>
    public static decimal[] GenerateRange(int count, decimal min, decimal max, int decimals = 2)
    {
        // for producing same output
        Seed =  new Random(338);

        ArgumentOutOfRangeException.ThrowIfNegative(count);

        if (min > max) throw new ArgumentException("min must be <= max");
        
        if (decimals is < 0 or > 28) throw new ArgumentOutOfRangeException(nameof(decimals));

        return Enumerable.Range(0, count)
            .Select(_ =>
            {
                var value = _faker.Random.Decimal(min, max);
                return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
            })
            .ToArray();
    }
}