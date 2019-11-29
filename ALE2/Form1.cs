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
        Parser parser = new Parser();
        string fileName;
        public Form1()
        {
            InitializeComponent();
        }

        private void browse_file_Click(object sender, EventArgs e)
        {
            ClearForm();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            fileName = ofd.FileName;
            file_name.Text = Path.GetFileName(fileName);
            if (file_name.Text != "")
            {
                read_file.Enabled = true;
            }
        }

        private void show_graph_Click(object sender, EventArgs e)
        {
            try
            {
                string str = parser.Automaton.CreateGraph();
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
                result_data.Columns.Clear();
                result_data.Rows.Clear();
                parser.ParsingFile(fileName);
                if (parser.IsDFA)
                {
                    is_Dfa.Text = "YES";
                    is_Dfa.BackColor = Color.LimeGreen;
                }
                else
                {
                    is_Dfa.Text = "NO";
                    is_Dfa.BackColor = Color.Red;
                }
                result_data.Columns.Add("Word","Word");
                result_data.Columns.Add("Is Accepted?", "Is Accepted?");
                foreach (Word word in parser.Automaton.ListWords)
                {
                    result_data.Rows.Add(word.Words,word.IsAccepted);
                }
                show_graph.Enabled = true;
                bt_word.Enabled = true;
                bt_add_comment.Enabled = true;
                bt_create_text_file.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clear_form_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void parse_notation_Click(object sender, EventArgs e)
        {
            try
            {
                string prefix = prefix_notation.Text;
                if (String.IsNullOrWhiteSpace(prefix))
                {
                    parser.ParsingPrefix(prefix_notation.Text);
                    Regular_Expression regular_Expression = parser.GetExpression();
                    tbRE.Text = regular_Expression.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearForm()
        {
            result_data.Columns.Clear();
            result_data.Rows.Clear();
            file_name.Text = "";
            is_Dfa.Text = "";
            is_Dfa.BackColor = Color.White;
            read_file.Enabled = false;
            show_graph.Enabled = false;
            bt_word.Enabled = false;
        }

        private void bt_word_Click(object sender, EventArgs e)
        {
            Word word = new Word(tB_word.Text);
            parser.Automaton.ListWords.Add(word);
            result_data.Rows.Add(word.Words, word.IsWordAccepted(parser.Automaton.ListStates,parser.Automaton.ListTransitions));
        }

        private void bt_add_comment_Click(object sender, EventArgs e)
        {
            parser.Automaton.Comment = tB_comment.Text;
        }

        private void bt_create_text_file_Click(object sender, EventArgs e)
        {
            parser.Automaton.CreateTextFile();
        }
    }
}
