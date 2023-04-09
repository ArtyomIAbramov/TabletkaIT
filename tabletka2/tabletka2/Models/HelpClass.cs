using System;
using System.Collections.Generic;
using System.Text;

namespace tabletka2.Models
{
    public class HelpClass
    {
        public string Name { get; set; }
        public string photo { get; set; }
        public HelpClass()
        {
        }
        public HelpClass(string Name, string photo)
        {
            this.Name = Name;
            this.photo = photo;
        }
    }
    
}
