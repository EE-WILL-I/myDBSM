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
public partial class MainWindow : Window
    {
        public static MainWindow MW;
        public static Style rowStyle, rowElementStyle, buttonStyle, splitterStyle, labelStyle;
        public static Table MainTable = new Table();
        public static double WindowWidth, SplitterWidth = 3;
        private static ColumnNameWindow colNameWin;
        private static string PathToSavedData = "C://table1.tbl";
        public MainWindow()
        {
            InitializeComponent();
            MW = this;
            this.PreviewKeyUp += new KeyEventHandler(OnKeyPressedHandler);
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

            MainTable.AddColumn(0);
            MainTable.AddRow(1, "Row 0");
        }

        private void table_add_col(object sender, RoutedEventArgs e)
        {
            MainTable.AddColumn(MainTable.RowCount);
        }

        private void table_rmv_col(object sender, RoutedEventArgs e)
        {
            MainTable.RemoveColumn(MainTable.Children.Count - 1);
        }

        private void menu_new_table(object sender, RoutedEventArgs e)
        {
            sp_table_main.Children.RemoveAt(0);
            MainTable = new Table();
            sp_table_main.Children.Add(MainTable);
        }

        private void menu_load_table(object sender, RoutedEventArgs e)
        {
            sp_table_main.Children.RemoveAt(0);

            var fileDialog = new System.Windows.Forms.OpenFileDialog();
            var result = fileDialog.ShowDialog();
            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:
                    {
                        PathToSavedData = fileDialog.FileName;
                        TableSerializableData tsd = BinarySerializator.Read<TableSerializableData>(PathToSavedData);
                        MainTable = null;
                        MainTable = tsd.Deserialize();
                        break;
                    }
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    break;
            }
            sp_table_main.Children.Add(MainTable);
        }

        private void menu_save_table(object sender, RoutedEventArgs e)
        {
            sp_table_main.Children.RemoveAt(0);

            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Filter = "Table file (*.tbl)|*.tbl";
            var result = saveFileDialog.ShowDialog();
            PathToSavedData = saveFileDialog.FileName;

            Console.WriteLine("Saving to " + PathToSavedData);

            BinarySerializator.Write<TableSerializableData>(PathToSavedData, new TableSerializableData(MainTable));

            sp_table_main.Children.Add(MainTable);
        }

        private void table_add_row(object sender, RoutedEventArgs e)
        {
            MainTable.AddRow(1);
        }

        private void table_rmv_row(object sender, RoutedEventArgs e)
        {
            MainTable.RemoveRow(MainTable.RowCount);
        }
        public static void ShowColNameWin(object sender, EventArgs e)
        {
            colNameWin = new ColumnNameWindow();
            colNameWin.ColName = (string)MainTable.SelectedColumn.ColumnName.Content;
            colNameWin.Closed += SetColName;
            colNameWin.ShowDialog();
        }
        private static void SetColName(object sender, EventArgs e)
        {
            if (!colNameWin.Value.Equals("") && MainTable.SelectedColumn != null)
                MainTable.SelectedColumn.ColumnName.Content = colNameWin.Value;
            colNameWin.Closed -= SetColName;
        }
        private void OnKeyPressedHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                case Key.Down:
                    {
                        MainTable.SelectedColumn?.SelectNextRow();
                        break;
                    }
                case Key.Up:
                    {
                        MainTable.SelectedColumn?.SelectPrewRow();
                        break;
                    }
            }
            if (e.Key == Key.Enter && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                table_add_row(sender, e);
            }
            else if (e.Key == Key.Right && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                MainTable.SelectRightColumn();
            }
            else if (e.Key == Key.Left && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                MainTable.SelectLeftColumn();
            }
            else if (e.Key == Key.Back && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                table_rmv_row(sender, e);
            }
            else if (e.Key == Key.Enter && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                table_add_col(sender, e);
            }
            else if (e.Key == Key.Back && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                table_rmv_col(sender, e);
            }
        }
    }
}