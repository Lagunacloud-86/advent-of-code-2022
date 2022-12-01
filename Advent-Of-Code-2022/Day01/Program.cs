
using AOCProject;
using Day01;

const string inputPath = "../../../../Inputs";

Console.WriteLine($"The result of day 01 test 1 is: {ProjectRunner.Run<Test1, int>(Path.Combine(inputPath, "day01-test-1.txt"))}");
Console.WriteLine($"The result of day 01 part 1 is: {ProjectRunner.Run<Part1, int>(Path.Combine(inputPath, "day01-input.txt"))}");
Console.WriteLine($"The result of day 01 test 2 is: {ProjectRunner.Run<Test2, int>(Path.Combine(inputPath, "day01-test-1.txt"))}");
Console.WriteLine($"The result of day 01 part 2 is: {ProjectRunner.Run<Part2, int>(Path.Combine(inputPath, "day01-input.txt"))}");