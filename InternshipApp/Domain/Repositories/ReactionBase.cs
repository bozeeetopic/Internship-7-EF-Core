using Data.Entities;
using System.Linq;
using Domain.Enums;
using Data.Entities.Models;
using System.Collections.Generic;

namespace Domain.Repositories
{
    public class ReactionBase : RepositoryBase
    {
        public ReactionBase(InternshipAppDbContext dbContext) : base(dbContext)
        {
        }

        public ResponseResultType Add(int reactorId, int commentId, bool isUpvote)
        {
            Reaction reaction = new() { ReactorId = reactorId, CommentId = commentId, IsUpVote = isUpvote};
            DbContext.Reactions.Add(reaction);

            return SaveChanges();
        }
        public ResponseResultType Delete(int reactionId)
        {
            var deletingReaction = DbContext.Reactions.Find(reactionId);
            if (deletingReaction is null)
            {
                return ResponseResultType.NotFound;
            }

            DbContext.Reactions.Remove(deletingReaction);

            return SaveChanges();
        }

        public ICollection<Reaction> GetAll() => DbContext.Reactions.ToList();
    }
}
