namespace EmployeeAPP.Model;

public class Family_location
{
    public int ID { get; set; }
    public string Name { get; set; }

    public Family_location() { }

    public Family_location(int ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }
}