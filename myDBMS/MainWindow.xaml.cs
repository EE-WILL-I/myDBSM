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

namespace myDBMS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
public partial class MainWindow : Window
    {
        
        public static Style rowStyle, rowElementStyle, buttonStyle, splitterStyle, labelStyle;
        public static Table MainTable = new Table();
        public static double WindowWidth, SplitterWidth = 3;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void InitializeStyles()
        {
            WindowWidth = this.Width;
            rowStyle = FindResource("ROW") as Style;
            rowElementStyle = FindResource("ROW_ELEMENT") as Style;
            buttonStyle = FindResource("BUTTON") as Style;
            splitterStyle = FindResource("SPLITTER") as Style;
            labelStyle = FindResource("LABEL") as Style;
        }

        private void sp_onInit(object sender, EventArgs e)
        {
            InitializeStyles();
            sp_table_main.Children.Add(MainTable);

            MainTable.AddColumn(1);
        }

        private void table_add_col(object sender, RoutedEventArgs e)
        {
            MainTable.AddColumn(MainTable.ColCount);
        }

        private void table_rmv_col(object sender, RoutedEventArgs e)
        {
            MainTable.RemoveColumn();
        }

        private void table_add_row(object sender, RoutedEventArgs e)
        {
            MainTable.AddRow();
        }

        private void table_rmv_row(object sender, RoutedEventArgs e)
        {
            MainTable.RemoveRow();
        }
    }
}
