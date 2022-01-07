using Data.Entities.Models;
using System.Collections.Generic;
using Domain.Factories;
using Domain.Repositories;
using System.Linq;

namespace Domain.Models
{
    public static class CurrentComment
    {
        public static Comment Comment { get; set; }
        public static Comment InsertingComment { get; set; }
        public static List<Comment> Comments { get; set; }
        public static string CommentToString(Comment comment)
        {
            var authorUsername = RepositoryFactory
                .Create<MemberBase>().GetAll().Where(i => i.Id == comment.AuthorId).Select(u => u.Username).FirstOrDefault();
            return $"{authorUsername}\t{comment.Date}\t{comment.UpVotes} - {comment.DownVotes}\n{comment.Text}\n";
        }
    }
}
