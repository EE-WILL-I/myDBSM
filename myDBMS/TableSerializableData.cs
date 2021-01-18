using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myDBMS
{
    [Serializable]
    class TableSerializableData : List<string>
    {
        //private List<string> data = new List<string>();
        public TableSerializableData(Table t)
        {
            this.Add(t.ColCount.ToString());
            this.Add(t.RowCount.ToString());

            for (int i = 0; i < t.Children.Count - 1; i += 2)
                for (int j = 0; j < t.RowCount; j++)
                {
                    this.Add(i.ToString());
                    this.Add(j.ToString());
                    object child = ((Column)t.Children[i]).Children[j];
                    if (child is Row)
                        this.Add(((Row)child).Text);
                    else if (child is System.Windows.Controls.Label)
                        this.Add(((System.Windows.Controls.Label)child).Content as string);
                }
        }
        public Table Deserialize()
        {
            Table t = new Table();
            int colCount = System.Convert.ToInt32(this[0]);
            int rowCount = System.Convert.ToInt32(this[1]);

            t.AddColumn(colCount, rowCount);

            for(int i = 2; i < this.Count; i+=3)
            {
                int c = System.Convert.ToInt32(this[i]);
                int r = System.Convert.ToInt32(this[i  + 1]);
                string v = this[i + 2];
                t.SetValue(c, r, v);
            }

            return t;
        }
    }
}
