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
        public class Column : StackPanel
        {
            //StackPanel mainTable;
            List<TextBox> rows;
            public Column(int rowCount)
            {
                //table = sp_table_main;
                rows = new List<TextBox>();
                for (int i = 0; i < rowCount; i++)
                {
                    TextBox textBox = new TextBox();
                    textBox.Style = MainWindow.rowElementStyle;
                    //textBox.Width = ((MainWindow.WindowWidth - 36) / rowCount) - ((rowCount - 1) * SplitterWidth);
                    textBox.Text = "Row " + i; 
                    rows.Add(textBox);
                }
                this.SetValue(Grid.ColumnProperty, Columns.ColumnDefinitions.Count - 1);
                foreach(TextBox tb in rows)
                    this.Children.Add(tb);
            }
            public Column() : this(1) 
            {

            }
        }
        public static Style rowStyle, rowElementStyle, buttonStyle, splitterStyle;
        public static Grid Columns;
        public static double WindowWidth, SplitterWidth = 5;
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
        }

        private void sp_onInit(object sender, EventArgs e)
        {
            InitializeStyles();
            Columns = sp_table_main.Children[0] as Grid;

            AddColumn(1);
        }

        public void AddColumn(int rowCount)
        {
            AddColumnDifinition();
            Columns.Children.Add(new Column(rowCount));
            AddColumnDifinition();
            AddSplitter();
        }

        public void RemoveColumn()
        {
            int size = Columns.Children.Count;
            if (size >= 2)
            {
                Columns.Children.RemoveAt(size - 1);
                Columns.Children.RemoveAt(size - 2);
                RemoveColumnDifinition();
            }
        }

        private void AddColumnDifinition()
        {
            ColumnDefinition cd = new ColumnDefinition();
            cd.Width = GridLength.Auto;
            Columns.ColumnDefinitions.Add(cd);
        }

        private void RemoveColumnDifinition()
        {
            int size = Columns.Children.Count;
            if (size >= 2)
            {
                Columns.ColumnDefinitions.RemoveAt(size - 1);
                Columns.ColumnDefinitions.RemoveAt(size - 2);
            }
        }

        private void AddSplitter()
        {
            GridSplitter gs = new GridSplitter();
            gs.Style = splitterStyle;
            gs.Width = SplitterWidth;
            gs.SetValue(Grid.ColumnProperty, Columns.ColumnDefinitions.Count - 1);
            Columns.Children.Add(gs);
        }

        private void table_add_col(object sender, RoutedEventArgs e)
        {
            AddColumn(1);
        }

        private void table_rmv_col(object sender, RoutedEventArgs e)
        {
            RemoveColumn();
        }
    }
}
