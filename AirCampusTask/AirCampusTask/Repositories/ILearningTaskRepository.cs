using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirCampusTask.Models;

namespace AirCampusTask.Repositories
{
    public interface ILearningTaskRepository
    {
        event EventHandler<LearningTask> OnLearningTaskAdded;
        event EventHandler<LearningTask> OnLearningTaskUpdated;
        
        Task<List<LearningTask>> GetLearningTasks();
        Task AddLearningTask(LearningTask learningTask);
        Task UpdateLearningTask(LearningTask learningTask);
        Task AddOrUpdate(LearningTask learningTask);
        
    }
}