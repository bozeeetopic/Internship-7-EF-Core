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
            RepositoryFactory.Create<ResourceBase>().Add(Resources.ChangingResource);
        }
        public static void SetPopularResources()
        {
            List<(Resource, int)> ResourceComments = new();
            var allResources = RepositoryFactory
                .Create<ResourceBase>()
                .GetAll().ToList();
            List<int> commentCounter = new();
            foreach(var resource in allResources)
            {
                commentCounter.Add(CommentServices.GetCommentsCount(resource.Id));
            }
            for(var index = 0; index < allResources.Count; index++)
            {
                ResourceComments.Add((allResources[index],commentCounter[index]));
            }
            ResourceComments = ResourceComments.OrderBy(i => i.Item2).ToList();
            foreach(var resourceComment in ResourceComments)
            {
                Resources.ResourcesList = new();
                Resources.ResourcesList.Add(resourceComment.Item1);
            }
        }
    }
}
