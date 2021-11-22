using System;
using System.Collections.Generic;
using EFCore_ConsoleApp.Controllers;
using EFCore_ConsoleApp.Models;

namespace EFCore_ConsoleApp
{
    public class UserInterface
    {
        private readonly Controller _controller = new Controller();

        public void Run()
        {
            int i = 0;
            while (i != 9)
            {
                i = GetChoiceFromMenu();

                switch (i)
                {
                    case 1:
                        listTaskPriorities();
                        break;

                    case 2:
                        AddTaskPriority();
                        break;

                    case 3:
                        UpdateTaskPriority();
                        break;

                    case 4:
                        DeleteTaskPriority();
                        break;

                    case 5:
                        ListTasks();
                        break;

                    case 6:
                        AddTask();
                        break;

                    case 7:
                        UpdateTask();
                        break;

                    case 8:
                        DeleteTask();
                        break;

                    case 9:
                        break;

                    default:
                        Console.WriteLine("\nChoice not recognized");
                        break;
                }
            }
        }

        private int GetChoiceFromMenu()
        {
            Console.WriteLine("\nMENU");
            Console.WriteLine(new String('-', 43));
            Console.WriteLine("1. List task priorities      5. List tasks       9. Exit");
            Console.WriteLine("2. Add task priority         6. Add task");
            Console.WriteLine("3. Update task priority      7. Update task");
            Console.WriteLine("4. Delete task priority      8. Delete task");
            Console.Write("\nEnter number of your choice: ");
            var input = Console.ReadLine();

            int choice;
            if (int.TryParse(input, out choice))
            {
                if (choice < 10 && choice > 0)
                    return choice;
            }

            Console.WriteLine("Option not valid. Please try again");
            return 0;
        }

        private void listTaskPriorities()
        {
            // Print command title
            Console.WriteLine("\n* List of task priorities *");
            Console.WriteLine($"\n{"Id",-4} {"Priority name",-20}");
            Console.WriteLine(new string('-', 25));

            // List task priorities
            IList<TaskPriority> list = _controller.GetTaskPriorities();
            foreach (var item in list)
            {
                Console.WriteLine($"{item.TaskPriorityId,-4} {item.Name,-20}");
            }
        }

        private void AddTaskPriority()
        {
            // Print command title and request a new name for the task priority.
            Console.WriteLine("\n* New task priority *");
            Console.Write("\nTask priority name? ");
            string name = Console.ReadLine();

            // Validate name and cancel if it is an empty string.
            if (name == string.Empty)
            {
                Console.WriteLine("WARNING: A name for the task priority must be provided. Operation cancelled.");
                return;
            }

            // Validation was ok, save the data.
            var taskPriority = new TaskPriority() { Name = name };
            _controller.AddTaskPriority(taskPriority);

            Console.WriteLine($"Task priority with id {taskPriority.TaskPriorityId} successfully added");
        }

        private void UpdateTaskPriority()
        {
            // Print command title and request the task priority ID.
            Console.WriteLine("\n* Update task priority *");
            Console.Write("\nTask priority id? ");
            string buffer = Console.ReadLine();

            // Validate if the ID is a long integer.
            long id;
            if (!long.TryParse(buffer, out id))
            {
                Console.WriteLine("WARNING: A valid ID for the task priority must be provided. Operation cancelled.");
                return;
            }

            // Validate if such data exists in the database.
            var taskPriority = _controller.GetTaskPriority(id);
            if (taskPriority == null)
            {
                Console.WriteLine($"WARNING: Task priority with ID {id} not found. Operation cancelled.");
                return;
            }

            // Show current data for task priority.
            Console.WriteLine("\nCurrent task priority: ");
            Console.WriteLine($"\n{"Id",-4} {"Priority name",-20}");
            Console.WriteLine(new string('-', 25));
            Console.WriteLine($"{taskPriority.TaskPriorityId,-4} {taskPriority.Name,-20}");

            // Request new name for task priority.
            Console.Write("\nNew task priority name? ");
            string name = Console.ReadLine();

            // Validate for the new name.
            if (name == string.Empty)
            {
                Console.WriteLine("A name for the task priority must be provided. Operation cancelled.");
                return;
            }

            // Validation was ok, save the data.
            taskPriority.Name = name;
            _controller.UpdateTaskPriority(taskPriority);

            Console.WriteLine($"Task priority with id {taskPriority.TaskPriorityId} successfully updated");
        }

