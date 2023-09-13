using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Intrinsics.Arm;
using System.Windows.Documents;
using System.Windows.Media;
using CountersLibrary;

namespace CountersWPF
{
    public partial class MainWindow : Window
    {
        private const double PowerTarif = 4.44;
        private const double ColdTarif = 31.57;
        private const double HotTarif = 31.57;
        private const double DisposalTarif = 23.75;
        private const double GasTarif = 9.32;
        private User user;
        
        public MainWindow()
        {
            new ApplContext();
            InitializeComponent();
            /*User user = User.FindByID(1);
            user.Name = "Буторина Ольга Владимировна";
            user.Address = "г.Киров, ул.Лепсе, д.32, кв.93";
            User.Update();
            user = User.FindByID(2);
            user.Name = "Буторин Дэниель Михайлович";
            user.Address = "г.Киров, ул.Орджоникидзе, д.24, кв.67";
            User.Update();
            User user = new User("Буторина Ольга Владимировна", "г.Киров, ул.Лепсе, д.32, кв.93");
            User.Add(user);
            Tarif tarif = new Tarif(user,4.44, 31.57, 23.75, 31.57, 9.32);
            Tarif.Add(tarif);
            user = new User("Буторин Дэниель Михайлович", "г.Киров, ул.Орджоникидзе, д.24, кв.67");
            User.Add(user);
            tarif = new Tarif(user, 3.38, 34.41, 25.75, 202, 0);
            Tarif.Add(tarif);*/
        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = 0;
                if (UserBox.IsChecked == true)
                    id=2;
                else
                    id=1;
                user = User.FindByID(id);
                Record record = new Record(DateOnly.FromDateTime(DateTime.Today),
                    int.Parse(PowerBox.Text),
                    int.Parse(ColdBox.Text),
                    int.Parse(HotBox.Text),
                    int.Parse(GasBox.Text),user
                    );
                Record.Add(record);
                int powerDifference;
                int coldDifference;
                int hotDifference;
                int gasDifference;
                if (record.ID == 1)
                {
                    powerDifference = Record.GetLastRecordWithUserID(id).Power - Record.GetLastRecordWithUserID(id).Power;
                    coldDifference = Record.GetLastRecordWithUserID(id).Cold - Record.GetLastRecordWithUserID(id).Cold;
                    hotDifference = Record.GetLastRecordWithUserID(id).Hot - Record.GetLastRecordWithUserID(id).Hot;
                    gasDifference = Record.GetLastRecordWithUserID(id).Gas - Record.GetLastRecordWithUserID(id).Gas;
                }
                else
                {
                    powerDifference = Record.GetLastRecordWithUserID(id).Power - Record.GetBeforeLastRecordWithUserID(id).Power;
                    coldDifference = Record.GetLastRecordWithUserID(id).Cold - Record.GetBeforeLastRecordWithUserID(id).Cold;
                    hotDifference = Record.GetLastRecordWithUserID(id).Hot - Record.GetBeforeLastRecordWithUserID(id).Hot;
                    gasDifference = Record.GetLastRecordWithUserID(id).Gas - Record.GetBeforeLastRecordWithUserID(id).Gas;
                }

                if (powerDifference < 0)
                    powerDifference = 0;
                if (coldDifference < 0)
                    coldDifference = 0;
                if (hotDifference < 0)
                    hotDifference = 0;  
                if (gasDifference < 0)
                    gasDifference = 0;
                Cost cost = new Cost(record, 
                    powerDifference,
                    coldDifference,
                    hotDifference,
                    gasDifference,
                    Math.Round(Double.Parse(powerDifference.ToString())*PowerTarif,3),
                    Math.Round(Double.Parse(coldDifference.ToString())*ColdTarif+
                               Double.Parse(hotDifference.ToString())*HotTarif+
                               Double.Parse((coldDifference+hotDifference).ToString())*DisposalTarif,3),
                    Math.Round(Double.Parse(gasDifference.ToString())*GasTarif,3),
                    user
                );
                Cost.Add(cost);
                
                ResultBlock.Text = "";
                ResultBlock.Text = $"Дата - {record.Date}\n" +
                                   $"Электричество - {record.Power}; " +
                                   $"Холодная вода - {record.Cold}; " +
                                   $"Горячая вода - {record.Hot}; " +
                                   $"Газ - {record.Gas}\n\n";
                if(record.ID == 1)
                    ColoringText(cost,Cost.GetLastCostWithUserID(id));
                else
                    ColoringText(cost,Cost.GetBeforeLastCostWithUserID(id));
                PowerBox.Text = "";
                ColdBox.Text = "";
                HotBox.Text = "";
                GasBox.Text = "";
                ChequeMove();
            }
            catch
            {
                MessageBox.Show("Ошибка где-то случилась");
            }
            
        }

