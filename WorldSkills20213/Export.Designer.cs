namespace WorldSkills20213
{
    partial class Export
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
            this.cb_project = new System.Windows.Forms.CheckBox();
            this.cb_issue = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cb_project
            // 
            this.cb_project.AutoSize = true;
            this.cb_project.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_project.Location = new System.Drawing.Point(12, 12);
            this.cb_project.Name = "cb_project";
            this.cb_project.Size = new System.Drawing.Size(132, 35);
            this.cb_project.TabIndex = 0;
            this.cb_project.Text = "Projects";
            this.cb_project.UseVisualStyleBackColor = true;
            // 
            // cb_issue
            // 
            this.cb_issue.AutoSize = true;
            this.cb_issue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_issue.Location = new System.Drawing.Point(12, 53);
            this.cb_issue.Name = "cb_issue";
            this.cb_issue.Size = new System.Drawing.Size(113, 35);
            this.cb_issue.TabIndex = 1;
            this.cb_issue.Text = "Issues";
            this.cb_issue.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(12, 94);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(166, 59);
            this.button3.TabIndex = 9;
            this.button3.Text = "Export";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(203, 162);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.cb_issue);
            this.Controls.Add(this.cb_project);
            this.Name = "Export";
            this.Text = "Export";
            this.Load += new System.EventHandler(this.Export_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cb_project;
        private System.Windows.Forms.CheckBox cb_issue;
        private System.Windows.Forms.Button button3;
    }
}