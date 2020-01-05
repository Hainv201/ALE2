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
        Automaton automaton, tmp;
        public Form1()
        {
            InitializeComponent();
            result_data.Columns.Add("Word", "Word");
            result_data.Columns.Add("Is Accepted?", "Is Accepted?");
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
                parse_notation.Enabled = false;
                prefix_notation.Enabled = false;
            }
        }

        private void show_graph_Click(object sender, EventArgs e)
        {
            try
            {
                string str = automaton.CreateGraph();
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
                parser.ParsingFile(fileName);
                automaton = parser.Automaton;
                ShowInfo();
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
                if (!String.IsNullOrWhiteSpace(prefix))
                {
                    parser.ParsingPrefix(prefix);
                    Regular_Expression regular_Expression = parser.GetExpression();
                    tbRE.Text = regular_Expression.ToString();
                    List<Transition> transitions = new List<Transition>();
                    List<State> states = new List<State>();
                    int i = 0;
                    List<Alphabet> alphabets = new List<Alphabet>();
                    regular_Expression.GetAutomaton(ref i, ref transitions, ref states, ref alphabets);
                    automaton = new Automaton(alphabets, states, transitions,null,null);
                    ShowInfo();
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
            tB_Isfinite.Text = "";
            tB_Isfinite.BackColor = Color.White;
            read_file.Enabled = false;
            show_graph.Enabled = false;
            bt_word.Enabled = false;
            bt_add_comment.Enabled = false;
            bt_create_text_file.Enabled = false;
            parse_notation.Enabled = true;
            browse_file.Enabled = true;
            prefix_notation.Enabled = true;
            tB_word.Text = "";
            tbRE.Text = "";
            tB_comment.Text = "";
            prefix_notation.Text = "";
            result_data.Columns.Add("Word", "Word");
            result_data.Columns.Add("Is Accepted?", "Is Accepted?");
            ConvertToDFA.Checked = false;
            ConvertToDFA.Enabled = false;
            NormalForm.Checked = false;
            NormalForm.Enabled = false;
            tmp = null;
        }

        private void ShowInfo()
        {
            result_data.Columns.Clear();
            result_data.Rows.Clear();
            if (automaton.IsDFA)
            {
                is_Dfa.Text = "YES";
                is_Dfa.BackColor = Color.LimeGreen;
            }
            else
            {
                is_Dfa.Text = "NO";
                is_Dfa.BackColor = Color.Red;
                ConvertToDFA.Enabled = true;
            }
            if (automaton.IsFinite)
            {
                tB_Isfinite.Text = "YES";
                tB_Isfinite.BackColor = Color.LimeGreen;
            }
            else
            {
                tB_Isfinite.Text = "NO";
                tB_Isfinite.BackColor = Color.Red;
            }
            result_data.Columns.Add("Word", "Word");
            result_data.Columns.Add("Is Accepted?", "Is Accepted?");
            foreach (Word word in automaton.ListWords)
            {
                result_data.Rows.Add(word.Words, word.IsAccepted);
            }
            show_graph.Enabled = true;
            bt_word.Enabled = true;
            bt_add_comment.Enabled = true;
            bt_create_text_file.Enabled = true;
            NormalForm.Enabled = true;
        }
        private void bt_word_Click(object sender, EventArgs e)
        {
            string words = tB_word.Text;
            if (!parser.DoesWordContainIncorrectCharacter(words,automaton.ListAlphabets))
            {
                Word word = new Word(tB_word.Text);
                automaton.ListWords.Add(word);
                word.IsWordAccepted(automaton.ListStates,automaton.ListTransitions);
                result_data.Rows.Add(word.Words, word.IsAccepted);
            }
            else
            {
                MessageBox.Show("Incorrect Input");
            }
        }

        private void bt_add_comment_Click(object sender, EventArgs e)
        {
            automaton.Comment = tB_comment.Text;
        }

        private void bt_create_text_file_Click(object sender, EventArgs e)
        {
            automaton.CreateTextFile();
        }

        private void ConvertToDFA_CheckedChanged(object sender, EventArgs e)
        {
            if (ConvertToDFA.Checked)
            {
                tmp = automaton;
                automaton = automaton.ConvertToDFA();
                ShowInfo();
            }
        }

        private void NormalForm_CheckedChanged(object sender, EventArgs e)
        {
            if (NormalForm.Checked)
            {
                if (tmp!= null)
                {
                    automaton = tmp;
                }
                ShowInfo();
            }
        }
    }
}
