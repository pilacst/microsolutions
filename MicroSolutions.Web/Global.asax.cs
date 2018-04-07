using MicroSolutions.DAL;
using MicroSolutions.Task.ImportDataForEmailAlert;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;

namespace MicroSolutions.Web
{
    public class MvcApplication : System.Web.HttpApplication
	{
		private static SimpleMembershipInitializer _initializer;
		private static object _initializerLock = new object();
		private static bool _isInitialized;
		public static string CurruntUser { get; set; }

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();
			LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
			JobScheduler.Start();
			if (User != null)
			{
				CurruntUser = WebSecurity.GetUserId(User.Identity.Name).ToString();
			}
		}

		public class SimpleMembershipInitializer
		{
			public SimpleMembershipInitializer()
			{
				Database.SetInitializer<MicroSolutionsContext>(null);

				try
				{
					using (var context = new MicroSolutionsContext())
					{
						if (!context.Database.Exists())
						{
							// Create the SimpleMembership database without Entity Framework migration schema
							((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
						}
					}

					WebSecurity.InitializeDatabaseConnection("MicroSolutionsContext", "UserProfile", "ID", "UserName", autoCreateTables: true);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
				}
			}
		}
	}
}
