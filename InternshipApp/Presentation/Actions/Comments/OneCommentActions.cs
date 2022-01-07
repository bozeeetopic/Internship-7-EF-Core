using Domain.Factories;
using Domain.Models;
using Domain.Repositories;
using Presentation.Actions.ActionHelpers;
using Presentation.Actions.Dashboard.Resource;
using Presentation.Enums;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.Actions.Comments
{
    public class OneCommentActions
    {
        public static void AddComment()
        {
            CurrentComment.InsertingComment.CommentId = null;
            CurrentComment.InsertingComment.Date = DateTime.Now;
            CurrentComment.InsertingComment.ResourceId = CurrentResource.Resource.Id;
            CurrentComment.InsertingComment.Date = DateTime.Now;
            CurrentComment.InsertingComment.Author = CurrentUser.User;
            RepositoryFactory.Create<CommentBase>().Add(CurrentComment.InsertingComment);

            ChosenResourceActions.ResourceActions();
        }
        public static void AddComment(int parentCommentId)
        {
            CurrentComment.InsertingComment.CommentId = parentCommentId;
            CurrentComment.InsertingComment.Date = DateTime.Now;
            CurrentComment.InsertingComment.ResourceId = CurrentResource.Resource.Id;
            CurrentComment.InsertingComment.Date = DateTime.Now;
            CurrentComment.InsertingComment.Author = CurrentUser.User;
            RepositoryFactory.Create<CommentBase>().Add(CurrentComment.InsertingComment);
            ChosenCommentActions.CommentActions();
        }
        public static void SetText(List<Template> actions)
        {
            CurrentComment.InsertingComment.Text = Reader.UserStringInput(actions[0].Name, "", 1);
            actions[0].Status = InputStatus.Done;
            actions[1].Status = InputStatus.WaitingForInput;
            actions[2].Status = InputStatus.Warning;
        }
        public static void EditComment()
        {
            var text = CurrentComment.InsertingComment.Text;
            CurrentComment.InsertingComment = CurrentComment.Comment;
            CurrentComment.InsertingComment.Text = text;
            RepositoryFactory.Create<CommentBase>().Edit(CurrentComment.InsertingComment,CurrentComment.Comment.Id);

            ChosenCommentActions.CommentActions();
        }
        public static void DeleteComment()
        {
            if(CurrentComment.Comment.ParentComment == null)
            {
                DeleteCommentRecursively();
                ChosenResourceActions.ResourceActions();
            }
            else
            {
                var parentComment = RepositoryFactory
                .Create<CommentBase>()
                .GetAll()
                .Where(ci => ci.Id == CurrentComment.Comment.CommentId)
                .FirstOrDefault();

                DeleteCommentRecursively();
                CurrentComment.Comment = parentComment;
                ChosenCommentActions.CommentActions();
            }
        }
        public static void DeleteCommentRecursively()
        {
            var commentToDelete = CurrentComment.Comment;
            var comments = RepositoryFactory
                .Create<CommentBase>()
                .GetAll()
                .Where(ci => ci.CommentId == CurrentComment.Comment.Id);
            foreach(var comment in comments)
            {
                CurrentComment.Comment = comment;
                DeleteCommentRecursively();
            }
            CurrentComment.Comment = commentToDelete;
            RepositoryFactory.Create<CommentBase>().Delete(commentToDelete.Id);
        }
    }
}
