namespace Jap2Hieroglyph
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lang_jap = new System.Windows.Forms.TextBox();
            this.lang_hiero = new System.Windows.Forms.TextBox();
            this.btn_SwitchLang = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lang_jap
            // 
            this.lang_jap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lang_jap.Location = new System.Drawing.Point(12, 12);
            this.lang_jap.Multiline = true;
            this.lang_jap.Name = "lang_jap";
            this.lang_jap.Size = new System.Drawing.Size(524, 122);
            this.lang_jap.TabIndex = 2;
            this.lang_jap.TextChanged += new System.EventHandler(this.lang_jap_TextChanged);
            // 
            // lang_hiero
            // 
            this.lang_hiero.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lang_hiero.Location = new System.Drawing.Point(12, 140);
            this.lang_hiero.Multiline = true;
            this.lang_hiero.Name = "lang_hiero";
            this.lang_hiero.ReadOnly = true;
            this.lang_hiero.Size = new System.Drawing.Size(524, 95);
            this.lang_hiero.TabIndex = 3;
            this.lang_hiero.TextChanged += new System.EventHandler(this.lang_hiero_TextChanged);
            // 
            // btn_SwitchLang
            // 
            this.btn_SwitchLang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SwitchLang.Location = new System.Drawing.Point(12, 241);
            this.btn_SwitchLang.Name = "btn_SwitchLang";
            this.btn_SwitchLang.Size = new System.Drawing.Size(524, 23);
            this.btn_SwitchLang.TabIndex = 4;
            this.btn_SwitchLang.Text = "入力言語を入れ替える";
            this.btn_SwitchLang.UseVisualStyleBackColor = true;
            this.btn_SwitchLang.Click += new System.EventHandler(this.btn_SwitchLang_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 267);
            this.Controls.Add(this.btn_SwitchLang);
            this.Controls.Add(this.lang_hiero);
            this.Controls.Add(this.lang_jap);
            this.Name = "Form1";
            this.Text = "Jap2Hieroglyph";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox lang_jap;
        private TextBox lang_hiero;
        private Button btn_SwitchLang;
    }
}