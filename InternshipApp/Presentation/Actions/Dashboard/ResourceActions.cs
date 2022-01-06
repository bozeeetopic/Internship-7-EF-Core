using Presentation.Helpers;
using System.Collections.Generic;
using System;
using Domain.Enums;
using Presentation.Actions.ActionHelpers;
using Domain.Models;
using Domain.Factories;
using Domain.Repositories;
using System.Linq;
using Presentation.Enums;
using Presentation.Actions.Dashboard.Resource;

namespace Presentation.Actions.Dashboard
{
    public static class ResourceActions
    {
        private static readonly List<(string, ResourceType.ResourceTypes)> _resourceDomainList = new()
        {
            ( "Dev", ResourceType.ResourceTypes.Dev ),
            ( "Multimedija", ResourceType.ResourceTypes.Multimedia ),
            ( "Marketing", ResourceType.ResourceTypes.Marketing ),
            ( "Dizajn", ResourceType.ResourceTypes.Design ),
            ( "General", ResourceType.ResourceTypes.General )
        };
        public static void ChooseDomain()
        {
            foreach(var domainType in _resourceDomainList)
            {
                Console.WriteLine(domainType.Item1);
            }
            var choice = Reader.UserNumberInput("područje resoursa",1,5);
            ResourceType.Set(_resourceDomainList[choice].Item2);
        }
        public static void SetActionCallabilityStatus(List<Template> actions)
        {
            if(CurrentResource.Resources.Count == 0)
            {
                actions.RemoveAt(2);
                actions.RemoveAt(1);
            }
            if (CurrentUser.User.ReputationPoints <= 100000)
            {
                actions.RemoveAt(0);
            }
        }
        public static string ResourcesToString(bool listAll)
        {
            CurrentResource.Resources = RepositoryFactory
                .Create<ResourceBase>().
                GetAll()
                .Where(r => r.Domain == CurrentResource.ResourceDomain)
                .ToList();

            var idsOfResourcesWithComments = RepositoryFactory
                .Create<CommentBase>()
                .GetAll().
                Select(x => x.ResourceId)
                .ToArray();

            if (!listAll)
            {
                CurrentResource.Resources = CurrentResource.Resources
                    .Where(r => !idsOfResourcesWithComments
                    .Contains(r.Id))
                    .ToList();
            }
            var stringToReturn = "";
            if (CurrentResource.Resources.Count == 0)
            {
                return "Ne postoje resursi!";
            }
            else
            {
                stringToReturn = "Redni broj\tPodručje\tIme resursa\t\tBroj pregleda";
                var index = 1;
                foreach(var resource in CurrentResource.Resources)
                {
                    stringToReturn += $"{index}\t{resource.Domain}\t{resource.Header}\t\t{resource.SeenCounter}\n";
                    index++;
                }
            }
            return stringToReturn;
        }
        public static void EnterResource() 
        {
        }
        public static void AddResource(bool listAll)
        {
            List<Template> actions = new()
            {
                new() { Status = InputStatus.WaitingForInput, Name = "Unos naslova", Function = null },
                new() { Status = InputStatus.WaitingForInput, Name = "Unos teksta", Function = null },
                new() { Status = InputStatus.WaitingForInput, Name = "Unos područja", Function = null },
                new() { Status = InputStatus.Error, Name = "Dodaj resurs", Function = () => AddResourceActions.AddResource(listAll) },
                new() { Status = InputStatus.WaitingForInput, Name = "Odustani", Function = () => DashboardAction.ListResourceActions(listAll)}
            };
            actions[0].Function = () => AddResourceActions.SetHeader(actions);
            actions[1].Function = () => AddResourceActions.SetText(actions);
            actions[2].Function = () => AddResourceActions.SetDomain(actions);

            ActionsHelper.GenericMenu(actions, "");
        }
    }
}
