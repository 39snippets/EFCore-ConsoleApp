using EFCore_ConsoleApp.Models;
using EFCore_ConsoleApp.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EFCore_ConsoleApp.Controllers
{
    public class Controller
    {
        private readonly TodoDbContext _todoContext;

        public Controller()
        {
            _todoContext = new TodoDbContext();
        }

        public IList<TaskPriority> GetTaskPriorities()
        {
            return _todoContext.TaskPriorities.ToList();
        }

        public TaskPriority GetTaskPriority(long taskPriorityId)
        {
            return _todoContext.TaskPriorities.Find(taskPriorityId);
        }

        public void AddTaskPriority(TaskPriority taskPriority)
        {
            _todoContext.TaskPriorities.Add(taskPriority);
            _todoContext.SaveChanges();
        }

        public void UpdateTaskPriority(TaskPriority taskPriority)
        {
            _todoContext.TaskPriorities.Update(taskPriority);
            _todoContext.SaveChanges();
        }

        public void DeleteTaskPriority(long taskPriorityId)
        {
            var taskPriority = _todoContext.TaskPriorities.Find(taskPriorityId);

            if (taskPriority == null)
                throw new KeyNotFoundException($"Task priority with id {taskPriorityId} could not be found");

            _todoContext.TaskPriorities.Remove(taskPriority);
            _todoContext.SaveChanges();
        }

        public IList<Task> GetTasks()
        {
            return _todoContext.Tasks.Include(t => t.TaskPriority).ToList();
        }

        public Task GetTask(long taskId)
        {
            return _todoContext.Tasks.Find(taskId);
        }

        public void AddTask(Task task)
        {
            _todoContext.Tasks.Add(task);
            _todoContext.SaveChanges();
        }

        public void UpdateTask(Task task)
        {
            _todoContext.Tasks.Update(task);
            _todoContext.SaveChanges();
        }

        public void DeleteTask(long taskId)
        {
            var task = _todoContext.Tasks.Find(taskId);

            if (task == null)
                throw new KeyNotFoundException($"Task with id {taskId} could not be found");

            _todoContext.Tasks.Remove(task);
            _todoContext.SaveChanges();
        }
    }
}