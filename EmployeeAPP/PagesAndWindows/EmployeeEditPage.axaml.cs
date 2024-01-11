using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Avalonia;
using Avalonia.Controls;
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
    private EmployeeToWork _employeeToWork { get; set; }
    
    
    
    private List<Gender> _DataGender { get; set; }
    private List<FamilyLocation> _DataFamilyLocation { get; set; }
    
    private List<Dismissal> _DataDismissal { get; set; }
    private List<Dismissal> _ViewDismissal { get; set; }
    
    private List<EmploymentOrder> _DataEmploymentOrder { get; set; }
    private List<EmploymentOrder> _ViewEmploymentOrder { get; set; }
    
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
       // DownloadDataGrid();
    }
    public EmployeeEditPage(Employee employee)
    {
        InitializeComponent();
        DownloadData();
        _employeeEdit = employee;
        _employeePassport = DataBaseManager.GetPassport().Where(e => e.ID == employee.Passport_ID).First() ??
                            new Passport();
        _employeeToWork = DataBaseManager.GetEmployeeToWork().Where(e => e.Employee_ID == _employeeEdit.ID).First() ??
                          new EmployeeToWork();
        addPage = false;
        Tab1UpdateView();
        Tab2UpdateView();
    }


    public void DownloadData()
    {
        _DataDismissal = DataBaseManager.GetDismissal();
        _DataGender = DataBaseManager.GetGender();
        _DataPosition = DataBaseManager.GetPosition();
        _DataUnit = DataBaseManager.GetUnit();
        _DataFamilyLocation = DataBaseManager.GetFamilyLocation();
        _DataEmploymentOrder = DataBaseManager.GetEmploymentOrder();
        
        DataGridEmployeeToWork.ItemsSource = _ViewEmployeeToWork;
        
        UpdateComBox();
        FilterViewData();
    }

    public void FilterViewData()
    {
        if (addPage)
        {
            _ViewDismissal = new List<Dismissal>();
            _ViewEmploymentOrder = new List<EmploymentOrder>();
        }
        else
        {
            _ViewDismissal = _DataDismissal.Where(d => d.ID == _employeeToWork.Dismissal_ID).ToList();
            _ViewEmploymentOrder = _DataEmploymentOrder.Where(d=>d.ID == _employeeToWork.employment_order_ID).ToList();
        }

    }
    public void UpdateComBox()
    {
        CBoxGender.ItemsSource = _DataGender;
        CBoxFamily.ItemsSource = _DataFamilyLocation;
        CBoxPosition.ItemsSource = _DataPosition;
        CBoxUnit.ItemsSource = _DataUnit;
        CBoxOrderDismish.ItemsSource = _DataDismissal;
        CBoxOrderWork.ItemsSource = _DataEmploymentOrder;
    }





    private void BtnResetEmpToWork_OnClick(object? sender, RoutedEventArgs e)
    {
        DataGridEmployeeToWork.SelectedItem = null;
    }

    private bool addEmptToWork = true;
    private void BtnAddEmpToWork_OnClick(object? sender, RoutedEventArgs e)
    {
        DataGridEmployeeToWork.SelectedItem = null;
        addEmptToWork = true;
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
            CBoxOrderDismish.SelectedItem =  null; 
            CBoxOrderWork.SelectedItem = null; 

            addEmptToWork = true;
            return;
        }

        addEmptToWork = false;
        EmployeeToWork emptwo = DataGridEmployeeToWork.SelectedItem as EmployeeToWork;
        
        CBoxPosition.SelectedItem = _DataPosition.Where(d=>d.ID==emptwo.Position_ID).FirstOrDefault()  ?? null;
        CBoxUnit.SelectedItem = _DataUnit.Where(d=>d.ID==emptwo.Position_ID).FirstOrDefault()  ?? null;   
        CBoxOrderDismish.SelectedItem = _ViewDismissal.Where(d=>d.ID==emptwo.Position_ID).FirstOrDefault()  ?? null; 
        CBoxOrderWork.SelectedItem = _ViewEmploymentOrder.Where(d=>d.ID==emptwo.Position_ID).FirstOrDefault()  ?? null; 
    }


    private void BtnAddOrderDismish_OnClick(object? sender, RoutedEventArgs e)
    {
        CBoxOrderDismish.SelectedItem = null;
    }
    


    private void BtnAddOrderWork_OnClick(object? sender, RoutedEventArgs e)
    {
        CBoxOrderWork.SelectedItem = null;
    }
    private bool addOrderDismish = true;
    private void CBoxOrderDismish_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (CBoxOrderDismish.SelectedItem == null)
        {
            addOrderDismish = true;
            Dismissal addet = new Dismissal();

            TBOxDismiseOrderID.Text = "";
            DPicerkDismiseDate.SelectedDate = DateTime.Now;
            DPicerkDismiseDateDismise.SelectedDate = DateTime.Now;

        }
        else
        {
            addOrderDismish = false;
            Dismissal selected = CBoxOrderDismish.SelectedItem as Dismissal;

            TBOxDismiseOrderID.Text = selected.ID.ToString();
            DPicerkDismiseDate.SelectedDate = selected.Date;
            DPicerkDismiseDateDismise.SelectedDate = selected.Date_Dismissal;
        }
    }

    private bool addOrderWork = true;
    private void CBoxOrderWork_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (CBoxOrderWork.SelectedItem == null)
        {
            addOrderWork = true;
            EmploymentOrder addet = new EmploymentOrder();
            
            TBOxWorkOrderID.Text = "";
            DPicerkWorkOrderDate.SelectedDate = DateTime.Now;
            DPicerkWorkOrderDatemploy.SelectedDate = DateTime.Now;

        }
        else
        {
            addOrderWork = false;
            EmploymentOrder selected = CBoxOrderWork.SelectedItem as EmploymentOrder;

            TBOxWorkOrderID.Text = selected.ID.ToString();
            DPicerkWorkOrderDate.SelectedDate = selected.Date;
            DPicerkWorkOrderDatemploy.SelectedDate = selected.Date_Employment;
        }
    }
    
    /// <summary>
    /// 3 Четверть
    /// </summary>

    private void BtnSaveWorkOrder_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DPicerkWorkOrderDate.SelectedDate == null || DPicerkWorkOrderDatemploy.SelectedDate == null)
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка","Данные не заполнены",ButtonEnum.Ok).ShowAsync();
            return;
        } 
        
        if (addOrderWork)
        {
            EmploymentOrder addOrder = new EmploymentOrder();
            addOrder.Date = DPicerkWorkOrderDate.SelectedDate.Value.Date;
            addOrder.Date_Employment = DPicerkDismiseDateDismise.SelectedDate.Value.Date;
            
            DataBaseManager.AddEmploymentOrder(addOrder);
        }
        else
        {
            EmploymentOrder editOrder = CBoxOrderWork.SelectedItem as EmploymentOrder;
            editOrder.Date = DPicerkWorkOrderDate.SelectedDate.Value.Date;
            editOrder.Date_Employment = DPicerkDismiseDateDismise.SelectedDate.Value.Date;
             
            DataBaseManager.UpdateEmploymentOrder(editOrder);
        }
    }
    
    /// <summary>
    /// 4 Четверть
    /// </summary>




    private void BtnSaveDismision_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DPicerkDismiseDate.SelectedDate == null || DPicerkDismiseDateDismise.SelectedDate == null)
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка","Данные не заполнены",ButtonEnum.Ok).ShowAsync();
            return;
        } 
        
        if (addOrderDismish)
        {
            Dismissal addOrder = new Dismissal();
            addOrder.Date = DPicerkDismiseDate.SelectedDate.Value.Date;
            addOrder.Date_Dismissal = DPicerkDismiseDateDismise.SelectedDate.Value.Date;
            
            DataBaseManager.AddDismissal(addOrder);
        }
        else
        {
            Dismissal editOrder = CBoxOrderWork.SelectedItem as Dismissal;
            editOrder.Date = DPicerkDismiseDate.SelectedDate.Value.Date;
            editOrder.Date_Dismissal = DPicerkDismiseDateDismise.SelectedDate.Value.Date;

            DataBaseManager.UpdateDismissal(editOrder);
        }
    }




}