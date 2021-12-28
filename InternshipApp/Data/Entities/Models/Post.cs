using System;
using System.Collections.Generic;
using Data.Entities.Enums;

namespace Data.Entities.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Domain Domain { get; set; }

        public int AuthorId { get; set; }
        public Member Author { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
