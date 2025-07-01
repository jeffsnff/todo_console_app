using System;
using System.Data.Common;

namespace ToDoApp
{
  public class Item
  {
    public Guid _id { get; }
    public DateTime _dateCreated { get; } = DateTime.Now;
    public bool _completed { get; set; } = false;
    public DateTime _dueDate { get; set; }
    public string _title { get; set; }
    public string _details { get; set; }
    public Item(string title, string details, DateTime dueDate)
    {
      _title = title;
      _details = details;
      _dueDate = dueDate;
      _id =  Guid.NewGuid();
    }
  }
}