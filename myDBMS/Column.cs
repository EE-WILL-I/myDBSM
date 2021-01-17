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
        public Column(int rowCount, String name)
        {
            ColumnName.Content = name;
            ColumnName.Style = MainWindow.labelStyle;
            ColumnName.MouseDoubleClick += MainWindow.ShowColNameWin;
            ColumnName.MouseDoubleClick += MainWindow.MainTable.SetSelected;
            this.Children.Add(ColumnName);

            for (int i = 0; i < rowCount; i++)
            {
                AddRow($"Row {i}");
            }
            this.SetValue(Grid.ColumnProperty, MainWindow.MainTable.ColumnDefinitions.Count - 1);
        }
        public Column(int rowCount) : this(rowCount, "Column") { }
        public Column(String name) : this(1, name) { }
        public Column() : this(0, "Column") { }
        ~Column() { ColumnName.MouseDoubleClick -= MainWindow.ShowColNameWin; }
        public void AddRow(String value)
        {
            TextBox textBox = new TextBox();
            textBox.Style = MainWindow.rowElementStyle;
            textBox.Text = value;
            textBox.PreviewMouseDown += MainWindow.MainTable.SetSelected;
            textBox.TextChanged += MainWindow.MainTable.SetSelected;
            this.Children.Add(textBox);
        }
        public void AddRow()
        {
            TextBox textBox = new TextBox();
            textBox.Style = MainWindow.rowElementStyle;
            textBox.Text = "Row " + (this.Children.Count - 1);
            textBox.PreviewMouseDown += MainWindow.MainTable.SetSelected;
            this.Children.Add(textBox);
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
    }

}
