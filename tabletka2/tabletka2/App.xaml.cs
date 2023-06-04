using System;
using System.IO;
using tabletka2.Views;
using Xamarin.Forms;
using System.Reflection;
using tabletka2.Data;
using Android.Content;
using tabletka2.ViewModels;

namespace tabletka2
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "PillsDateBase.db3";
        public static PillsDB database;
        public static PillsDB Database
        {
            get
            {
                if (database == null)
                {
                    // путь, по которому будет находиться база данных
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PillsDateBase.db3");
                    // если база данных не существует (еще не скопирована)
                    if (!File.Exists(dbPath))
                    {
                        // получаем текущую сборку
                        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                        // берем из нее ресурс базы данных и создаем из него поток
                        using (Stream stream = assembly.GetManifestResourceStream($"tabletka2.PillsDateBase.db3"))
                        {
                            using (MemoryStream fs = new MemoryStream())
                            {
                                stream.CopyTo(fs);  // копируем файл базы данных в нужное нам место
                                File.WriteAllBytes(dbPath, fs.ToArray());

                            }
                        }
                    }
                    database = new PillsDB(dbPath);
                }
                return database;
            }
        }
        string check_is_took;
        string title;
        public App()
        {
            InitializeComponent();

        }
        public App(string t, string title)
        {
            check_is_took = t;
            this.title = title;
            InitializeComponent();
        }
        protected override void OnStart()
        {
            if (check_is_took == "Took")
            {
                MainPage = new NavigationPage(new Took(this.title));
                check_is_took = null;
            }
            else
            {
                MainPage = new NavigationPage(new Login());
            }
        }
        public void CreateNotificationFromIntent1(Intent intent, string title)
        {
            Current.MainPage = new NavigationPage(new Took(title));
        }

        protected override void OnSleep()
        {
            
        }

        protected override void OnResume()
        {
            
        }
    }
}
