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
using System.Windows.Shapes;

namespace myDBMS
{
    /// <summary>
    /// Логика взаимодействия для ColumnNameWindow.xaml
    /// </summary>
    public partial class ColumnNameWindow : Window
    {
        public String Value;
        public ColumnNameWindow()
        {
            InitializeComponent();
        }

        private void btn_accept(object sender, RoutedEventArgs e)
        {
            Value = tb_input.Text;
            Close();
        }

        private void btn_decline(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
