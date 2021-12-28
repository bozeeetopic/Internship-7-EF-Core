using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int AuthorId { get; set; }
        public Member Author { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public int? CommentId { get; set; }
        public Comment ParentComment { get; set; }

        public ICollection<Comment> Answers { get; set; }
    }
}
