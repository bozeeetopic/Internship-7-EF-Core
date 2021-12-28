namespace Data.Entities.Models
{
    public class Comment : Post
    {
        public int ResourceId { get; set; }
        public Resource Resource { get; set; }

        public int? CommentId { get; set; }
        public Comment ParentComment { get; set; }
    }
}
