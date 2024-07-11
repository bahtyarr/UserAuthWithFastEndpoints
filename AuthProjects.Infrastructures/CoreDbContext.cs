using System.Reflection;
using AuthProjects.Core;
using AuthProjects.Core.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthProjects.Infrastructures
{
    public class CoreDbContext : DbContext
    {
        #region Properties

        private readonly IServiceProvider _serviceProvider;

        #endregion Properties

        #region Constructors

        public CoreDbContext(DbContextOptions<CoreDbContext> options, IServiceProvider serviceProvider)
            : base(options)
        {
            _serviceProvider = serviceProvider;
        }

        #endregion Constructors

        #region Protected Methods

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var services = _serviceProvider.GetServices<IApplyEntityConfiguration>();

            foreach (var service in services)
            {
                service.Apply(builder);
            }

            base.OnModelCreating(builder);
        }

        #endregion
    }
}