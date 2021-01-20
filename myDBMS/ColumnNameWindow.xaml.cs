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
        
        public string Value = "";
        public ColumnNameWindow(string oldName)
        {
            InitializeComponent();
            this.Owner = MainWindow.MW;
            MainWindow.MW.Closed += Exit;
            this.KeyDown += new KeyEventHandler(OnKeyPressedHandler);

            tb_input.Text = oldName;
            tb_input.Focus();
            tb_input.SelectAll();
        }
        public ColumnNameWindow() : this("Column") { }

        void OnKeyPressedHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.btn_accept(sender, e);
            }
            if(e.Key == Key.Escape)
            {
                this.btn_decline(sender, e);
            }
        }

        private void btn_accept(object sender, RoutedEventArgs e)
        {
            Value = tb_input.Text;
            Close(); 
        }

        private void btn_decline(object sender, RoutedEventArgs e)
        {
            Value = "";
            Close();
        }
        
        private void Exit(object sender, EventArgs e)
        {
            Close();
        }
    }
}
