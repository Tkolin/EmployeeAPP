using System;
using System.Linq;

namespace EmployeeAPP.Model;

public class EmployeeToWork
{
    public int ID { get; set; }
    public int Employee_ID { get; set; }
    public int Position_ID { get; set; }
    public int Unit_ID { get; set; }
    public DateTime DateCreateEmployment { get; set; }
    public DateTime DateEmployment { get; set; }
    public DateTime DateCreateDosmissal { get; set; }
    public DateTime DateDosmissal { get; set; }
    
    public string UnitData { get; set; }
    public string PositionData { get; set; }
    public EmployeeToWork() { }
    public EmployeeToWork(
        int id,
        int employeeId,
        int positionId,
        int unitId,
        DateTime dateCreateEmployment,
        DateTime dateEmployment,
        DateTime dateCreateDosmissal,
        DateTime dateDosmissal,
        string unitData,
        string positionData

    )
    {
        ID = id;
        Employee_ID = employeeId;
        Position_ID = positionId;
        Unit_ID = unitId;
        DateEmployment = dateEmployment;
        DateCreateEmployment = dateCreateEmployment;
        DateDosmissal = dateDosmissal;
        DateCreateDosmissal = dateCreateDosmissal;
        UnitData = unitData;
        PositionData = positionData;


    }
}