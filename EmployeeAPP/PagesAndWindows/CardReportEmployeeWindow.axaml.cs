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
    public CardReportEmployeeWindow(Employee employee)
    {
        InitializeComponent();
        _employee = employee;
    }

    private void BtnCrateCard_OnClick(object? sender, RoutedEventArgs e)
    {
        CreateCard();
    }

    private void CreateCard()
    {
        T2Form wind = new T2Form();
        wind.ShowDialog(this);



    }
}