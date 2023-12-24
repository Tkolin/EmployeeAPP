using System;

namespace EmployeeAPP.Model;

public class EmploymentOrder
{
    public int ID { get; set; }
    public DateTime Date { get; set; }
    public int Contract_Number { get; set; }
    public DateTime Date_Employment { get; set; }

    public EmploymentOrder() { }

    public EmploymentOrder(
        int id,
        DateTime date,
        int contractNumber,
        DateTime dateEmployment
    )
    {
        ID = id;
        Date = date;
        Contract_Number = contractNumber;
        Date_Employment = dateEmployment;
    }
}