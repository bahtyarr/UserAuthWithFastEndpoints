namespace AuthProjects.Infrastructures
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]

    public class IgnoreMigrationAttribute : Attribute
    {

    }
}