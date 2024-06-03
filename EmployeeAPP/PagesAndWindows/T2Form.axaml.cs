using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using Avalonia.VisualTree;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Interactivity;
using EmployeeAPP.Model;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace EmployeeAPP.PagesAndWindows;

public partial class T2Form : Window
{
    public T2Form(Employee employee ,EmployeeToWork employeeToWork )
    {
        InitializeComponent();
        TBoxLastName.Text = employee.Last_Name;
        TBoxPatronymic.Text = employee.Patronymic;
        TBoxFirstName.Text = employee.First_Name;
        TBoxPoint.Text = employee.PassportAdress;
        TBoxPosition.Text = DataBaseManager.GetPosition().Where(p=>p.ID == employeeToWork.Position_ID).First().Name;
        TBoxUnit.Text = DataBaseManager.GetUnit().Where(p=>p.ID == employeeToWork.Unit_ID).First().Name;
        TBoxIDWork.Text = employeeToWork.ID.ToString();
        TBoxDateWork.Text = employeeToWork.DateCreateEmployment.ToString("yy-MM-dd");
        TBoxDateBirth.Text = employee.Birth_Date.ToString("yy-MM-dd");
        
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        AvaloniaUI.PrintToPDF.Print.ToFile(
            "app_screenshot.pdf",
            this
        );    
    }
}