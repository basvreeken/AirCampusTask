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
                    Title = "Welcome to Priming & Staging",
                    Category = "",
                    Instruction =
                        "Work your way through the tasks from top to the bottom. After finishing your task, a few questions will be asked and your tasked is synced to the AirCampus server. If you have no internet connection you can resync tour task by clicking on it. ",
                    Due = DateTime.Now
                });

                List<LearningTask> ttnTasks = new List<LearningTask>();
                ttnTasks.Add(new LearningTask()
                    {Category = "Maak CIS en IV Planning", Title = "Installeer het MWS.", Instruction = ""});
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Maak CIS en IV Planning",
                    Title = "Installeer de CIS Planner en de IV Planner op het MWS.",
                    Instruction =
                        "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.1/5.2"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Maak CIS en IV Planning",
                    Title = "Maak een CIS Plan en een IV Plan en initialiseer deze.",
                    Instruction = "Lees de stappen in het document CIS Planner SIM.pdf/IV Planner SIM.pdf"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Maak CIS en IV Planning", Title = "Voer de Cold Steel Test uit.",
                    Instruction =
                        "Volgens het document TITAAN 4.3 - Cold Steel Test - Server Container C3-A4 en MB10KN (SCE Type 1).pdf"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Installeer het netwerkdeel", Title = "Installeer UNI op het MWS.",
                    Instruction = "Lees de stappen in het document TITAAN 4.2 SR04 - UNI 2.3 - SIM.pdf"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Installeer het netwerkdeel", Title = "Gebruik UNI en het CIS Plan.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - UNI - SUM.pdf"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Installeer het netwerkdeel",
                    Title = "Installeer, configureer en controleer de Red Router en de L3-switch.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - UNI - SUM.pdf"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Installeer het netwerkdeel",
                    Title = "Installeer, configureer en controleer de overige netwerk componenten (SCE en de boxen).",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - UNI - SUM.pdf"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Installeer storage", Title = "Algemene informatie",
                    Instruction =
                        "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.3"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Installeer storage",
                    Title = "Start de TITAAN Storage Array Configuration Utility (SACU) op het MWS.",
                    Instruction =
                        "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.3.1"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Installeer storage",
                    Title = "Configureer de HP MSA met de TITAAN SACU en het CIS Plan.",
                    Instruction =
                        "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.3.1"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Installeer storage", Title = "Installeer de firmware-updates opde HP MSA.",
                    Instruction =
                        "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.3.2"
                });
                //ttnTasks.Add(new LearningTask() {Category = "", Title = "", Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf"});

                await _connection.InsertAllAsync(ttnTasks);
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