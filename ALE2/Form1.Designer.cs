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
            this.label1 = new System.Windows.Forms.Label();
            this.dfa_value = new System.Windows.Forms.Label();
            this.clear_form = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // file_name
            // 
            this.file_name.Location = new System.Drawing.Point(13, 13);
            this.file_name.Name = "file_name";
            this.file_name.Size = new System.Drawing.Size(276, 20);
            this.file_name.TabIndex = 0;
            // 
            // browse_file
            // 
            this.browse_file.Location = new System.Drawing.Point(295, 11);
            this.browse_file.Name = "browse_file";
            this.browse_file.Size = new System.Drawing.Size(75, 23);
            this.browse_file.TabIndex = 1;
            this.browse_file.Text = "Browse";
            this.browse_file.UseVisualStyleBackColor = true;
            this.browse_file.Click += new System.EventHandler(this.browse_file_Click);
            // 
            // read_file
            // 
            this.read_file.Enabled = false;
            this.read_file.Location = new System.Drawing.Point(376, 11);
            this.read_file.Name = "read_file";
            this.read_file.Size = new System.Drawing.Size(75, 23);
            this.read_file.TabIndex = 2;
            this.read_file.Text = "Read File";
            this.read_file.UseVisualStyleBackColor = true;
            this.read_file.Click += new System.EventHandler(this.read_file_Click);
            // 
            // show_graph
            // 
            this.show_graph.Enabled = false;
            this.show_graph.Location = new System.Drawing.Point(295, 40);
            this.show_graph.Name = "show_graph";
            this.show_graph.Size = new System.Drawing.Size(75, 23);
            this.show_graph.TabIndex = 3;
            this.show_graph.Text = "Graph";
            this.show_graph.UseVisualStyleBackColor = true;
            this.show_graph.Click += new System.EventHandler(this.show_graph_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Is DFA?";
            // 
            // dfa_value
            // 
            this.dfa_value.AutoSize = true;
            this.dfa_value.Location = new System.Drawing.Point(64, 63);
            this.dfa_value.Name = "dfa_value";
            this.dfa_value.Size = new System.Drawing.Size(54, 13);
            this.dfa_value.TabIndex = 5;
            this.dfa_value.Text = "dfa_value";
            // 
            // clear_form
            // 
            this.clear_form.Location = new System.Drawing.Point(376, 40);
            this.clear_form.Name = "clear_form";
            this.clear_form.Size = new System.Drawing.Size(75, 23);
            this.clear_form.TabIndex = 6;
            this.clear_form.Text = "Clear";
            this.clear_form.UseVisualStyleBackColor = true;
            this.clear_form.Click += new System.EventHandler(this.clear_form_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 115);
            this.Controls.Add(this.clear_form);
            this.Controls.Add(this.dfa_value);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.show_graph);
            this.Controls.Add(this.read_file);
            this.Controls.Add(this.browse_file);
            this.Controls.Add(this.file_name);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox file_name;
        private System.Windows.Forms.Button browse_file;
        private System.Windows.Forms.Button read_file;
        private System.Windows.Forms.Button show_graph;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label dfa_value;
        private System.Windows.Forms.Button clear_form;
    }
}

