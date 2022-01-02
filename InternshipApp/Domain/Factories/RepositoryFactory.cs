using Domain.Repositories;
using System;

namespace Domain.Factories
{
    public class RepositoryFactory
    {
        public static TRepository Create<TRepository>() where TRepository : RepositoryBase
        {
            var context = DbContextFactory.GetInternshipAppDbContext();
            var repositoryInstance = Activator
                .CreateInstance(typeof(TRepository), context) as TRepository;
        
            return repositoryInstance;
        }
    }
}
