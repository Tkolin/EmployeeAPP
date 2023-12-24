namespace EmployeeAPP.Model;

public class Unit
{
    public int ID { get; set; }
    public string Name { get; set; }

    public Unit() { }

    public Unit(int ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }
}