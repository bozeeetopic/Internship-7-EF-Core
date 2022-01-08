using Domain.Models;
using Domain.Services;
using Presentation.Actions.ActionHelpers;
using Presentation.Actions.Resource;
using Presentation.Enums;
using Presentation.Helpers;
using System;
using System.Collections.Generic;

namespace Presentation.Actions.Comments
{
    public class OneCommentActions
    {
        public static void AddComment()
        {
            Domain.Models.Comments.InsertingComment.UpVotes = 0;
            Domain.Models.Comments.InsertingComment.DownVotes = 0;
            Domain.Models.Comments.InsertingComment.ResourceId = Resources.CurrentResource.Id;
            Domain.Models.Comments.InsertingComment.Date = DateTime.Now;
            Domain.Models.Comments.InsertingComment.AuthorId = Users.CurrentUser.Id;
            CommentServices.AddComment();

            ChosenResourceActions.ResourceActions();
        }
        public static void AddComment(int parentCommentId)
        {
            Domain.Models.Comments.InsertingComment.CommentId = parentCommentId;
            AddComment();
        }
        public static void SetText(List<Template> actions)
        {
            Domain.Models.Comments.InsertingComment.Text = Reader.UserStringInput(actions[0].Name, "", 1);
            actions[0].Status = InputStatus.Done;
            actions[1].Status = InputStatus.WaitingForInput;
            actions[2].Status = InputStatus.Warning;
        }
        public static void EditComment()
        {
            Domain.Models.Comments.CurrentComment.Text = Domain.Models.Comments.InsertingComment.Text;
            CommentServices.Edit();

            ChosenCommentActions.CommentActions();
        }
        public static void DeleteComment()
        {
            if(Domain.Models.Comments.CurrentComment.ParentComment == null)
            {
                DeleteCommentRecursively();
                ChosenResourceActions.ResourceActions();
            }
            else
            {
                var parentComment = CommentServices.ReturnPreviousComment();

                DeleteCommentRecursively();
                Domain.Models.Comments.CurrentComment = parentComment;
                ChosenCommentActions.CommentActions();
            }
        }
        public static void DeleteCommentRecursively()
        {
            var commentToDelete = Domain.Models.Comments.CurrentComment;
            var comments = CommentServices.GetComments();
            foreach(var comment in comments)
            {
                Domain.Models.Comments.CurrentComment = comment;
                DeleteCommentRecursively();
            }
            Domain.Models.Comments.CurrentComment = commentToDelete;
            CommentServices.Delete(Domain.Models.Comments.CurrentComment.Id);
        }
    }
}
