using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using tabletka2.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tabletka2.ViewModels;

namespace tabletka2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Popupcalendar : Rg.Plugins.Popup.Pages.PopupPage
    {
        INotificationManager notificationManager;
        DateTime helperFornewNote;
        Calendari calendari;
        Note note;
        string prevnote, prevnote1;
        bool check = false;
        CollectionView collection;
        List<TimeSpan> Items;
        public Popupcalendar(Note note, CollectionView collection1,DateTime date)
        {
            InitializeComponent();
            BindingContext = note;
            prevnote = note.Name_of_pill;
            collection = collection1;
            helperFornewNote = date;
            Items = new List<TimeSpan>();
            if(note.dateTakeTime1!=null)
            {
                ParseToTimeSpan(note.dateTakeTime1);
            }
            notificationManager = DependencyService.Get<INotificationManager>();
        }
        public Popupcalendar(Note note, CollectionView collection1)
        {
            InitializeComponent();
            BindingContext = note;
            collection = collection1;
            Items = new List<TimeSpan>();
            check = true;
            prevnote1 = note.Name_of_pill;
        }
        public Popupcalendar(DateTime date, CollectionView collection1, Calendari calendari)
        {
            InitializeComponent();
            BindingContext = new Note();
            helperFornewNote = date;
            this.calendari = calendari;
            collection = collection1;
            Items = new List<TimeSpan>();
            notificationManager = DependencyService.Get<INotificationManager>();
        }
        private void ParseToTimeSpan(string str)
        {
            string[] note1 = str.Split('\n');
            TimeSpan timerspan;
            for (int i = 0; i!= note1.Length-1; ++i)
            {
            TimeSpan.TryParse(note1[i], out timerspan);
            Items.Add(timerspan);
            }
            collectionviewfortime.ItemsSource = Items;
            reload();
        }
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if(prevnote!=null)
            {
                Calendari.notesDB.deleteGetNotescurr(prevnote);
                Calendari.notesDBForSearch.deleteGetNotescurr(prevnote);
                prevnote = null;
            }
            if (prevnote1 != null)
            {
                Calendari.notesDBForSearch.deleteGetNotescurr(prevnote1);
                prevnote1 = null;
            }
            note = (Note)BindingContext;
            if (!string.IsNullOrEmpty(note.Name_of_pill))
            {
                note.Name_of_pill = note.Name_of_pill.Trim(Environment.NewLine.ToCharArray());
            }
            else
                note.Name_of_pill = "";
            if (!string.IsNullOrEmpty(note.Measure))
            {
                note.Measure = note.Measure.Trim(Environment.NewLine.ToCharArray());
            }
            else
                note.Measure = "";
            if (!string.IsNullOrEmpty(note.Description))
            {
                note.Description = note.Description.Trim(Environment.NewLine.ToCharArray());
            }
            else
                note.Description = "";
            note.dateTakeTime1 = null;
            if(Items.Count!=0)
            {
                Items.Sort();
                for (int i = 0; i != Items.Count; ++i)
                {
                    note.dateTakeTime1 = note.dateTakeTime1 + Items[i] + "\n";
                }
            }
            note.ID = 0;
            if (Days_of_Take.Text==null)
            {
                note.days = 1.ToString();
            }
            else
            {
                note.days = Days_of_Take.Text;
            }
            if(check==true)
            {
                check = false;
                note.dateTakeTime = DateTime.Now;
                note.ID = 0;
                 Calendari.notesDBForSearch.AddnewNote(note);
                collection.ItemsSource = await Calendari.notesDBForSearch.GetNotes();
                await Navigation.PopPopupAsync();
            }
            else
            {
                note.ID = 0;
                note.dateTakeTime = helperFornewNote;
                Calendari.notesDBForSearch.AddnewNote(note);
                Calendari.notesDB.AddnewNote(note);
                ParseToTimeSpanList(note, note.dateTakeTime);
                note.dateTakeTime = note.dateTakeTime.AddDays(1);
                for (int i = 0; i < int.Parse(Days_of_Take.Text)-1; ++i)
                    {
                    note.ID = 0;
                    Calendari.notesDB.AddnewNote(note);
                    ParseToTimeSpanList(note, note.dateTakeTime);
                    note.dateTakeTime = note.dateTakeTime.AddDays(1);
                    }
                collection.ItemsSource = await Calendari.notesDB.GetNotes(helperFornewNote);
                await Navigation.PopPopupAsync();
            }
        }
        private void ParseToTimeSpanList(Note note, DateTime dt)
        {
            if (note.dateTakeTime1!=null)
            {
                string[] note1 = note.dateTakeTime1.Split('\n');
                TimeSpan timerspan;
                for (int i = 0; i != note1.Length - 1; ++i)
                {
                    if (TimeSpan.TryParse(note1[i], out timerspan))
                    {
                        dt = dt.Add(timerspan);
                        if (DateTime.Now < dt)
                        {
                            notificationManager.SendNotification(note.Name_of_pill, note.Measure, dt);
                        }
                    }
                }
            }
           
        }
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (prevnote1 != null)
            {
                Note note = (Note)BindingContext;
                await Calendari.notesDBForSearch.Delete(note);
                collection.ItemsSource = await Calendari.notesDBForSearch.GetNotes();
                prevnote1 = null;
            }
            else
            {
                Note note = (Note)BindingContext;
                await Calendari.notesDB.Delete(note);
                collection.ItemsSource = await Calendari.notesDB.GetNotes(helperFornewNote);
            }
                await Navigation.PopPopupAsync();
        }
        private void AddnewTime_Clicked(object sender, EventArgs e)
        {
            timePicker.Focus();

        }
        private void timePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Time")
            {
                Items.Add(timePicker.Time);
                collectionviewfortime.ItemsSource = Items;
                reload();
            }
         
        }
        private void reload()
        {
            var template = new DataTemplate(typeof(TextCell));
            template.SetValue(TextCell.TextColorProperty, Color.White);
            template.SetBinding(TextCell.TextProperty, ".");
            collectionviewfortime.ItemTemplate = template;
        }
        private void collectionviewfortime_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Items.Remove((TimeSpan)collectionviewfortime.SelectedItem);
            timePicker.Focus();
        }

    }
}