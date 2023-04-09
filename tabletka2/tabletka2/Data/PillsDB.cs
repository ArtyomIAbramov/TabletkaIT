using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using tabletka2.Models;
namespace tabletka2.Data
{
    public class PillsDB
    {
        public readonly SQLiteAsyncConnection db;
        public PillsDB(string connectionstring)
        {
            db = new SQLiteAsyncConnection(connectionstring);
            db.CreateTableAsync<Pill>().Wait();
        }
        public Task<List<Pill>> GetNotes()
        {
            return db.Table<Pill>().ToListAsync();
        }
        public Task<int> AddnewNote(Pill pil)
        {
            if (pil.ID != 0)
                return db.UpdateAsync(pil);
            else
                return db.InsertAsync(pil);
        }
        public Task<int> Delete(Pill pil)
        {
            return db.DeleteAsync(pil);
        }
    }
}
