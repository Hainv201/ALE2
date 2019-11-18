using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ALE2
{
    public partial class Form1 : Form
    {
        Parser parser;
        string fileName;
        public Form1()
        {
            InitializeComponent();
        }

        private void browse_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            fileName = ofd.FileName;
            file_name.Text = Path.GetFileName(fileName);
            read_file.Enabled = true;
        }

        private void show_graph_Click(object sender, EventArgs e)
        {
            try
            {
                string str = parser.CreateGraph();
                File.WriteAllText(@"graph.dot", str);

                Process dot = new Process();
                dot.StartInfo.FileName = @"dot.exe";
                dot.StartInfo.Arguments = "-Tpng -ograph.png graph.dot";
                dot.Start();
                dot.WaitForExit();

                Process.Start(@"graph.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void read_file_Click(object sender, EventArgs e)
        {
            try
            {
                parser = new Parser(fileName);
                if (parser.IsDFA)
                {
                    dfa_value.Text = "YES";
                }
                else
                {
                    dfa_value.Text = "NO";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            show_graph.Enabled = true;
        }

        private void clear_form_Click(object sender, EventArgs e)
        {
            dfa_value.Text = "dfa_value";
            file_name.Text = "";
        }
    }
}
