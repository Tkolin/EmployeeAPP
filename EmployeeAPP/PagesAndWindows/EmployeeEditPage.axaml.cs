using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using EmployeeAPP.Model;

namespace EmployeeAPP.PagesAndWindows;

public partial class EmployeeEditPage : Window
{
    private bool addPage = true;
    private Employee _employeeEdit { get; set; }
    private Passport _employeePassport { get; set; }
    private List<Gender> _DataGender { get; set; }
    private List<FamilyLocation> _DataFamilyLocation { get; set; }
    
    private List<Dismissal> _DataDismissal { get; set; }
    private List<Dismissal> _ViewDismissal { get; set; }
    
    private List<Passport> _DataPassport { get; set; }
    private List<Passport> _ViewPassport { get; set; }
    
    private List<Position> _DataPosition { get; set; }
    private List<Position> _ViewPosition { get; set; }
    
    private List<Unit> _DataUnit { get; set; }
    private List<Unit> _ViewUnit { get; set; }
    
    private List<EmployeeToWork> _DataEmployeeToWork { get; set; }
    private List<EmployeeToWork> _ViewEmployeeToWork { get; set; }
    

    public EmployeeEditPage()
    {
        InitializeComponent();
        DownloadDataGrid();
        UpdateComBox();
    }
    public EmployeeEditPage(Employee employee)
    {
        InitializeComponent();
        DownloadDataGrid();
        UpdateComBox();
        _employeeEdit = employee;
        addPage = false;
        ViewDataEmployee();
    }
    public void DownloadDataGrid()
    {
        _DataDismissal = DataBaseManager.GetDismissal();
        _DataPassport = DataBaseManager.GetPassport();
        _DataPosition = DataBaseManager.GetPosition();
        _DataUnit = DataBaseManager.GetUnit();
        _DataEmployeeToWork = DataBaseManager.GetEmployeeToWork();
        _DataGender = DataBaseManager.GetGender();
        if (!addPage)
        {
            _ViewDismissal = _DataDismissal;
            _ViewPassport = _DataPassport;
            _ViewPosition = _DataPosition;
            _ViewUnit = _DataUnit;
            _ViewEmployeeToWork = _DataEmployeeToWork;

            DataGridEmployeeToWork.ItemsSource = _ViewEmployeeToWork;
            
            // DataGridDismise.ItemsSource = _ViewDismissal;
            // DataGridPassport.ItemsSource = _ViewPassport;
            // DataGridPosition.ItemsSource = _ViewPosition;
            // //DataGridUnit.ItemsSource = _ViewUnit;
            // DataGridOrderToWork.ItemsSource = _ViewEmployeeToWork;
        }
    }

    public void UpdateComBox()
    {
        CBoxGender.ItemsSource = _DataGender;
        CBoxFamily.ItemsSource = _DataFamilyLocation;
    }

    public void ViewDataEmployee()
    {
        TBoxID.Text = _employeeEdit.ID.ToString();
        TBoxLastName.Text = _employeeEdit.Last_Name;
        TBoxFirstName.Text = _employeeEdit.First_Name;
        TBoxPatronymic.Text = _employeeEdit.Patronymic;
        CBoxGender.SelectedItem = _DataGender.Where(d=>d.ID == _employeeEdit.Gender_ID).First();
        CBoxFamily.SelectedItem = _DataFamilyLocation.Where(d=>d.ID == _employeeEdit.Family_Location_ID).First();
        DPicerDateBirth.SelectedDate = _employeeEdit.Birth_Date;
        TBoxPointStart.Text = _employeeEdit.Birth_Adress;

        TBoxPhoneNumber.Text = _employeeEdit.Phone_Number;
        TBoxAdressReg.Text = _employeePassport.Registration_Address;
        TBoxAdressPos.Text = _employeePassport.Positions_Address;
        TBoxPassportNumber.Text = _employeePassport.Passport_Number;
        TBoxPassportSeria.Text = _employeePassport.Passport_Series;
        
    }


    private void BtnResetEmpToWork_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void BtnAddEmpToWork_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}