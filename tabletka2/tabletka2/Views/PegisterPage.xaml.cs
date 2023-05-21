using System;
using System.IO;
using System.Xml.Serialization;
using tabletka2.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tabletka2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PegisterPage : ContentPage
    {
        public static User user;
        public PegisterPage()
        {
            InitializeComponent();
        }
        public void SerializeXML(User user)
        {
            XmlSerializer xml = new XmlSerializer(typeof(User));
            using(FileStream fs = new FileStream(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User.xml"), FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, user);
            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            user = new User();
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User.xml")))
            {
               File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User.xml"));
            }
            if (!string.IsNullOrEmpty(Username.Text))
            {
                user.username = Username.Text.Trim(Environment.NewLine.ToCharArray());
                if (!string.IsNullOrEmpty(Email.Text))
                {
                    user.email = Email.Text.Trim(Environment.NewLine.ToCharArray());
                    if (!string.IsNullOrEmpty(Userpassword.Text))
                    {
                        user.userpassword = Userpassword.Text.Trim(Environment.NewLine.ToCharArray());
                        if (!string.IsNullOrEmpty(Userpasswordagain.Text) && Userpasswordagain.Text== Userpassword.Text)
                        {
                            SerializeXML(user);
                            Navigation.PushAsync(new privetstvie());
                       
                        }
                        else
                        {
                            DisplayAlert("Ошибка", "Пароли не совпадают", "ОК");
                        }
                    }
                    else
                    {
                        DisplayAlert("Ошибка", "Введите пароль", "ОК");
                    }
                }
                else
                {
                    DisplayAlert("Ошибка", "Введите Email", "ОК");
                }
            }
            else
            {
                DisplayAlert("Ошибка", "Введите логин", "ОК");
            }
        }
    }
}