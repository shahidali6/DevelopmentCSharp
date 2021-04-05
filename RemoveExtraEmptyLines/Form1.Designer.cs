
namespace RemoveExtraEmptyLines
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
            this.lbFileText = new System.Windows.Forms.Label();
            this.btnRemoveEmptyLines = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbRemoveLine = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbFileType = new System.Windows.Forms.ComboBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // lbFileText
            // 
            this.lbFileText.AutoSize = true;
            this.lbFileText.Location = new System.Drawing.Point(27, 71);
            this.lbFileText.Name = "lbFileText";
            this.lbFileText.Size = new System.Drawing.Size(55, 13);
            this.lbFileText.TabIndex = 0;
            this.lbFileText.Text = "Text in file";
            // 
            // btnRemoveEmptyLines
            // 
            this.btnRemoveEmptyLines.Location = new System.Drawing.Point(30, 32);
            this.btnRemoveEmptyLines.Name = "btnRemoveEmptyLines";
            this.btnRemoveEmptyLines.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveEmptyLines.TabIndex = 1;
            this.btnRemoveEmptyLines.Text = "Remove Empty Lines";
            this.btnRemoveEmptyLines.UseVisualStyleBackColor = true;
            this.btnRemoveEmptyLines.Click += new System.EventHandler(this.btnRemoveEmptyLines_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(262, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Remove Empty Line After";
            // 
            // tbRemoveLine
            // 
            this.tbRemoveLine.Location = new System.Drawing.Point(395, 42);
            this.tbRemoveLine.Name = "tbRemoveLine";
            this.tbRemoveLine.Size = new System.Drawing.Size(100, 20);
            this.tbRemoveLine.TabIndex = 3;
            this.tbRemoveLine.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(262, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Type of Files";
            // 
            // cbFileType
            // 
            this.cbFileType.FormattingEnabled = true;
            this.cbFileType.Location = new System.Drawing.Point(395, 84);
            this.cbFileType.Name = "cbFileType";
            this.cbFileType.Size = new System.Drawing.Size(100, 21);
            this.cbFileType.TabIndex = 5;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cbFileType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbRemoveLine);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRemoveEmptyLines);
            this.Controls.Add(this.lbFileText);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbFileText;
        private System.Windows.Forms.Button btnRemoveEmptyLines;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbRemoveLine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbFileType;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