        private void DeleteTaskPriority()
        {
            // Print command title and request the task priority ID.
            Console.WriteLine("\n* Delete task priority *");
            Console.Write("\nTask priority id? ");
            string buffer = Console.ReadLine();

            // Validate the input ID
            long id;
            if (!long.TryParse(buffer, out id))
            {
                Console.WriteLine("WARNING: A valid ID for the task priority must be provided. Operation cancelled.");
                return;
            }

            // Validations were ok, delete the data.
            try
            {
                _controller.DeleteTaskPriority(id);
                Console.WriteLine($"Task priority with id {id} successfully deleted");
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine($"WARNING: {e.Message}. Operation cancelled.");
            }
        }

        private void ListTasks()
        {
            // Print command title
            Console.WriteLine("\n* List of tasks *");
            Console.WriteLine($"\n{"Id",-4} {"Task name",-20} {"Done",-5} {"Due date",-20} {"Priority",-24}");
            Console.WriteLine(new string('-', 77));

            // List tasks    
            IList<Task> list = _controller.GetTasks();
            foreach (var item in list)
            {
                Console.WriteLine($"{item.TaskId,-4} {item.Name,-20} {item.Done,-5} {item.DueDate,-20} ({item.TaskPriority.TaskPriorityId}){item.TaskPriority.Name,-20}");
            }
        }

        private void AddTask()
        {
            // Print command title and ask for new data for the task priority.
            Console.WriteLine("\n* New task *");
            Console.Write("\nTask name? ");
            string name = Console.ReadLine();

            // Validate name and cancel if it is an empty string.
            if (name == string.Empty)
            {
                Console.WriteLine("WARNING: A name for the task must be provided. Operation cancelled.");
                return;
            }

            // List task priorities and request for an ID.
            Console.WriteLine("\nList of task priorities:");
            Console.WriteLine($"\n{"Id",-4} {"Priority name",-20}");
            Console.WriteLine(new string('-', 25));

            IList<TaskPriority> listPriorities = _controller.GetTaskPriorities();
            foreach (var item in listPriorities)
            {
                Console.WriteLine($"{item.TaskPriorityId,-4} {item.Name,-20}");
            }

            Console.Write("\nTask priority ID? ");
            string priorityIdString = Console.ReadLine();

            // Validate id and cancel if it is an empty string.            
            long priorityId;
            if (!long.TryParse(priorityIdString, out priorityId))
            {
                Console.WriteLine("WARNING: A valid priority ID for the task must be provided. Operation cancelled.");
                return;
            }

            // Validations were ok, save the data.
            var task = new Task() { Name = name, TaskPriorityId = priorityId };
            _controller.AddTask(task);

            Console.WriteLine($"Task with id {task.TaskId} successfully added.");
        }

        private void UpdateTask()
        {
            // Print command title and request the task ID.
            Console.WriteLine("\n* Update task *");
            Console.Write("\nTask id? ");
            string buffer = Console.ReadLine();

            long id;
            if (!long.TryParse(buffer, out id))
            {
                Console.WriteLine("WARNING: A valid ID for the task must be provided. Operation cancelled.");
                return;
            }

            var task = _controller.GetTask(id);
            if (task == null)
            {
                Console.WriteLine($"WARNING: Task with ID {id} not found. Operation cancelled.");
                return;
            }

            Console.WriteLine("\nCurrent task:");
            Console.WriteLine($"\n{"Id",-4} {"Task name",-20} {"Done",-5} {"Due date",-20} {"Priority",-24}");
            Console.WriteLine(new string('-', 77));
            Console.WriteLine($"{task.TaskId,-4} {task.Name,-20} {task.Done,-5} {task.DueDate,-20} ({task.TaskPriority.TaskPriorityId}){task.TaskPriority.Name,-20}");

            Console.Write("\nNew task name? ");
            string name = Console.ReadLine();

            if (name == string.Empty)
            {
                Console.WriteLine("A name for the task must be provided. Operation cancelled.");
                return;
            }

            // Validations were ok, save the data.
            task.Name = name;
            _controller.UpdateTask(task);

            Console.WriteLine($"Task with id {task.TaskId} successfully updated.");
        }

        private void DeleteTask()
        {
            // Print command title and request the task ID.
            Console.WriteLine("\n* Delete task *");
            Console.Write("\nTask id? ");
            string buffer = Console.ReadLine();

            long id;
            if (!long.TryParse(buffer, out id))
            {
                Console.WriteLine("WARNING: A valid ID for the task must be provided. Operation cancelled.");
                return;
            }

            var task = _controller.GetTask(id);
            if (task == null)
            {
                Console.WriteLine($"WARNING: Task with ID {id} not found. Operation cancelled.");
                return;
            }

            // Validations were ok, delete the data.
            try
            {
                _controller.DeleteTask(id);
                Console.WriteLine($"Task with id {id} successfully deleted");
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine($"WARNING: {e.Message}. Operation cancelled.");
            }
        }
    }
}