namespace EmployeeAPP.Model;

public class Position
{
    public int ID { get; set; }
    public string Name { get; set; }

    public Position() { }

    public Position(int ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }
}