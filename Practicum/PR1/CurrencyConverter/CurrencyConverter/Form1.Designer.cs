namespace CurrencyConverter
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonSwapCurrencies = new System.Windows.Forms.Button();
            this.textBoxAmount = new System.Windows.Forms.TextBox();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.comboBoxFrom = new System.Windows.Forms.ComboBox();
            this.comboBoxTo = new System.Windows.Forms.ComboBox();
            this.buttonUpdateRates = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.listBoxRates = new System.Windows.Forms.ListBox();
            this.labelRatesTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(229, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Конвертер валют";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Из:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "В:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Сумма:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 287);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Результат:";
            // 
            // buttonSwapCurrencies
            // 
            this.buttonSwapCurrencies.Location = new System.Drawing.Point(460, 92);
            this.buttonSwapCurrencies.Name = "buttonSwapCurrencies";
            this.buttonSwapCurrencies.Size = new System.Drawing.Size(32, 51);
            this.buttonSwapCurrencies.TabIndex = 6;
            this.buttonSwapCurrencies.Text = "->\r\n<-\r\n";
            this.buttonSwapCurrencies.UseVisualStyleBackColor = true;
            // 
            // textBoxAmount
            // 
            this.textBoxAmount.Location = new System.Drawing.Point(132, 221);
            this.textBoxAmount.Name = "textBoxAmount";
            this.textBoxAmount.Size = new System.Drawing.Size(246, 22);
            this.textBoxAmount.TabIndex = 7;
            // 
            // textBoxResult
            // 
            this.textBoxResult.Location = new System.Drawing.Point(132, 281);
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ReadOnly = true;
            this.textBoxResult.Size = new System.Drawing.Size(246, 22);
            this.textBoxResult.TabIndex = 8;
            // 
            // comboBoxFrom
            // 
            this.comboBoxFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFrom.FormattingEnabled = true;
            this.comboBoxFrom.Location = new System.Drawing.Point(132, 80);
            this.comboBoxFrom.Name = "comboBoxFrom";
            this.comboBoxFrom.Size = new System.Drawing.Size(246, 24);
            this.comboBoxFrom.TabIndex = 9;
            // 
            // comboBoxTo
            // 
            this.comboBoxTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTo.FormattingEnabled = true;
            this.comboBoxTo.Location = new System.Drawing.Point(132, 133);
            this.comboBoxTo.Name = "comboBoxTo";
            this.comboBoxTo.Size = new System.Drawing.Size(246, 24);
            this.comboBoxTo.TabIndex = 10;
            // 
            // buttonUpdateRates
            // 
            this.buttonUpdateRates.Location = new System.Drawing.Point(440, 235);
            this.buttonUpdateRates.Name = "buttonUpdateRates";
            this.buttonUpdateRates.Size = new System.Drawing.Size(131, 46);
            this.buttonUpdateRates.TabIndex = 12;
            this.buttonUpdateRates.Text = "Обновить курсы";
            this.buttonUpdateRates.UseVisualStyleBackColor = true;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(354, 409);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(74, 16);
            this.labelStatus.TabIndex = 0;
            this.labelStatus.Text = "labelStatus";
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(354, 376);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(66, 16);
            this.labelDate.TabIndex = 1;
            this.labelDate.Text = "labelDate";
            // 
            // listBoxRates
            // 
            this.listBoxRates.FormattingEnabled = true;
            this.listBoxRates.ItemHeight = 16;
            this.listBoxRates.Location = new System.Drawing.Point(52, 349);
            this.listBoxRates.Name = "listBoxRates";
            this.listBoxRates.Size = new System.Drawing.Size(286, 132);
            this.listBoxRates.TabIndex = 13;
            // 
            // labelRatesTitle
            // 
            this.labelRatesTitle.AutoSize = true;
            this.labelRatesTitle.Location = new System.Drawing.Point(49, 323);
            this.labelRatesTitle.Name = "labelRatesTitle";
            this.labelRatesTitle.Size = new System.Drawing.Size(99, 16);
            this.labelRatesTitle.TabIndex = 14;
            this.labelRatesTitle.Text = "labelRatesTitle";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 493);
            this.Controls.Add(this.labelRatesTitle);
            this.Controls.Add(this.listBoxRates);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.buttonUpdateRates);
            this.Controls.Add(this.comboBoxTo);
            this.Controls.Add(this.comboBoxFrom);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.textBoxAmount);
            this.Controls.Add(this.buttonSwapCurrencies);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonSwapCurrencies;
        private System.Windows.Forms.TextBox textBoxAmount;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.ComboBox comboBoxFrom;
        private System.Windows.Forms.ComboBox comboBoxTo;
        private System.Windows.Forms.Button buttonUpdateRates;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.ListBox listBoxRates;
        private System.Windows.Forms.Label labelRatesTitle;
    }
}

