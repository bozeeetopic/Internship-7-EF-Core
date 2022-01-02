using System;
using System.Collections.Generic;

namespace Data.Entities.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int ReputationPoints { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Reaction> Reactions { get; set; }
        public ICollection<Perceive> Perceptions { get; set; }
    }
}
