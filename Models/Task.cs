using System;

namespace EFCore_ConsoleApp.Models
{
    public class Task 
    {
        public long TaskId { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }
        public DateTime DueDate { get; set; }
        public long TaskPriorityId { get; set; }
        public TaskPriority TaskPriority { get; set; }
    }
}