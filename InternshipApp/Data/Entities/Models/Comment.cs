using System.Collections.Generic;

namespace Data.Entities.Models
{
    public class Comment : Post
    {
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }

        public int ResourceId { get; set; }
        public Resource Resource { get; set; }

        public int? CommentId { get; set; }
        public Comment ParentComment { get; set; }

        public ICollection<Reaction> Reactions { get; set; }
    }
}
