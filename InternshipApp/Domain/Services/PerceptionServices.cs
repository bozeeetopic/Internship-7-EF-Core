using Data.Entities.Models;
using Domain.Factories;
using Domain.Models;
using Domain.Repositories;
using System.Linq;

namespace Domain.Services
{
    public class PerceptionServices
    {
        public static void Add(int userId, int resourceId)
        {
            RepositoryFactory
                .Create<PerceptionBase>().Add(userId, resourceId);
        }
        public static Perception GetPerception()
        {
            return RepositoryFactory
                .Create<PerceptionBase>()
                .GetAll()
                .Where(r => r.ResourceId == Resources.CurrentResource.Id)
                .Where(r => r.PerceiverId == Users.CurrentUser.Id)
                .FirstOrDefault();
        }
    }
}
