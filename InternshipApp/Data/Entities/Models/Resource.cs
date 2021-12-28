using Data.Entities.Enums;

namespace Data.Entities.Models
{
    public class Resource : Post
    {
        public string Header { get; set; }
        public Domain Domain { get; set; }
    }
}
