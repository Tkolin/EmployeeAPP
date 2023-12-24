using System;

namespace EmployeeAPP.Model;

public class Dismissal
{
    public int ID { get; set; }
    public DateTime Date { get; set; }

    public Dismissal() { }

    public Dismissal(int ID, DateTime Date)
    {
        this.ID = ID;
        this.Date = Date;
    }
}