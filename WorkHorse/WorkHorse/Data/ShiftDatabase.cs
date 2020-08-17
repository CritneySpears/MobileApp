using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using WorkHorse.Models;

namespace WorkHorse.Data
{
    public class ShiftDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public ShiftDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ShiftInstance>().Wait();
            _database.CreateTableAsync<State>().Wait();
        }

        public Task<State> GetState()
        {
            return _database.Table<State>().FirstOrDefaultAsync();
        }

        public Task<int> SetState(State state)
        {
            if (state.ClockState != null)
            {
                return _database.UpdateAsync(state);
            }
            else
            {
                return _database.InsertAsync(state);
            }
        }

        public Task<List<ShiftInstance>> GetShiftsAsync()
        {
            return _database.Table<ShiftInstance>().ToListAsync();
        }

        public Task<ShiftInstance> GetShiftAsync(int id)
        {
            return _database.Table<ShiftInstance>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<ShiftInstance> GetLastShiftAsync()
        {
            return _database.Table<ShiftInstance>()
                            .OrderByDescending(i => i.ID)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveShiftAsync(ShiftInstance instance)
        {
            if (instance.ID != 0)
            {
                return _database.UpdateAsync(instance);
            }
            else
            {
                return _database.InsertAsync(instance);
            }
        }

        public Task<int> DeleteShiftAsync(ShiftInstance instance)
        {
            return _database.DeleteAsync(instance);
        }

        public Task WipeDatabase()
        {
            _database.DeleteAllAsync<ShiftInstance>();
            return _database.DeleteAllAsync<State>();
        }
    }
}
