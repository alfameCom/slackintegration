//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Host;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Linq;
//using System.Reflection;

//namespace SlackIntegration.Functions
//{
//    public static class Init
//    {
//        [FunctionName("Tick")]
//        public static void Run(
//            [TimerTrigger("0 */5 * * * *")]TimerInfo myTime,
//            ILogger log)
//        {
//            log.LogTrace($"Tick isPastDue:{myTime.IsPastDue}");
//            Redirect.Run();
//        }
//    }

//    /// <summary>
//    /// Varastettu jostain varmasti
//    /// </summary>
//    public static class Redirect
//    {
//        public static void Run()
//        {
//            var list = AppDomain.CurrentDomain.GetAssemblies().OrderByDescending(a => a.FullName).Select(a => a.FullName).ToList();
//            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
//        }

//        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
//        {
//            var requestedAssembly = new AssemblyName(args.Name);
//            Assembly assembly = null;
//            AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
//            try
//            {
//                assembly = Assembly.Load(requestedAssembly.Name);
//            }
//            catch (Exception)
//            {
//            }
//            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
//            return assembly;
//        }
//    }
//}