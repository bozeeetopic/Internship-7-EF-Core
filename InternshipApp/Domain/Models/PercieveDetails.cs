using Data.Entities.Enums;
namespace Domain.Models
{
    public class PercieveDetails
    {
        public string Header { get; set; }
        public ResourceDomain Domain { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
    }
}
