namespace EmployeeAPP.Model;

public class EmployeeToWork
{
    public int ID { get; set; }
    public int Employee_ID { get; set; }
    public int Position_ID { get; set; }
    public int Unit_ID { get; set; }
    public int Dismissal_ID { get; set; }
    public int employment_order_ID { get; set; }
    
    public string PositionData  {get; set; }
    public string UnitData  {get; set; }
    public string DismissalnData  {get; set; }
    public string EmploymentOrderData  {get; set; }

    public EmployeeToWork() { }

    public EmployeeToWork(
        int id,
        int employeeId,
        int positionId,
        int unitId,
        int dismissalId,
        int employmentOrderId,
        string positionData,
        string unitData,
        string dsmissalnData,
        string employmentOrderData
    )
    {
        ID = id;
        Employee_ID = employeeId;
        Position_ID = positionId;
        Unit_ID = unitId;
        Dismissal_ID = dismissalId;
        employment_order_ID = employmentOrderId;
        PositionData =positionData;
            UnitData=unitData;
        DismissalnData=dsmissalnData;
            EmploymentOrderData=employmentOrderData;
    }
}