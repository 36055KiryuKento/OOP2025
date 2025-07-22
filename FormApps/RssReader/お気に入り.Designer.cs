namespace RssReader {
    partial class お気に入り {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            tbURLt = new TextBox();
            comboBox1 = new ComboBox();
            button1 = new Button();
            button2 = new Button();
            listBox1 = new ListBox();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            SuspendLayout();
            // 
            // tbURLt
            // 
            tbURLt.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            tbURLt.Location = new Point(254, 87);
            tbURLt.Name = "tbURLt";
            tbURLt.Size = new Size(199, 33);
            tbURLt.TabIndex = 1;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(254, 23);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(552, 33);
            comboBox1.TabIndex = 2;
            // 
            // button1
            // 
            button1.Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            button1.Location = new Point(822, 23);
            button1.Name = "button1";
            button1.Size = new Size(75, 33);
            button1.TabIndex = 3;
            button1.Text = "取得";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            button2.Location = new Point(472, 87);
            button2.Name = "button2";
            button2.Size = new Size(75, 33);
            button2.TabIndex = 4;
            button2.Text = "登録";
            button2.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(96, 126);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(889, 94);
            listBox1.TabIndex = 5;
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Location = new Point(96, 226);
            webView21.Name = "webView21";
            webView21.Size = new Size(889, 364);
            webView21.TabIndex = 6;
            webView21.ZoomFactor = 1D;
            // 
            // お気に入り
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 602);
            Controls.Add(webView21);
            Controls.Add(listBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            Controls.Add(tbURLt);
            Name = "お気に入り";
            Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox tbURLt;
        private ComboBox comboBox1;
        private Button button1;
        private Button button2;
        private ListBox listBox1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
    }
}