using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Linq;
using System.Windows.Controls;
using System.Xaml;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using CountersLibrary;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using Axis = LiveCharts.Wpf.Axis;

namespace CountersWPF;

public partial class DopWindow : Window
{
    // ID - позволяет указывать для какого адреса будут показываться графики
    // List<Int> ...Value - листы для хранения 12-ти последних записей по использованию ресурса (Имя ресурса в названии)
    // List<Cost> - список всех записей "Cost" хранящихся в базе данных
    public static int ID = 0;
    public static List<Cost> costs = Cost.GetAll();
    public static List<int> PowerValue = new List<int>();
    public static List<int> ColdValue = new List<int>();
    public static List<int> HotValue = new List<int>();
    public static List<int> GasValue = new List<int>();
    
    public DopWindow(bool? userBoxChecked)
    {
        InitializeComponent();
        
        // "Если чекбокс отмечен, ID присваивается значение 2" (графики будут показаны для адреса Орджоникидзе)
        if (userBoxChecked==true)
            ID=2;
        // "Иначе значение 1" (графики будут показаны для адреса Лепсе)
        else
            ID=1;
        
        // Получаем все записи "Cost" из БД
        costs = Cost.GetAllWithID(ID);
        
        // "Если записей больше 12, то берутся только последние 12" (реализовано в цикле for)
        if(costs.Count>=12)
            for (int i = 12; i >= 1; i--)
            {
                // Записываем полученные данные в соответствующие листы
                PowerValue.Add(costs[costs.Count-i].PowerSpend);
                ColdValue.Add(costs[costs.Count-i].ColdSpend);
                HotValue.Add(costs[costs.Count-i].HotSpend);
                GasValue.Add(costs[costs.Count-i].GasSpend);
            }
        // "Иначе берем все записи которые есть" (также реализовано в цикле for)
        else
            for (int i = costs.Count; i >= 1; i--)
            {
                // Записываем полученные данные в соответствующие листы
                PowerValue.Add(costs[costs.Count-i].PowerSpend);
                ColdValue.Add(costs[costs.Count-i].ColdSpend);
                HotValue.Add(costs[costs.Count-i].HotSpend);
                GasValue.Add(costs[costs.Count-i].GasSpend);
            }
        
        // Что-то
        DataContext = this;
        
        // Заполение resultBoxа
        ResultBox.Text = "";
        foreach (var item in costs)
            ResultBox.Text += item.ToString();
        PowerValue = new List<int>();
        ColdValue = new List<int>();
        HotValue = new List<int>();
        GasValue = new List<int>();
    }
    
    public ISeries[] PowerSeries { get; set; } =
    {
        new ColumnSeries<int>
        {
            Name = "Электричество",
            Values = PowerValue,
            //TooltipLabelFormatter = (chartPoint) => $"{chartPoint.Context.Series.Name}: {chartPoint.PrimaryValue} Куб.м.",
            TooltipLabelFormatter = (chartPoint) => $"{chartPoint.PrimaryValue} КВт/ч",
            Fill = new SolidColorPaint(new SKColor(175,151,0)),
        },
    };
    public ISeries[] ColdSeries { get; set; } =
    {
        new ColumnSeries<int>
        {
            Name = "Холодная вода",
            Values = ColdValue,
            //TooltipLabelFormatter = (chartPoint) => $"{chartPoint.Context.Series.Name}: {chartPoint.PrimaryValue} Куб.м."
            TooltipLabelFormatter = (chartPoint) => $"{chartPoint.PrimaryValue} Куб.м.",
            Fill = new SolidColorPaint(new SKColor(4,128,158)),
        },
    };
    public ISeries[] HotSeries { get; set; } =
    {
        new ColumnSeries<int>
        {
            Name = "Горячая вода",
            Values = HotValue,
            //TooltipLabelFormatter = (chartPoint) => $"{chartPoint.Context.Series.Name}: {chartPoint.PrimaryValue} Куб.м."
            TooltipLabelFormatter = (chartPoint) => $"{chartPoint.PrimaryValue} Куб.м.",
            Fill = new SolidColorPaint(new SKColor(205,73,0)),
        },
    };
    public ISeries[] GasSeries { get; set; } =
    {
        new ColumnSeries<int>
        {
            Name = "Газ",
            Values = GasValue,
            //TooltipLabelFormatter = (chartPoint) => $"{chartPoint.Context.Series.Name}: {chartPoint.PrimaryValue} Куб.м."
            TooltipLabelFormatter = (chartPoint) => $"{chartPoint.PrimaryValue} Куб.м.",
            Fill = new SolidColorPaint(new SKColor(14,72,169
            )),
        },
    };

    public Axis[] XAxes { get; set; } =
    {
        new Axis
        {
            Labels = new[] {
                costs[costs.Count-12].RRecord.Date.Month+"-"+costs[costs.Count-12].RRecord.Date.Year,
                costs[costs.Count-11].RRecord.Date.Month+"-"+costs[costs.Count-11].RRecord.Date.Year,
                costs[costs.Count-10].RRecord.Date.Month+"-"+costs[costs.Count-10].RRecord.Date.Year,
                costs[costs.Count-9].RRecord.Date.Month+"-"+costs[costs.Count-9].RRecord.Date.Year,
                costs[costs.Count-8].RRecord.Date.Month+"-"+costs[costs.Count-8].RRecord.Date.Year,
                costs[costs.Count-7].RRecord.Date.Month+"-"+costs[costs.Count-7].RRecord.Date.Year,
                costs[costs.Count-6].RRecord.Date.Month+"-"+costs[costs.Count-6].RRecord.Date.Year,
                costs[costs.Count-5].RRecord.Date.Month+"-"+costs[costs.Count-5].RRecord.Date.Year,
                costs[costs.Count-4].RRecord.Date.Month+"-"+costs[costs.Count-4].RRecord.Date.Year,
                costs[costs.Count-3].RRecord.Date.Month+"-"+costs[costs.Count-3].RRecord.Date.Year,
                costs[costs.Count-2].RRecord.Date.Month+"-"+costs[costs.Count-2].RRecord.Date.Year,
                costs[costs.Count-1].RRecord.Date.Month+"-"+costs[costs.Count-1].RRecord.Date.Year
                
            }
        }
    };
}