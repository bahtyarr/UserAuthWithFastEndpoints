using Microsoft.EntityFrameworkCore;

namespace AuthProjects.Core
{
    public interface IApplyEntityConfiguration
    {
        void Apply(ModelBuilder builder);
    }
}