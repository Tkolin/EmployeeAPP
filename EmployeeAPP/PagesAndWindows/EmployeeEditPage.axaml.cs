using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using EmployeeAPP.Model;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace EmployeeAPP.PagesAndWindows;

public partial class EmployeeEditPage : Window
{
    private bool addPage = true;
    private Employee _employeeEdit { get; set; }
    private Passport _employeePassport { get; set; }

    
    
    
    private List<Gender> _DataGender { get; set; }
    private List<FamilyLocation> _DataFamilyLocation { get; set; }
    

    private EmployeeToWork _employeeToWork { get; set; }
    private List<Position> _DataPosition { get; set; }
    
    private List<Unit> _DataUnit { get; set; }
    
    private List<EmployeeToWork> _ViewEmployeeToWork { get; set; }
    
    /// <summary>
    /// Общая логика
    /// </summary>
    public EmployeeEditPage()
    {
        InitializeComponent();
        DownloadData();
        _employeeEdit = new Employee();
        _employeePassport = new Passport();
        UpdateComBox();
        FilterViewData();
       // DownloadDataGrid();
    }
    public EmployeeEditPage(Employee employee)
    {
        InitializeComponent();
        DownloadData();
                
        UpdateComBox();
        FilterViewData();
        _employeeEdit = employee;
        _employeePassport = DataBaseManager.GetPassport().Where(e => e.ID == employee.Passport_ID).FirstOrDefault() ??
                            new Passport();
        _employeeToWork = DataBaseManager.GetEmployeeToWork().Where(e => e.Employee_ID == _employeeEdit.ID).FirstOrDefault() ??
                          new EmployeeToWork();
        addPage = false;
        Tab1UpdateView();
        Tab2UpdateView();
    }


    public void DownloadData()
    {
        
        _DataGender = DataBaseManager.GetGender();
        _DataPosition = DataBaseManager.GetPosition();
        _DataUnit = DataBaseManager.GetUnit();
        _DataFamilyLocation = DataBaseManager.GetFamilyLocation();

        
        DataGridEmployeeToWork.ItemsSource = _ViewEmployeeToWork;

    }

    public void FilterViewData()
    {


    }
    public void UpdateComBox()
    {
        CBoxGender.ItemsSource = _DataGender;
        CBoxFamily.ItemsSource = _DataFamilyLocation;
        CBoxPosition.ItemsSource = _DataPosition;
        CBoxUnit.ItemsSource = _DataUnit;
    }





    private void BtnResetEmpToWork_OnClick(object? sender, RoutedEventArgs e)
    {
        DataGridEmployeeToWork.SelectedItem = null;
        DataGridEmployeeToWork_OnSelectionChanged(sender,null);
        
        Panel2.IsEnabled = false;


    }

    private bool addEmptToWork = true;
    private void BtnAddEmpToWork_OnClick(object? sender, RoutedEventArgs e)
    {
        DataGridEmployeeToWork.SelectedItem = null;
        DataGridEmployeeToWork_OnSelectionChanged(sender,null);
        addEmptToWork = true;
        Panel2.IsEnabled = true;

    }
    private void BtnEditEmpToWork_OnClick(object? sender, RoutedEventArgs e)
    {
        Panel2.IsEnabled = true;

        addEmptToWork = false;
    }
    /// <summary>
    /// 1 Раздел 
    /// </summary>
    public void Tab1UpdateView()
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
    /// <summary>
    /// 2 Раздел 
    /// </summary>
    public void Tab2UpdateView()
    {
        _ViewEmployeeToWork = DataBaseManager.GetEmployeeToWork().Where(emt => emt.Employee_ID == _employeeEdit.ID).ToList();
        DataGridEmployeeToWork.ItemsSource = _ViewEmployeeToWork;

    }


        /// <summary>
        /// 1-2 Четверть
        /// </summary>
        
    private void DataGridEmployeeToWork_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (DataGridEmployeeToWork.SelectedItem == null)
        {
            CBoxPosition.SelectedItem =  null;
            CBoxUnit.SelectedItem =  null;   

            TBOxEmpToWorkID.Text = null;
            DPickCreateEmploy.SelectedDate = null;
            DPickEmploy.SelectedDate = null;
            DPickCreateDismission.SelectedDate = null;
            DPickDismission.SelectedDate   = null;
            return;
        }
        
        addEmptToWork = false;
        EmployeeToWork emptwo = DataGridEmployeeToWork.SelectedItem as EmployeeToWork;
        
