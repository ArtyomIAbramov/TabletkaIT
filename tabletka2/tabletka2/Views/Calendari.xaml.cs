using System;
using Xamarin.Forms;
using tabletka2.Models;
using tabletka2.Data;
using System.IO;
using Rg.Plugins.Popup.Extensions;
using System.Xml.Serialization;
using Xamarin.Essentials;

namespace tabletka2.Views
{
    //байдинг хелп
    public partial class Calendari : ContentPage
    {
        DateTime currdateTime;
        DateTime datefordatepicker;
        public static NoteDB notesDB;
        public static NoteDB notesDBForSearch;
        public static DoctorsDB doctorDB;
        public static string photopicker;
        static int i_for_change_profile=0;
        static int i_for_week = 0;
        static int i_for_datapicker = 0;
        DateTime dt;
        DateTime Foraddnewnote;
        object grid;
        public Calendari()
        {
            InitializeComponent();
            datefordatepicker = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            currdateTime = DateTime.Now;
            Currentmonthofview.Text = currdateTime.ToString("MMMM");
            notesDB = new NoteDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NotesDateBase.db3"));
            notesDBForSearch = new NoteDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notesDBForSearch.db3"));
            doctorDB = new DoctorsDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DoctorsDateBase.db3"));
            dt = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            Foraddnewnote = currdateTime.Date;
            int day = ((int)DateTime.Now.DayOfWeek + 6) % 7 + 1;
            ChangeBackColorToPurple("Grid" + day);
            grid = "Grid" + day;
            Changeweek(0);
            EntryName.IsReadOnly = true;
            EntryBirhtday.IsReadOnly = true;
            EntryBlood.IsReadOnly = true;
            EntryCity.IsReadOnly = true;
            EntryHeight.IsReadOnly = true;
            EntryWidht.IsReadOnly = true;
            YourPhoto.Source = "addphoto.png";
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "personaldatauser.xml")))
            {
                personaldatauser us = DeserializeXML();
                EntryName.Text = us.Name;
                EntryBlood.Text = us.Blood;
                EntryBirhtday.Text= us.DateBirthday;
                EntryHeight.Text= us.Height;
                EntryCity.Text = us.Location;
                EntryWidht.Text = us.Widnt;
                YourPhoto.Source = ImageSource.FromFile(us.Image);
            }
        }
        public void Changeweek(int swipe)
        {
            if(dt.AddDays(0 + i_for_week * 7).Month!=currdateTime.Month || dt.AddDays(1 + i_for_week * 7).Month != currdateTime.Month || dt.AddDays(2 + i_for_week * 7).Month != currdateTime.Month || dt.AddDays(3 + i_for_week * 7).Month != currdateTime.Month || dt.AddDays(4 + i_for_week * 7).Month != currdateTime.Month || dt.AddDays(5 + i_for_week * 7).Month != currdateTime.Month || dt.AddDays(6 + i_for_week * 7).Month != currdateTime.Month)
            {
                if(swipe==1)
                {
                    currdateTime = currdateTime.AddMonths(1);
                }
                else
                {
                    if (swipe == -1)
                    {
                        currdateTime = currdateTime.AddMonths(-1);
                    }
                }
                if (currdateTime.Year != DateTime.Now.Year)
                {
                    Currentmonthofview.Text = currdateTime.ToString("MMMM") + ", " + dt.Year;
                }
                else
                    Currentmonthofview.Text = currdateTime.ToString("MMMM");
            }
            DateforMonday.Text = dt.AddDays(0 + i_for_week * 7).ToString("dd");
            DateforTuesday.Text = dt.AddDays(1 + i_for_week * 7).ToString("dd");
            DateforWednesday.Text = dt.AddDays(2 + i_for_week * 7).ToString("dd");
            DateforThursday.Text = dt.AddDays(3 + i_for_week * 7).ToString("dd");
            DateforFriday.Text = dt.AddDays(4 + i_for_week * 7).ToString("dd");
            DateforSaturday.Text = dt.AddDays(5 + i_for_week * 7).ToString("dd");
            DateforSunday.Text = dt.AddDays(6 + i_for_week * 7).ToString("dd");
        }
        protected override async void OnAppearing()
        {

            collectionviewfordoctors.ItemsSource = await Calendari.doctorDB.GetNotes();
            collectionviewfornotes.ItemsSource = await Calendari.notesDB.GetNotes(Foraddnewnote);
            base.OnAppearing();
        }
        private void AddnewNote_Clicked(object sender, EventArgs e)
        {
           Navigation.PushPopupAsync(new Popupcalendar(Foraddnewnote, collectionviewfornotes, this));

        }
        private async void collectionviewfornotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null || e.PreviousSelection !=null)
            {
                Navigation.PushPopupAsync(new Popupcalendar((Note)e.CurrentSelection[0], collectionviewfornotes, Foraddnewnote));
            }
        }

        private void viewallcalendar_Clicked_1(object sender, EventArgs e)
        {
            datapicker.Focus();
        }
        private void ForgotPasswordCommand(object sender, EventArgs e)
        {
            if (grid != null)
            {
                switch (grid)
                {
                    case "DateforMonday":
                    case "Grid1":
                        Grid1.BackgroundColor = Color.Transparent;
                        break;
                    case "DateforTuesday":
                    case "Grid2":
                        Grid2.BackgroundColor = Color.Transparent;
                        break;
                    case "DateforWednesday":
                    case "Grid3":
                        Grid3.BackgroundColor = Color.Transparent;
                        break;
                    case "DateforThursday":
                    case "Grid4":
                        Grid4.BackgroundColor = Color.Transparent;
                        break;
                    case "DateforFriday":
                    case "Grid5":
                        Grid5.BackgroundColor = Color.Transparent;
                        break;
                    case "DateforSaturday":
                    case "Grid6":
                        Grid6.BackgroundColor = Color.Transparent;
                        break;
                    case "DateforSunday":
                    case "Grid7":
                        Grid7.BackgroundColor = Color.Transparent;
                        break;
                }
            }
            var param = ((TappedEventArgs)e).Parameter;
            grid = param.ToString();
            ChangeBackColorToPurple(param.ToString());
        }
        private async void ChangeBackColorToPurple(string param)
        {
            switch (param)
            {
                case "DateforMonday":
                case "Grid1":
                    Foraddnewnote = dt.AddDays(0 + i_for_week * 7).Date;
                    collectionviewfornotes.ItemsSource = await Calendari.notesDB.GetNotes(dt.AddDays(0 + i_for_week * 7).Date);
                    Grid1.BackgroundColor = Color.FromHex("#5E0DAC");
                    break;
                case "DateforTuesday":
                case "Grid2":
                    Foraddnewnote = dt.AddDays(1 + i_for_week * 7).Date;
                    collectionviewfornotes.ItemsSource = await Calendari.notesDB.GetNotes(dt.AddDays(1 + i_for_week * 7).Date);
                    Grid2.BackgroundColor = Color.FromHex("#5E0DAC");
                    break;
                case "DateforWednesday":
                case "Grid3":
                    Foraddnewnote = dt.AddDays(2 + i_for_week * 7).Date;
                    collectionviewfornotes.ItemsSource = await Calendari.notesDB.GetNotes(dt.AddDays(2 + i_for_week * 7).Date);
                    Grid3.BackgroundColor = Color.FromHex("#5E0DAC");
                    break;
                case "DateforThursday":
                case "Grid4":
                    Foraddnewnote = dt.AddDays(3 + i_for_week * 7).Date;
                    collectionviewfornotes.ItemsSource = await Calendari.notesDB.GetNotes(dt.AddDays(3 + i_for_week * 7).Date);
                    Grid4.BackgroundColor = Color.FromHex("#5E0DAC");
                    break;
                case "DateforFriday":
                case "Grid5":
                    Foraddnewnote = dt.AddDays(4 + i_for_week * 7).Date;
                    collectionviewfornotes.ItemsSource = await Calendari.notesDB.GetNotes(dt.AddDays(4 + i_for_week * 7).Date);
                    Grid5.BackgroundColor = Color.FromHex("#5E0DAC");
                    break;
                case "DateforSaturday":
                case "Grid6":
                    Foraddnewnote = dt.AddDays(5 + i_for_week * 7).Date;
                    collectionviewfornotes.ItemsSource = await Calendari.notesDB.GetNotes(dt.AddDays(5 + i_for_week * 7).Date);
                    Grid6.BackgroundColor = Color.FromHex("#5E0DAC");
                    break;
                case "DateforSunday":
                case "Grid7":
                    Foraddnewnote = dt.AddDays(6 + i_for_week * 7).Date;
                    collectionviewfornotes.ItemsSource = await Calendari.notesDB.GetNotes(dt.AddDays(6 + i_for_week * 7).Date);
                    Grid7.BackgroundColor = Color.FromHex("#5E0DAC");
                    break;
            }
        }
        void AllGridsTransparent()
        {
            Grid1.BackgroundColor = Color.Transparent;
            Grid2.BackgroundColor = Color.Transparent;
            Grid3.BackgroundColor = Color.Transparent;
            Grid4.BackgroundColor = Color.Transparent;
            Grid5.BackgroundColor = Color.Transparent;
            Grid6.BackgroundColor = Color.Transparent;
            Grid7.BackgroundColor = Color.Transparent;
        }
        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    i_for_week++;
                    Changeweek(1);
                    AllGridsTransparent();
                    break;
                case SwipeDirection.Right:
                    i_for_week--;
                    Changeweek(-1);
                    AllGridsTransparent();
                    break;
                case SwipeDirection.Up:
                    break;
                case SwipeDirection.Down:
                    break;
            }
        }
        private async void datapicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            collectionviewfornotes.ItemsSource = await Calendari.notesDB.GetNotes(e.NewDate);
            if (e.NewDate > DateTime.Now)
            {
                changetonewdatapickerdateselected(e.NewDate, true);
            }
            else
            {
                changetonewdatapickerdateselected(e.NewDate, false);
            }
        }
        private void changetonewdatapickerdateselected(DateTime e, bool d)
        {
            if (d == true)
            {
                while (datefordatepicker.AddDays(0 + i_for_datapicker * 7).Date != e.Date && datefordatepicker.AddDays(1 + i_for_datapicker * 7).Date != e.Date && datefordatepicker.AddDays(2 + i_for_datapicker * 7).Date != e.Date && datefordatepicker.AddDays(3 + i_for_datapicker * 7).Date != e.Date && datefordatepicker.AddDays(4 + i_for_datapicker * 7).Date != e.Date && datefordatepicker.AddDays(5 + i_for_datapicker * 7).Date != e.Date && datefordatepicker.AddDays(6 + i_for_datapicker * 7).Date != e.Date)
                {
                    i_for_datapicker++;
                }
                for(int i=0;i<i_for_datapicker;++i)
                {
                    Changeweek(1);
                }
          
            }
            else
            {
                while (datefordatepicker.AddDays(0 + i_for_datapicker * 7).Date != e.Date && datefordatepicker.AddDays(1 + i_for_datapicker * 7).Date != e.Date && datefordatepicker.AddDays(2 + i_for_datapicker * 7).Date != e.Date && datefordatepicker.AddDays(3 + i_for_datapicker * 7).Date != e.Date && datefordatepicker.AddDays(4 + i_for_datapicker * 7).Date != e.Date && datefordatepicker.AddDays(5 + i_for_datapicker * 7).Date != e.Date && datefordatepicker.AddDays(6 + i_for_datapicker * 7).Date != e.Date)
                {
                    i_for_datapicker--;
                }
                for (int i = 0; i < i_for_datapicker; ++i)
                {
                    Changeweek(-1);
                }
            }
            DateforMonday.Text = datefordatepicker.AddDays(0 + i_for_datapicker * 7).ToString("dd");
            DateforTuesday.Text = datefordatepicker.AddDays(1 + i_for_datapicker * 7).ToString("dd");
            DateforWednesday.Text = datefordatepicker.AddDays(2 + i_for_datapicker * 7).ToString("dd");
            DateforThursday.Text = datefordatepicker.AddDays(3 + i_for_datapicker * 7).ToString("dd");
            DateforFriday.Text = datefordatepicker.AddDays(4 + i_for_datapicker * 7).ToString("dd");
            DateforSaturday.Text = datefordatepicker.AddDays(5 + i_for_datapicker * 7).ToString("dd");
            DateforSunday.Text = datefordatepicker.AddDays(6 + i_for_datapicker * 7).ToString("dd");
            i_for_datapicker = 0;
            switch (e.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    AllGridsTransparent();
                    Grid1.BackgroundColor = Color.FromHex("#5E0DAC");
                    break;
                case DayOfWeek.Tuesday:
                    AllGridsTransparent();
                    Grid2.BackgroundColor = Color.FromHex("#5E0DAC");
                    break;
                case DayOfWeek.Wednesday:
                    AllGridsTransparent();
                    Grid3.BackgroundColor = Color.FromHex("#5E0DAC");
                    break;
                case DayOfWeek.Thursday:
                    AllGridsTransparent();
                    Grid4.BackgroundColor = Color.FromHex("#5E0DAC");
                    break;
                case DayOfWeek.Friday:
                    AllGridsTransparent();
                    Grid5.BackgroundColor = Color.FromHex("#5E0DAC");
                    break;
                case DayOfWeek.Saturday:
                    AllGridsTransparent();
                    Grid6.BackgroundColor = Color.FromHex("#5E0DAC");
                    break;
                case DayOfWeek.Sunday:
                    AllGridsTransparent();
                    Grid7.BackgroundColor = Color.FromHex("#5E0DAC");
                    break;
            }
        }
        private async void PickPhoto(object sender, EventArgs e)
        {

            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                YourPhoto.Source = ImageSource.FromFile(photo.FullPath);
                photopicker = photo.FullPath;
               
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }
      


        private async void Newdoctor(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new Popupdoctors(collectionviewfordoctors));
            collectionviewfordoctors.ItemsSource = await doctorDB.GetNotes();
        }

        private async void collectionviewfordoctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null || e.PreviousSelection != null)
            {
                Navigation.PushPopupAsync(new Popupdoctors((Doctor)e.CurrentSelection[0], collectionviewfordoctors));
                collectionviewfordoctors.ItemsSource = await doctorDB.GetNotes();
            }
        }

        private void Changeprofile(object sender, EventArgs e)
        {
            if (i_for_change_profile%2==0)
            {
                EntryName.IsReadOnly = false;
                EntryBirhtday.IsReadOnly = false;
                EntryBlood.IsReadOnly = false;
                EntryCity.IsReadOnly = false;
                EntryHeight.IsReadOnly = false;
                EntryWidht.IsReadOnly = false;
                EntryName.BackgroundColor = Color.FromHex("#f0f0f0"); 
                EntryBirhtday.BackgroundColor = Color.FromHex("#f0f0f0"); 
                EntryBlood.BackgroundColor = Color.FromHex("#f0f0f0"); 
                EntryCity.BackgroundColor = Color.FromHex("#f0f0f0"); 
                EntryHeight.BackgroundColor = Color.FromHex("#f0f0f0"); 
                EntryWidht.BackgroundColor = Color.FromHex("#f0f0f0");
                YourPhoto.IsEnabled = true;
                Changeprofilephoto.Source = "check.png";
            }
            else
            {
                EntryName.IsReadOnly = true;
                EntryBirhtday.IsReadOnly = true;
                EntryBlood.IsReadOnly = true;
                EntryCity.IsReadOnly = true;
                EntryHeight.IsReadOnly = true;
                EntryWidht.IsReadOnly = true;
                YourPhoto.IsEnabled = false;
                Changeprofilephoto.Source = "note.png";
                EntryName.BackgroundColor = Color.FromHex("#fff");
                EntryBirhtday.BackgroundColor = Color.FromHex("#fff");
                EntryBlood.BackgroundColor = Color.FromHex("#fff");
                EntryCity.BackgroundColor = Color.FromHex("#fff");
                EntryHeight.BackgroundColor = Color.FromHex("#fff");
                EntryWidht.BackgroundColor = Color.FromHex("#fff");
                personaldatauser user = new personaldatauser();
                user.Name = EntryName.Text;
                user.Blood = EntryBlood.Text;
                user.DateBirthday = EntryBirhtday.Text;
                user.Height = EntryHeight.Text;
                user.Location = EntryCity.Text;
                user.Widnt = EntryWidht.Text;
                user.Image = photopicker;
                SerializeXML(user);
            }
            i_for_change_profile++;

        }

        public void SerializeXML(personaldatauser user)
        {
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "personaldatauser.xml")))
            {
                File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "personaldatauser.xml"));
            }
            XmlSerializer xml = new XmlSerializer(typeof(personaldatauser));
            using (FileStream fs = new FileStream(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "personaldatauser.xml"), FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, user);
            }
         
        }
        public personaldatauser DeserializeXML()
        {
            XmlSerializer xml = new XmlSerializer(typeof(personaldatauser));
            using (FileStream fs = new FileStream(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "personaldatauser.xml"), FileMode.OpenOrCreate))
            {
                return (personaldatauser)xml.Deserialize(fs);
            }
        }

        private void Help_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Help());
        }

        private void pillssearch_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new pillssearch());
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }

        private void Id_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new IdishkaPage());
        }
    }
}
