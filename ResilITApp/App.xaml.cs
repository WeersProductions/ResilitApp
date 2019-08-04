using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ResilITApp
{
    public partial class App : Application
    {
        private const string ConfigFile = "config.json";
        public App()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{ assembly.GetName().Name}.{ ConfigFile}");
            Config config = null;
            using(var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                config = JsonConvert.DeserializeObject<Config>(json);
            }

            if(config != null)
            {
                Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(config.SyncFusionKey);
            }
            
            InitializeComponent();

            MainPage = new ResilITApp.MainPage();
            if (config == null)
            {
                MainPage.DisplayAlert("Error", "Could not find config.json. Please report this bug.", "OK");
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
