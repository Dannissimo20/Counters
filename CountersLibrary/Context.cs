namespace CountersLibrary;

public class Context
{
    public static ApplContext db { get; private set; }
    internal static void AddDb(ApplContext application) => db = application;
}