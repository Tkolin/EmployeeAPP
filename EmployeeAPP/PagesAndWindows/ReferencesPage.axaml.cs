using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EmployeeAPP.Model;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace EmployeeAPP.PagesAndWindows;

public partial class ReferencesPage : Window
{

    
    private List<Passport> _DataPassport { get; set; }
    private List<Passport> _ViewPassport { get; set; }
    
    private List<Position> _DataPosition { get; set; }
    private List<Position> _ViewPosition { get; set; }
    
    private List<Unit> _DataUnit { get; set; }
    private List<Unit> _ViewUnit { get; set; }
    
    private List<EmployeeToWork> _DataEmployeeToWork { get; set; }
    private List<EmployeeToWork> _ViewEmployeeToWork { get; set; }
    
    public ReferencesPage()
    {
        InitializeComponent();
        DownloadDataGrid();
    }
    public void DownloadDataGrid()
    {
        _DataPassport = DataBaseManager.GetPassport();
        _DataPosition = DataBaseManager.GetPosition();
        _DataUnit = DataBaseManager.GetUnit();
        _DataEmployeeToWork = DataBaseManager.GetEmployeeToWork();
        
        _ViewPassport = _DataPassport;
        _ViewPosition = _DataPosition;
        _ViewUnit = _DataUnit;
        _ViewEmployeeToWork = _DataEmployeeToWork;
        
        DataGridPassport.ItemsSource = _ViewPassport;
        DataGridPosition.ItemsSource = _ViewPosition;
        DataGridUnit.ItemsSource = _ViewUnit;
      
    }
}