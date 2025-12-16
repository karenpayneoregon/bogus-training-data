# About

This class project provides data generators for developers to use for learning, testing, and pre-populating data sources, such as Microsoft EF Core.

## Core

Uses NuGet package [Bogus](https://www.nuget.org/packages/Bogus/35.6.5?_src=template) to generate fake data.

## Classes

- This is a small .NET class library (target: net8.0) that provides fake/test data generators built on the Bogus library. Key folders:
  - `Generators/`: Generator classes that produce `Models/*` instances (e.g., `HumanGenerator`, `ProductGenerator`, `UserGenerator`).
  - `Models/`: Plain data models used across generators and the `DataContainer` singleton.
  - `Classes/`: helpers like `DataContainer` which bootstraps example datasets.
  - `LanguageExtensions/`: small utility/extension methods.

## Typical data flow & patterns
- Generators expose `Create(int count, bool random = false)` and `CreateOne(bool random = false)` helpers. Example: `Generators/HumanGenerator.cs` and `Generators/ProductGenerator.cs`.
- Most generators use Bogus `Faker<T>` and often set `Seed = new Random(338)` when `random == false` to produce deterministic, repeatable output. Respect and preserve this seed behavior when changing APIs.
- Some generators use `StrictMode(true)` on the faker to ensure all model properties are populated (see `ProductGenerator`).
- `Generators/JsonGenerator.cs` provides `GenerateJson()` and typed helpers like `HumansAsJson(...)` — use these for quick serialized output examples.
- `Classes/DataContainer` constructs sample datasets on demand using the generators — it's a useful runnable example of how pieces fit together.


