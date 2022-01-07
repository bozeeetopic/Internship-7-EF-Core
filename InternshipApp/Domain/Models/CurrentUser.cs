using Data.Entities.Models;
using System.Collections.Generic;

namespace Domain.Models
{
    public static class CurrentUser
    {
        public static Member User { get; set; }
        public static List<Member> Users { get; set; }
    }
}
