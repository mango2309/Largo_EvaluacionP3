using ExamenProgreso3_SebastianLargo.Models_SL;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenProgreso3_SebastianLargo.Services
{
    public class DatabaseService_SL
    {
        private SQLiteAsyncConnection _database;

        public DatabaseService_SL()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "narutoXX.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<NarutoCharacter_SL>().Wait();
        }
        public async Task<List<NarutoCharacter_SL>> GetCharactersAsync()
        {
            return await _database.Table<NarutoCharacter_SL>().ToListAsync();
        }
        public async Task SaveCharacterAsync(NarutoCharacter_SL character)
        {
            await _database.InsertAsync(character);
        }
    }
}
