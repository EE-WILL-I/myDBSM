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
        public Table Table;
        public Column(Table table, int index, int rowCount, String name)
        {
            Table = table;
            ColumnName.Content = name;
            ColumnName.Style = MainWindow.labelStyle;
            if (Table != null) ColumnName.MouseDoubleClick += Table.SetSelected;
            ColumnName.MouseDoubleClick += MainWindow.ShowColNameWin;
            this.Children.Add(ColumnName);

            for (int i = 0; i < rowCount; i++)
            {
                AddRow();
            }
            this.SetValue(Grid.ColumnProperty, Table.ColumnDefinitions.Count - 1);
        }
        public Column(int rowCount) : this(null, 0, rowCount, "Column") { }
        public Column(String name) : this(null, 0, 1, name) { }
        public Column() : this(null, 0, 0, "Column") { }
        ~Column() { 
            ColumnName.MouseDoubleClick -= MainWindow.ShowColNameWin;
        }
        public void AddRow(String value)
        {
            this.Children.Add(new Row(this, this.Children.Count, value));
        }
        public void AddRow()
        {
            this.Children.Add(new Row(this, this.Children.Count));
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
            if (Table.SelectedRow != null && Table.SelectedRow.Index + 1 < this.Children.Count)
            {
                Table.SelectedRow = this.Children[Table.SelectedRow.Index + 1] as Row;
                Table.SelectedRow.Focus();
            }
        }
        public void SelectPrewRow()
        {
            if (Table.SelectedRow != null && Table.SelectedRow.Index - 1 > 0)
            {
                Table.SelectedRow = this.Children[Table.SelectedRow.Index - 1] as Row;
                Table.SelectedRow.Focus();
            }
        }
    }

}
