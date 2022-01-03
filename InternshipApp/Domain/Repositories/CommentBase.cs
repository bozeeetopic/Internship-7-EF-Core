using Data.Entities;
using Data.Entities.Models;
using Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Repositories
{
    public class CommentBase : RepositoryBase
    {
        public CommentBase(InternshipAppDbContext dbContext) : base(dbContext)
        {
        }

        public ResponseResultType Add(Comment comment)
        {
            DbContext.Comments.Add(comment);

            return SaveChanges();
        }
        public ResponseResultType Edit(Comment comment, int commentId)
        {
            var edittingComment = DbContext.Comments.Find(commentId);
            if (edittingComment is null)
            {
                return ResponseResultType.NotFound;
            }
            edittingComment.Text = comment.Text;
            edittingComment.UpVotes = comment.UpVotes;
            edittingComment.DownVotes = comment.DownVotes;

            return SaveChanges();
        }
        public ResponseResultType Delete(int commentId)
        {
            var deletingComment = DbContext.Comments.Find(commentId);
            if (deletingComment is null)
            {
                return ResponseResultType.NotFound;
            }

            DbContext.Comments.Remove(deletingComment);

            return SaveChanges();
        }

        public ICollection<Comment> GetAll() => DbContext.Comments.ToList();
    }
}
