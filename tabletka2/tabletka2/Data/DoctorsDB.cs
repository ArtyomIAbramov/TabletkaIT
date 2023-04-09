using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using tabletka2.Models;
namespace tabletka2.Data
{
    public class DoctorsDB
    {
        readonly SQLiteAsyncConnection db;
        public DoctorsDB(string connectionstring)
        {
            db = new SQLiteAsyncConnection(connectionstring);
            db.CreateTableAsync<Doctor>().Wait();
        }
        public Task<List<Doctor>> GetNotes()
        {
            return db.Table<Doctor>().ToListAsync();
        }
        public Task<int> AddnewNote(Doctor doc)
        {
            if (doc.ID != 0)
                return db.UpdateAsync(doc);
            else
                return db.InsertAsync(doc);
        }
        public Task<int> Delete(Doctor doc)
        {
            return db.DeleteAsync(doc);
        }
    }
}
