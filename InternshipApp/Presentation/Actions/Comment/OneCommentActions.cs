using Domain.Models;
using Domain.Services;
using Presentation.Actions.ActionHelpers;
using Presentation.Actions.Resource;
using Presentation.Enums;
using Presentation.Helpers;
using System;
using System.Collections.Generic;

namespace Presentation.Actions.Comment
{
    public class OneCommentActions
    {
        public static void AddComment()
        {
            Comments.InsertingComment.UpVotes = 0;
            Comments.InsertingComment.DownVotes = 0;
            Comments.InsertingComment.ResourceId = Resources.CurrentResource.Id;
            Comments.InsertingComment.Date = DateTime.Now;
            Comments.InsertingComment.AuthorId = Users.CurrentUser.Id;
            CommentServices.AddComment();

            ChosenResourceActions.ResourceActions();
        }
        public static void AddComment(int parentCommentId)
        {
            Comments.InsertingComment.CommentId = parentCommentId;
            AddComment();
        }
        public static void SetText(List<Template> actions)
        {
            Comments.InsertingComment.Text = Reader.UserStringInput(actions[0].Name, "", 1);
            actions[0].Status = InputStatus.Done;
            actions[1].Status = InputStatus.WaitingForInput;
            actions[2].Status = InputStatus.Warning;
        }
        public static void EditComment()
        {
            Comments.CurrentComment.Text = Comments.InsertingComment.Text;
            CommentServices.Edit();

            ChosenCommentActions.CommentActions();
        }
        public static void DeleteComment()
        {
            if(Comments.CurrentComment.ParentComment == null)
            {
                DeleteCommentRecursively();
                ChosenResourceActions.ResourceActions();
            }
            else
            {
                var parentComment = CommentServices.ReturnPreviousComment();

                DeleteCommentRecursively();
                Comments.CurrentComment = parentComment;
                ChosenCommentActions.CommentActions();
            }
        }
        public static void DeleteCommentRecursively()
        {
            var commentToDelete = Comments.CurrentComment;
            var comments = CommentServices.GetComments();
            foreach(var comment in comments)
            {
                Comments.CurrentComment = comment;
                DeleteCommentRecursively();
            }
            Comments.CurrentComment = commentToDelete;
            CommentServices.Delete(Comments.CurrentComment.Id);
        }
    }
}
