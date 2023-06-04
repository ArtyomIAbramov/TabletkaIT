using Plugin.Clipboard;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using tabletka2.Data;
using tabletka2.Models;
using tabletka2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tabletka2.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IdishkaPage : Rg.Plugins.Popup.Pages.PopupPage
	{
		public IdishkaPage ()
		{
			InitializeComponent ();
			idishka.Text = DeserializeXML().ID;
		}
		private async void idishka_Clicked(object sender, EventArgs e)
        {
			CrossClipboard.Current.SetText(idishka.Text);
			idishka.ScaleTo(0.9, 125);
			await idishka.TranslateTo(0, -5, 125);
			idishka.ScaleTo(1, 125);
			await idishka.TranslateTo(0, 5, 125);
		}
		private async void idishka1_Clicked(object sender, EventArgs e)
		{
			CrossClipboard.Current.SetText(idishka.Text);
			idishka1.ScaleTo(0.9, 125);
			await idishka1.TranslateTo(0, -5, 125);
			idishka1.ScaleTo(1, 125);
			await idishka1.TranslateTo(0, 5, 125);
		}
		public User DeserializeXML()
		{
			XmlSerializer xml = new XmlSerializer(typeof(User));
            using (FileStream fs = new FileStream(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User.xml"), FileMode.OpenOrCreate))
			{
				return (User)xml.Deserialize(fs);
			}
		}
	}
}