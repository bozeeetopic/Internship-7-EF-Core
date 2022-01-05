using Domain.Enums;
using Presentation.Helpers;
using System.Collections.Generic;
using System;

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
        public static ResourceType.ResourceTypes ChooseDomain()
        {
            foreach(var domainType in _resourceDomainList)
            {
                Console.WriteLine(domainType.Item1);
            }
            var choice = Reader.UserNumberInput("područje resoursa",1,5);
            return _resourceDomainList[choice].Item2;
        }
    }
}
