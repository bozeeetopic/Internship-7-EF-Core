using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Domain.Models;
using System.Linq;

namespace Domain.Repositories
{
    public class PerceiveBase : RepositoryBase
    {
        public PerceiveBase(InternshipAppDbContext dbContext) : base(dbContext)
        {
        }

        public PercieveDetails GetPaymentDetails(int perceiveId)
        {
            var perceiverDetails = DbContext.Perceptions
                .Where(p => p.Id == perceiveId)
                .Include(pr => pr.Perceiver)
                .Select(pr => new
                {
                    pr.Perceiver.Username,
                    PerceiverFullName = $"{ pr.Perceiver.Name} {pr.Perceiver.Surname}",
                });

            var resourceDetails = DbContext.Perceptions
                .Where(i => i.Id == perceiveId)
                .Include(r => r.Resource)
                .Select(r => new
                {
                   r.Resource.Header,
                   r.Resource.Domain
                })
                .FirstOrDefault();

            if (resourceDetails is null || perceiverDetails is null)
            {
                return null;
            }

            var percieveDetails = perceiverDetails
                .Select(pd => new PercieveDetails
                {
                    Header = resourceDetails.Header,
                    Domain = resourceDetails.Domain,
                    //Username = perceiverDetails.Username,
                    //FullName = perceiverDetails.PerceiverFullName,
                })
                .FirstOrDefault();

            return percieveDetails;
        }
    }
}
