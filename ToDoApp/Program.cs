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
      List<Item> toDoList = new List<Item>();
      while (true)
      {
        RunCommand(Enum.GetNames(typeof(Action)), toDoList);
      }
    }
    private static void RunCommand(string[] options, List<Item> toDoList)
    {
      while (true)
      {
        string selectedAction = DisplayOptions(options);
        switch (selectedAction)
        {
          case "Add":
            ApplicationTitle();
            Console.WriteLine("Add");
            Console.ReadKey();
            break;
          case "Update":
            ApplicationTitle();
            Console.WriteLine("Update");
            Console.ReadKey();
            break;
          case "Delete":
            ApplicationTitle();
            Console.WriteLine("Delete");
            Console.ReadKey();
            break;
          case "Read":
            ApplicationTitle();
            Console.WriteLine("Read");
            Console.ReadKey();
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
    private static string DisplayOptions(string[] options)
    {
      ApplicationTitle();
      Console.WriteLine("Use ⬆️  and ⬇️  to navigate and press \u001b[32mEnter/Return\u001b[0m to select:");
      (int left, int top) = Console.GetCursorPosition();
      int selectedOption = 0;
      string decorator = "✅  \u001b[32m";
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