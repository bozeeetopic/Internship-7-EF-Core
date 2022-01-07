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
        public ResponseResultType Edit(int reactionId, bool isUpvote)
        {
            var edittingReaction = DbContext.Reactions.Find(reactionId);
            edittingReaction.IsUpVote = isUpvote;
            return SaveChanges();
        }

        public ICollection<Reaction> GetAll() => DbContext.Reactions.ToList();
    }
}
