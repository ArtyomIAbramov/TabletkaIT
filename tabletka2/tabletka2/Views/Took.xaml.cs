using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabletka2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tabletka2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Took : ContentPage
    {
        public Took(string title)
        {

            InitializeComponent();
            name_of_pill.Text = "Вы приняли: "+ title+"!";
            imagedone.Scale = 0.1;
            imagedone.ScaleTo(0.9, 1000);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}