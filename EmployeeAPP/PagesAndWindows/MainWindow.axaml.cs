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
        int? positionID = DataBaseManager.GetEmployeeToWork()
            .Where(w => w.Employee_ID == _employeeAuth.ID)
            .FirstOrDefault().Position_ID;
        switch (positionID)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                Close();
                break;
        }

        DownloadDataGrid();
    }
    public void DownloadDataGrid()
    {
        _DataEmployee = DataBaseManager.GetEmployee();
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
        throw new System.NotImplementedException();
    }

    private void BtnAdd_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void BtnOpenCart_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataGridEmploy.SelectedItem == null)
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Работник не выбран!", ButtonEnum.Ok).ShowAsync();
            return;
        }
        throw new System.NotImplementedException();
    }
}