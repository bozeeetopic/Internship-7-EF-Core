using Data.Entities.Enums;
namespace Domain.Models
{
    public class ReactionDetails
    {
        public string Header { get; set; }
        public ResourceDomain Domain { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public bool IsUpVote { get; set; }
    }
}
