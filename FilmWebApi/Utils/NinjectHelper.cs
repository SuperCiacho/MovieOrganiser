// File created by Bartosz Nowak on 20/04/2015 20:12

using Ninject;

namespace Yorgi.FilmWebApi.Utils
{
    internal class NinjectHelper
    {
        static NinjectHelper()
        {
            Kernel = new StandardKernel(new NinjectSettings()
            {
                InjectNonPublic = true
            });

            Kernel.Bind<IApiHelper>().To<ApiHelper>().InSingletonScope();
            Kernel.Bind<IContentAnalyzer>().To<ContentAnalyzer>().InSingletonScope();
            Kernel.Bind<Connection>().ToSelf().InSingletonScope();
        }

        private static IKernel Kernel { get; set; }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}