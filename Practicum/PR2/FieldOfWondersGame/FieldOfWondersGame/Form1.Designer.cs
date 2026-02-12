namespace FieldOfWondersGame
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
            this.textBoxInputWord = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.labelMixedTitle = new System.Windows.Forms.Label();
            this.flowLayoutPanelMixed = new System.Windows.Forms.FlowLayoutPanel();
            this.labelResultTitle = new System.Windows.Forms.Label();
            this.flowLayoutPanelResult = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.buttonNewGame = new System.Windows.Forms.Button();
            this.labelResult = new System.Windows.Forms.Label();
            this.buttonGenerateWord = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Algerian", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(219, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "ПОЛЕ ЧУДЕС";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(215, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Введите слово для угадывания:";
            // 
            // textBoxInputWord
            // 
            this.textBoxInputWord.Location = new System.Drawing.Point(245, 103);
            this.textBoxInputWord.Name = "textBoxInputWord";
            this.textBoxInputWord.Size = new System.Drawing.Size(214, 22);
            this.textBoxInputWord.TabIndex = 2;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(484, 103);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(127, 26);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "Начать игру";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // labelMixedTitle
            // 
            this.labelMixedTitle.AutoSize = true;
            this.labelMixedTitle.Location = new System.Drawing.Point(9, 156);
            this.labelMixedTitle.Name = "labelMixedTitle";
            this.labelMixedTitle.Size = new System.Drawing.Size(151, 16);
            this.labelMixedTitle.TabIndex = 4;
            this.labelMixedTitle.Text = "Перепутанные буквы:";
            this.labelMixedTitle.Visible = false;
            // 
            // flowLayoutPanelMixed
            // 
            this.flowLayoutPanelMixed.Location = new System.Drawing.Point(9, 198);
            this.flowLayoutPanelMixed.Name = "flowLayoutPanelMixed";
            this.flowLayoutPanelMixed.Size = new System.Drawing.Size(739, 70);
            this.flowLayoutPanelMixed.TabIndex = 5;
            // 
            // labelResultTitle
            // 
            this.labelResultTitle.AutoSize = true;
            this.labelResultTitle.Location = new System.Drawing.Point(9, 282);
            this.labelResultTitle.Name = "labelResultTitle";
            this.labelResultTitle.Size = new System.Drawing.Size(161, 16);
            this.labelResultTitle.TabIndex = 6;
            this.labelResultTitle.Text = "Собрать буквы в слово:";
            this.labelResultTitle.Visible = false;
            // 
            // flowLayoutPanelResult
            // 
            this.flowLayoutPanelResult.Location = new System.Drawing.Point(9, 320);
            this.flowLayoutPanelResult.Name = "flowLayoutPanelResult";
            this.flowLayoutPanelResult.Size = new System.Drawing.Size(739, 56);
            this.flowLayoutPanelResult.TabIndex = 6;
            // 
            // buttonCheck
            // 
            this.buttonCheck.Location = new System.Drawing.Point(44, 403);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(126, 35);
            this.buttonCheck.TabIndex = 7;
            this.buttonCheck.Text = "Проверить";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.ButtonCheck_Click);
            // 
            // buttonNewGame
            // 
            this.buttonNewGame.Location = new System.Drawing.Point(485, 403);
            this.buttonNewGame.Name = "buttonNewGame";
            this.buttonNewGame.Size = new System.Drawing.Size(126, 35);
            this.buttonNewGame.TabIndex = 8;
            this.buttonNewGame.Text = "Заново";
            this.buttonNewGame.UseVisualStyleBackColor = true;
            this.buttonNewGame.Click += new System.EventHandler(this.ButtonNewGame_Click);
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Location = new System.Drawing.Point(75, 467);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(0, 16);
            this.labelResult.TabIndex = 10;
            // 
            // buttonGenerateWord
            // 
            this.buttonGenerateWord.Location = new System.Drawing.Point(485, 148);
            this.buttonGenerateWord.Name = "buttonGenerateWord";
            this.buttonGenerateWord.Size = new System.Drawing.Size(126, 23);
            this.buttonGenerateWord.TabIndex = 11;
            this.buttonGenerateWord.Text = "Генерировать";
            this.buttonGenerateWord.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 713);
            this.Controls.Add(this.buttonGenerateWord);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.buttonNewGame);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.flowLayoutPanelResult);
            this.Controls.Add(this.labelResultTitle);
            this.Controls.Add(this.flowLayoutPanelMixed);
            this.Controls.Add(this.labelMixedTitle);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.textBoxInputWord);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Click += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxInputWord;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label labelMixedTitle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMixed;
        private System.Windows.Forms.Label labelResultTitle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelResult;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.Button buttonNewGame;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Button buttonGenerateWord;
    }
}

