using Rg.Plugins.Popup.Extensions;
using System;
using System.Linq;
using tabletka2.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tabletka2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pillssearch : ContentPage
    {
        public pillssearch()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            collectionviewfornotes.ItemsSource = await Calendari.notesDBForSearch.GetNotes();
            base.OnAppearing();
        }
        private void collectionviewforpills_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pill doc = (Pill)e.CurrentSelection[0];
            Device.OpenUri(new Uri(doc.pharmacy_sity));
        }
        private async void collectionviewfornotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null || e.PreviousSelection != null)
            {
                Navigation.PushPopupAsync(new Popupcalendar((Note)e.CurrentSelection[0], collectionviewfornotes));
            }
        }
        private void searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(e.NewTextValue==null||e.NewTextValue=="")
            {
                collectionviewforpills.IsVisible = false;
                collectionviewforpills.IsEnabled = false;
                collectionviewfornotes.IsVisible = true;
                collectionviewfornotes.IsEnabled = true;
            }
            else
            {
                collectionviewfornotes.IsVisible = false;
                collectionviewfornotes.IsEnabled = false;
                collectionviewforpills.IsVisible = true;
                collectionviewforpills.IsEnabled = true;
                collectionviewforpills.ItemsSource = App.Database.GetNotes().Result.Where(s => s.Name_of_pill_in_search.StartsWith(e.NewTextValue));
            }
        }
    }
}