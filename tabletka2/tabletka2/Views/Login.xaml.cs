using System;
using System.IO;
using System.Xml.Serialization;
using tabletka2.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tabletka2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        User us;
        public Login()
        {
            InitializeComponent();
            if(File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User.xml")))
            {
                us = DeserializeXML();
                Username.Text = us.username;
                Userpassword.Text = us.userpassword;
            }
        }

        public User DeserializeXML()
        {
            XmlSerializer xml = new XmlSerializer(typeof(User));
            using (FileStream fs = new FileStream(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User.xml"), FileMode.OpenOrCreate))
            {
                return (User)xml.Deserialize(fs);
            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            if(us!=null)
            {
                if (string.IsNullOrWhiteSpace(Username.Text) || string.IsNullOrWhiteSpace(Userpassword.Text) || us.username != Username.Text || us.userpassword != Userpassword.Text)
                {
                    DisplayAlert("Ошибка", "Пароль или логин набран неправильно", "ОК");
                }
                else
                {
                    Navigation.PushAsync(new Calendari());
                }
            }
            else
            {
                DisplayAlert("Ошибка", "Пароль или логин набран неправильно", "ОК");
            }
            
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            
            Navigation.PushAsync(new PegisterPage());
        }
    }
}