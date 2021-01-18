using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Text;
using System.Threading.Tasks;

namespace myDBMS
{
    public class Row : TextBox
    {
        public int Index = 0;
        public Row(int index, String value)
        {
            Index = index;
            this.Style = MainWindow.rowElementStyle;
            this.PreviewMouseDown += MainWindow.MainTable.SetSelected;
            this.TextChanged += MainWindow.MainTable.SetSelected;
            this.Text = value;
        }
        public Row(int index) : this(index, "") { }
        public Row() : this(0, "") { }
        ~Row()
        {
            this.PreviewMouseDown -= MainWindow.MainTable.SetSelected;
            this.TextChanged -= MainWindow.MainTable.SetSelected;
        }
    }
}
