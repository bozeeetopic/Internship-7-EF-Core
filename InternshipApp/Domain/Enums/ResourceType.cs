using Data.Entities.Enums;
using Domain.Models;

namespace Domain.Enums
{
    public static class ResourceType
    {
        public enum ResourceTypes
        {
            Dev,
            Design,
            Multimedia,
            Marketing,
            General
        }
        public static void Set(ResourceTypes choice)
        {
            CurrentResource.Resource = new();
            switch (choice)
            {
                case ResourceTypes.Dev:
                    CurrentResource.ResourceDomain = ResourceDomain.Dev;
                    break;
                case ResourceTypes.Marketing:
                    CurrentResource.ResourceDomain = ResourceDomain.Marketing;
                    break;
                case ResourceTypes.Multimedia:
                    CurrentResource.ResourceDomain = ResourceDomain.Multimedia;
                    break;
                case ResourceTypes.Design:
                    CurrentResource.ResourceDomain = ResourceDomain.Design;
                    break;
                case ResourceTypes.General:
                    CurrentResource.ResourceDomain = ResourceDomain.General;
                    break;
                default:
                    break;
            }
        }
    }
}
