using System;
using System.Collections.Generic;

namespace Data.Entities.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }

        public int AuthorId { get; set; }
        public Member Author { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Reaction> Reactions { get; set; }
    }
}
