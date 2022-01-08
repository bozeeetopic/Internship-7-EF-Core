using Data.Entities.Models;
using Domain.Factories;
using Domain.Models;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class CommentServices
    {
        public static void SetCommentsFromComment()
        {
            Comments.CommentsList = RepositoryFactory
                .Create<CommentBase>()
                .GetAll()
                .Where(ri => ri.ResourceId == Resources.CurrentResource.Id)
                .Where(c => c.CommentId == Comments.CurrentComment.Id)
                .ToList();
        }
        public static void SetComments()
        {
            Comments.CommentsList = RepositoryFactory
                .Create<CommentBase>()
                .GetAll()
                .Where(ri => ri.ResourceId == Resources.CurrentResource.Id)
                .ToList();
        }
        public static Comment ReturnPreviousComment()
        {
            return RepositoryFactory
                .Create<CommentBase>()
                .GetAll()
                .Where(ci => ci.Id == Comments.CurrentComment.CommentId)
                .FirstOrDefault();
        }
        public static List<Comment> GetComments(int id)
        {
            return RepositoryFactory
                .Create<CommentBase>()
                .GetAll()
                .Where(ci => ci.CommentId == id)
                .ToList();
        }
        public static void Edit()
        {
            RepositoryFactory
                .Create<CommentBase>()
                .Edit(Comments.CurrentComment, Comments.CurrentComment.Id);
        }
        public static void AddComment()
        {
            RepositoryFactory.Create<CommentBase>().Add(Comments.CommentBeingWorkedOn);
        }
        public static void Delete(int id)
        {
            RepositoryFactory.Create<CommentBase>().Delete(id);
        }
        public static int GetCommentsCount(int resourceId)
        {
            return RepositoryFactory
                .Create<CommentBase>()
                .GetAll()
                .Where(ri => ri.ResourceId == resourceId)
                .Where(ri => DateTime.Now.Subtract(ri.Date).Days < 1)
                .Count();
        }
    }
}
