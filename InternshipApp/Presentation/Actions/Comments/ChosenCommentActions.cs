using Domain.Factories;
using Domain.Models;
using Domain.Repositories;
using Presentation.Actions.ActionHelpers;
using Presentation.Actions.Dashboard;
using Presentation.Enums;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

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
                new() { Status = InputStatus.WaitingForInput, Name = "Briši komentar", Function = () => EraseCommentActions() },
                new() { Status = InputStatus.WaitingForInput, Name = "Editaj komentar", Function = () => EditCommentActions() },
                new() { Status = InputStatus.WaitingForInput, Name = "Dodaj upvote", Function = () => AddReaction(true) },
                new() { Status = InputStatus.WaitingForInput, Name = "Dodaj downvote", Function = () => AddReaction(false) },
                new() { Status = InputStatus.WaitingForInput, Name = "Uđi u odgovore", Function = () => EnterComment() },
                new() { Status = InputStatus.WaitingForInput, Name = "Povratak u prethodni komentar", Function = () => GoToPreviousComment() },
                new() { Status = InputStatus.WaitingForInput, Name = "Povratak u resurse", Function = () => DashboardActions.ListResourceActions() },
                new() { Status = InputStatus.WaitingForInput, Name = "Povratak u meni", Function = () => ActionsCaller.PrintMenuAndDoAction(ActionsCaller.DashboardActions) }
            };
            GetCommentsFromDB();
            SetActionCallabilityStatus(actions);
            var commentString = CurrentComment.CommentToString(CurrentComment.Comment);
            ActionsHelper.GenericMenuAndMessage(actions, commentString);
        }
        public static void CheckForReactions(List<Template> actions)
        {
            var reaction = RepositoryFactory
                .Create<ReactionBase>()
                .GetAll()
                .Where(ci => ci.CommentId == CurrentComment.Comment.Id)
                .Where(ri => ri.ReactorId == CurrentUser.User.Id)
                .FirstOrDefault();
            if(reaction != null)
            {
                actions[3].Status = InputStatus.Error;
                actions[4].Status = InputStatus.Error;
            }
        }
        public static void SetActionCallabilityStatus(List<Template> actions)
        {
            CheckForReactions(actions);
            if (CurrentComment.Comment.ParentComment == null)
            {
                actions.RemoveAt(6);
            }
            if (CurrentComment.Comments.Count == 0)
            {
                actions.RemoveAt(5);
            }
            switch (CurrentUser.User.ReputationPoints)
            {
                case >= 500:
                    break;
                case >= 250:
                    actions.RemoveAt(1);
                    break;
                case >= 100:
                    actions.RemoveAt(1);
                    if(CurrentComment.Comment.AuthorId != CurrentUser.User.Id)
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
                    if (CurrentComment.Comment.ParentComment == null)
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
                    if (CurrentComment.Comment.ParentComment != null)
                    {
                        actions.RemoveAt(0);
                    }
                    break;
            }
        }
        public static void GetCommentsFromDB()
        {
            CurrentComment.Comments = RepositoryFactory
                .Create<CommentBase>()
                .GetAll()
                .Where(ri => ri.ResourceId == CurrentResource.Resource.Id)
                .Where(c => c.CommentId == CurrentComment.Comment.Id)
                .ToList();
        }
        public static void EnterComment()
        {
            Console.Clear();
            Console.WriteLine("Redni broj - \tAutor\tDatum objave\tUpvotes\tDownvotes\tText(iduca linija)");
            var i = 1;
            foreach (var comment in CurrentComment.Comments)
            {
                var authorReputation = RepositoryFactory
                .Create<MemberBase>()
                .GetAll()
                .Where(ci => ci.Id == comment.AuthorId)
                .Select(rp => rp.ReputationPoints)
                .FirstOrDefault();

                if(authorReputation >= 1000)
                {
                    ConsoleHelpers.WriteInColor($"{i} - {CurrentComment.CommentToString(comment)}",ConsoleColor.Cyan);
                }
                else
                {
                    Console.Write($"{i} - {CurrentComment.CommentToString(comment)}");
                }
                i++;
            }
            Console.WriteLine();
            var chosenComment = Reader.UserNumberInput("Unesi redni broj komentara", 1, CurrentComment.Comments.Count) - 1;
            CurrentComment.Comment = CurrentComment.Comments[chosenComment];
            CommentActions();
        }
        public static void AddComment()
        {
            Console.Clear();
            List<Template> actions = new()
            {
                new() { Status = InputStatus.WaitingForInput, Name = "Unos teksta", Function = null },
                new() { Status = InputStatus.Error, Name = "Dodaj komentar", Function = () => OneCommentActions.AddComment(CurrentComment.Comment.Id) },
                new() { Status = InputStatus.WaitingForInput, Name = "Odustani", Function = () => CommentActions() }
            };
            actions[0].Function = () => OneCommentActions.SetText(actions);
            CurrentComment.InsertingComment = new();

            ActionsHelper.GenericMenuAndMessage(actions, "");
        }
        public static void EraseCommentActions()
        {
            Console.Clear();
            List<Template> actions = new()
            {
                new() { Status = InputStatus.Error, Name = "Izbriši komentar", Function = () => OneCommentActions.DeleteComment() },
                new() { Status = InputStatus.WaitingForInput, Name = "Odustani", Function = () => CommentActions() }
            };

            ActionsHelper.GenericMenuAndMessage(actions, "Jeste li sigurni da želite izbrisati komentar?");
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
            var reactions = RepositoryFactory
                .Create<ReactionBase>();
            var users = RepositoryFactory
                .Create<MemberBase>();

            var user = users
                .GetAll()
                .Where(i => i.Id == CurrentComment.Comment.AuthorId)
                .FirstOrDefault();

            var reaction = reactions
                .GetAll()
                .Where(ci => ci.CommentId == CurrentComment.Comment.Id)
                .Where(ri => ri.ReactorId == CurrentUser.User.Id)
                .FirstOrDefault();

            reactions.Add(CurrentUser.User.Id, CurrentComment.Comment.Id, isUpvote);
            if(user.Id != CurrentUser.User.Id)
            {
                if (isUpvote)
                {
                    user.ReputationPoints = ReduceReputationPoints(user.ReputationPoints, 15);
                    if (CurrentComment.Comment.ParentComment == null)
                    {
                        CurrentUser.User.ReputationPoints = ReduceReputationPoints(CurrentUser.User.ReputationPoints, 10);
                    }
                    else
                    {
                        CurrentUser.User.ReputationPoints = ReduceReputationPoints(CurrentUser.User.ReputationPoints, 5);
                    }
                }
                else
                {
                    CurrentUser.User.ReputationPoints = ReduceReputationPoints(CurrentUser.User.ReputationPoints,-1);
                    if (CurrentComment.Comment.ParentComment == null)
                    {
                        user.ReputationPoints = ReduceReputationPoints(user.ReputationPoints,-2);
                    }
                    else
                    {
                        user.ReputationPoints = ReduceReputationPoints(user.ReputationPoints,-3);
                    }
                }
                users.Edit(CurrentUser.User,CurrentUser.User.Id);
                users.Edit(user,user.Id);
            }

            if (isUpvote)
            {
                CurrentComment.Comment.UpVotes++;
            }
            else
            {
                CurrentComment.Comment.DownVotes++;
            }
            RepositoryFactory
                .Create<CommentBase>()
                .Edit(CurrentComment.Comment, CurrentComment.Comment.Id);
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
            CurrentComment.Comment = RepositoryFactory
                .Create<CommentBase>()
                .GetAll()
                .Where(ci => ci.Id == CurrentComment.Comment.CommentId)
                .FirstOrDefault();
            CommentActions();
        }
    }
}
