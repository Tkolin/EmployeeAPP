namespace EmployeeAPP.Model;

public class Passport
{
    public int ID { get; set; }
    public string Passport_Series { get; set; }
    public string Passport_Number { get; set; }
    public string Registration_Address { get; set; }

    public Passport() { }

    public Passport(
        int id,
        string passportSeries,
        string passportNumber,
        string registrationAddress
    )
    {
        ID = id;
        Passport_Series = passportSeries;
        Passport_Number = passportNumber;
        Registration_Address = registrationAddress;
    }
}