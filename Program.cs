using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<string> tasks = new List<string>();
    static string filename;

    static void Main()
    {
        Console.Clear();
        Console.Write("Enter name: ");
        filename = "profiles/" + Console.ReadLine().ToUpper() + ".txt";
        if (File.Exists(filename))
        {
            // if file exists read content to list
            using (var streamReader = new StreamReader(filename))
            {
                // While there is something to read
                while (!streamReader.EndOfStream)
                {
                    tasks.Add(streamReader.ReadLine());
                }
            }
        }
        Update();
    }

    static void MenuFunctionality()
    {
    // Handle menu functionality with switch case
    // Try getting input from user and handle that user might type non numeric value
    start:
        try
        {
            Console.Write("Your Choice: ");
            int input = int.Parse(Console.ReadLine());
            switch (input)
            {
                case 1:
                    AddTask();
                    break;
                case 2:
                    EditTask();
                    break;
                case 3:
                    RemoveTask();
                    break;
                case 4:
                    ExitApp();
                    break;
                default:
                    Console.WriteLine("This function does not exist in current context.");
                    goto start;
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            goto start;
        }
    }

    static void MenuListing()
    {
        Console.WriteLine();
        Console.WriteLine("------------MENU---------");
        Console.WriteLine("1. Add Task\n2. Edit Task\n3. Remove Task\n4. Exit Program");
    }


    static void ListTasks()
    {
        Console.WriteLine("----------TASKS----------");
        if (tasks.Count == 0)
        {
            Console.WriteLine("No current tasks.");
        }
        else
        {
            foreach (string item in tasks)
            {
                Console.WriteLine(tasks.IndexOf(item) + 1 + ". " + item);
            }
        }
    }

    static void AddTask()
    {
        Console.Write("Task Name: ");
        tasks.Add(Console.ReadLine());
        Update();
    }

    static void EditTask()
    {
        // if the list is empty, there are no tasks to edit
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks to edit.");
            System.Threading.Thread.Sleep(1000);
            Update();
        }
        else
        {
        start:
            Console.Write("Which task do you want to edit: ");
            try
            {
                int index = int.Parse(Console.ReadLine());
                if (index <= 0 || index > tasks.Count)
                {
                    Console.WriteLine("Please enter valid task number");
                    goto start;
                }
                else
                {
                    Console.Write("New task name: ");
                    tasks[index - 1] = Console.ReadLine();
                    Update();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                goto start;
            }
        }
    }

    static void RemoveTask()
    {
        // if the list is empty, there are no tasks to be removed
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks to remove.");
            System.Threading.Thread.Sleep(1000);
            Update();
        }
        else
        {
        start:
            Console.Write("Which task do you want to remove: ");
            try
            {
                int index = int.Parse(Console.ReadLine());
                // if given number is out of range
                if (index <= 0 || index > tasks.Count)
                {
                    Console.WriteLine("Please enter valid task number");
                    goto start;
                }
                else
                {
                    // index - 1 because user can see item number + 1
                    tasks.RemoveAt(index - 1);
                    Console.WriteLine("Task removed successfully.");
                    Update();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                goto start;
            }
        }
    }


    static void ExitApp()
    {
        Console.Write("Do you want to save your results? (Y/N): ");
        char response = char.Parse(Console.ReadLine().ToUpper());
        
		if (response == 'Y')
        {
            using (TextWriter streamWriter = new StreamWriter(filename))
            {
                foreach (string s in tasks)
                {
                    streamWriter.WriteLine(s);
                }
				Console.WriteLine("Data written successfully.");
            }
        }
        Console.WriteLine("Thank you for using our application.");
        System.Environment.Exit(0);

    }
    static void Update()
    {
        Console.Clear();
        ListTasks();
        MenuListing();
        MenuFunctionality();
    }


}
