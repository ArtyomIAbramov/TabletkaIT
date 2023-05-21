using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tabletka2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class privetstvie : CarouselPage
    {
        public privetstvie()
        {
            InitializeComponent();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
         
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            secondimage.Scale = 0.1;
            secondimage.ScaleTo(0.9, 2200);
            await secondimage.TranslateTo(0, 50, 500);
            await secondimage.TranslateTo(0, -50, 500);
            await secondimage.TranslateTo(0, 50, 500);
            await secondimage.TranslateTo(0, 0, 500);
        }

        private async void ContentPage_Appearing_1(object sender, EventArgs e)
        {
            //firstimage.TranslationX = -400;
            firstimage.Scale = 0.1;
            firstimage.ScaleTo(0.9, 2200);
            //firstimage.RotateTo(360, 1500);
            //await firstimage.TranslateTo(0, 0, 1500);
            await firstimage.TranslateTo(0, -50, 500);
            await firstimage.TranslateTo(0, 50, 500);
            await firstimage.TranslateTo(0, 0, 500);


        }

        private async void ContentPage_Appearing_2(object sender, EventArgs e)
        {
            thirdimage.Scale = 0.1;
            thirdimage.ScaleTo(0.75, 2200);
            await thirdimage.TranslateTo(0, 50, 500);
            await thirdimage.TranslateTo(0, -50, 500);
            await thirdimage.TranslateTo(0, 50, 500);
            await thirdimage.TranslateTo(0, 0, 500);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Calendari());
        }
    }
}