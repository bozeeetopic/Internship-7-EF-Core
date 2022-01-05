using Data.Entities.Enums;

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
        public static ResourceDomain Get(ResourceTypes choice)
        {
            switch (choice)
            {
                case ResourceTypes.Dev:
                    return ResourceDomain.Dev;
                case ResourceTypes.Marketing:
                    return ResourceDomain.Marketing;
                case ResourceTypes.Multimedia:
                    return ResourceDomain.Multimedia;
                case ResourceTypes.Design:
                    return ResourceDomain.Design;
                case ResourceTypes.General:
                    return ResourceDomain.General;
                default:
                    break;
            }
            return ResourceDomain.General;
        }
    }
}
