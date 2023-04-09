using System;
using SQLite;
namespace tabletka2.Models
{
    public class Doctor
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name_of_doctor { get; set; }
        public string Meeting_day { get; set; }
        public string Meeting_time { get; set; }
        public Doctor Clone()
        {
            return new Doctor(ID,Name_of_doctor, Meeting_day, Meeting_time);
        }
        public Doctor() { }
        public Doctor(int ID, string Name_of_doctor,string Meeting_day,string Meeting_time)
        {
            this.ID = ID;
            this.Name_of_doctor = Name_of_doctor;
            this.Meeting_day = Meeting_day;
            this.Meeting_time = Meeting_time;
        }
    }
}
