using System;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using EducaMais.Models;

namespace EducaMais.Services
{
    public class LocalDbService
    {
        private static LocalDbService? _instance;
        public static LocalDbService Instance => _instance ??= new LocalDbService();

        private readonly SQLiteAsyncConnection _db;

        private LocalDbService()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbPath = Path.Combine(folder, "educamais_local.db3");
            _db = new SQLiteAsyncConnection(dbPath);

            // Create tables if they don't exist
            _db.CreateTableAsync<SchoolClass>().Wait();
            _db.CreateTableAsync<Student>().Wait();
            _db.CreateTableAsync<Attendance>().Wait();
            _db.CreateTableAsync<Grade>().Wait();
            _db.CreateTableAsync<LessonPlan>().Wait();
        }

        
        public async Task ClearDatabaseAsync()
        {
            await _db.DeleteAllAsync<SchoolClass>();
            await _db.DeleteAllAsync<Student>();
            await _db.DeleteAllAsync<Attendance>();
            await _db.DeleteAllAsync<Grade>();
            await _db.DeleteAllAsync<LessonPlan>();
        }

        public SQLiteAsyncConnection GetConnection() => _db;
    }
}
