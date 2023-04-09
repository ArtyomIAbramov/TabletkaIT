using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using tabletka2.Models;
namespace tabletka2.Data
{
    public class NoteDB
    {
        readonly SQLiteAsyncConnection db;
        public NoteDB(string connectionstring)
        {
            db = new SQLiteAsyncConnection(connectionstring);
            db.CreateTableAsync<Note>().Wait();
        }
        public void ClearNotesoutofdate(DateTime dateTime)
        {
            int count = db.Table<Note>().Where(i => i.dateTakeTime < dateTime).ToArrayAsync().Result.Length;
            if(count!=0)
            {
                for (int i = 0; i < count; ++i)
                {
                    Delete(db.Table<Note>().Where(g => g.dateTakeTime < dateTime).FirstAsync().Result);
                }
            }
        }
        public Task<List<Note>> GetNotes(DateTime dateTime)
        {
            return db.Table<Note>().Where(i => i.dateTakeTime == dateTime).ToListAsync();
        }
        public Task<List<Note>> GetNotes()
        {
            return db.Table<Note>().ToListAsync();
        }
        public void deleteGetNotescurr(string name)
        {
            int count = db.Table<Note>().Where(i => i.Name_of_pill == name).ToArrayAsync().Result.Length;
            if (count != 0)
            {
                for (int i = 0; i < count; ++i)
                {
                    Delete(db.Table<Note>().Where(g => g.Name_of_pill == name).FirstAsync().Result);

                }
            }
        }
        public Task<int> AddnewNote(Note note)
        {
            if (note.ID != 0)
                return db.UpdateAsync(note);
            else
                return db.InsertAsync(note);
        }
        public Task<int> Delete(Note note)
        {
           return db.DeleteAsync(note);
        }
    }
}
