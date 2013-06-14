using StructureMap;
using System.Web.Mvc;

namespace FeedR.Factories
{
    /// <summary>
    /// Controller factory that will attempt to get all the dependencies from the container.
    /// </summary>
    public class SMControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, System.Type controllerType)
        {
            if (requestContext == null || controllerType == null)
                return null;
            return (IController)ObjectFactory.GetInstance(controllerType);
        }
    }
}