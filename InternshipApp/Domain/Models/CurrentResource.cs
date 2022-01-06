using Data.Entities.Models;
using Data.Entities.Enums;
using System.Collections.Generic;

namespace Domain.Models
{
    public static class CurrentResource
    {
        public static ResourceDomain ResourceDomain { get; set; }
        public static Resource Resource { get; set; }
        public static List<Resource> Resources { get; set; }
    }
}
