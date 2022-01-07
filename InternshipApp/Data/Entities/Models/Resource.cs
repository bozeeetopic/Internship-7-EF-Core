using Data.Entities.Enums;
using System.Collections.Generic;

namespace Data.Entities.Models
{
    public class Resource : Post
    {
        public string Header { get; set; }
        public ResourceDomain Domain { get; set; }
        public int SeenCounter { get; set; }
        public ICollection<Perception> Perceptions { get; set; }
    }
}
