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
        public class TableRow : StackPanel
        {
            //StackPanel mainTable;
            List<TextBox> columns;
            public TableRow(int colCount)
            {
                //table = sp_table_main;
                columns = new List<TextBox>();
                for (int i = 0; i < colCount; i++)
                {
                    TextBox textBox = new TextBox();
                    textBox.Style = MainWindow.rowElementStyle;
                    textBox.Width = (MainWindow.WINDOW_WIDTH - 36) / colCount;
                    textBox.Text = "Row " + columns.Count; 
                    columns.Add(textBox);
                }
                this.Style = rowStyle;
                foreach(TextBox tb in columns)
                    this.Children.Add(tb);
            }
            public TableRow() : this(1) 
            {

            }
        }
        public static Style rowStyle, rowElementStyle, buttonStyle;
        public static double WINDOW_WIDTH;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void InitializeStyles()
        {
            WINDOW_WIDTH = this.Width;
            rowStyle = FindResource("ROW") as Style;
            rowElementStyle = FindResource("ROW_ELEMENT") as Style;
            buttonStyle = FindResource("BUTTON") as Style;
        }

        private void sp_onInit(object sender, EventArgs e)
        {
            InitializeStyles();
            TableRow row = new TableRow(1);
            sp_table_main.Children.Add(row);
        }

        private void table_add_row(object sender, RoutedEventArgs e)
        {
            TableRow row = new TableRow(sp_table_main.Children.Count + 1);
            sp_table_main.Children.Add(row);
        }

        private void table_rmv_row(object sender, RoutedEventArgs e)
        {
            int size = sp_table_main.Children.Count;
            if(size > 0)
                sp_table_main.Children.RemoveAt(size - 1);
        }
    }
}
