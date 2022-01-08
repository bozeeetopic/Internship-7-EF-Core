using Data.Entities.Models;
using Domain.Factories;
using Domain.Models;
using Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class ResourceServices
    {
        public static List<Resource> GetAll()
        {
            return RepositoryFactory
                .Create<ResourceBase>()
                .GetAll()
                .Where(r => r.Domain == Resources.ResourceDomain)
                .ToList();
        }
        public static int[] GetAllIds()
        {
            return RepositoryFactory
                .Create<CommentBase>()
                .GetAll()
                .Select(x => x.ResourceId)
                .ToArray();
        }
        public static List<Resource> AllWhereIdNotIn(int[] forbiddenIds)
        {
            return Resources.ResourcesList
                    .Where(r => !forbiddenIds.Contains(r.Id))
                    .ToList();
        }
        public static void Edit(Resource resource)
        {
            RepositoryFactory
                .Create<ResourceBase>().Edit(resource, resource.Id);
        }
        public static void Add()
        {
            RepositoryFactory.Create<ResourceBase>().Add(Resources.CurrentResource);
        }
    }
}
