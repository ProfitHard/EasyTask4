using DevExpress.XtraReports.Design;
using EasyTask4.BLL;
using EasyTask4.DAL;
using Ninject;
using System;
using System.Data;
using System.Data.SqlClient;


namespace EasyTask4
{
    public class Global : System.Web.HttpApplication
    {
        private static IKernel _kernel;
        protected void Application_Start(object sender, EventArgs e)
        {
            _kernel = new StandardKernel();
            ConfigureBindings();
        }

        private void ConfigureBindings()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FirebirdDB"].ConnectionString;
            _kernel.Bind<ITaskRepository>().To<TaskRepository>().WithConstructorArgument("connectionString", connectionString);
            _kernel.Bind<IUserRepository>().To<UserRepository>().WithConstructorArgument("connectionString", connectionString);
            _kernel.Bind<IStatusRepository>().To<StatusRepository>().WithConstructorArgument("connectionString", connectionString);
            _kernel.Bind<IUserService>().To<UserService>(); 
            _kernel.Bind<TaskService>().ToSelf();
            _kernel.Bind<StatusService>().ToSelf();
        }

        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }
    }
}