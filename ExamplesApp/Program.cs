using BogusLibrary.Classes;
using BogusLibrary.LanguageExtensions;
using ExamplesApp.Classes;
using ExamplesApp.Classes.Configuration;
using Spectre.Console;

namespace ExamplesApp;
internal partial class Program
{
    static void Main(string[] args)
    {

        Samples.DecimalExamples();
        //Samples.DisplayHighValueProducts();
        //Samples.DisplayClothingProducts();
        //Samples.HumansBornBetween1950And1980();
        //Samples.GenerateAndDeserialize();
        Samples.GroupAndDisplayHumansByGender();
        //Samples.VehicleSample();
        
        SpectreConsoleHelpers.ExitPrompt();
    }
}
