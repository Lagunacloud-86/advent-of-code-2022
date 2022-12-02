using AOCProject;
using Day03;

const string inputPath = "../../../../Inputs";

Console.WriteLine($"The result of day 03 test 1 is: {ProjectRunner.Run<Part1, int>(Path.Combine(inputPath, "day03-test-1.txt"))}");
Console.WriteLine($"The result of day 03 part 1 is: {ProjectRunner.Run<Part1, int>(Path.Combine(inputPath, "day03-input.txt"))}");
Console.WriteLine($"The result of day 03 test 2 is: {ProjectRunner.Run<Part2, int>(Path.Combine(inputPath, "day03-test-1.txt"))}");
Console.WriteLine($"The result of day 03 part 2 is: {ProjectRunner.Run<Part2, int>(Path.Combine(inputPath, "day03-input.txt"))}");