using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BogusLibrary.Models;
public class Vehicle
{
    public int Id { get; set; }
    public required string Manufacturer { get; set; }
    public required string Model { get; set; }
    public required int Year { get; set; }
    public required string Vin { get; set; }
    public required string Type { get; set; }
}


