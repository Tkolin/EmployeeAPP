namespace EmployeeAPP.Model;

public class FamilyLocation
{
    public int ID { get; set; }
    public string Name { get; set; }

    public FamilyLocation() { }

    public FamilyLocation(int ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }
}