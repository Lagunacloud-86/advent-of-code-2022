using AOCProject;
using Day02;

const string inputPath = "../../../../Inputs";

Console.WriteLine($"The result of day 02 test 1 is: {ProjectRunner.Run<Part1, int>(Path.Combine(inputPath, "day02-test-1.txt"))}");
Console.WriteLine($"The result of day 02 part 1 is: {ProjectRunner.Run<Part1, int>(Path.Combine(inputPath, "day02-input.txt"))}");
Console.WriteLine($"The result of day 02 test 2 is: {ProjectRunner.Run<Part2, int>(Path.Combine(inputPath, "day02-test-1.txt"))}");
Console.WriteLine($"The result of day 02 part 2 is: {ProjectRunner.Run<Part2, int>(Path.Combine(inputPath, "day02-input.txt"))}");