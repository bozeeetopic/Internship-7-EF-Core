using Data.Entities.Models;
using Domain.Factories;
using Domain.Models;
using Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class ReactionServices
    {
        public static bool ReactionExists()
        {
            var reaction = RepositoryFactory
                .Create<ReactionBase>()
                .GetAll()
                .Where(ci => ci.CommentId == Comments.CurrentComment.Id)
                .Where(ri => ri.ReactorId == Users.CurrentUser.Id)
                .FirstOrDefault();
            if(reaction is not null)
            {
                return true;
            }
            return false;
        }
        public static Reaction ReturnReaction()
        {
            return RepositoryFactory
                .Create<ReactionBase>()
                .GetAll()
                .Where(ci => ci.CommentId == Comments.CurrentComment.Id)
                .Where(ri => ri.ReactorId == Users.CurrentUser.Id)
                .FirstOrDefault();
        }
        public static void Add(int userId, int commentId, bool isUpvote)
        {
            RepositoryFactory
                .Create<ReactionBase>()
                .Add(userId, commentId, isUpvote);
        }
        public static List<Reaction> ReturnReactionsFromComment(int commentId)
        {
            return RepositoryFactory
                .Create<ReactionBase>()
                .GetAll()
                .Where(ci => ci.CommentId == commentId)
                .ToList();
        }
        public static void Delete(int reactionId)
        {
            RepositoryFactory
                .Create<ReactionBase>()
                .Delete(reactionId);
        }
    }
}
