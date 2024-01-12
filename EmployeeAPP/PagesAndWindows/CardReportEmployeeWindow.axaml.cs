using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using EmployeeAPP.Model;
using Window = Avalonia.Controls.Window;

namespace EmployeeAPP.PagesAndWindows;

public partial class CardReportEmployeeWindow : Window
{
    private Employee _employee { get; set; }
    private EmployeeToWork _employeeToWork { get; set; }
    public CardReportEmployeeWindow(Employee employee, EmployeeToWork employeeToWork)
    {
        InitializeComponent();
        _employee = employee;
        _employeeToWork = employeeToWork;
    }

    private void BtnCrateCard_OnClick(object? sender, RoutedEventArgs e)
    {
        CreateCard();
    }

    private void CreateCard()
    {
        T2Form wind = new T2Form(_employee, _employeeToWork );
        wind.ShowDialog(this);



    }
}