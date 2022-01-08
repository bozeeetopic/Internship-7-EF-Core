using Data.Entities;
using Data.Entities.Models;
using Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Repositories
{
    public class ResourceBase : RepositoryBase
    {
        public ResourceBase(InternshipAppDbContext dbContext) : base(dbContext)
        {
        }

        public ResponseResultType Add(Resource resource)
        {
            DbContext.Resources.Add(resource);

            return SaveChanges();
        }
        public ResponseResultType Edit(Resource resource, int resourceId)
        {
            var edittingResource = DbContext.Resources.Find(resourceId);
            if (edittingResource is null)
            {
                return ResponseResultType.NotFound;
            }
            edittingResource.SeenCounter = resource.SeenCounter;

            return SaveChanges();
        }
        public ICollection<Resource> GetAll() => DbContext.Resources.ToList();
    }
}
