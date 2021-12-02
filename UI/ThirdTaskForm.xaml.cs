using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using ParseLib.Core.PictureParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Логика взаимодействия для ThirdTaskForm.xaml
    /// </summary>
    public partial class ThirdTaskForm : Window
    {
        PictureParserWorker parser = new PictureParserWorker();
        public ThirdTaskForm()
        {
            InitializeComponent();
            parser = new PictureParserWorker();
            parser.UnsuccessfulDownload += parser_UnsuccessfulDownload;
            parser.OnCompleted += parser_OnComlited;
            parser.SuccessfulDownload += parser_SuccessfulDownload;
        }
        private void parser_OnComlited(object sender)
        {
            StatusListBox.Items.Add("Скачивание завершено!");
        }
        private void parser_SuccessfulDownload(object sender, string uri)
        {
            StatusListBox.Items.Add("Картинка: " + uri);
        }
        private void parser_UnsuccessfulDownload(object sender, string uri)
        {
            StatusListBox.Items.Add("Неудалось скачать картинку: " + uri);
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            parser.Settings.Subject = SubjectTextBox.Text;
            parser.Settings.Count = Convert.ToInt32(CountTextBox.Text);
            parser.StartPictureWorker();
        }
        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) || CountTextBox.Text.Length > 1)
            {   
                    e.Handled = true;
            }
            if (CountTextBox.Text != "")
            {
                if (Convert.ToInt32(CountTextBox.Text[0].ToString()) > 2)
                {
                    e.Handled = true;
                }
            }
        }

        private void PathButton_Click(object sender, RoutedEventArgs e)
        {
            using (CommonOpenFileDialog dialogDirectory = new CommonOpenFileDialog { IsFolderPicker = true })
            {
                parser.Settings.Path = dialogDirectory.ShowDialog() == CommonFileDialogResult.Ok ?
                                dialogDirectory.FileName : null;
                StatusListBox.Items.Add("Путь: " + parser.Settings.Path);
            }
        }
    }
}
