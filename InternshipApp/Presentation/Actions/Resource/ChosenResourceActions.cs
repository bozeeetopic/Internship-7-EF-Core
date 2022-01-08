using Domain.Models;
using Domain.Services;
using Presentation.Actions.ActionHelpers;
using Presentation.Actions.Comment;
using Presentation.Actions.Dashboard;
using Presentation.Enums;
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
                new() { Status = InputStatus.WaitingForInput, Name = "Uđi u komentare", Function = () => ChosenCommentActions.EnterComment() },
                new() { Status = InputStatus.WaitingForInput, Name = "Povratak u odabir područja", Function = () => DashboardActions.ChooseDomainAndListResourceAction(Resources.ListAll) },
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
            if (Comments.CommentsList.Count == 0)
            {
                actions.RemoveAt(1);
            }
        }
        public static void GetCommentsFromDB()
        {
            CommentServices.SetComments();
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

            Comments.InsertingComment = new();
            ActionsHelper.GenericMenuAndMessage(actions, "");
        }
    }
}
