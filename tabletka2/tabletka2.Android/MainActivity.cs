using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Android.Content;
using Xamarin.Forms;
using tabletka2.ViewModels;
using Android.Provider;
using System;
using Android.Database;
using Android.Content.Res;
using tabletka2.Views;
using Java.Lang;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Xml.Serialization;

namespace tabletka2.Droid
{

    [Activity(Label = "Таблетка", Icon = "@drawable/iconapp", LaunchMode = LaunchMode.SingleTop, Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        App a;
        static bool check_if_pay = false, first = true, second= false, serialchatid=true;
        TelegramBotClient client;
        internal static MainActivity Instance { get; private set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            client = new TelegramBotClient("6220772826:AAEiCEI-8MZQj9w715gzadevY5doJ9NOxLA");
            client.StartReceiving(Update, Error);
            CreateNotificationFromIntent(Intent);
            Instance = this;
            check_if_pay = false;
            first = true;
            second = false;

        }
        protected override void OnNewIntent(Intent intent)
        {
            CreateNotificationFromIntent(intent);
        }
        private Task Error(ITelegramBotClient arg1, System.Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
        private IReplyMarkup Getbutton()
        {
            check_if_pay = true;
            return new ReplyKeyboardMarkup(new KeyboardButton ( "Купить!" ));

        }
        static Telegram.Bot.Types.Message message1;
        public void SerializeXML(long user)
        {
            if (File.Exists(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "longidchat.xml")))
            {
                File.Delete(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "longidchat.xml"));
            }
            XmlSerializer xml = new XmlSerializer(typeof(long));
            using (FileStream fs = new FileStream(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "longidchat.xml"), FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, user);
            }

        }
        public long DeserializeXML()
        {
            XmlSerializer xml = new XmlSerializer(typeof(long));
            using (FileStream fs = new FileStream(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "longidchat.xml"), FileMode.OpenOrCreate))
            {
                return (long)xml.Deserialize(fs);
            }
        }
        async Task Update(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken token)
        {
            if(serialchatid)
            {
                message1 = update.Message;
                serialchatid = false;
                SerializeXML(message1.Chat.Id);
            }
            if (message1.Text != null)
            {
                if (update.Message.Text == "/start")
                {
                    if (check_if_pay == false && first)
                    {
                        await botClient.SendTextMessageAsync(update.Message.Chat.Id, "У вас нет подписки?\nКупите за 100р/месяц", replyMarkup: Getbutton());
                        return;
                    }

                }
                else
                {
                    if (second)
                    {
                        await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Вы подключились к клиенту!");
                        second = false;
                        return;
                    }
                }
            }
            if (check_if_pay)
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Поздравляем вы подключены!\nВведите ID клиента:");
                check_if_pay = false;
                first = false;
                second = true;
                return;
            }
        }
        int offset = 23;
        void CreateNotificationFromIntent(Intent intent)
        {
          
            if (intent?.Extras != null)
            {
                string title = intent.GetStringExtra(AndroidNotificationManager.TitleKey);
                string message = intent.GetStringExtra(AndroidNotificationManager.MessageKey);
                DependencyService.Get<INotificationManager>().ReceiveNotification(title, message);
                if (a != null)
                {
                    if (File.Exists(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "longidchat.xml")))
                    {
                        client.SendTextMessageAsync(DeserializeXML(), "Клиент принял: " + title + "\n" + "В количестве: " + message, null, null, null, null, null, null, null, null, null, default);
                    }
                    a.CreateNotificationFromIntent1(intent, title);
                    return;
                }
                else
                {
                    if(title!=null)
                    {
                        LoadApplication(new App("Took", title));
                        if (File.Exists(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "longidchat.xml")))
                        {
                            client.SendTextMessageAsync(DeserializeXML(), "Клиент принял: " + title + "\n" + "В количестве: " + message, null, null, null, null, null, null, null, null, null, default);
                        }
                    }
                    else
                    {
                        a = new App();
                        LoadApplication(a);
                    }
                }
            }
            else
            {

                a = new App();
                LoadApplication(a);
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }
    }
}