        private void ChequeBut_Click(object sender, RoutedEventArgs e)
        {
            ChequeMove();
        }
        
        private void LastPokazBut_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            if (UserBox.IsChecked == true)
                id=2;
            else
                id=1;
            Record rec = Record.GetLastRecordWithUserID(id);
            Cost cost = Cost.GetLastCostWithUserID(id);
            Cost beforeCost ;
            if(id==2 && rec.ID==34)
                beforeCost = Cost.GetLastCostWithUserID(id);
            else
                beforeCost = Cost.GetBeforeLastCostWithUserID(id);
            ResultBlock.Text = "";
            ResultBlock.Text = $"Дата - {rec.Date}\n" +
                               $"Адрес - {rec.User.Address}\n" +
                               $"Электричество - {rec.Power}; " +
                               $"Холодная вода - {rec.Cold}; " +
                               $"Горячая вода - {rec.Hot}; " +
                               $"Газ - {rec.Gas};\n\n";
            ColoringText(cost,beforeCost);
        }
        
        private void AllPokazBut_Click(object sender, RoutedEventArgs e)
        {
            List<Record> records = Record.GetAll();
            ResultBlock.Text = "";
            foreach (var item in records)
            {
                // Служебная переменная для записи строки
                string str = $"Дата - {item.Date}  " +
                             $"Адрес - {item.User.Address}; " +
                             $"Электричество - {item.Power}; " +
                             $"Холодная вода - {item.Cold}; " +
                             $"Горячая вода - {item.Hot}; " +
                             $"Газ - {item.Gas};\n";
                ResultBlock.Text += str;
            }
        }
        private void AllResourcesBut_Click(object sender, RoutedEventArgs e)
        {
            DopWindow dw = new DopWindow(UserBox.IsChecked);
            dw.Show();
        }

        public void ChequeMove()
        {
            // Путь к папке куда скачиваются чеки
            string pathForDownloads = @"C:\Users\denie\Downloads";
                
            // Путь к папке куда нужно переместить чеки. Конечная папка это папка с названием "Текущий год-текущий месяц"
            string mainPathForCheque = @$"C:\Users\denie\Desktop\Счётчики\Чеки\{DateTime.Today.Year}-{DateTime.Today.Month}";

            // Создаем регулярное выражение которое ищет файлы с названием начинающимся на "Dokument-текущий год-текущий месяц..."
            Regex reg = null;
            if (DateTime.Today.Month < 10)
                reg = new Regex(@$"Dokument-{DateTime.Today.Year}-0{DateTime.Today.Month}\S*");
            else
                reg = new Regex(@$"Dokument-{DateTime.Today.Year}-{DateTime.Today.Month}\S*");
                
            // Список всех файлов в папке куда скачиваются чеки (записи формата:"C:\\dir\file.txt)
            string[] dirs = Directory.GetFiles(pathForDownloads);


            // Проверка "Существует ли папка в которую нужно переместить чеки?"
            if (!Directory.Exists($"{mainPathForCheque}"))
                //Если не существует то создаем эту папку
                Directory.CreateDirectory($"{mainPathForCheque}");
            
            // Проходимся по всем файлам в папке куда скачались чеки
            for (int i = 0; i < dirs.Length; i++)
            {
                //Если название файла соответствует регулярному выражению то...
                if (reg.IsMatch(dirs[i]))
                {
                    // (Дополнительный массив в котором последний элемент это имя файла)
                    string[] str = dirs[i].Split(@"\");
                    // ...создаем переменную в которую запишем этот файл
                    FileInfo cheque = new FileInfo(dirs[i]);
                    // Перемещаем этот файл в нужную нам директорию
                    cheque.MoveTo(mainPathForCheque+@"\"+str[str.Length-1]);
                }
            }
        }

        public void ColoringText(Cost cost, Cost beforeCost)
        {
            #region Оформление строчек с потраченными ресурсами и оформлением разницы потраченных ресурсов
            if (cost.PowerSpend - beforeCost.PowerSpend > 0)
                ResultBlock.Text += $"Электричества потрачено - {cost.PowerSpend} КВт/ч. На {cost.PowerSpend - beforeCost.PowerSpend} КВт/ч больше, чем в предыдущем месяце\n" +
                                    $"Стоимость электричетсва - {cost.PowerCost} руб. На {Math.Round(cost.PowerCost - beforeCost.PowerCost,3)} руб больше, чем в предыдущем месяце\n\n";
            else
                ResultBlock.Text += $"Электричества потрачено - {cost.PowerSpend} КВт/ч. На {beforeCost.PowerSpend - cost.PowerSpend} КВт/ч меньше, чем в предыдущем месяце\n" +
                                    $"Стоимость электричетсва - {cost.PowerCost} руб. На {Math.Round(beforeCost.PowerCost - cost.PowerCost,3)} руб меньше, чем в предыдущем месяце\n\n";
            if (cost.ColdSpend - beforeCost.ColdSpend > 0)
                ResultBlock.Text += $"Холодной воды потрачено - {cost.ColdSpend} куб. м. На {cost.ColdSpend - beforeCost.ColdSpend} куб. м больше, чем в предыдущем месяце\n";
            else
                ResultBlock.Text += $"Холодной воды потрачено - {cost.ColdSpend} куб. м. На {beforeCost.ColdSpend - cost.ColdSpend} куб. м меньше, чем в предыдущем месяце\n";
            if (cost.HotSpend - beforeCost.HotSpend > 0)
                ResultBlock.Text += $"Горячей воды потрачено - {cost.HotSpend} куб. м. На {cost.HotSpend - beforeCost.HotSpend} куб. м больше, чем в предыдущем месяце\n" +
                                    $"Стоимость воды - {cost.WaterCost} руб. На {Math.Round(cost.WaterCost - beforeCost.WaterCost,3)} руб больше, чем в предыдущем месяце\n\n";
            else
                ResultBlock.Text += $"Горячей воды потрачено - {cost.HotSpend} куб. м. На {beforeCost.HotSpend - cost.HotSpend} куб. м меньше, чем в предыдущем месяце\n" +
                                    $"Стоимость воды - {cost.WaterCost} руб. На {Math.Round(beforeCost.WaterCost - cost.WaterCost,3)} руб меньше, чем в предыдущем месяце\n\n";
            if (cost.GasSpend - beforeCost.GasSpend > 0)
                ResultBlock.Text += $"Газа потрачено - {cost.GasSpend} куб. м. На {cost.GasSpend - beforeCost.GasSpend} куб. м больше, чем в предыдущем месяце\n" +
                                    $"Стоимость газа - {cost.GasCost} руб. На {Math.Round(cost.GasCost - beforeCost.GasCost,3)} руб больше, чем в предыдущем месяце\n\n";
            else
                ResultBlock.Text += $"Газа потрачено - {cost.GasSpend} куб. м. На {beforeCost.GasSpend - cost.GasSpend} куб. м меньше, чем в предыдущем месяце\n" +
                                    $"Стоимость газа - {cost.GasCost} руб. На {Math.Round(beforeCost.GasCost - cost.GasCost,3)} руб меньше, чем в предыдущем месяце\n\n";
            if (cost.PowerSpend - beforeCost.PowerSpend > 0)
            {
                TextManipulation.FromTextPointer(ResultBlock.ContentStart, ResultBlock.ContentEnd, $"На {cost.PowerSpend - beforeCost.PowerSpend} КВт/ч больше, чем в предыдущем месяце", FontStyle,FontWeight,Brushes.Crimson, null,FontSize=18);
                TextManipulation.FromTextPointer(ResultBlock.ContentStart, ResultBlock.ContentEnd, $"На {Math.Round(cost.PowerCost - beforeCost.PowerCost,3)} руб больше, чем в предыдущем месяце", FontStyle,FontWeight,Brushes.Crimson, null,FontSize=18);
            }
            else
            {
                TextManipulation.FromTextPointer(ResultBlock.ContentStart, ResultBlock.ContentEnd, $"На {beforeCost.PowerSpend - cost.PowerSpend} КВт/ч меньше, чем в предыдущем месяце", FontStyle,FontWeight,Brushes.Green, null,FontSize=18);
                TextManipulation.FromTextPointer(ResultBlock.ContentStart, ResultBlock.ContentEnd, $"На {Math.Round(beforeCost.PowerCost - cost.PowerCost,3)} руб меньше, чем в предыдущем месяце", FontStyle,FontWeight,Brushes.Green, null,FontSize=18);
            }
            if (cost.ColdSpend - beforeCost.ColdSpend > 0)     
                TextManipulation.FromTextPointer(ResultBlock.ContentStart, ResultBlock.ContentEnd, $"На {cost.ColdSpend - beforeCost.ColdSpend} куб. м больше, чем в предыдущем месяце", FontStyle,FontWeight,Brushes.Crimson, null,FontSize=18);
            else
                TextManipulation.FromTextPointer(ResultBlock.ContentStart, ResultBlock.ContentEnd, $"На {beforeCost.ColdSpend - cost.ColdSpend} куб. м меньше, чем в предыдущем месяце", FontStyle,FontWeight,Brushes.Green, null,FontSize=18);
            if (cost.HotSpend - beforeCost.HotSpend > 0)
            {
                TextManipulation.FromTextPointer(ResultBlock.ContentStart, ResultBlock.ContentEnd, $"На {cost.HotSpend - beforeCost.HotSpend} куб. м больше, чем в предыдущем месяце", FontStyle,FontWeight,Brushes.Crimson, null,FontSize=18);
                TextManipulation.FromTextPointer(ResultBlock.ContentStart, ResultBlock.ContentEnd, $"На {Math.Round(cost.WaterCost - beforeCost.WaterCost,3)} руб больше, чем в предыдущем месяце", FontStyle,FontWeight,Brushes.Crimson, null,FontSize=18);
            }
            else
            {
                TextManipulation.FromTextPointer(ResultBlock.ContentStart, ResultBlock.ContentEnd, $"На {beforeCost.HotSpend - cost.HotSpend} куб. м меньше, чем в предыдущем месяце", FontStyle,FontWeight,Brushes.Green, null,FontSize=18);
                TextManipulation.FromTextPointer(ResultBlock.ContentStart, ResultBlock.ContentEnd, $"На {Math.Round(beforeCost.WaterCost - cost.WaterCost,3)} руб меньше, чем в предыдущем месяце", FontStyle,FontWeight,Brushes.Green, null,FontSize=18);
            }

            if (cost.GasSpend - beforeCost.GasSpend > 0)
            {
                TextManipulation.FromTextPointer(ResultBlock.ContentStart, ResultBlock.ContentEnd, $"На {cost.GasSpend - beforeCost.GasSpend} куб. м больше, чем в предыдущем месяце", FontStyle,FontWeight,Brushes.Crimson, null,FontSize=18);
                TextManipulation.FromTextPointer(ResultBlock.ContentStart, ResultBlock.ContentEnd, $"На {Math.Round(cost.GasCost - beforeCost.GasCost,3)} руб больше, чем в предыдущем месяце", FontStyle,FontWeight,Brushes.Crimson, null,FontSize=18);
            }
            else
            {
                TextManipulation.FromTextPointer(ResultBlock.ContentStart, ResultBlock.ContentEnd, $"На {beforeCost.GasSpend - cost.GasSpend} куб. м меньше, чем в предыдущем месяце", FontStyle,FontWeight,Brushes.Green, null,FontSize=18);
                TextManipulation.FromTextPointer(ResultBlock.ContentStart, ResultBlock.ContentEnd, $"На {Math.Round(beforeCost.GasCost - cost.GasCost,3)} руб меньше, чем в предыдущем месяце", FontStyle,FontWeight,Brushes.Green, null,FontSize=18);
            }
            #endregion
        }
    }
    
    public class TextManipulation
    {
    /// <summary>
    /// Is manipulating a specific string inside of a TextPointer Range (TextBlock, TextBox...)
    /// </summary>
    /// <param name="startPointer">Starting point where to look</param>
    /// <param name="endPointer">Endpoint where to look</param>
    /// <param name="keyword">This is the string you want to manipulate</param>
    /// <param name="fontStyle">The new FontStyle</param>
    /// <param name="fontWeight">The new FontWeight</param>
    /// <param name="foreground">The new foreground</param>
    /// <param name="background">The new background</param>
    /// <param name="fontSize">The new FontSize</param>
    public static void FromTextPointer(TextPointer startPointer, TextPointer endPointer, string keyword, FontStyle fontStyle, FontWeight fontWeight, Brush foreground, Brush background, double fontSize)
    {
        FromTextPointer(startPointer, endPointer, keyword, fontStyle, fontWeight, foreground, background, fontSize, null);
    }

    /// <summary>
    /// Is manipulating a specific string inside of a TextPointer Range (TextBlock, TextBox...)
    /// </summary>
    /// <param name="startPointer">Starting point where to look</param>
    /// <param name="endPointer">Endpoint where to look</param>
    /// <param name="keyword">This is the string you want to manipulate</param>
    /// <param name="fontStyle">The new FontStyle</param>
    /// <param name="fontWeight">The new FontWeight</param>
    /// <param name="foreground">The new foreground</param>
    /// <param name="background">The new background</param>
    /// <param name="fontSize">The new FontSize</param>
    /// <param name="newString">The New String (if you want to replace, can be null)</param>
    public static void FromTextPointer(TextPointer startPointer, TextPointer endPointer, string keyword, FontStyle fontStyle, FontWeight fontWeight, Brush foreground, Brush background, double fontSize, string newString)
    {
        if(startPointer == null)throw new ArgumentNullException(nameof(startPointer));
        if(endPointer == null)throw new ArgumentNullException(nameof(endPointer));
        if(string.IsNullOrEmpty(keyword))throw new ArgumentNullException(keyword);

        TextRange text = new TextRange(startPointer, endPointer);
        TextPointer current = text.Start.GetInsertionPosition(LogicalDirection.Forward);
        while (current != null)
        {
            string textInRun = current.GetTextInRun(LogicalDirection.Forward);
            if (!string.IsNullOrWhiteSpace(textInRun))
            {
                int index = textInRun.IndexOf(keyword);
                if (index != -1)
                {
                    TextPointer selectionStart = current.GetPositionAtOffset(index,LogicalDirection.Forward);
                    TextPointer selectionEnd = selectionStart.GetPositionAtOffset(keyword.Length,LogicalDirection.Forward);
                    TextRange selection = new TextRange(selectionStart, selectionEnd);

                    if(!string.IsNullOrEmpty(newString))
                        selection.Text = newString;

                    selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
                    selection.ApplyPropertyValue(TextElement.FontStyleProperty, fontStyle);
                    selection.ApplyPropertyValue(TextElement.FontWeightProperty, fontWeight);
                    selection.ApplyPropertyValue(TextElement.ForegroundProperty, foreground);
                    selection.ApplyPropertyValue(TextElement.BackgroundProperty, background);
                }
            }
            current = current.GetNextContextPosition(LogicalDirection.Forward);
        }
    }
    }
}