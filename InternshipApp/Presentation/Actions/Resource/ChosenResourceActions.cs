using Domain.Factories;
using Domain.Models;
using Domain.Repositories;
using Presentation.Actions.ActionHelpers;
using Presentation.Actions.Comments;
using Presentation.Actions.Dashboard;
using Presentation.Enums;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.Actions.Resource
{
    public static  class ChosenResourceActions
    {
        public static void ResourceActions()
        {
            Console.Clear();
            List<Template> actions = new()
            {
                new() { Status = InputStatus.WaitingForInput, Name = "Dodaj komentar", Function = () => AddComment() },
                new() { Status = InputStatus.WaitingForInput, Name = "Uđi u komentare", Function = () => EnterComment() },
                new() { Status = InputStatus.WaitingForInput, Name = "Povratak u resurse", Function = () => DashboardActions.ListResourceActions()},
                new() { Status = InputStatus.WaitingForInput, Name = "Povratak u meni", Function = () => ActionsCaller.PrintMenuAndDoAction(ActionsCaller.DashboardActions) }
           };
            GetCommentsFromDB();
            SetActionCallabilityStatus(actions);
            var ResourceString = CurrentResource.ResourceToString(CurrentResource.Resource);
            ActionsHelper.GenericMenuAndMessage(actions, ResourceString);
        }
        public static void SetActionCallabilityStatus(List<Template> actions)
        {
            if (CurrentComment.Comments.Count == 0)
            {
                actions.RemoveAt(1);
            }
        }
        public static void GetCommentsFromDB()
        {
            CurrentComment.Comments = RepositoryFactory
                .Create<CommentBase>()
                .GetAll()
                .Where(ri => ri.ResourceId == CurrentResource.Resource.Id)
                .Where(c => c.CommentId == null)
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

                if (authorReputation >= 1000)
                {
                    ConsoleHelpers.WriteInColor($"{i} - {CurrentComment.CommentToString(comment)}", ConsoleColor.Cyan);
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
            ChosenCommentActions.CommentActions();
        }
        public static void AddComment()
        {
            Console.Clear();
            List<Template> actions = new()
            {
                new() { Status = InputStatus.WaitingForInput, Name = "Unos teksta", Function = null },
                new() { Status = InputStatus.Error, Name = "Dodaj komentar", Function = () => OneCommentActions.AddComment() },
                new() { Status = InputStatus.WaitingForInput, Name = "Odustani", Function = () => DashboardActions.ListResourceActions() }
            };
            actions[0].Function = () => OneCommentActions.SetText(actions);

            CurrentComment.InsertingComment = new();
            ActionsHelper.GenericMenuAndMessage(actions, "");
        }
    }
}
