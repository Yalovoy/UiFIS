namespace ReliabilityAppV14
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.txtResult = new System.Windows.Forms.MaskedTextBox();
            this.txtTime = new System.Windows.Forms.MaskedTextBox();
            this.txtTb = new System.Windows.Forms.MaskedTextBox();
            this.txtT = new System.Windows.Forms.TextBox();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFormula = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(4, 285);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(759, 214);
            this.richTextBox1.TabIndex = 40;
            this.richTextBox1.Text = "";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(433, 214);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(100, 22);
            this.txtResult.TabIndex = 39;
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(433, 142);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(100, 22);
            this.txtTime.TabIndex = 38;
            // 
            // txtTb
            // 
            this.txtTb.Location = new System.Drawing.Point(433, 78);
            this.txtTb.Name = "txtTb";
            this.txtTb.Size = new System.Drawing.Size(100, 22);
            this.txtTb.TabIndex = 37;
            // 
            // txtT
            // 
            this.txtT.Location = new System.Drawing.Point(433, 25);
            this.txtT.Name = "txtT";
            this.txtT.Size = new System.Drawing.Size(100, 22);
            this.txtT.TabIndex = 36;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(231, 256);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(109, 23);
            this.btnCalculate.TabIndex = 35;
            this.btnCalculate.Text = "Рассчитать";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(542, 148);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 16);
            this.label8.TabIndex = 34;
            this.label8.Text = "t,час";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(539, 217);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 16);
            this.label7.TabIndex = 33;
            this.label7.Text = "t, час";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(142, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 16);
            this.label1.TabIndex = 27;
            this.label1.Text = "Средняя наработка на отказ:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(142, 217);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(271, 16);
            this.label6.TabIndex = 32;
            this.label6.Text = "Коэффициент оперативной готовности:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(539, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 16);
            this.label2.TabIndex = 28;
            this.label2.Text = "Т, час";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(146, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 16);
            this.label5.TabIndex = 31;
            this.label5.Text = "Время для расчета:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(221, 16);
            this.label3.TabIndex = 29;
            this.label3.Text = "Среднее время восстановления:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(539, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 16);
            this.label4.TabIndex = 30;
            this.label4.Text = "Tв,час";
            // 
            // lblFormula
            // 
            this.lblFormula.AutoSize = true;
            this.lblFormula.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFormula.Location = new System.Drawing.Point(142, 502);
            this.lblFormula.Name = "lblFormula";
            this.lblFormula.Size = new System.Drawing.Size(404, 22);
            this.lblFormula.TabIndex = 41;
            this.lblFormula.Text = "Формула: Kог(t) = Kг * e^(-λ*t), где Kг = T/(T + Tв), λ = 1/T";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(407, 256);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(109, 23);
            this.btnClear.TabIndex = 42;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 543);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblFormula);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.txtTb);
            this.Controls.Add(this.txtT);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.MaskedTextBox txtResult;
        private System.Windows.Forms.MaskedTextBox txtTime;
        private System.Windows.Forms.MaskedTextBox txtTb;
        private System.Windows.Forms.TextBox txtT;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFormula;
        private System.Windows.Forms.Button btnClear;
    }
}

