using AuthProjects.Core.Domains;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthProjects.Infrastructures.EntityConfigurations
{
    public class UserEntityConfiguration : BaseEntityConfiguration<User>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
        {

        }
    }
}