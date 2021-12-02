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

namespace UI
{
    /// <summary>
    /// Логика взаимодействия для StartForm.xaml
    /// </summary>
    public partial class StartForm : Window
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void FirstTaskButton_Click(object sender, RoutedEventArgs e)
        {
            FirstTaskForm FTF = new FirstTaskForm();
            FTF.Show();
        }
        private void SecondTaskButton_Click(object sender, RoutedEventArgs e)
        {
            SecondTaskForm FTF = new SecondTaskForm();
            FTF.Show();
        }

        private void ThirdTaskButton_Click(object sender, RoutedEventArgs e)
        {
            ThirdTaskForm FTF = new ThirdTaskForm();
            FTF.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
