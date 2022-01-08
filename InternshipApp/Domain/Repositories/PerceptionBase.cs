using Data.Entities;
using System.Linq;
using Domain.Enums;
using Data.Entities.Models;
using System.Collections.Generic;

namespace Domain.Repositories
{
    public class PerceptionBase : RepositoryBase
    {
        public PerceptionBase(InternshipAppDbContext dbContext) : base(dbContext)
        {
        }

        public ResponseResultType Add(int perceiverId, int resourceId)
        {
            Perception perception = new() { PerceiverId = perceiverId, ResourceId = resourceId };
            DbContext.Perceptions.Add(perception);

            return SaveChanges();
        }

        public ICollection<Perception> GetAll() => DbContext.Perceptions.ToList();
    }
}
