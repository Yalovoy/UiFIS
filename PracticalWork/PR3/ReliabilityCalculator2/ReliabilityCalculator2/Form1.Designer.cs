 namespace ReliabilityCalculatorV7
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxT = new System.Windows.Forms.MaskedTextBox();
            this.textBoxVx = new System.Windows.Forms.MaskedTextBox();
            this.textBoxMt = new System.Windows.Forms.MaskedTextBox();
            this.textBoxN = new System.Windows.Forms.TextBox();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBoxResult = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // textBoxT
            // 
            this.textBoxT.Location = new System.Drawing.Point(293, 211);
            this.textBoxT.Name = "textBoxT";
            this.textBoxT.Size = new System.Drawing.Size(100, 22);
            this.textBoxT.TabIndex = 25;
            // 
            // textBoxVx
            // 
            this.textBoxVx.Location = new System.Drawing.Point(293, 139);
            this.textBoxVx.Name = "textBoxVx";
            this.textBoxVx.Size = new System.Drawing.Size(100, 22);
            this.textBoxVx.TabIndex = 24;
            // 
            // textBoxMt
            // 
            this.textBoxMt.Location = new System.Drawing.Point(293, 75);
            this.textBoxMt.Name = "textBoxMt";
            this.textBoxMt.Size = new System.Drawing.Size(100, 22);
            this.textBoxMt.TabIndex = 23;
            // 
            // textBoxN
            // 
            this.textBoxN.Location = new System.Drawing.Point(293, 21);
            this.textBoxN.Name = "textBoxN";
            this.textBoxN.Size = new System.Drawing.Size(100, 22);
            this.textBoxN.TabIndex = 22;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(216, 253);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(109, 23);
            this.btnCalculate.TabIndex = 21;
            this.btnCalculate.Text = "Рассчитать";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(402, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 16);
            this.label8.TabIndex = 20;
            this.label8.Text = "v";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(399, 214);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 16);
            this.label7.TabIndex = 19;
            this.label7.Text = "t, час";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Количество изделий на испытаниях:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 214);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(257, 16);
            this.label6.TabIndex = 18;
            this.label6.Text = "Наработка для расчета отказа (часы):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(399, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "N";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(199, 16);
            this.label5.TabIndex = 17;
            this.label5.Text = "Коэффициент вариации (0-1):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(241, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "Средняя наработка на отказ (часы):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(399, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "Mt, ч";
            // 
            // richTextBoxResult
            // 
            this.richTextBoxResult.Location = new System.Drawing.Point(12, 282);
            this.richTextBoxResult.Name = "richTextBoxResult";
            this.richTextBoxResult.Size = new System.Drawing.Size(561, 226);
            this.richTextBoxResult.TabIndex = 26;
            this.richTextBoxResult.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 519);
            this.Controls.Add(this.richTextBoxResult);
            this.Controls.Add(this.textBoxT);
            this.Controls.Add(this.textBoxVx);
            this.Controls.Add(this.textBoxMt);
            this.Controls.Add(this.textBoxN);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox textBoxT;
        private System.Windows.Forms.MaskedTextBox textBoxVx;
        private System.Windows.Forms.MaskedTextBox textBoxMt;
        private System.Windows.Forms.TextBox textBoxN;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBoxResult;
    }
}

