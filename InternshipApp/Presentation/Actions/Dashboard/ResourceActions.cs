using Presentation.Helpers;
using System.Collections.Generic;
using System;
using Domain.Enums;
using Presentation.Actions.ActionHelpers;
using Domain.Models;
using Presentation.Enums;
using Presentation.Actions.Resource;
using Domain.Services;

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
            Console.Clear();
            for (var index = 0; index < _resourceDomainList.Count; index++)
            {
                Console.WriteLine($"{index+1} - {_resourceDomainList[index].Item1}");
            }
            var choice = Reader.UserNumberInput(" područje resoursa",1,5);
            ResourceType.Set(_resourceDomainList[choice-1].Item2);
        }
        public static void SetActionCallabilityStatus(List<Template> actions)
        {
            if(Resources.ResourcesList.Count == 0)
            {
                actions.RemoveAt(1);
            }
            if (Users.CurrentUser.ReputationPoints <= 100000)
            {
                actions.RemoveAt(0);
            }
        }
        public static void GetResourcesFromDB()
        {
            Resources.ResourcesList = ResourceServices.GetAll();

            var idsOfResourcesWithComments = ResourceServices.GetAllIds();

            if (!Resources.ListAll)
            {
                Resources.ResourcesList = ResourceServices.AllWhereIdNotIn(idsOfResourcesWithComments);
            }
        }
        public static void EnterResource() 
        {
            Console.Clear();
            Console.WriteLine("Redni broj - Naslov\t\t\tDatum\t\tBroj pregleda");
            var i = 1;
            foreach(var resource in Resources.ResourcesList)
            {
                Console.WriteLine($"\t{i}   -   {resource.Header}\t\t{resource.Date}\t{resource.SeenCounter}");
                i++;
            }
            var chosenResource = Reader.UserNumberInput(" redni broj resursa", 1, Resources.ResourcesList.Count)-1;
            Resources.CurrentResource = Resources.ResourcesList[chosenResource];

            var perception = PerceptionServices.GetPerception();
            if(perception == null)
            {
                PerceptionServices.Add(Users.CurrentUser.Id, Resources.CurrentResource.Id);
                Resources.CurrentResource.SeenCounter++;
                ResourceServices.Edit(Resources.CurrentResource);
            }
            ChosenResourceActions.ResourceActions();
        }
        public static void AddResource()
        {
            Resources.ChangingResource = new();
            Console.Clear();
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
