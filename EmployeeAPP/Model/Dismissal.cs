using System;

namespace EmployeeAPP.Model;

public class Dismissal
{
    public int ID { get; set; }
    public DateTime Date { get; set; }
    

    public DateTime Date_Dismissal { get; set; }
    public Dismissal() { }

    public Dismissal(int ID, DateTime Date, DateTime Date_Dismissal)
    {
        this.ID = ID;
        this.Date = Date;
        this.Date_Dismissal = Date_Dismissal;
    }
}