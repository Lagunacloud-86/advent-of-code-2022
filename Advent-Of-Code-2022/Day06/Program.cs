using AOCProject;
using Day06;

const string inputPath = "../../../../Inputs";

Console.WriteLine($"The result of day 06 test 1 is: {ProjectRunner.Run<Part1, int>(Path.Combine(inputPath, "Day06-test.txt"))}");
Console.WriteLine($"The result of day 06 test 2 is: {ProjectRunner.Run<Part1, int>(Path.Combine(inputPath, "Day06-input.txt"))}");
Console.WriteLine($"The result of day 06 part 1 is: {ProjectRunner.Run<Part2, int>(Path.Combine(inputPath, "Day06-test.txt"))}");
Console.WriteLine($"The result of day 06 part 2 is: {ProjectRunner.Run<Part2, int>(Path.Combine(inputPath, "Day06-input.txt"))}");