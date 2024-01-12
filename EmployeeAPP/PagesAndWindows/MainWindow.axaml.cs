using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using EmployeeAPP.Model;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace EmployeeAPP.PagesAndWindows;

public partial class MainWindow : Window
{
    private List<Employee> _DataEmployee { get; set; }
    private List<Employee> _ViewEmployee { get; set; }
    private List<EmployeeToWork> _DataEmployeeToWork { get; set; }
    private List<EmployeeToWork> _ViewEmployeeToWork { get; set; }
    private Employee _employeeAuth { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        DownloadDataGrid();
    }
    public MainWindow(Employee _employeeAuth)
    {
        InitializeComponent();
        this._employeeAuth = _employeeAuth;
        DownloadDataGrid();
    }
    public void DownloadDataGrid()
    {
        _DataEmployee = DataBaseManager.GetEmployee();
        _DataEmployeeToWork = DataBaseManager.GetEmployeeToWork();
        TBoxLog.Text = " | " + "Загружено сотрудников из базы: " + _DataEmployee.Count.ToString();
        TBoxLog.Text += " | " + "Трудовых договоров из базы: " + _DataEmployeeToWork.Count.ToString();
        UpdateDataGrid();
    }

    private void UpdateDataGrid()
    {
        _ViewEmployee = _DataEmployee;
        if (TBoxSearchBox.Text.Length >= 1)
            _ViewEmployee = _DataEmployee.Where(c =>
                  c.ID.ToString().ToLower().Contains(TBoxSearchBox.Text.ToLower()) ||
                  c.First_Name.ToString().ToLower().Contains(TBoxSearchBox.Text.ToLower()) ||
                  c.Last_Name.ToString().ToLower().Contains(TBoxSearchBox.Text.ToLower()) ||
                  c.Login.ToString().ToLower().Contains(TBoxSearchBox.Text.ToLower()) ||
                  c.Password.ToString().ToLower().Contains(TBoxSearchBox.Text.ToLower()) ||
                  c.Patronymic.ToString().ToLower().Contains(TBoxSearchBox.Text.ToLower()) ||
                  c.Birth_Date.ToString().ToLower().Contains(TBoxSearchBox.Text.ToLower()) ||
                  c.Phone_Number.ToString().ToLower().Contains(TBoxSearchBox.Text.ToLower()) ||
                  c.INN.ToString().ToLower().Contains(TBoxSearchBox.Text.ToLower()) ||
                  c.Passport_ID .ToString().ToLower().Contains(TBoxSearchBox.Text.ToLower()) ||
                  c.Gender_ID.ToString().ToLower().Contains(TBoxSearchBox.Text.ToLower()) ||
                  c.Family_Location_ID.ToString().ToLower().Contains(TBoxSearchBox.Text.ToLower()) ||
                  c.FamilyNameData.ToString().ToLower().Contains(TBoxSearchBox.Text.ToLower()) ||
                  c.PassportAdress.ToString().ToLower().Contains(TBoxSearchBox.Text.ToLower()) ||
                  c.GenderNameData.ToString().ToLower().Contains(TBoxSearchBox.Text.ToLower()) ||
                  c.PassportNameData.ToString().ToLower().Contains(TBoxSearchBox.Text.ToLower())
            ).ToList();
        TBoxLog.Text = " | " + "Данных после фильтрации: " + _ViewEmployee.Count.ToString();
        DataGridEmploy.ItemsSource = _ViewEmployee;
    }


    private void BtnReset_OnClick(object? sender, RoutedEventArgs e)
    {
        TBoxSearchBox.Text = "";
    }

    private void TBoxSearchBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        UpdateDataGrid();
    }

    private void BtnUpdateDataGrid_OnClick(object? sender, RoutedEventArgs e)
    {
        DownloadDataGrid();
    }

    private void BtnDelet_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataGridEmploy.SelectedItem == null)
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Работник не выбран!", ButtonEnum.Ok).ShowAsync();
            return;
        }
        // DataBaseManager.Remote(DataGrid.SelectedItem as DiseaseRecord);
        DownloadDataGrid();
    }

    private void BtnEdit_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataGridEmploy.SelectedItem == null)
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Работник не выбран!", ButtonEnum.Ok).ShowAsync();
            return;
        }

        EmployeeEditPage empEdiWind = new EmployeeEditPage(DataGridEmploy.SelectedItem as Employee);
        empEdiWind.ShowDialog(this);
    }

    private void BtnAdd_OnClick(object? sender, RoutedEventArgs e)
    {
        EmployeeEditPage empEdWind = new EmployeeEditPage();
        empEdWind.ShowDialog(this);
    }

    private void BtnOpenCart_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataGridEmploy.SelectedItem == null)
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Работник не выбран!", ButtonEnum.Ok).ShowAsync();
            return;
        }
        if (DataGridOrderToWork.SelectedItem == null)
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Акт работы не выбран!", ButtonEnum.Ok).ShowAsync();
            return;
        }
        CardReportEmployeeWindow cardWind = new CardReportEmployeeWindow(DataGridEmploy.SelectedItem as Employee, DataGridOrderToWork.SelectedItem as EmployeeToWork);
        
        cardWind.ShowDialog(this);
    }

    private void DataGridEmploy_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (DataGridEmploy.SelectedItem == null)
        {
            _ViewEmployeeToWork.Clear();
            TBoxLog.Text += " | " + "Нет трудовых договоров для этого сотрудника";
            return;
        }

        int employID = (DataGridEmploy.SelectedItem as Employee).ID;
        _ViewEmployeeToWork = _DataEmployeeToWork.Where(etw => etw.Employee_ID == employID).ToList();
        TBoxLog.Text = " | " + "Договоров " + employID+ " : " + _ViewEmployeeToWork.Count.ToString();
        DataGridOrderToWork.ItemsSource = _ViewEmployeeToWork;
        
    }

    private void MenuItem1_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void MenuItem2_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void MenuItem3_OnClick(object? sender, RoutedEventArgs e)
    {
        ReferencesPage refWind = new ReferencesPage();
        refWind.ShowDialog(this);
    }

}