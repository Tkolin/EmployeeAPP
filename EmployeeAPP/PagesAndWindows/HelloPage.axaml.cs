using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using EmployeeAPP.Model;
using EmployeeAPP.PagesAndWindows;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace EmployeeAPP;

public partial class HelloPage : Window
{
    public HelloPage()
    {
        InitializeComponent();
    }
    private Employee _employeeAuth { get; set; }
    private void BtnAuth_OnClick(object? sender, RoutedEventArgs e)
    {
        _employeeAuth = null;
        if (TBoxLogin.Text.Length <= 1 || TBoxPassword.Text.Length <= 1)
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Поля не заполнены", ButtonEnum.Ok).ShowAsync();
            return;
        }

        List<Employee> employees = DataBaseManager.GetEmployee();
      
        for (int i = 0; i < employees.Count; i++)
        {
            if (employees[i].Login.Contains(TBoxLogin.Text) &&
                employees[i].Password.Contains(TBoxPassword.Text))
            {
                _employeeAuth = employees[i];
                break;
            }
        }

        if (_employeeAuth == null)
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Данные не верны", ButtonEnum.Ok).ShowAsync();
            return;
        }
        else
        {
            MainWindow wMeny = new MainWindow(_employeeAuth);
            wMeny.Show();
            this.Hide();
        }
    }
    

    private void BtnClose_OnClick(object? sender, RoutedEventArgs e)
    {
        MainWindow wMeny = new MainWindow();
        wMeny.Show();
        this.Hide();
    }
}