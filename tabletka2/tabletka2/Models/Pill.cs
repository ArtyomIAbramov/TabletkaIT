using System;
using SQLite;

namespace tabletka2.Models
{
    [Table("Pills")]
    public class Pill
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }
        public string Name_of_pill_in_search { get; set; }
        public string Price { get; set; }
        public string Dose { get; set; }
        public string Description_in_search { get; set; }
        public string pharmacy_sity { get; set; }
        public string Photo { get; set; }
        public string Photo_apteki { get; set; }
    }
}
