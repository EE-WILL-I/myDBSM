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
        public static Style rowStyle, rowElementStyle, buttonStyle, splitterStyle;
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
            splitterStyle = FindResource("SPLITTER") as Style;
        }

        private void sp_onInit(object sender, EventArgs e)
        {
            InitializeStyles();
            Grid columns = sp_table_main.Children[0] as Grid;

            ColumnDefinition cd1 = new ColumnDefinition(), cd2 = new ColumnDefinition();
            cd1.Width = GridLength.Auto;
            cd2.Width = GridLength.Auto;

            GridSplitter gs = new GridSplitter();
            gs.Style = splitterStyle;
            columns.ColumnDefinitions.Add(cd1);
            gs.SetValue(Grid.ColumnProperty, columns.ColumnDefinitions.Count - 1);
            columns.Children.Add(gs);

            StackPanel sp = new StackPanel();
            TextBox tb = new TextBox();
            tb.Style = rowElementStyle;
            sp.Children.Add(tb);
            columns.ColumnDefinitions.Add(cd2);
            sp.SetValue(Grid.ColumnProperty, columns.ColumnDefinitions.Count - 1);
            columns.Children.Add(sp);
        }

        private void table_add_col(object sender, RoutedEventArgs e)
        {
            TableRow row = new TableRow(sp_table_main.Children.Count + 1);
            sp_table_main.Children.Add(row);
        }

        private void table_rmv_col(object sender, RoutedEventArgs e)
        {
            int size = sp_table_main.Children.Count;
            if(size > 0)
                sp_table_main.Children.RemoveAt(size - 1);
        }
    }
}
