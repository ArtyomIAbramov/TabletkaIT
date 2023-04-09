using System;
using SQLite;

namespace tabletka2.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name_of_pill { get; set; }
        public string Measure { get; set; }
        public string Description { get; set; }
        public DateTime dateTakeTime { get; set; }
        public string dateTakeTime1{ get; set; }
        public string days { get; set; }
    }
}
