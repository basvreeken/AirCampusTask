using System;
using SQLite;

namespace AirCampusTask.Models
{
    public class LearningTask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
        public DateTime Due { get; set; }
        
    }
}