        TBOxEmpToWorkID.Text = emptwo.ID.ToString();
        CBoxPosition.SelectedItem = _DataPosition.Where(d=>d.ID==emptwo.Position_ID).FirstOrDefault()  ?? null;
        CBoxUnit.SelectedItem = _DataUnit.Where(d=>d.ID==emptwo.Position_ID).FirstOrDefault()  ?? null;   
        DPickCreateEmploy.SelectedDate = emptwo.DateCreateEmployment;
        DPickEmploy.SelectedDate = emptwo.DateEmployment;
        DPickCreateDismission.SelectedDate = emptwo.DateCreateDosmissal;
        DPickDismission.SelectedDate   = emptwo.DateDosmissal;
    }
    
    /// <summary>
    /// Подвал
    /// </summary>



    
    private void InputElement_OnTapped(object? sender, TappedEventArgs e)
    {
        DataGridEmployeeToWork.SelectedItem = null;
    }

    private void BtnSaveEmpToWork_OnClick(object? sender, RoutedEventArgs e)
    {
        if (
            CBoxPosition.SelectedItem == null ||
            CBoxUnit.SelectedItem == null ||
            DPickCreateEmploy.SelectedDate == null ||
            DPickEmploy.SelectedDate == null ||
            addPage
        )
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Данные не заполнены!", ButtonEnum.Ok).ShowAsync();
            return;
        }

        if (addEmptToWork)
            _employeeToWork = new EmployeeToWork();
        else
         _employeeToWork = DataGridEmployeeToWork.SelectedItem as EmployeeToWork;

        _employeeToWork.Employee_ID = _employeeEdit.ID;
        _employeeToWork.Position_ID = (CBoxPosition.SelectedItem as Position).ID;
        _employeeToWork.Unit_ID = (CBoxUnit.SelectedItem as Unit).ID;
        _employeeToWork.DateEmployment = DPickEmploy.SelectedDate.Value.DateTime;
        _employeeToWork.DateCreateEmployment = DPickCreateEmploy.SelectedDate.Value.DateTime;
        if(DPickDismission.SelectedDate != null )
            _employeeToWork.DateDosmissal = DPickDismission.SelectedDate.Value.DateTime;
        if(DPickCreateDismission.SelectedDate != null )
            _employeeToWork.DateCreateDosmissal = DPickCreateDismission.SelectedDate.Value.DateTime;
        
        if (addEmptToWork)
        {
            DataBaseManager.AddEmployeeToWork(_employeeToWork);
            DownloadData();
                    
            UpdateComBox();
            FilterViewData();
            MessageBoxManager.GetMessageBoxStandard("Успех", "Данные успешно добавлены!", ButtonEnum.Ok).ShowAsync();
            Tab2UpdateView();
        }
        else
        {

            DataBaseManager.UpdateEmployeeToWork(_employeeToWork);
            DownloadData();
                    
            UpdateComBox();
            FilterViewData();
            Panel2.IsEnabled = false;
            MessageBoxManager.GetMessageBoxStandard("Успех", "Данные успешно изменены!", ButtonEnum.Ok).ShowAsync();
            Tab2UpdateView();
        }
        
        
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        if (
            TBoxLastName.Text.Length <= 0 ||
            TBoxFirstName.Text.Length <= 0 ||
            TBoxPatronymic.Text.Length <= 0 ||
            CBoxGender.SelectedItem == null ||
            CBoxFamily.SelectedItem == null ||
            DPicerDateBirth.SelectedDate == null  ||
            TBoxPointStart.Text.Length <= 0 ||
            TBoxPhoneNumber.Text.Length <= 0 ||
            TBoxAdressReg.Text.Length <= 0 ||
            TBoxAdressPos.Text.Length <= 0 ||
            TBoxPassportSeria.Text.Length <= 0 ||
            TBoxPassportNumber.Text.Length <= 0 
        )
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Данные не заполнены!", ButtonEnum.Ok).ShowAsync();
            return;
        }

        _employeePassport.Passport_Number = TBoxPassportNumber.Text;
        _employeePassport.Passport_Series = TBoxPassportSeria.Text;
        _employeePassport.Registration_Address = TBoxAdressReg.Text;
        _employeePassport.Positions_Address = TBoxAdressPos.Text;
        
        if (addPage)
        {
            DataBaseManager.AddPassport(_employeePassport);
            DownloadData();
            _employeePassport = DataBaseManager.GetPassport().Last();
        }
        else
        {
            DataBaseManager.UpdatePassport(_employeePassport);
            DownloadData();
        }
        
        _employeeEdit.First_Name = TBoxFirstName.Text;
        _employeeEdit.Last_Name = TBoxLastName.Text;
        _employeeEdit.Patronymic = TBoxPatronymic.Text;
        _employeeEdit.Gender_ID = (CBoxGender.SelectedItem as Gender).ID;
        _employeeEdit.Passport_ID = _employeePassport.ID;
        _employeeEdit.Family_Location_ID = (CBoxFamily.SelectedItem as FamilyLocation).ID;
        _employeeEdit.INN = TBoxFirstName.Text;
        _employeeEdit.Birth_Date = DPicerDateBirth.SelectedDate.Value.DateTime;
        _employeeEdit.Birth_Adress = TBoxPointStart.Text;
        _employeeEdit.Phone_Number = TBoxPhoneNumber.Text;
        
        if (addPage)
        {
            DataBaseManager.AddEmployee(_employeeEdit);
            DownloadData();
            _employeeEdit = DataBaseManager.GetEmployee().Last();
        }
        else
        {
            DataBaseManager.UpdateEmployee(_employeeEdit);
            DownloadData();
        }


        addEmptToWork = false;
    }


}