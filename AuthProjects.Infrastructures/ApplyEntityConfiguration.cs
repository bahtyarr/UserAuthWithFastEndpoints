using System.Reflection;
using AuthProjects.Core;
using Microsoft.EntityFrameworkCore;

namespace AuthProjects.Infrastructures
{
    public class ApplyEntityConfiguration : IApplyEntityConfiguration
    {
        public void Apply(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
                opt => opt.GetCustomAttribute<IgnoreMigrationAttribute>() == null);
        }
    }
}