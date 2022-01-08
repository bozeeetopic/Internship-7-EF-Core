using Domain.Models;
using Domain.Services;
using Presentation.Actions.ActionHelpers;
using Presentation.Actions.Dashboard;
using Presentation.Enums;
using Presentation.Helpers;
using System;
using System.Collections.Generic;

namespace Presentation.Actions.Comments
{
    public class ChosenCommentActions
    {
        public static void CommentActions()
        {
            Console.Clear();
            List<Template> actions = new()
            {
                new() { Status = InputStatus.WaitingForInput, Name = "Dodaj komentar", Function = () => AddComment() },
                new() { Status = InputStatus.Warning, Name = "Briši komentar", Function = () => OneCommentActions.DeleteComment() },
                new() { Status = InputStatus.WaitingForInput, Name = "Editaj komentar", Function = () => EditCommentActions() },
                new() { Status = InputStatus.WaitingForInput, Name = "Dodaj upvote", Function = () => AddReaction(true) },
                new() { Status = InputStatus.WaitingForInput, Name = "Dodaj downvote", Function = () => AddReaction(false) },
                new() { Status = InputStatus.WaitingForInput, Name = "Uđi u odgovore", Function = () => EnterComment() },
                new() { Status = InputStatus.WaitingForInput, Name = "Povratak u prethodni komentar", Function = () => GoToPreviousComment() },
                new() { Status = InputStatus.WaitingForInput, Name = "Povratak u resurse", Function = () => DashboardActions.ListResourceActions() },
                new() { Status = InputStatus.WaitingForInput, Name = "Povratak u meni", Function = () => ActionsCaller.PrintMenuAndDoAction(DashboardActions.DashboardActionsList) }
            };
            GetCommentsFromDB();
            SetActionCallabilityStatus(actions);
            var commentString = Domain.Models.Comments.CommentToString(Domain.Models.Comments.CurrentComment);
            ActionsHelper.GenericMenuAndMessage(actions, commentString);
        }
        public static void CheckForReactions(List<Template> actions)
        {
            var reactionExists = ReactionServices.ReactionExists();
            if(!reactionExists)
            {
                actions[3].Status = InputStatus.Error;
                actions[4].Status = InputStatus.Error;
            }
        }
        public static void SetActionCallabilityStatus(List<Template> actions)
        {
            CheckForReactions(actions);
            if (Domain.Models.Comments.CurrentComment.ParentComment == null)
            {
                actions.RemoveAt(6);
            }
            if (Domain.Models.Comments.CommentsList.Count == 0)
            {
                actions.RemoveAt(5);
            }
            switch (Users.CurrentUser.ReputationPoints)
            {
                case >= 500:
                    break;
                case >= 250:
                    actions.RemoveAt(1);
                    break;
                case >= 100:
                    actions.RemoveAt(1);
                    if(Domain.Models.Comments.CurrentComment.AuthorId != Users.CurrentUser.Id)
                    {
                        actions.RemoveAt(1);
                    }
                    break;
                case >= 20:
                    actions.RemoveAt(1);
                    actions.RemoveAt(1);
                    break;
                case >= 15:
                    actions.RemoveAt(1);
                    actions.RemoveAt(1);
                    if (Domain.Models.Comments.CurrentComment.ParentComment == null)
                    {
                        actions.RemoveAt(2);
                    }
                    break;
                case >= 5:
                    actions.RemoveAt(1);
                    actions.RemoveAt(1);
                    actions.RemoveAt(2);
                    break;
                case >= 3:
                    actions.RemoveAt(1);
                    actions.RemoveAt(1);
                    actions.RemoveAt(1);
                    actions.RemoveAt(1);
                    break;
                default:
                    actions.RemoveAt(1);
                    actions.RemoveAt(1);
                    actions.RemoveAt(1);
                    actions.RemoveAt(1);
                    if (Domain.Models.Comments.CurrentComment.ParentComment != null)
                    {
                        actions.RemoveAt(0);
                    }
                    break;
            }
        }
        public static void GetCommentsFromDB()
        {
            CommentServices.SetCommentsFromComment();
        }
        public static void EnterComment()
        {
            Console.Clear();
            Console.WriteLine("Redni broj - \tAutor\tDatum objave\tUpvotes\tDownvotes\tText(iduca linija)");
            var i = 1;
            foreach (var comment in Domain.Models.Comments.CommentsList)
            {
                var authorReputation = UserServices.ReturnUsersReputationPoints(comment.AuthorId);

                if(authorReputation >= 1000)
                {
                    ConsoleHelpers.WriteInColor($"{i} - {Domain.Models.Comments.CommentToString(comment)}", ConsoleColor.Cyan);
                }
                else
                {
                    Console.Write($"{i} - {Domain.Models.Comments.CommentToString(comment)}");
                }
                i++;
            }
            Console.WriteLine();
            var chosenComment = Reader.UserNumberInput("Unesi redni broj komentara", 1, Domain.Models.Comments.CommentsList.Count) - 1;
            Domain.Models.Comments.CurrentComment = Domain.Models.Comments.CommentsList[chosenComment];
            CommentActions();
        }
        public static void AddComment()
        {
            Console.Clear();
            List<Template> actions = new()
            {
                new() { Status = InputStatus.WaitingForInput, Name = "Unos teksta", Function = null },
                new() { Status = InputStatus.Error, Name = "Dodaj komentar", Function = () => OneCommentActions.AddComment(Domain.Models.Comments.CurrentComment.Id) },
                new() { Status = InputStatus.WaitingForInput, Name = "Odustani", Function = () => CommentActions() }
            };
            actions[0].Function = () => OneCommentActions.SetText(actions);
            Domain.Models.Comments.InsertingComment = new();

            ActionsHelper.GenericMenuAndMessage(actions, "");
        }
        public static void EditCommentActions()
        {
            Console.Clear();
            List<Template> actions = new()
            {
                new() { Status = InputStatus.WaitingForInput, Name = "Unos promjenjenog teksta", Function = null },
                new() { Status = InputStatus.Error, Name = "Promjeni komentar", Function = () => OneCommentActions.EditComment() },
                new() { Status = InputStatus.WaitingForInput, Name = "Odustani", Function = () => CommentActions() }
            };
            actions[0].Function = () => OneCommentActions.SetText(actions);

            ActionsHelper.GenericMenuAndMessage(actions, "");
        }
        public static void AddReaction(bool isUpvote)
        {

            var user = UserServices.ReturnUserById();

            ReactionServices.Add(Users.CurrentUser.Id, Domain.Models.Comments.CurrentComment.Id, isUpvote);

            if(user.Id != Users.CurrentUser.Id)
            {
                if (isUpvote)
                {
                    user.ReputationPoints = ReduceReputationPoints(user.ReputationPoints, 15);
                    if (Domain.Models.Comments.CurrentComment.ParentComment == null)
                    {
                        Users.CurrentUser.ReputationPoints = ReduceReputationPoints(Users.CurrentUser.ReputationPoints, 10);
                    }
                    else
                    {
                        Users.CurrentUser.ReputationPoints = ReduceReputationPoints(Users.CurrentUser.ReputationPoints, 5);
                    }
                }
                else
                {
                    Users.CurrentUser.ReputationPoints = ReduceReputationPoints(Users.CurrentUser.ReputationPoints,-1);
                    if (Domain.Models.Comments.CurrentComment.ParentComment == null)
                    {
                        user.ReputationPoints = ReduceReputationPoints(user.ReputationPoints,-2);
                    }
                    else
                    {
                        user.ReputationPoints = ReduceReputationPoints(user.ReputationPoints,-3);
                    }
                }
                UserServices.Edit(Users.CurrentUser);
                UserServices.Edit(user);
            }

            if (isUpvote)
            {
                Domain.Models.Comments.CurrentComment.UpVotes++;
            }
            else
            {
                Domain.Models.Comments.CurrentComment.DownVotes++;
            }
            CommentServices.Edit();
        }
        private static int ReduceReputationPoints(int reputationPoints, int addition)
        {
            if(reputationPoints >= 100000)
            {
                return reputationPoints;
            }
            if(reputationPoints + addition <= 1)
            {
                return 1;
            }
            else
            {
                return reputationPoints + addition;
            }
        }
        public static void GoToPreviousComment()
        {
            Domain.Models.Comments.CurrentComment = CommentServices.ReturnPreviousComment();
            CommentActions();
        }
    }
}
