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
            Resources.CurrentResource = new();
            switch (choice)
            {
                case ResourceTypes.Dev:
                    Resources.ResourceDomain = ResourceDomain.Dev;
                    break;
                case ResourceTypes.Marketing:
                    Resources.ResourceDomain = ResourceDomain.Marketing;
                    break;
                case ResourceTypes.Multimedia:
                    Resources.ResourceDomain = ResourceDomain.Multimedia;
                    break;
                case ResourceTypes.Design:
                    Resources.ResourceDomain = ResourceDomain.Design;
                    break;
                case ResourceTypes.General:
                    Resources.ResourceDomain = ResourceDomain.General;
                    break;
                default:
                    break;
            }
        }
    }
}
