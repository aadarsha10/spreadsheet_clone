using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace Assignment2019
{
    public partial class Barchart_aadarsha : Form
    {
        IDictionary<string, string> aadarsha = new Dictionary<string, string>();

        public Barchart_aadarsha(IDictionary<string, string> value)
        {
            InitializeComponent();  
            this.aadarsha = value;
        }

        private void Barchart_aadarsha_Load(object sender, EventArgs e)
        {
            Series s1 = new Series();
            Chart_aadarsha.Series.Add(s1);
            Chart_aadarsha.Series["Series1"].Points.DataBindXY(aadarsha.Keys, 
                aadarsha.Values);
        }

        
    }
}
