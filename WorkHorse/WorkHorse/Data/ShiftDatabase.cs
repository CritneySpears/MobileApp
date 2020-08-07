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
            _database.CreateTableAsync<ClockInstance>().Wait();
        }

        public Task<List<ClockInstance>> GetShiftsAsync()
        {
            return _database.Table<ClockInstance>().ToListAsync();
        }

        public Task<ClockInstance> GetShiftAsync(int id)
        {
            return _database.Table<ClockInstance>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveShiftAsync(ClockInstance instance)
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

        public Task<int> DeleteShiftAsync(ClockInstance instance)
        {
            return _database.DeleteAsync(instance);
        }

        public Task WipeDatabase() => _database.DeleteAllAsync<ClockInstance>();

        public Task<List<DateTime>> GetShiftStartTimes()
        {
            return _database.QueryAsync<DateTime>("SELECT [Time] FROM [ClockInstance] WHERE [ClockString] == 'Shift Started'");
        }

        public Task<List<DateTime>> GetShiftEndTimes()
        {
            return _database.QueryAsync<DateTime>("SELECT [Time] FROM [ClockInstance] WHERE [ClockString] == 'Shift Ended'");
        }
    }
}
