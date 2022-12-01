
const string inputPath = "../../../../Inputs";
const int dayCount = 25;
if (!Directory.Exists(inputPath))
{
    Console.WriteLine("Could not find the inputs folder...");
    return;
}

for(int i = 0; i < dayCount; i++)
{
    string path = Path.Combine(inputPath, $"Day{i+1:00}-input.txt");
    if (File.Exists(path))
        continue;

    File.WriteAllText(path, "1,2,3\n4,5,6\n7,8,9");
    Console.WriteLine($"'{path}' was created...");
}


