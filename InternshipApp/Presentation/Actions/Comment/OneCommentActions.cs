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
            Comments.CommentBeingWorkedOn.UpVotes = 0;
            Comments.CommentBeingWorkedOn.DownVotes = 0;
            Comments.CommentBeingWorkedOn.ResourceId = Resources.CurrentResource.Id;
            Comments.CommentBeingWorkedOn.Date = DateTime.Now;
            Comments.CommentBeingWorkedOn.AuthorId = Users.CurrentUser.Id;
            CommentServices.AddComment();

            ChosenResourceActions.ResourceActions();
        }
        public static void AddComment(int parentCommentId)
        {
            Comments.CommentBeingWorkedOn.CommentId = parentCommentId;
            AddComment();
        }
        public static void SetText(List<Template> actions)
        {
            Comments.CommentBeingWorkedOn.Text = Reader.UserStringInput(actions[0].Name, "", 1);
            actions[0].Status = InputStatus.Done;
            actions[1].Status = InputStatus.WaitingForInput;
            actions[2].Status = InputStatus.Warning;
        }
        public static void EditComment()
        {
            Comments.CurrentComment.Text = Comments.CommentBeingWorkedOn.Text;
            CommentServices.Edit();

            ChosenCommentActions.CommentActions();
        }
        public static void DeleteComment()
        {
            Comments.CommentBeingWorkedOn = Comments.CurrentComment;
            if (Comments.CurrentComment.ParentComment == null)
            {
                DeleteCommentRecursively();
                ChosenResourceActions.ResourceActions();
            }
            else
            {
                Comments.CurrentComment = CommentServices.ReturnPreviousComment();
                DeleteCommentRecursively();
                ChosenCommentActions.CommentActions();
            }
        }
        public static void DeleteCommentRecursively()
        {
            var commentToDelete = Comments.CommentBeingWorkedOn;

            if(commentToDelete != null)
            {
                var reactions = ReactionServices.ReturnReactionsFromComment(commentToDelete.Id);
                foreach (var reaction in reactions)
                {
                    ReactionServices.Delete(reaction.Id);
                }
                var comments = CommentServices.GetComments(commentToDelete.Id);
                foreach (var comment in comments)
                {
                    Comments.CommentBeingWorkedOn = comment;
                    DeleteCommentRecursively();
                }
            }
            CommentServices.Delete(commentToDelete.Id);
        }
    }
}
