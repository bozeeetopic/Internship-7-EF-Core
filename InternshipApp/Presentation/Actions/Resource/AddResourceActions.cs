using Domain.Models;
using Domain.Services;
using Presentation.Actions.ActionHelpers;
using Presentation.Actions.Dashboard;
using Presentation.Enums;
using Presentation.Helpers;
using System;
using System.Collections.Generic;

namespace Presentation.Actions.Resource
{
    public static  class AddResourceActions
    {
        public static string UserPropertiesInput(List<Template> actions, int index)
        {
            var userInput = Reader.UserStringInput(actions[index].Name, "", 1);
            actions[index].Status = InputStatus.Done;
            actions[4].Status = InputStatus.Warning;
            AllInputsDone(actions);
            return userInput;
        }
        private static void AllInputsDone(List<Template> actions)
        {
            for (var i = 0; i < 3; i++)
            {
                if (actions[i].Status != InputStatus.Done)
                {
                    return;
                }
            }
            actions[3].Status = InputStatus.WaitingForInput;
            return;
        }
        public static void AddResource()
        {
            Resources.CurrentResource.Date = DateTime.Now;
            Resources.CurrentResource.Author = Users.CurrentUser;
            ResourceServices.Add();
            DashboardActions.ListResourceActions();
        }
        public static void SetHeader(List<Template> actions)
        {
            Resources.CurrentResource.Header = UserPropertiesInput(actions, 1);
        }
        public static void SetText(List<Template> actions)
        {
            Resources.CurrentResource.Text = UserPropertiesInput(actions, 2);
        }
        public static void SetDomain(List<Template> actions)
        {
            var currentValue = Resources.ResourceDomain;
            ResourceActions.ChooseDomain();
            Resources.CurrentResource.Domain = Resources.ResourceDomain;
            Resources.ResourceDomain = currentValue;
            actions[2].Status = InputStatus.Done;
            actions[4].Status = InputStatus.Warning;
            AllInputsDone(actions);
        }
    }
}
