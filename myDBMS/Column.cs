using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Text;
using System.Threading.Tasks;

namespace myDBMS
{
    public class Column : StackPanel
    {
        public int Index;
        public Label ColumnName = new Label();
        public Column(int index, int rowCount, String name)
        {
            ColumnName.Content = name;
            ColumnName.Style = MainWindow.labelStyle;
            ColumnName.MouseDoubleClick += MainWindow.MainTable.SetSelected;
            ColumnName.MouseDoubleClick += MainWindow.ShowColNameWin;
            this.Children.Add(ColumnName);

            for (int i = 0; i < rowCount; i++)
            {
                AddRow();
            }
            this.SetValue(Grid.ColumnProperty, MainWindow.MainTable.ColumnDefinitions.Count - 1);
        }
        public Column(int rowCount) : this(0, rowCount, "Column") { }
        public Column(String name) : this(0, 1, name) { }
        public Column() : this(0, 0, "Column") { }
        ~Column() { ColumnName.MouseDoubleClick -= MainWindow.ShowColNameWin; }
        public void AddRow(String value)
        {
            this.Children.Add(new Row(this.Children.Count, value));
        }
        public void AddRow()
        {
            this.Children.Add(new Row(this.Children.Count));
        }
        public void RemoveRow(int index)
        {
            if (index > 0 && index < this.Children.Count)
                this.Children.RemoveAt(index);
        }
        public void RemoveRow()
        {
            RemoveRow(this.Children.Count - 1);
        }
        public void SelectNextRow()
        {
            if (MainWindow.MainTable.SelectedRow != null && MainWindow.MainTable.SelectedRow.Index + 1 < this.Children.Count)
            {
                MainWindow.MainTable.SelectedRow = this.Children[MainWindow.MainTable.SelectedRow.Index + 1] as Row;
                MainWindow.MainTable.SelectedRow.Focus();
            }
        }
        public void SelectPrewRow()
        {
            if (MainWindow.MainTable.SelectedRow != null && MainWindow.MainTable.SelectedRow.Index - 1 > 0)
            {
                MainWindow.MainTable.SelectedRow = this.Children[MainWindow.MainTable.SelectedRow.Index - 1] as Row;
                MainWindow.MainTable.SelectedRow.Focus();
            }
        }
    }

}
