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
            for(var index = 0; index < _resourceDomainList.Count; index++)
            {
                Console.WriteLine($"{index+1} - {_resourceDomainList[index].Item1}");
            }
            var choice = Reader.UserNumberInput("područje resoursa",1,5);
            ResourceType.Set(_resourceDomainList[choice-1].Item2);
        }
        public static void SetActionCallabilityStatus(List<Template> actions)
        {
            if(CurrentResource.Resources.Count == 0)
            {
                actions.RemoveAt(1);
            }
            if (CurrentUser.User.ReputationPoints <= 100000)
            {
                actions.RemoveAt(0);
            }
        }
        public static void GetResourcesFromDB()
        {
            CurrentResource.Resources = RepositoryFactory
                .Create<ResourceBase>()
                .GetAll()
                .Where(r => r.Domain == CurrentResource.ResourceDomain)
                .ToList();

            var idsOfResourcesWithComments = RepositoryFactory
                .Create<CommentBase>()
                .GetAll()
                .Select(x => x.ResourceId)
                .ToArray();

            if (!CurrentResource.ListAll)
            {
                CurrentResource.Resources = CurrentResource.Resources
                    .Where(r => !idsOfResourcesWithComments.Contains(r.Id))
                    .ToList();
            }
        }
        public static void EnterResource() 
        {
            Console.WriteLine("Redni broj - Naslov\tPodručje\tDatum\tBroj pregleda");
            var i = 1;
            foreach(var resource in CurrentResource.Resources)
            {
                Console.WriteLine($"{i} - {resource.Header}\t{resource.Domain}\t{resource.Date}\t{resource.SeenCounter}");
                i++;
            }
            var chosenResource = Reader.UserNumberInput("Unesi redni broj resursa", 1, CurrentResource.Resources.Count)-1;
            CurrentResource.Resource = CurrentResource.Resources[chosenResource];

            var perceptions = RepositoryFactory
                .Create<PerceiveBase>();
            var perception = perceptions.GetAll()
                .Where(r => r.ResourceId == CurrentResource.Resource.Id)
                .Where(r => r.PerceiverId == CurrentUser.User.Id)
                .ToList();
            if(perception == null)
            {
                perceptions.Add(CurrentUser.User.Id, CurrentResource.Resource.Id);
                CurrentResource.Resource.SeenCounter++;
                RepositoryFactory
                    .Create<ResourceBase>()
                    .Edit(CurrentResource.Resource, CurrentResource.Resource.Id);
            }
            ChosenResourceActions.ResourceActions();
        }
        public static void AddResource()
        {
            List<Template> actions = new()
            {
                new() { Status = InputStatus.WaitingForInput, Name = "Unos naslova", Function = null },
                new() { Status = InputStatus.WaitingForInput, Name = "Unos teksta", Function = null },
                new() { Status = InputStatus.WaitingForInput, Name = "Unos područja", Function = null },
                new() { Status = InputStatus.Error, Name = "Dodaj resurs", Function = () => AddResourceActions.AddResource() },
                new() { Status = InputStatus.WaitingForInput, Name = "Odustani", Function = () => DashboardActions.ListResourceActions()}
            };
            actions[0].Function = () => AddResourceActions.SetHeader(actions);
            actions[1].Function = () => AddResourceActions.SetText(actions);
            actions[2].Function = () => AddResourceActions.SetDomain(actions);

            ActionsHelper.GenericMenuAndMessage(actions,"");
        }
    }
}
