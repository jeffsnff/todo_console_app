using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualBasic;

namespace ToDoApp
{
  internal class Program
  {
    static void Main(string[] args)
    {
      List toDoList = new List();
      while (true)
      {
        RunCommand(Enum.GetNames(typeof(Action)), toDoList);
      }
    }
    private static void CreateNewToDo(List toDoList)
    {
      Console.Clear();
      ApplicationTitle();
      Console.WriteLine();
      string title = GetTitle();
      string details = GetDetail();
      DateTime dueDate = GetDate();

      Item newItem = new Item(title, details, dueDate);
      toDoList.Add(newItem);
    }
    private static void UpdateItem(List toDoList)
    {
      ApplicationTitle();

      string toDoUpdate;
      Item toUpdate;
      while (true)
      {
        toDoUpdate = UserChoice(toDoList, "Update");
        if (toDoUpdate.Equals(""))
        {
          return;
        }
        toUpdate = toDoList.Find(toDoUpdate);

        if (toUpdate == null)
        {
          Console.Clear();
          Console.WriteLine("That To Do item does not exist.");
          Console.ReadKey();
          continue;
        }
        break;
      }

      string selectedOption = DisplayOptions(Enum.GetNames(typeof(SectionUpdate)));

      switch (selectedOption)
      {
        case "Title":
          Console.WriteLine($"Updating: {toUpdate._title}");
          Console.Write("New Title: ");
          Console.CursorVisible = true;
          toUpdate._title = Console.ReadLine();
          break;
        case "Details":
          Console.WriteLine($"Updateing {toUpdate._title} details");
          Console.WriteLine($"Details:\n{toUpdate._details}");
          Console.WriteLine("\nNew Details:");
          Console.CursorVisible = true;
          toUpdate._details = Console.ReadLine();
          break;
        case "Date":
          Console.WriteLine($"Updating {toUpdate._dueDate}");
          toUpdate._dueDate = GetDate();
          break;
        case "Exit":
          break;
      }
    }
    private static void DeleteToDo(List toDoList)
    {
      string toDoDelete;
      Item itemToDelete;
      while (true)
      {
        toDoDelete = UserChoice(toDoList, "Delete"); 
        if (toDoDelete.Equals(""))
        {
          break;
        }
        if (toDoList.Delete(toDoDelete))
        {
          Console.WriteLine("Item has been deleted");
          Console.ReadKey();
          break;
        }
      }
    }
    private static void RunCommand(string[] options, List toDoList)
    {
      while (true)
      {
        string selectedAction = DisplayOptions(options);
        switch (selectedAction)
        {
          case "Add":
            CreateNewToDo(toDoList);
            break;
          case "Update":
            ApplicationTitle();
            if (toDoList.Count() == 0)
            {
              Console.WriteLine("List is empty");
              Console.ReadKey();
              break;
            }
            UpdateItem(toDoList);
            break;
          case "Delete":
            ApplicationTitle();
            if (toDoList.Count() == 0)
            {
              Console.WriteLine("List is empty");
              Console.ReadKey();
              break;
            }
            DeleteToDo(toDoList);
            break;
          case "Read":
            ApplicationTitle();
            if (toDoList.Count() == 0)
            {
              Console.WriteLine("List is empty");
              Console.ReadKey();
              break;
            }
            toDoList.Read();
            break;
          case "End":
            Environment.Exit(0);
            break;
          default:
            Console.Clear();
            Console.WriteLine("That is no an option.");
            Console.ReadKey();
            continue;
        }
      }
    }
    private static string GetTitle()
    {
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("To Do Title: ");
      Console.ResetColor();
      Console.CursorVisible = true;
      return Console.ReadLine();
    }
    private static string GetDetail()
    {
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("Give some details: ");
      Console.ResetColor();
      Console.CursorVisible = true;
      return Console.ReadLine();
    }
    private static DateTime GetDate()
    {
      DateTime dueDate;
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.WriteLine("Enter a due date following the YYYY MM DD format.");
      Console.WriteLine("Press \u001b[32mEnter/Return\u001b[0m for tomorrows date.");
      Console.ResetColor();
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("Due Date: ");
      Console.ResetColor();
      Console.CursorVisible = true;

      if (DateTime.TryParse(Console.ReadLine(), out DateTime dateResult))
      {
        dueDate = dateResult;
      }
      else
      {
        TimeSpan duration = new TimeSpan(1, 0, 0, 0);
        dueDate = DateTime.Now.Add(duration);
      }
      return dueDate;
    }
    private static string DisplayOptions(string[] options)
    {
      ApplicationTitle();
      Console.WriteLine("Use ⬆️  and ⬇️  to navigate and press \u001b[32mEnter/Return\u001b[0m to select:");
      (int left, int top) = Console.GetCursorPosition();
      int selectedOption = 0;
      string decorator = "✅ \u001b[32m";
      ConsoleKeyInfo key;
      bool isSelected = false;

      while (!isSelected)
      {
        Console.SetCursorPosition(left, top);
        for (int i = 0; i < options.Length; i++)
        {
          Console.WriteLine($"{(selectedOption == i ? decorator : "   ")}{options[i]}\u001b[0m");
        }

        key = Console.ReadKey(false);
        switch (key.Key)
        {
          case ConsoleKey.UpArrow:
            selectedOption = selectedOption == 0 ? options.Length - 1 : selectedOption - 1;
            break;

          case ConsoleKey.DownArrow:
            selectedOption = selectedOption == options.Length - 1 ? 0 : selectedOption + 1;
            break;

          case ConsoleKey.Enter:
            isSelected = true;
            break;
        }
      }
      return options[selectedOption];
    }

    private static string UserChoice(List toDoList, string option)
    {
      Console.WriteLine($"What ToDo Item would you like to {option}?");
      Console.WriteLine("Press \u001b[32mEnter/Return\u001b[0m to exit.");
      Console.WriteLine();

      toDoList.Read();
      Console.Write($"{option}: ");
      Console.CursorVisible = true;
      return Console.ReadLine();
    }

    private static void ApplicationTitle()
    {
      Console.Clear();
      Console.CursorVisible = false;
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine("To Do List");
      Console.ResetColor();
      Console.WriteLine();
    }
  }
}