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
        public Column Column;
        public Row(Column column, int index, String value)
        {
            Column = column;
            Index = index;
            this.Style = MainWindow.rowElementStyle;
            if (Column != null)
            {
                this.TextChanged += Column.Table.SetSelected;
                this.PreviewMouseDown += Column.Table.SetSelected;
            }
            this.Text = value;
        }
        public Row(Column column, int index) : this(column, index, "") { }
        public Row(int index) : this(null, index, "") { }
        public Row() : this(null, 0, "") { }
        ~Row()
        {
            this.PreviewMouseDown -= Column.Table.SetSelected;
            this.TextChanged -= Column.Table.SetSelected;
        }
    }
}
