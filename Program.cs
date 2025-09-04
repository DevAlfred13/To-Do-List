using System;
using System.Collections.Generic;
using System.IO;

namespace ToDoList
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "tasks.txt");
            List<string> tasks = new List<string>();   // one shared list

            // Load existing tasks
            if (File.Exists(filePath))
            {
                tasks.AddRange(File.ReadAllLines(filePath));
            }

            Console.WriteLine("Welcome to the To-Do List Application!");
            Console.WriteLine($"(Saving to: {filePath})");

            while (true)
            {
                try
                {
                    Console.WriteLine("\n\n1. Add a task\n\n2. View task\n\n3. Remove a task\n\n4. Exit\n\n");
                    int input = Convert.ToInt32(Console.ReadLine());

                    if (input == 1)
                    {
                        AddTask(tasks, filePath);
                    }
                    else if (input == 2)
                    {
                        ViewTask(tasks);
                    }
                    else if (input == 3)
                    {
                        RemoveTask(tasks, filePath);
                    }
                    else if (input == 4)
                    {
                        // Save before exiting
                        File.WriteAllLines(filePath, tasks);
                        Console.WriteLine("Exiting the application. Goodbye!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        public static void AddTask(List<string> tasks, string filePath)
        {
            Console.Write("Enter task: ");
            string task = Console.ReadLine();
            tasks.Add(task);
            File.WriteAllLines(filePath, tasks); // save after adding
            Console.WriteLine($"Added: {task}");
        }

        public static void ViewTask(List<string> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks yet!");
            }
            else
            {
                Console.WriteLine("\nYour tasks:");
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {tasks[i]}");
                }
            }
        }

        public static void RemoveTask(List<string> tasks, string filePath)
        {
            ViewTask(tasks);

            if (tasks.Count == 0)
            {
                return; // Nothing to remove
            }

            Console.Write("\nEnter task number to remove: ");
            int taskNum = Convert.ToInt32(Console.ReadLine());

            if (taskNum < 1 || taskNum > tasks.Count)
            {
                Console.WriteLine("Invalid task number.");
            }
            else
            {
                string removedTask = tasks[taskNum - 1];
                tasks.RemoveAt(taskNum - 1);
                File.WriteAllLines(filePath, tasks); // save after removing
                Console.WriteLine($"Removed: {removedTask}");
            }
        }
    }
}
