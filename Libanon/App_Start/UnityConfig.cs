using Libanon.Repository;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Libanon
{
    public class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            // e.g. container.RegisterType<ITestService, TestService>(); 

            container.RegisterType<IBookRepository,BookRepository>();
            container.RegisterType<IImageRepository, ImageRepository>();
            container.RegisterType<IUserRepository, UserRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}