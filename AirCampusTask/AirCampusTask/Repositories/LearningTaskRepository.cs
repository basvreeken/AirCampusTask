using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AirCampusTask.Models;
using SQLite;

namespace AirCampusTask.Repositories
{
    public class LearningTaskRepository : ILearningTaskRepository
    {
        private SQLiteAsyncConnection _connection;
        
        public event EventHandler<LearningTask> OnLearningTaskAdded;
        public event EventHandler<LearningTask> OnLearningTaskUpdated;

        private async Task CreateConnection()
        {
            if (_connection != null)
            {
                return;
            }

            // todo: Use the special data folder.
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var databasePath = Path.Combine(documentPath, "LearningTasks.db");

            _connection = new SQLiteAsyncConnection(databasePath);
            await _connection.CreateTableAsync<LearningTask>();

            if (await _connection.Table<LearningTask>().CountAsync() == 0)
            {
                await _connection.InsertAsync(new LearningTask()
                {
                    Title = "The quick brown fox jumps over the lazy dog.",
                    Due = DateTime.Now
                });
            }
        }
        
        public async Task<List<LearningTask>> GetLearningTasks()
        {
            await CreateConnection();
            return await _connection.Table<LearningTask>().ToListAsync();
        }

        public async Task AddLearningTask(LearningTask learningTask)
        {
            await CreateConnection();
            await _connection.InsertAsync(learningTask);
            OnLearningTaskAdded?.Invoke(this, learningTask);
        }

        public async Task UpdateLearningTask(LearningTask learningTask)
        {
            await CreateConnection();
            await _connection.UpdateAsync(learningTask);
            OnLearningTaskUpdated?.Invoke(this, learningTask);
        }

        public async Task AddOrUpdate(LearningTask learningTask)
        {
            if (learningTask.Id == 0)
            {
                await AddLearningTask(learningTask);
            }
            else
            {
                await UpdateLearningTask(learningTask);
            }
        }
        
    }
}