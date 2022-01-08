using Domain.Models;
using Domain.Services;
using Presentation.Actions.ActionHelpers;
using Presentation.Actions.Comments;
using Presentation.Actions.Dashboard;
using Presentation.Enums;
using Presentation.Helpers;
using System;
using System.Collections.Generic;

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
                new() { Status = InputStatus.WaitingForInput, Name = "Povratak u meni", Function = () => ActionsCaller.PrintMenuAndDoAction(DashboardActions.DashboardActionsList) }
           };
            GetCommentsFromDB();
            SetActionCallabilityStatus(actions);
            var ResourceString = Resources.ResourceToString(Resources.CurrentResource);
            ActionsHelper.GenericMenuAndMessage(actions, ResourceString);
        }
        public static void SetActionCallabilityStatus(List<Template> actions)
        {
            if (Domain.Models.Comments.CommentsList.Count == 0)
            {
                actions.RemoveAt(1);
            }
        }
        public static void GetCommentsFromDB()
        {
            CommentServices.SetComments();
        }
        public static void EnterComment()
        {
            Console.Clear();
            Console.WriteLine("Redni broj - \tAutor\tDatum objave\tUpvotes\tDownvotes\tText(iduca linija)");
            var i = 1;
            foreach (var comment in Domain.Models.Comments.CommentsList)
            {
                var authorReputation = UserServices.ReturnUsersReputationPoints(comment.AuthorId);

                if (authorReputation >= 1000)
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

            Domain.Models.Comments.InsertingComment = new();
            ActionsHelper.GenericMenuAndMessage(actions, "");
        }
    }
}
