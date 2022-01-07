using Data.Entities.Models;
using Data.Entities.Enums;
using System.Collections.Generic;
using Domain.Factories;
using Domain.Repositories;
using System.Linq;

namespace Domain.Models
{
    public static class CurrentResource
    {
        public static ResourceDomain ResourceDomain { get; set; }
        public static bool ListAll { get; set; }
        public static Resource Resource { get; set; }
        public static List<Resource> Resources { get; set; }
        public static string ResourceToString(Resource resource)
        {
            var authorUsername = RepositoryFactory
                .Create<MemberBase>().GetAll().Where(i => i.Id == resource.AuthorId).Select(u => u.Username).FirstOrDefault();
            return $"{resource.Domain} - {resource.Header}\tBy: {authorUsername}\t\t{resource.Date}\tViews: {resource.SeenCounter}\n\t{resource.Text}";
        }
    }
}
