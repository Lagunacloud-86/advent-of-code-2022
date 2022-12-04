using AOCProject;
using Day04;

const string inputPath = "../../../../Inputs";

Console.WriteLine($"The result of day 04 test 1 is: {ProjectRunner.Run<Part1, int>(Path.Combine(inputPath, "Day04-test.txt"))}");
Console.WriteLine($"The result of day 04 test 2 is: {ProjectRunner.Run<Part1, int>(Path.Combine(inputPath, "Day04-input.txt"))}");
Console.WriteLine($"The result of day 04 part 1 is: {ProjectRunner.Run<Part2, int>(Path.Combine(inputPath, "Day04-test.txt"))}");
Console.WriteLine($"The result of day 04 part 2 is: {ProjectRunner.Run<Part2, int>(Path.Combine(inputPath, "Day04-input.txt"))}");