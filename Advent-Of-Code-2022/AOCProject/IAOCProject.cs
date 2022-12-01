using Apt8.Utilities.InputParser.Lib;

namespace AOCProject;

public interface IAOCProject<TResult>
{
    void Init(in string input);
    TResult Run();
}