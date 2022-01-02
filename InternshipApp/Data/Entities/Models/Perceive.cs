﻿
namespace Data.Entities.Models
{
    public class Perceive
    {
        public int Id { get; set; }

        public int PerceiverId { get; set; }
        public Member Perceiver { get; set; }

        public int ResourceId { get; set; }
        public Resource Resource { get; set; }
    }
}
