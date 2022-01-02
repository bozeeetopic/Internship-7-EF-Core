using Data.Entities;
using Domain.Enums;

namespace Domain.Repositories
{
    public abstract class RepositoryBase
    {
        protected readonly InternshipAppDbContext DbContext;

        protected RepositoryBase(InternshipAppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected ResponseResultType SaveChanges()
        {
            var hasChanges = DbContext.SaveChanges() > 0;
            if (!hasChanges)
            {
                return ResponseResultType.NoChanges;
            }

            return ResponseResultType.Success;
        }
    }
}
