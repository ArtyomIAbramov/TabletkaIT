using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using tabletka2.Models;
using tabletka2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace tabletka2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Popupdoctors : Rg.Plugins.Popup.Pages.PopupPage
    {
        INotificationManager notificationManager;
        Doctor doc;
        Doctor prevdoc;
        CollectionView collection;
        public Popupdoctors(CollectionView collection1)
        {
            InitializeComponent();
            collection = collection1;
            BindingContext = new Doctor();
            notificationManager = DependencyService.Get<INotificationManager>();
        }
        public Popupdoctors(Doctor doc,CollectionView collection1)
        {
            InitializeComponent();
            collection = collection1;
            prevdoc = doc.Clone();
            BindingContext = doc;
            notificationManager = DependencyService.Get<INotificationManager>();
        }
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if(prevdoc!=null)
            {
                await Calendari.doctorDB.Delete(prevdoc);
                prevdoc = null;
            }
            doc = (Doctor)BindingContext;
            if (!string.IsNullOrEmpty(doc.Name_of_doctor))
            {
                doc.Name_of_doctor = doc.Name_of_doctor.Trim(Environment.NewLine.ToCharArray());
            }
            if (!string.IsNullOrEmpty(doc.Meeting_day))
            {
                doc.Meeting_day = doc.Meeting_day.Trim(Environment.NewLine.ToCharArray());
            }
            if (!string.IsNullOrEmpty(doc.Meeting_time))
            {
                doc.Meeting_time = doc.Meeting_time.Trim(Environment.NewLine.ToCharArray());
            }
            doc.ID=0;
            if(DateTime.Now< datapciker.Date || DateTime.Now.Date == datapciker.Date)
            {
                await Calendari.doctorDB.AddnewNote(doc);
                collection.ItemsSource = await Calendari.doctorDB.GetNotes();
                try {
                    notificationManager.SendNotification(doc.Name_of_doctor, doc.Meeting_time, datapciker.Date);
                }
                catch (Exception ex)
                {
                    DisplayAlert("Ошибка", "Некорректно заполнены данные", "ОК");
                }
                await Navigation.PopPopupAsync();
            }
            else
            {
                DisplayAlert("Ошибка", "Выбранная дата уже прошла", "ОК");
            }
        }
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (prevdoc != null)
            {
                await Calendari.doctorDB.Delete(prevdoc);
                prevdoc = null;
            }
            collection.ItemsSource = await Calendari.doctorDB.GetNotes();
            await Navigation.PopPopupAsync();
            //Doctor doc = (Doctor)BindingContext;
            //await Calendari.doctorDB.Delete(doc);
            //collection.ItemsSource = await Calendari.doctorDB.GetNotes();
            //await Navigation.PopPopupAsync();
        }

        private void Datapicker(object sender, EventArgs e)
        {
            datapciker.Focus();
        }

        private void datapciker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Date")
            {
                Meeting_day.Text = datapciker.Date.ToString("d");
            }
        }
        private void timePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Time")
            {
                Meeting_time.Text = $"{timePicker1.Time.Hours} : {timePicker1.Time.Minutes}";
            }

        }
        private void AddnewTime_Clicked(object sender, EventArgs e)
        {
            timePicker1.Focus();

        }
    }
}