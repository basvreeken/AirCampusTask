using System;
using SQLite;

namespace AirCampusTask.Models
{
    public class LearningTask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public string Category { get; set; }
        public string Title { get; set; }
        
        public string Instruction { get; set; }
        
        public DateTime Started { get; set; }
        
        public DateTime Finished { get; set; }
        
        public int Rating { get; set; }
        
        public string Action { get; set; }
        
        public string Reaction { get; set; }
        public bool Synced { get; set; }
        public DateTime Due { get; set; }
        
    }
}