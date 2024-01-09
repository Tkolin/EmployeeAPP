using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Excel = Microsoft.Office.Interop.Excel;
using Window = Avalonia.Controls.Window;

namespace EmployeeAPP.PagesAndWindows;

public partial class CardReportEmployeeWindow : Window
{
    public CardReportEmployeeWindow()
    {
        InitializeComponent();
    }

    private void BtnCrateCard_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void CreateCard()
    {
        var excelApp = new Excel.Application();
        // Make the object visible.
        excelApp.Visible = true;

        excelApp.Workbooks.Add("../Resursed/T-2FORM.xls");
        Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;
        workSheet.Cells[9, "B"] = "";
        
        workSheet.Cells[13, "C"] = "";
        workSheet.Cells[13, "D"] = "";
        workSheet.Cells[13, "E"] = "";
        workSheet.Cells[13, "F"] = "";
        workSheet.Cells[13, "G"] = "";
        workSheet.Cells[13, "H"] = "";
        workSheet.Cells[13, "I"] = "";
        
        workSheet.Cells[18, "I"] = "";
        
        workSheet.Cells[19, "I"] = "";
        
        workSheet.Cells[20, "C"] = "";
        workSheet.Cells[20, "F"] = "";
        workSheet.Cells[20, "I"] = "";
        
        workSheet.Cells[21, "D"] = "";
        
        workSheet.Cells[23, "D"] = "";
        workSheet.Cells[23, "I"] = "";
        
        workSheet.Cells[21, "D"] = "";
        workSheet.Cells[23, "I"] = "";
        
        workSheet.Cells[21, "D"] = "";
        workSheet.Cells[23, "I"] = "";
        workSheet.Cells[21, "D"] = "";
    }
}