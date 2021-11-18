using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ParseLib.Core.YandexParser;
namespace UI
{
    /// <summary>
    /// Логика взаимодействия для SecondTaskForm.xaml
    /// </summary>
    public partial class SecondTaskForm : Window
    {
        YandexParserWorker parser;
        public SecondTaskForm()
        {
            InitializeComponent();
            parser = new YandexParserWorker(new YandexParser());
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
        
       
        private void AbortButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            ParsingTextBox.Text = "";
            parser.Settings = new YandexSettings(CategoryTextBox.Text);
            //parser.StartCategoriesWorker();
            parser.StartArticlesWorker();
        }

        private void StartArtButton_Click(object sender, RoutedEventArgs e)
        {
            ParsingTextBox.Text = "";
            parser.Settings = new YandexSettings(CategoryTextBox.Text);
            parser.StartArticlesWorker();
        }
    }
}
