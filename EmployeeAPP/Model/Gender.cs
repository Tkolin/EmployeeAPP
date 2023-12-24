namespace EmployeeAPP.Model;

public class Gender
{
    public int ID { get; set; }
    public string Name { get; set; }

    public Gender() { }

    public Gender(int ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }
}