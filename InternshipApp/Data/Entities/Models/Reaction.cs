
namespace Data.Entities.Models
{
    public class Reaction
    {
        public int Id { get; set; }
        public bool IsUpVote { get; set; }

        public int ReactorId { get; set; }
        public Member Reactor { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
