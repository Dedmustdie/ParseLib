using ParseLib.Core;
using ParseLib.Core.HabrParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Логика взаимодействия для FirstTaskForm.xaml
    /// </summary>
    public partial class FirstTaskForm : Window
    {
        iKnigiParserWorker<List<string>> parser;
        public FirstTaskForm()
        {
            InitializeComponent();
            parser = new iKnigiParserWorker<List<string>>(new iKnigiParser());
            parser.OnCompleted += parser_OnCompleted;
            parser.OnNewData += parser_OnNewData;
        }
        private void parser_OnCompleted(object sender)
        {
            ParsingTextBox.Text += "Парсинг завершен!";
        }
        private void parser_OnNewData(object sender, List<string> args)
        {
            foreach (string item in args)
            {
                ParsingTextBox.Text += item + "\n";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ParsingTextBox.Text = "";           
            int start = int.Parse(StartPageIntBox.Text);
            int end = int.Parse(EndPageIntBox.Text);
            parser.Settings = new iKnigiSettings(start, end);
            parser.Start();

        }

        private void AbortButton_Click(object sender, RoutedEventArgs e)
        {
            parser.Abort();
        }
    }
}
