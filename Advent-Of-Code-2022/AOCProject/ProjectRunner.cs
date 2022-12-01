namespace AOCProject;

public static class ProjectRunner
{
    public static TResult Run<TProject, TResult>(in string inputFile)
        where TProject : class, IAOCProject<TResult>, new ()
    {
        var project = new TProject();
        project.Init(in inputFile);
        return project.Run();
    }
}