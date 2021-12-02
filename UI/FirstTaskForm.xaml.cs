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
            int start = int.Parse(StartPageTextBox.Text);
            int end = int.Parse(EndPageTextBox.Text);
            parser.Settings = new iKnigiParserSettings(start, end);
            parser.Start();

        }

        private void AbortButton_Click(object sender, RoutedEventArgs e)
        {
            parser.Abort();
        }

        private void StartPageTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
            if (StartPageTextBox.Text.Length > 0)
            {
                if (Convert.ToInt32(StartPageTextBox.Text[0].ToString()) != 0)
                {
                    e.Handled = true;
                }
            }
            if (EndPageTextBox.Text.Length > 0)
            {
                if (e.Text[0] > Convert.ToInt32(EndPageTextBox.Text[0]))
                {
                    e.Handled = true;
                }
            }
        }

        private void EndPageTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
            if (EndPageTextBox.Text.Length > 0)
            {
                if (Convert.ToInt32(EndPageTextBox.Text[0].ToString()) != 0)
                {
                    e.Handled = true;
                }
            }
            if (StartPageTextBox.Text.Length > 0)
            {
                if (e.Text[0] < Convert.ToInt32(StartPageTextBox.Text[0]))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
