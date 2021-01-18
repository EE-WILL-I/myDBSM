using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Text;
using System.Threading.Tasks;

namespace myDBMS
{
    public class Table : Grid
    {
        public int ColCount = 0, RowCount = 0;
        public Row SelectedRow;
        public Column SelectedColumn;

        public Table() { }
        public void AddColumn(int rowCount)
        {
            if (this.ColumnDefinitions.Count == 0) AddColumnDifinition();
            Column column = new Column(rowCount, rowCount, $"Column {this.ColCount}");
            column.Index = this.Children.Count;
            this.Children.Add(column);
            AddColumnDifinition();
            AddSplitter();
            AddColumnDifinition(); // add empty to have ability to resize the last column width

            //RowCount += rowCount;
            ColCount++;
        }

        public void RemoveColumn(int index)
        {
            int size = this.Children.Count;
            if (size >= 2 && index < size)
            {
                this.Children.RemoveAt(index);
                this.Children.RemoveAt(index - 1);
                RemoveColumnDifinition();
                RemoveColumnDifinition();
                RemoveColumnDifinition();
                AddColumnDifinition();

                ColCount--;
            }
        }

        public void AddRow(int count)
        {
            for (int i = 0; i < count; i++)
            {
                foreach (Object child in this.Children) if (child is Column) ((Column)child).AddRow();
                RowCount++;
            }
        }

        public void AddRow(int count, string name)
        {
            for (int i = 0; i < count; i++)
            {
                foreach (Object child in this.Children) if (child is Column) ((Column)child).AddRow(name);
                RowCount++;
            }
        }

        public void RemoveRow(int index)
        {
            if (index >= 0)
            {
                foreach (Object child in this.Children) if (child is Column) ((Column)child).RemoveRow(index);
                if(index > 0) RowCount--;
            }
        }

        private void AddColumnDifinition()
        {
            ColumnDefinition cd = new ColumnDefinition();
            cd.Width = GridLength.Auto;
            this.ColumnDefinitions.Add(cd);
        }

        private void RemoveColumnDifinition()
        {
            int size = this.ColumnDefinitions.Count;
            if (size > 0)
            {
                this.ColumnDefinitions.RemoveAt(size - 1);
            }
        }

        private void AddSplitter()
        {
            GridSplitter gs = new GridSplitter();
            gs.Style = MainWindow.splitterStyle;
            gs.Width = MainWindow.SplitterWidth;
            gs.SetValue(Grid.ColumnProperty, this.ColumnDefinitions.Count - 1);
            this.Children.Add(gs);
        }

        public void SetSelected(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                SelectedColumn = ((Label)sender).Parent as Column;
                if(SelectedColumn.Children.Count > 1) SelectedRow = SelectedColumn.Children[1] as Row;
            }
            else
            {
                SelectedRow = (Row)sender;
                SelectedColumn = ((TextBox)SelectedRow).Parent as Column;
            }
            Console.WriteLine($"selected row: {SelectedRow?.Index}, col: {SelectedColumn?.Index / 2}");
        }
        public void SetSelected(int col, int row)
        {
            if (col % 2 == 1) col--;
            SelectedColumn = this.Children[col] as Column;
            SelectedRow = SelectedColumn.Children[row] as Row;
        }
        public void SelectRightColumn()
        {
            if (SelectedColumn.Index + 2 < this.Children.Count)
            {
                SelectedColumn = this.Children[SelectedColumn.Index + 2] as Column;
                SelectedRow = SelectedColumn.Children[SelectedRow.Index] as Row;
                SelectedRow.Focus();
            }
        }
        public void SelectLeftColumn()
        {
            if (SelectedColumn.Index - 2 >= 0)
            {
                SelectedColumn = this.Children[SelectedColumn.Index - 2] as Column;
                SelectedRow = SelectedColumn.Children[SelectedRow.Index] as Row;
                SelectedRow.Focus();
            }
        }
    }
}
