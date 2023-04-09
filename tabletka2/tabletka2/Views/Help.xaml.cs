﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tabletka2.Models;

namespace tabletka2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Help : ContentPage
    {

        public string SomeImage;
        public List<HelpClass> observableCollection;

        public Help()
        {
            InitializeComponent();
            observableCollection = new List<HelpClass>();
            if (observableCollection.Count != 0)
            { observableCollection.Clear(); }
            observableCollection.Add(new HelpClass("Как это работает?", "https://cdn-icons-png.flaticon.com/512/471/471664.png"));
            observableCollection.Add(new HelpClass("Как добавить напоминание о приёме лекарства?", "pluspill.png"));
            observableCollection.Add(new HelpClass("Для кого приложение?", "https://cdn-icons-png.flaticon.com/512/860/860800.png"));
            observableCollection.Add(new HelpClass("Как добавить напоминание о посещении врача?", "plusdoctor.png"));
            observableCollection.Add(new HelpClass("Как выбрать дату? Перелистывание недели?", "https://cdn-icons-png.flaticon.com/512/2838/2838779.png"));
            observableCollection.Add(new HelpClass("Редактирование профиля", "note.png"));
            observableCollection.Add(new HelpClass("Как выбрать время при добавлении?", "timer.png"));
            observableCollection.Add(new HelpClass("Поиск лекарств", "search.png"));
            observableCollection.Add(new HelpClass("Как выбрать дату при добавлении?", "calendar.png"));
            collectionforkelp.ItemsSource = observableCollection;
        }

        private void forwho_Clicked(object sender, EventArgs e)
        {
            switch ((sender as Button).Text)
            {
                case "Как это работает?":
                    (sender as Button).Text = "Главная задача приложения - оповещения людей, принимающий лекарства на постоянной основе, о необходимости приёма препарата в указанное время через оповещение. Помимо этого есть и другие функции:\n Оповещение о посещении врача в указанный день\n Важная информация о своём организме\n Быстрый поиск назначенных лекарств";
                    break;
                case "Для кого приложение?":
                    (sender as Button).Text = "Для людей, принимающий лекарства на постоянной основе";
                    break;
                case "Как добавить напоминание о приёме лекарства?":
                    (sender as Button).Text = "Для добавление намоминания о приёме лекарства необходимо нажать на картинку, как у этого пункта, а затем заполнить данные";
                    break;
                case "Как добавить напоминание о посещении врача?":
                    (sender as Button).Text = "Для добавление намоминания о посещении врача необходимо нажать на картинку, как у этого пункта, а затем заполнить данные";
                    break;
                case "Как выбрать дату? Перелистывание недели?":
                    (sender as Button).Text = "Чтобы выбрать конкретную дату надо нажать на название месяц и выбрать подходящую дату\nДля перелистывания недели достаточно свайпнуть панель с числами недели (вправо - предыдущая, влево - следующая)";
                    break;
                case "Как выбрать время при добавлении?":
                    (sender as Button).Text = "Для выбора времени надо нажать на картинку часов, таких как в этом пункте";
                    break;
                case "Как выбрать дату при добавлении?":
                    (sender as Button).Text = "Для выбора даты надо нажать на картинку календаря, такого как в этом пункте";
                    break;
                case "Редактирование профиля":
                    (sender as Button).Text = "Для редактирование профиля требуется в окне профиля нажать на картинку заметок слево внизу, затем внести необходимые изменения и снова нажать на галочку в этом же месте для сохранения";
                    break;
                case "Поиск лекарств":
                    (sender as Button).Text = "Для поиска лекарств нажмите на картинку как в этом пункте, вам станет виден список назначенных вам лекарств, затем начните вводить название лекарства в поле. Выбрав и нажав на него вы перейдёте на сайт продавца";
                    break;



                case "Главная задача приложения - оповещения людей, принимающий лекарства на постоянной основе, о необходимости приёма препарата в указанное время через оповещение. Помимо этого есть и другие функции:\n Оповещение о посещении врача в указанный день\n Важная информация о своём организме\n Быстрый поиск назначенных лекарств":
                    (sender as Button).Text = "Как это работает?";
                    break;
                case "Для людей, принимающий лекарства на постоянной основе":
                    (sender as Button).Text = "Для кого приложение?";
                    break;
                case "Для добавление намоминания о приёме лекарства необходимо нажать на картинку, как у этого пункта, а затем заполнить данные":
                    (sender as Button).Text = "Как добавить напоминание о приёме лекарства?";
                    break;
                case "Для добавление намоминания о посещении врача необходимо нажать на картинку, как у этого пункта, а затем заполнить данные":
                    (sender as Button).Text = "Как добавить напоминание о посещении врача?";
                    break;
                case "Чтобы выбрать конкретную дату надо нажать на название месяц и выбрать подходящую дату\nДля перелистывания недели достаточно свайпнуть панель с числами недели (вправо - предыдущая, влево - следующая)":
                    (sender as Button).Text = "Как выбрать дату? Перелистывание недели?";
                    break;
                case "Для выбора времени надо нажать на картинку часов, таких как в этом пункте":
                    (sender as Button).Text = "Как выбрать время при добавлении?";
                    break;
                case "Для выбора даты надо нажать на картинку календаря, такого как в этом пункте":
                    (sender as Button).Text = "Как выбрать дату при добавлении?";
                    break;
                case "Для редактирование профиля требуется в окне профиля нажать на картинку заметок слево внизу, затем внести необходимые изменения и снова нажать на галочку в этом же месте для сохранения":
                    (sender as Button).Text = "Редактирование профиля";
                    break;
                case "Для поиска лекарств нажмите на картинку как в этом пункте, вам станет виден список назначенных вам лекарств, затем начните вводить название лекарства в поле. Выбрав и нажав на него вы перейдёте на сайт продавца":
                    (sender as Button).Text = "Поиск лекарств";
                    break;
            }

        }
    }
}