using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Assignment2019
{
    public partial class Spreadsheet_aadarsha : Form
    {
        //all the necessary attributes declaration
        FlowLayoutPanel flp; //flow layout panel variable declaration
        string[] cols = {"A","B","C","D","E","F","G", "H", "I", "J", "K", "L", "M", "N", "O", "P",
            "Q", "R","S", "T", "U", "V", "W", "X", "Y", "Z" };
        IDictionary<string, string> value_dictionary = new Dictionary<string, string>();
        IDictionary<string, string> formula = new Dictionary<string, string>();
        List<double> myvalues = new List<double>();
        IDictionary<string, string> calc_value = new Dictionary<string, string>();
        NumericalOp_AMS no = new NumericalOp_AMS();//NumericalOp_AMS object
        Grid_AMS gd = new Grid_AMS();//grid class object
        TextBox FB;//formulabar textbox object

        public Spreadsheet_aadarsha()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
            set_grpboxHeight();
            createTextbox();
            changeGroupBoxTop();
        }

        private void set_grpboxHeight()
        {
            groupBoxBottom.Height = this.Height - 150;

            groupBoxTop.Height = this.Height - groupBoxBottom.Height;
        }
    
        private void createTextbox()
        {
            gd.Width = 50;
            gd.Height = 48;
            flp= new FlowLayoutPanel();
            flp.Size=new Size(groupBoxBottom.Width, groupBoxBottom.Height); // width & height
            for (int i = 0; i<1; i++)
            {
                for (int j=0; j<26;j++)
                {
                    Label l = new Label();
                    l.Size = new Size(45, 20);
                    l.TextAlign = ContentAlignment.MiddleRight;
                    l.Font = new Font(l.Font, FontStyle.Bold);
                    l.Text = cols[j];
                    flp.Controls.Add(l);
                    if (j == 25)
                    {
                        flp.SetFlowBreak(l, true);
                    }
                } 
            }

            for (int i=1; i<27; i++)
            {
                Label l = new Label();
                l.Size = new Size(25, 20);
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Font = new Font(l.Font, FontStyle.Bold);
                l.Text = i.ToString();
                flp.Controls.Add(l);
                for (int j=0; j<26;j++)
                {
                    TextBox tb = new TextBox(); //column(j)=26 then flowlayout ko line break garney
                    tb.Margin = new Padding(0);
                    tb.Size = new Size(gd.Width, gd.Height);
                    tb.Name = cols[j] + (i).ToString();
                    flp.Controls.Add(tb); //Where to add {here in flowlayout} 
                    if (j == 25)
                    {
                        flp.SetFlowBreak(tb, true);
                    }
                    tb.Leave += Tb_Leave;
                    tb.TextChanged += Tb_TextChanged;
                    tb.Enter += Tb_Enter;
                }
            }
            groupBoxBottom.Controls.Add(flp); 
        }

        //Column name indicator
        private void Tb_Enter(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            TextBox tbcols = sender as TextBox;
            textBoxcell.Text = tbcols.Name;
        }

        // show textbox contents in the formula bar 
        private void Tb_TextChanged(object sender, EventArgs e)
        {
            
;            // throw new NotImplementedException();
            TextBox tb = sender as TextBox;
            string tb_contents = tb.Text.Trim();
            if (tb.Text != "" && tb.Text.Contains("="))
            {
                FB.Text = tb_contents;
            }
            else
            {
                FB.Text = "";
            }
        }

        private void changeGroupBoxTop()
        {
            FlowLayoutPanel grpboxT_panel = new FlowLayoutPanel();
            grpboxT_panel.Size = new Size(groupBoxTop.Width, groupBoxTop.Height);
            // generate chart button layout
            Button b = new Button();
            b.Margin = new Padding(0, 90, 0, 0);
            b.Size = new Size(150, 50);
            b.Text = "Generate barchart";
            b.Click += B_Click;
            
            // label for the formula bar
            Label l = new Label();
            l.Margin = new Padding(0, 95, 0, 30);
            l.Size = new Size(30, 30);
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.Font = new Font(FontFamily.GenericSansSerif,12, FontStyle.Italic);
            l.Text ="FX" ;

            //formulabar textbox layout
            FB = new TextBox();
            FB.Margin = new Padding(0, 98, 0, 0);
            FB.Size = new Size(600, 200);
            grpboxT_panel.Controls.Add(b);
            grpboxT_panel.Controls.Add(l);
            grpboxT_panel.Controls.Add(FB);
            groupBoxTop.Controls.Add(grpboxT_panel);
        }
     
        private void B_Click(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            new Barchart_aadarsha(value_dictionary).ShowDialog();
        }

        private void calc_add_value(string tb_name, string val)
        {
            calc_value.Add(tb_name, val);
        }

        private void Tb_Leave(object sender, EventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                string tb_contents = tb.Text.Trim();
                if (tb_contents != "")
                {
                    // adding value to the specific dictionary
                    if (tb.Text != "" && !tb.Text.Contains("="))//&& ! says "and not equals to". 
                    {
                        if (value_dictionary.ContainsKey(tb.Name))// value dictionary ma key paile dekhi cha vaney remove garxa (changing values of a textbox).
                        {
                            value_dictionary.Remove(tb.Name);
                        }
                        value_dictionary.Add(tb.Name, tb.Text);//  tb.name vaneko key ho and tb.text is the value of that specific textbox.
                    }
                    else
                    {
                        if (formula.ContainsKey(tb.Name))// yaha pani same gareko
                        {
                            formula.Remove(tb.Name);
                        }
                        formula.Add(tb.Name, tb.Text);
                    }

                    foreach (KeyValuePair<string, string> item in formula)//item vanney naya variable inside formula
                    {
                        string contents = item.Value;// formula dictionary bata formula chai item vanney variable ma aucha
                        
                        //SUM
                        if (contents.Contains("SUM") || contents.Contains("sum"))
                        {
                            //for columns
                            double sum = 0;
                            string start = contents.Substring(contents.IndexOf((char)32) + 1, 
                                contents.IndexOf(":") - (contents.IndexOf((char)32) + 1));// start variable ma contents lai break garera space(char 32) pachi ko value which is A lai store gareko

                            string end = contents.Substring(contents.IndexOf(":") + 1,
                                contents.Length - contents.IndexOf(":") - 1).Trim();// end variable ma same 
                            if (start.Substring(0, 1) == end.Substring(0, 1))
                            {
                                string ch = start.Substring(0, 1);
                                int start_index = int.Parse(start.Substring(1, start.Length - 1));
                                int end_index = int.Parse(end.Substring(1, end.Length - 1));

                                for (int i = start_index; i <= end_index; i++)
                                {
                                    try
                                    {
                                        string tb_name = ch + i.ToString();
                                        if (!value_dictionary.ContainsKey(tb_name))
                                        {
                                            throw new Null_exception_AMS();
                                        }
                                        string val = value_dictionary[tb_name];
                                        if (calc_value.ContainsKey(tb_name))
                                        {
                                            calc_value.Remove(tb_name);

                                        }
                                        calc_add_value(tb_name, val);
                                    }
                                    catch(Null_exception_AMS exc)
                                    {
                                        MessageBox.Show(exc.Message);
                                    }
                                } 
                                sum = no.sum(calc_value);
                                calc_value.Clear();

                                foreach (Control x in flp.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = sum.ToString();
                                    }
                                }
                            }
                            else
                            {
                                string start_ch = start.Substring(0, 1);
                                string end_ch = end.Substring(0, 1);
                                int row = int.Parse(start.Substring(1,start.Length-1));


                                int start_ch_index = Array.IndexOf(cols, start_ch);
                                int end_ch_index = Array.IndexOf(cols, end_ch);
                                for (int i = start_ch_index; i <= end_ch_index; i++)
                                {
                                    try
                                    {
                                        string tb_name = cols[i] + row.ToString();
                                        if (!value_dictionary.ContainsKey(tb_name))
                                        {
                                            throw new Null_exception_AMS();
                                        }
                                        string val = value_dictionary[tb_name];

                                        if (calc_value.ContainsKey(tb_name))
                                        {
                                            calc_value.Remove(tb_name);
                                        }
                                        calc_value.Add(tb_name, val);
                                    }
                                    catch (Null_exception_AMS exc)
                                    {
                                        MessageBox.Show(exc.Message);
                                    }
                                }
                                sum = no.sum(calc_value);
                                calc_value.Clear();
                            }
                            foreach (Control x in flp.Controls)
                            {
                                if (x.Name == item.Key)
                                {
                                    x.Text = sum.ToString();
                                }
                            }

                        }

                        //multiplication
                        if (contents.Contains("*"))
                        {
                            //for columns
                            double multi = 0;
                            string start = contents.Substring(contents.IndexOf((char)32) + 1,
                                contents.IndexOf("*") - (contents.IndexOf((char)32) + 1));// start variable 

                            string end = contents.Substring(contents.IndexOf("*") + 1, 
                                contents.Length - contents.IndexOf("*") - 1).Trim();// end variable ma same 
                            if (start.Substring(0, 1) == end.Substring(0, 1))
                            {
                                string ch = start.Substring(0, 1);
                                int start_index = int.Parse(start.Substring(1, start.Length-1));
                                int end_index = int.Parse(end.Substring(1, end.Length-1));
                                for (int i = start_index; i <= end_index; i++)
                                {
                                    try
                                    {
                                        string tb_name = ch + i.ToString();
                                        if (!value_dictionary.ContainsKey(tb_name))
                                        {
                                            throw new Null_exception_AMS();
                                        }
                                        string val = value_dictionary[tb_name];

                                        if (calc_value.ContainsKey(tb_name))
                                        {
                                            calc_value.Remove(tb_name);

                                        }
                                        calc_value.Add(tb_name, val);
                                    }
                                    catch (Null_exception_AMS exc)
                                    {
                                        MessageBox.Show(exc.Message);
                                    }

                                }
                                multi = no.mul(calc_value);
                                calc_value.Clear();

                                foreach (Control x in flp.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = multi.ToString();
                                    }
                                }
                            }
                            else
                            {
                                string start_ch = start.Substring(0, 1);
                                string end_ch = end.Substring(0, 1);
                                int row = int.Parse(start.Substring(1, start.Length-1));

                                int start_ch_index = Array.IndexOf(cols, start_ch);
                                int end_ch_index = Array.IndexOf(cols, end_ch);
                                for (int i = start_ch_index; i <= end_ch_index; i++)
                                {
                                    try
                                    {
                                        string tb_name = cols[i] + row.ToString();
                                        if (!value_dictionary.ContainsKey(tb_name))
                                        {
                                            throw new Null_exception_AMS();
                                        }
                                        string val = value_dictionary[tb_name];

                                        if (calc_value.ContainsKey(tb_name))
                                        {
                                            calc_value.Remove(tb_name);
                                        }
                                        calc_value.Add(tb_name, val);

                                    }
                                    catch (Null_exception_AMS exc)
                                    {
                                        MessageBox.Show(exc.Message);
                                    }

                                }
                                multi = no.mul(calc_value);
                                calc_value.Clear();
                            }
                            foreach (Control x in flp.Controls)
                            {
                                if (x.Name == item.Key)
                                {
                                    x.Text = multi.ToString();
                                }
                            }

                        }

                        //subtraction
                        if (contents.Contains("-"))
                        {
                            //for columns
                            double subt = 0;
                            string start = contents.Substring(contents.IndexOf((char)32) + 1, 
                                contents.IndexOf("-") - (contents.IndexOf((char)32) + 1));// start variable 
                            string end = contents.Substring(contents.IndexOf("-") + 1, 
                                contents.Length - contents.IndexOf("-") - 1).Trim();// end variable 
                            if (start.Substring(0, 1) == end.Substring(0, 1))
                            {
                                string ch = start.Substring(0, 1);
                                int start_index = int.Parse(start.Substring(1, start.Length-1));
                                int end_index = int.Parse(end.Substring(1, end.Length-1));

                                for (int i = start_index; i <= end_index; i++)
                                {
                                    try
                                    {
                                        string tb_name = ch + i.ToString();
                                        if (!value_dictionary.ContainsKey(tb_name))
                                        {
                                            throw new Null_exception_AMS();
                                        }
                                        string val = value_dictionary[tb_name];

                                        if (calc_value.ContainsKey(tb_name))
                                        {
                                            calc_value.Remove(tb_name);

                                        }
                                        calc_value.Add(tb_name, val);
                                    }
                                    catch (Null_exception_AMS exc)
                                    {
                                        MessageBox.Show(exc.Message);
                                    }

                                }
                                subt = no.sub(calc_value);
                                calc_value.Clear();

                                foreach (Control x in flp.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = subt.ToString();
                                    }
                                }
                            }
                            else
                            {
                                string start_ch = start.Substring(0, 1);
                                string end_ch = end.Substring(0, 1);
                                int row = int.Parse(start.Substring(1, start.Length-1));

                                int start_ch_index = Array.IndexOf(cols, start_ch);
                                int end_ch_index = Array.IndexOf(cols, end_ch);
                                for (int i = start_ch_index; i <= end_ch_index; i++)
                                {
                                    try
                                    {
                                        string tb_name = cols[i] + row.ToString();
                                        if (!value_dictionary.ContainsKey(tb_name))
                                        {
                                            throw new Null_exception_AMS();
                                        }
                                        string val = value_dictionary[tb_name];
                                        if (calc_value.ContainsKey(tb_name))
                                        {
                                            calc_value.Remove(tb_name);
                                        }
                                        calc_value.Add(tb_name, val);
                                    }
                                    catch (Null_exception_AMS exc)
                                    {
                                        MessageBox.Show(exc.Message);
                                    }
                                }
                                subt = no.sub(calc_value);
                                calc_value.Clear();
                            }
                            foreach (Control x in flp.Controls)
                            {
                                if (x.Name == item.Key)
                                {
                                    x.Text = subt.ToString();
                                }
                            }
                        }

                        //addition
                        if (contents.Contains("+"))
                        {
                            //for columns
                            double addt = 0;
                            string start = contents.Substring(contents.IndexOf((char)32) + 1, 
                                contents.IndexOf("+") - (contents.IndexOf((char)32) + 1));// start variable 
                            string end = contents.Substring(contents.IndexOf("+") + 1, 
                                contents.Length - contents.IndexOf("+") - 1).Trim();// end variable 
                            if (start.Substring(0, 1) == end.Substring(0, 1))
                            {
                                string ch = start.Substring(0, 1);
                                int start_index = int.Parse(start.Substring(1, start.Length-1));
                                int end_index = int.Parse(end.Substring(1, end.Length-1));

                                for (int i = start_index; i <= end_index; i++)
                                {
                                    try
                                    {
                                        string tb_name = ch + i.ToString();
                                        if (!value_dictionary.ContainsKey(tb_name))
                                        {
                                            throw new Null_exception_AMS();
                                        }
                                        string val = value_dictionary[tb_name];

                                        if (calc_value.ContainsKey(tb_name))
                                        {
                                            calc_value.Remove(tb_name);
                                        }
                                        calc_value.Add(tb_name, val);
                                    }
                                    catch (Null_exception_AMS exc)
                                    {
                                        MessageBox.Show(exc.Message);
                                    }

                                }
                                addt = no.sum(calc_value);
                                calc_value.Clear();   

                                foreach (Control x in flp.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = addt.ToString();
                                    }
                                }
                            }
                            else
                            {
                                string start_ch = start.Substring(0, 1);
                                string end_ch = end.Substring(0, 1);
                                int row = int.Parse(start.Substring(1, start.Length-1));

                                //MessageBox.Show(start_ch+""+end_ch);

                                int start_ch_index = Array.IndexOf(cols, start_ch);
                                int end_ch_index = Array.IndexOf(cols, end_ch);
                                for (int i = start_ch_index; i <= end_ch_index; i++)
                                {
                                    try
                                    {
                                        string tb_name = cols[i] + row.ToString();
                                        if (!value_dictionary.ContainsKey(tb_name))
                                        {
                                            throw new Null_exception_AMS();
                                        }
                                        string val = value_dictionary[tb_name];

                                        if (calc_value.ContainsKey(tb_name))
                                        {
                                            calc_value.Remove(tb_name);
                                        }
                                        calc_value.Add(tb_name, val);
                                    }
                                    catch (Null_exception_AMS exc)
                                    {
                                        MessageBox.Show(exc.Message);
                                    }
                                }
                                addt = no.sum(calc_value);
                                calc_value.Clear();
                            }
                            foreach (Control x in flp.Controls)
                            {
                                if (x.Name == item.Key)
                                {
                                    x.Text = addt.ToString();
                                }
                            }

                        }
                        //division
                        if (contents.Contains("/"))
                        {
                            //for columns
                            double div = 0;
                            string start = contents.Substring(contents.IndexOf((char)32) + 1, 
                                contents.IndexOf("/") - (contents.IndexOf((char)32) + 1)).Trim();// start variable 
                            string end = contents.Substring(contents.IndexOf("/") + 1, 
                                contents.Length - contents.IndexOf("/") - 1).Trim();// end variable 
                            if (start.Substring(0, 1) == end.Substring(0, 1))
                            {
                                string ch = start.Substring(0, 1);
                                int start_index = int.Parse(start.Substring(1, start.Length-1));
                                int end_index = int.Parse(end.Substring(1, end.Length-1));

                                for (int i = start_index; i <= end_index; i++)
                                {
                                    try
                                    {
                                        string tb_name = ch + i.ToString();
                                        if (!value_dictionary.ContainsKey(tb_name))
                                        {
                                            throw new Null_exception_AMS();
                                        }
                                        string val = value_dictionary[tb_name];

                                        if (calc_value.ContainsKey(tb_name))
                                        {
                                            calc_value.Remove(tb_name);
                                        }
                                        calc_value.Add(tb_name, val);
                                    }
                                    catch (Null_exception_AMS exc)
                                    {
                                        MessageBox.Show(exc.Message);
                                    }
                                }
                                div = no.div(calc_value);
                                calc_value.Clear();

                                foreach (Control x in flp.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = div.ToString();
                                    }
                                }
                            }
                            else
                            {
                                string start_ch = start.Substring(0, 1);
                                string end_ch = end.Substring(0, 1);
                                int row = int.Parse(start.Substring(1, start.Length-1));

                                int start_ch_index = Array.IndexOf(cols, start_ch);
                                int end_ch_index = Array.IndexOf(cols, end_ch);
                                for (int i = start_ch_index; i <= end_ch_index; i++)
                                {
                                    try
                                    {
                                        string tb_name = cols[i] + row.ToString();
                                        if (!value_dictionary.ContainsKey(tb_name))
                                        {
                                            throw new Null_exception_AMS();
                                        }
                                        string val = value_dictionary[tb_name];
                                        if (calc_value.ContainsKey(tb_name))
                                        {
                                            calc_value.Remove(tb_name);
                                        }
                                        calc_value.Add(tb_name, val);
                                    }
                                    catch (Null_exception_AMS exc)
                                    {
                                        MessageBox.Show(exc.Message);
                                    }
                                }
                                div = no.div(calc_value);
                                calc_value.Clear();
                            }
                            foreach (Control x in flp.Controls)
                            {
                                if (x.Name == item.Key)
                                {
                                    x.Text = div.ToString();
                                }
                            }

                        }


                        //MEAN 
                        if (contents.Contains("MEAN") || contents.Contains("mean"))
                        {
                            //for columns
                            double mean = 0;
                            string start = contents.Substring(contents.IndexOf((char)32) + 1,
                                contents.IndexOf(":") - (contents.IndexOf((char)32) + 1));// start variable 
                            string end = contents.Substring(contents.IndexOf(":") + 1,
                                contents.Length - contents.IndexOf(":") - 1).Trim();// end variable ma same 
                            if (start.Substring(0, 1) == end.Substring(0, 1))
                            {
                                string ch = start.Substring(0, 1);
                                int start_index = int.Parse(start.Substring(1, start.Length-1));
                                int end_index = int.Parse(end.Substring(1, end.Length-1));

                                for (int i = start_index; i <= end_index; i++)
                                {
                                   try
                                   {
                                        string tb_name = ch + i.ToString();
                                        if (!value_dictionary.ContainsKey(tb_name))
                                        {
                                            throw new Null_exception_AMS();
                                        }
                                        string val = value_dictionary[tb_name];

                                        if (calc_value.ContainsKey(tb_name))
                                        {
                                            calc_value.Remove(tb_name);

                                        }
                                        calc_add_value(tb_name, val);

                                   }

                                   catch (Null_exception_AMS exc)
                                   {
                                      MessageBox.Show(exc.Message);
                                   }

                                }
                                mean = no.Mean(calc_value);
                                calc_value.Clear();

                                foreach (Control x in flp.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = mean.ToString();
                                    }
                                }
                            }
                            else
                            {
                                string start_ch = start.Substring(0, 1);
                                string end_ch = end.Substring(0, 1);
                                int row = int.Parse(start.Substring(1, start.Length-1));

                                int start_ch_index = Array.IndexOf(cols, start_ch);
                                int end_ch_index = Array.IndexOf(cols, end_ch);
                                for (int i = start_ch_index; i <= end_ch_index; i++)
                                {
                                      try
                                      {
                                            string tb_name = cols[i] + row.ToString();
                                            if (!value_dictionary.ContainsKey(tb_name))
                                            {
                                                throw new Null_exception_AMS();
                                            }
                                            string val = value_dictionary[tb_name];
                                            if (calc_value.ContainsKey(tb_name))
                                            {
                                                calc_value.Remove(tb_name);
                                            }
                                            calc_add_value(tb_name, val);
                                      }
                                      catch (Null_exception_AMS exc)
                                      {
                                            MessageBox.Show(exc.Message);
                                      }

                                }
                                mean = no.Mean(calc_value);
                                calc_value.Clear();
                            }
                            foreach (Control x in flp.Controls)
                            {
                                if (x.Name == item.Key)
                                {
                                    x.Text = mean.ToString();
                                }
                            }
                        }

                        // AVERAGE
                        if (contents.Contains("AVERAGE") || contents.Contains("average"))
                        {
                            //for columns
                            double avg = 0;
                            string start = contents.Substring(contents.IndexOf((char)32) + 1, 
                                contents.IndexOf(":") - (contents.IndexOf((char)32) + 1));// start variable 
                            string end = contents.Substring(contents.IndexOf(":") + 1,
                                contents.Length - contents.IndexOf(":") - 1).Trim();// end variable ma same 
                            if (start.Substring(0, 1) == end.Substring(0, 1))
                            {
                                string ch = start.Substring(0, 1);
                                int start_index = int.Parse(start.Substring(1, start.Length - 1));
                                int end_index = int.Parse(end.Substring(1, end.Length - 1));

                                for (int i = start_index; i <= end_index; i++)
                                {
                                    try
                                    {
                                        string tb_name = ch + i.ToString();
                                        if (!value_dictionary.ContainsKey(tb_name))
                                        {
                                            throw new Null_exception_AMS();
                                        }
                                        string val = value_dictionary[tb_name];

                                        if (calc_value.ContainsKey(tb_name))
                                        {
                                            calc_value.Remove(tb_name);
                                        }
                                        calc_add_value(tb_name, val);
                                    }
                                    catch (Null_exception_AMS exc)
                                    {
                                        MessageBox.Show(exc.Message);
                                    }

                                }
                                avg = no.Average(calc_value);
                                calc_value.Clear();

                                foreach (Control x in flp.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = avg.ToString();
                                    }
                                }
                            }
                            else
                            {
                                string start_ch = start.Substring(0, 1);
                                string end_ch = end.Substring(0, 1);
                                int row = int.Parse(start.Substring(1, start.Length - 1));

                                int start_ch_index = Array.IndexOf(cols, start_ch);
                                int end_ch_index = Array.IndexOf(cols, end_ch);
                                for (int i = start_ch_index; i <= end_ch_index; i++)
                                {
                                    try
                                    {
                                        string tb_name = cols[i] + row.ToString();
                                        if (!value_dictionary.ContainsKey(tb_name))
                                        {
                                            throw new Null_exception_AMS();
                                        }
                                        string val = value_dictionary[tb_name];
                                        if (calc_value.ContainsKey(tb_name))
                                        {
                                            calc_value.Remove(tb_name);
                                        }
                                        calc_value.Add(tb_name, val);
                                    }
                                    catch (Null_exception_AMS exc)
                                    {
                                        MessageBox.Show(exc.Message);
                                    }
                                }
                                avg = no.Average(calc_value);
                                calc_value.Clear();
                            }
                            foreach (Control x in flp.Controls)
                            {
                                if (x.Name == item.Key)
                                {
                                    x.Text = avg.ToString();
                                }
                            }
                        }

                        //MODE
                        if (contents.Contains("MODE") || contents.Contains("mode"))
                        {
                            //for columns
                            double mode = 0;
                            string start = contents.Substring(contents.IndexOf((char)32) + 1,
                                contents.IndexOf(":") - (contents.IndexOf((char)32) + 1));// start variable
                            string end = contents.Substring(contents.IndexOf(":") + 1,
                                contents.Length - contents.IndexOf(":") - 1).Trim();// end variable ma same 
                            if (start.Substring(0, 1) == end.Substring(0, 1))
                            {
                                string ch = start.Substring(0, 1);
                                int start_index = int.Parse(start.Substring(1, start.Length-1));
                                int end_index = int.Parse(end.Substring(1, end.Length-1));
                                for (int i = start_index; i <= end_index; i++)
                                {
                                    try
                                    {
                                        string tb_name = ch + i.ToString();
                                        if (!value_dictionary.ContainsKey(tb_name))
                                        {
                                            throw new Null_exception_AMS();
                                        }
                                        string val = value_dictionary[tb_name];
                                        if (calc_value.ContainsKey(tb_name))
                                        {
                                            calc_value.Remove(tb_name);
                                        }
                                        myvalues.Add(double.Parse(val));
                                    }
                                    catch (Null_exception_AMS exc)
                                    {
                                        MessageBox.Show(exc.Message);
                                    }

                                }
                                mode = no.Mode(myvalues);
                                myvalues.Clear();

                                foreach (Control x in flp.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = mode.ToString();
                                    }
                                }
                            }
                            else
                            {
                                string start_ch = start.Substring(0,1);
                                string end_ch = end.Substring(0,1);
                                int row = int.Parse(start.Substring(1, start.Length - 1));

                                int start_ch_index = Array.IndexOf(cols, start_ch);
                                int end_ch_index = Array.IndexOf(cols, end_ch);
                                for (int i = start_ch_index; i <= end_ch_index; i++)
                                {
                                    try
                                    {
                                        string tb_name = cols[i] + row.ToString();
                                        if (!value_dictionary.ContainsKey(tb_name))
                                        {
                                            throw new Null_exception_AMS();
                                        }
                                        string val = value_dictionary[tb_name];
                                        if (calc_value.ContainsKey(tb_name))
                                        {
                                            calc_value.Remove(tb_name);
                                        }
                                        myvalues.Add(double.Parse(val));
                                    }
                                    catch (Null_exception_AMS exc)
                                    {
                                        MessageBox.Show(exc.Message);
                                    }
                                }
                                mode = no.Mode(myvalues);
                                myvalues.Clear();
                            }
                            foreach (Control x in flp.Controls)
                            {
                                if (x.Name == item.Key)
                                {
                                    x.Text = mode.ToString();
                                }
                            }
                        }

                        //MEDIAN
                        if (contents.Contains("MEDIAN") || contents.Contains("median"))
                        {
                            //for columns
                            double median = 0;
                            string start = contents.Substring(contents.IndexOf((char)32) + 1,
                                contents.IndexOf(":") - (contents.IndexOf((char)32) + 1));// start variable 
                            string end = contents.Substring(contents.IndexOf(":") + 1, 
                                contents.Length - contents.IndexOf(":") - 1).Trim();// end variable ma same 
                            if (start.Substring(0, 1) == end.Substring(0, 1))
                            {
                                string ch = start.Substring(0, 1);
                                int start_index = int.Parse(start.Substring(1, start.Length-1));
                                int end_index = int.Parse(end.Substring(1, end.Length-1));

                                for (int i = start_index; i <= end_index; i++)
                                {
                                    try
                                    {
                                        string tb_name = ch + i.ToString();
                                        if (!value_dictionary.ContainsKey(tb_name))
                                        {
                                            throw new Null_exception_AMS();
                                        }
                                        string val = value_dictionary[tb_name];
                                        if (calc_value.ContainsKey(tb_name))
                                        {
                                            calc_value.Remove(tb_name);
                                        }
                                        myvalues.Add(double.Parse(val));
                                    }
                                    catch (Null_exception_AMS exc)
                                    {
                                        MessageBox.Show(exc.Message);
                                    }
                                }
                                median = no.Median(myvalues);
                                myvalues.Clear();

                                foreach (Control x in flp.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = median.ToString();
                                    }
                                }
                            }
                            else
                            {
                                string start_ch = start.Substring(0,1);
                                string end_ch = end.Substring(0,1);
                                int row = int.Parse(start.Substring(1, start.Length - 1));

                                //MessageBox.Show(start_ch+""+end_ch);

                                int start_ch_index = Array.IndexOf(cols, start_ch);
                                int end_ch_index = Array.IndexOf(cols, end_ch);
                                for (int i = start_ch_index; i <= end_ch_index; i++)
                                {
                                    try
                                    {

                                        string tb_name = cols[i] + row.ToString();
                                        if (!value_dictionary.ContainsKey(tb_name))
                                        {
                                            throw new Null_exception_AMS();
                                        }
                                        string val = value_dictionary[tb_name];

                                        if (calc_value.ContainsKey(tb_name))
                                        {
                                            calc_value.Remove(tb_name);
                                        }
                                        myvalues.Add(double.Parse(val));
                                    }
                                    catch (Null_exception_AMS exc)
                                    {
                                        MessageBox.Show(exc.Message);
                                    }

                                }
                                median = no.Median(myvalues);
                                myvalues.Clear();
                            }
                            foreach (Control x in flp.Controls)
                            {
                                if (x.Name == item.Key)
                                {
                                    x.Text = median.ToString();
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        
    }
}
