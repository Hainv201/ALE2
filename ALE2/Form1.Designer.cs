namespace ALE2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.file_name = new System.Windows.Forms.TextBox();
            this.browse_file = new System.Windows.Forms.Button();
            this.read_file = new System.Windows.Forms.Button();
            this.show_graph = new System.Windows.Forms.Button();
            this.clear_form = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.prefix_notation = new System.Windows.Forms.TextBox();
            this.tbRE = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.is_Dfa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.result_data = new System.Windows.Forms.DataGridView();
            this.parse_notation = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tB_word = new System.Windows.Forms.TextBox();
            this.bt_word = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tB_comment = new System.Windows.Forms.TextBox();
            this.bt_create_text_file = new System.Windows.Forms.Button();
            this.bt_add_comment = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.result_data)).BeginInit();
            this.SuspendLayout();
            // 
            // file_name
            // 
            this.file_name.Enabled = false;
            this.file_name.Location = new System.Drawing.Point(124, 6);
            this.file_name.Name = "file_name";
            this.file_name.ReadOnly = true;
            this.file_name.Size = new System.Drawing.Size(276, 20);
            this.file_name.TabIndex = 0;
            // 
            // browse_file
            // 
            this.browse_file.Location = new System.Drawing.Point(406, 6);
            this.browse_file.Name = "browse_file";
            this.browse_file.Size = new System.Drawing.Size(75, 35);
            this.browse_file.TabIndex = 1;
            this.browse_file.Text = "Browse";
            this.browse_file.UseVisualStyleBackColor = true;
            this.browse_file.Click += new System.EventHandler(this.browse_file_Click);
            // 
            // read_file
            // 
            this.read_file.Enabled = false;
            this.read_file.Location = new System.Drawing.Point(487, 47);
            this.read_file.Name = "read_file";
            this.read_file.Size = new System.Drawing.Size(75, 35);
            this.read_file.TabIndex = 2;
            this.read_file.Text = "Read File";
            this.read_file.UseVisualStyleBackColor = true;
            this.read_file.Click += new System.EventHandler(this.read_file_Click);
            // 
            // show_graph
            // 
            this.show_graph.Enabled = false;
            this.show_graph.Location = new System.Drawing.Point(487, 88);
            this.show_graph.Name = "show_graph";
            this.show_graph.Size = new System.Drawing.Size(75, 35);
            this.show_graph.TabIndex = 3;
            this.show_graph.Text = "Graph";
            this.show_graph.UseVisualStyleBackColor = true;
            this.show_graph.Click += new System.EventHandler(this.show_graph_Click);
            // 
            // clear_form
            // 
            this.clear_form.Location = new System.Drawing.Point(487, 6);
            this.clear_form.Name = "clear_form";
            this.clear_form.Size = new System.Drawing.Size(75, 35);
            this.clear_form.TabIndex = 6;
            this.clear_form.Text = "Clear";
            this.clear_form.UseVisualStyleBackColor = true;
            this.clear_form.Click += new System.EventHandler(this.clear_form_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Prefix Notation:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Regular Expressions:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "File Name:";
            // 
            // prefix_notation
            // 
            this.prefix_notation.Location = new System.Drawing.Point(124, 58);
            this.prefix_notation.Name = "prefix_notation";
            this.prefix_notation.Size = new System.Drawing.Size(276, 20);
            this.prefix_notation.TabIndex = 10;
            // 
            // tbRE
            // 
            this.tbRE.Location = new System.Drawing.Point(124, 84);
            this.tbRE.Name = "tbRE";
            this.tbRE.ReadOnly = true;
            this.tbRE.Size = new System.Drawing.Size(276, 20);
            this.tbRE.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.is_Dfa);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.result_data);
            this.groupBox1.Location = new System.Drawing.Point(15, 170);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(547, 202);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Results";
            // 
            // is_Dfa
            // 
            this.is_Dfa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.is_Dfa.ForeColor = System.Drawing.SystemColors.WindowText;
            this.is_Dfa.Location = new System.Drawing.Point(57, 23);
            this.is_Dfa.Name = "is_Dfa";
            this.is_Dfa.ReadOnly = true;
            this.is_Dfa.Size = new System.Drawing.Size(100, 21);
            this.is_Dfa.TabIndex = 15;
            this.is_Dfa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Is DFA?";
            // 
            // result_data
            // 
            this.result_data.AllowUserToOrderColumns = true;
            this.result_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.result_data.Location = new System.Drawing.Point(6, 50);
            this.result_data.Name = "result_data";
            this.result_data.ReadOnly = true;
            this.result_data.Size = new System.Drawing.Size(535, 187);
            this.result_data.TabIndex = 7;
            // 
            // parse_notation
            // 
            this.parse_notation.Location = new System.Drawing.Point(406, 47);
            this.parse_notation.Name = "parse_notation";
            this.parse_notation.Size = new System.Drawing.Size(75, 35);
            this.parse_notation.TabIndex = 13;
            this.parse_notation.Text = "Parse Notation";
            this.parse_notation.UseVisualStyleBackColor = true;
            this.parse_notation.Click += new System.EventHandler(this.parse_notation_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Word:";
            // 
            // tB_word
            // 
            this.tB_word.Location = new System.Drawing.Point(124, 32);
            this.tB_word.Name = "tB_word";
            this.tB_word.Size = new System.Drawing.Size(276, 20);
            this.tB_word.TabIndex = 15;
            // 
            // bt_word
            // 
            this.bt_word.Enabled = false;
            this.bt_word.Location = new System.Drawing.Point(406, 88);
            this.bt_word.Name = "bt_word";
            this.bt_word.Size = new System.Drawing.Size(75, 35);
            this.bt_word.TabIndex = 16;
            this.bt_word.Text = "Parse Word";
            this.bt_word.UseVisualStyleBackColor = true;
            this.bt_word.Click += new System.EventHandler(this.bt_word_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Comment:";
            // 
            // tB_comment
            // 
            this.tB_comment.Location = new System.Drawing.Point(124, 110);
            this.tB_comment.Name = "tB_comment";
            this.tB_comment.Size = new System.Drawing.Size(276, 20);
            this.tB_comment.TabIndex = 18;
            // 
            // bt_create_text_file
            // 
            this.bt_create_text_file.Enabled = false;
            this.bt_create_text_file.Location = new System.Drawing.Point(487, 129);
            this.bt_create_text_file.Name = "bt_create_text_file";
            this.bt_create_text_file.Size = new System.Drawing.Size(75, 35);
            this.bt_create_text_file.TabIndex = 19;
            this.bt_create_text_file.Text = "Create Text File";
            this.bt_create_text_file.UseVisualStyleBackColor = true;
            this.bt_create_text_file.Click += new System.EventHandler(this.bt_create_text_file_Click);
            // 
            // bt_add_comment
            // 
            this.bt_add_comment.Enabled = false;
            this.bt_add_comment.Location = new System.Drawing.Point(406, 129);
            this.bt_add_comment.Name = "bt_add_comment";
            this.bt_add_comment.Size = new System.Drawing.Size(75, 35);
            this.bt_add_comment.TabIndex = 20;
            this.bt_add_comment.Text = "Add Comment";
            this.bt_add_comment.UseVisualStyleBackColor = true;
            this.bt_add_comment.Click += new System.EventHandler(this.bt_add_comment_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 384);
            this.Controls.Add(this.bt_add_comment);
            this.Controls.Add(this.bt_create_text_file);
            this.Controls.Add(this.tB_comment);
            this.Controls.Add(this.read_file);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.bt_word);
            this.Controls.Add(this.tB_word);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.parse_notation);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbRE);
            this.Controls.Add(this.prefix_notation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.clear_form);
            this.Controls.Add(this.show_graph);
            this.Controls.Add(this.browse_file);
            this.Controls.Add(this.file_name);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.result_data)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox file_name;
        private System.Windows.Forms.Button browse_file;
        private System.Windows.Forms.Button read_file;
        private System.Windows.Forms.Button show_graph;
        private System.Windows.Forms.Button clear_form;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox prefix_notation;
        private System.Windows.Forms.TextBox tbRE;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button parse_notation;
        private System.Windows.Forms.DataGridView result_data;
        private System.Windows.Forms.TextBox is_Dfa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tB_word;
        private System.Windows.Forms.Button bt_word;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tB_comment;
        private System.Windows.Forms.Button bt_create_text_file;
        private System.Windows.Forms.Button bt_add_comment;
    }
}

