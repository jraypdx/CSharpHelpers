using System;

namespace Example_App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(LoadFromSameFolder);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            base.OnStartup(e);
            ShutdownMode = ShutdownMode.OnLastWindowClose; //all open windows have to be closed, ex. if user has a MergeTool window open as well as the main one
        }


        //catches startup exceptions - useful for finding out why it crashes instead of getting no info
        public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unhandled Exception");
            MessageBox.Show(e.ExceptionObject.ToString());
        }


        /// <summary>
        /// An event that gets triggered when an assembly can't be found.  Used here to load HLK assemblies at run from the HLK install directory so that any version of HLK will work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Assembly LoadFromSameFolder(object sender, ResolveEventArgs args)
        {
            //https://weblog.west-wind.com/posts/2016/dec/12/loading-net-assemblies-out-of-seperate-folders
            //http://www.codingmurmur.com/2014/02/embedded-assembly-loading-with-support.html   (https://stackoverflow.com/questions/1518073/embed-pdb-into-assembly)
            string myAssemblyDir = @"C:\INSTALLED_PROGRAM_OR_OTHER_FOLDER_PATH_HERE";

            // Ignore missing resources
            if (args.Name.Contains(".resources"))
                return null;

            // check for assemblies already loaded
            Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            if (assembly != null)
                return assembly;
            
            // Try to load by filename - split out the filename of the full assembly name
            // and append the base path of the original assembly (ie. look in the same dir)
            string assemblyName = args.Name.Split(',')[0];
            string filename = assemblyName + ".dll".ToLower();

            //Handle nuget or other embedded assemblies
            if (filename.ToLower().Contains("something that signifies it's an embedded assembly"))
            {
                var executingAssembly = Assembly.GetExecutingAssembly();
                var resourceNames = executingAssembly.GetManifestResourceNames();
                string resourceName = resourceNames.SingleOrDefault(n => n.Contains(filename));
                
                if (String.IsNullOrWhiteSpace(resourceName))
                    return null;

                string pdb = assemblyName + ".pdb";
                string resourcePDB = resourceNames.SingleOrDefault(n => n.Contains(pdb));

                var assemblyData = LoadResourceBytes(executingAssembly, resourceName);

                if (string.IsNullOrWhiteSpace(resourcePDB)) // If no .pdb is found (usually isn't) just load the .dll
                {
                    return Assembly.Load(assemblyData);
                }

                else // If a .pdb is found, load the .dll and .pdb
                {
                    var symbolsData = LoadResourceBytes(executingAssembly, resourcePDB);
                    return Assembly.Load(assemblyData, symbolsData);
                }
                
            }

			// For assemblies that are not embedded, for example from another program that you app relies on, find them in the specified location
			// Tip: You can use multiple locations as long as you check if the file exists before loading it
            string asmFile = Path.Combine(myAssemblyDir, filename);

            try
            {
                return System.Reflection.Assembly.LoadFrom(asmFile);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static byte[] LoadResourceBytes(Assembly executingAssembly, string resourceName)
        {
            using (var stream = executingAssembly.GetManifestResourceStream(resourceName))
            {
                var data = new byte[stream.Length];

                stream.Read(data, 0, data.Length);

                return data;
            }
        }


    }
}
