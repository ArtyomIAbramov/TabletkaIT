using Android.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using tabletka2.ViewModels;
using Xamarin.Forms;

namespace tabletka2.Models
{
    public class personaldatauser
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string DateBirthday { get; set; }
        public string Height { get; set; }
        public string Widnt { get; set; }
        public string Blood { get; set; }
        public string Image { get; set; }
    }
}
