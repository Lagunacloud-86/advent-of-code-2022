
using AOCProject;
using Day01;

const string inputPath = "../../../../Inputs";

Console.WriteLine($"The result of day 01 part 1 is: {ProjectRunner.Run<Part1, int>(Path.Combine(inputPath, "Day01-input.txt"))}");
Console.WriteLine($"The result of day 01 part 2 is: {ProjectRunner.Run<Part2, int>(Path.Combine(inputPath, "Day01-input.txt"))}");