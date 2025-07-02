using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ToDoApp
{
  public class List
  {
    List<Item> ToDoList = new List<Item>();

    public List() { }

    public void Add(Item item)
    {
      ToDoList.Add(item);
    }
    public void Update(Guid itemID, Item updatedItem)
    {
      Remove(itemID);
      ToDoList.Add(updatedItem);
    }
    public bool Delete(string todo)
    {
      Item itemToDelete = Find(todo);
      
      if (itemToDelete == null)
      {
        Console.Clear();
        Console.WriteLine("That To Do item does not exist.");
        Console.ReadKey();
        return false;
      }
      else
      {
        Remove(itemToDelete._id);
        return true;
      }
    }
    public void Read()
    {
      // Console.Clear();
      foreach (Item ToDo in ToDoList)
      {
        Console.WriteLine(ToDo._title);
        Console.WriteLine(ToDo._details);
        Console.WriteLine(ToDo._dueDate);
        Console.WriteLine();
      }
      Console.ReadKey();
    }
    public int Count()
    {
      return ToDoList.Count();
    }
    public Item Find(string itemToFind)
    {
      foreach (Item item in ToDoList)
      {
        if (item._title.Equals(itemToFind))
        {
          return item;
        }
      }
      return null;
    }
    private void Remove(Guid id)
    {
      for (int i = 0; i < ToDoList.Count; i++)
      {
        if (ToDoList[i]._id == id)
        {
          ToDoList.Remove(ToDoList[i]);
        }
      }
    }
  }
}