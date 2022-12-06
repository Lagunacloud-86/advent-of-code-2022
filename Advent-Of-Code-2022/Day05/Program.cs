using AOCProject;
using Day05;

const string inputPath = "../../../../Inputs";

Console.WriteLine($"The result of day 04 test 1 is: {ProjectRunner.Run<Part1, string>(Path.Combine(inputPath, "Day05-test.txt"))}");
Console.WriteLine($"The result of day 04 test 2 is: {ProjectRunner.Run<Part1, string>(Path.Combine(inputPath, "Day05-input.txt"))}");
Console.WriteLine($"The result of day 04 part 1 is: {ProjectRunner.Run<Part2, string>(Path.Combine(inputPath, "Day05-test.txt"))}");
Console.WriteLine($"The result of day 04 part 2 is: {ProjectRunner.Run<Part2, string>(Path.Combine(inputPath, "Day05-input.txt"))}");