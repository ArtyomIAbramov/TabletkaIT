using System;
using System.IO;
using tabletka2.Views;
using Xamarin.Forms;
using System.Reflection;
using tabletka2.Data;

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
        readonly string y;
        public App()
        {
            InitializeComponent();
        }
        public App(string t)
        {
            y = t;
            InitializeComponent();
        }
        protected override void OnStart()
        {
            if(y=="Took")
            {
                MainPage = new NavigationPage(new Took());
            }
            else
            {
                MainPage = new NavigationPage(new Calendari());
            }
            //MainPage = new NavigationPage(new privetstvie());
            //MainPage = new privetstvie();
            //MainPage = new NavigationPage(new Login());
            //MainPage = new NavigationPage(new Calendari());
        }


        protected override void OnSleep()
        {
            
        }

        protected override void OnResume()
        {
            
        }
    }
}
