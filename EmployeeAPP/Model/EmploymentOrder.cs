using System;

namespace EmployeeAPP.Model;

public class EmploymentOrder
{
    public int ID { get; set; }
    public DateTime Date { get; set; }

    public DateTime Date_Employment { get; set; }

    public EmploymentOrder() { }

    public EmploymentOrder(
        int id,
        DateTime date,

        DateTime dateEmployment
    )
    {
        ID = id;
        Date = date;

        Date_Employment = dateEmployment;
    }
}