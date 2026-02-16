namespace SolarSystem
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.controlPanel = new System.Windows.Forms.Panel();
            this.lblZoom = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.btnSlower = new System.Windows.Forms.Button();
            this.btnFaster = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.controlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // animationTimer
            // 
            this.animationTimer.Enabled = true;
            this.animationTimer.Interval = 25;
            this.animationTimer.Tick += new System.EventHandler(this.animationTimer_Tick);
            // 
            // controlPanel
            // 
            this.controlPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.controlPanel.Controls.Add(this.lblZoom);
            this.controlPanel.Controls.Add(this.button5);
            this.controlPanel.Controls.Add(this.button4);
            this.controlPanel.Controls.Add(this.lblSpeed);
            this.controlPanel.Controls.Add(this.btnSlower);
            this.controlPanel.Controls.Add(this.btnFaster);
            this.controlPanel.Controls.Add(this.btnPause);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlPanel.Location = new System.Drawing.Point(0, 0);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(1594, 60);
            this.controlPanel.TabIndex = 0;
            // 
            // lblZoom
            // 
            this.lblZoom.AutoSize = true;
            this.lblZoom.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblZoom.ForeColor = System.Drawing.Color.Yellow;
            this.lblZoom.Location = new System.Drawing.Point(561, 17);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(59, 28);
            this.lblZoom.TabIndex = 6;
            this.lblZoom.Text = "160%";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(90)))));
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(502, 11);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(36, 39);
            this.button5.TabIndex = 5;
            this.button5.Text = "-";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(90)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(451, 11);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(36, 39);
            this.button4.TabIndex = 4;
            this.button4.Text = "+";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSpeed.Location = new System.Drawing.Point(371, 17);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(43, 28);
            this.lblSpeed.TabIndex = 3;
            this.lblSpeed.Text = "1,1x";
            this.lblSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSlower
            // 
            this.btnSlower.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(90)))));
            this.btnSlower.FlatAppearance.BorderSize = 0;
            this.btnSlower.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSlower.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnSlower.ForeColor = System.Drawing.Color.White;
            this.btnSlower.Location = new System.Drawing.Point(239, 15);
            this.btnSlower.Name = "btnSlower";
            this.btnSlower.Size = new System.Drawing.Size(126, 35);
            this.btnSlower.TabIndex = 2;
            this.btnSlower.Text = "Медленнее";
            this.btnSlower.UseVisualStyleBackColor = false;
            this.btnSlower.Click += new System.EventHandler(this.btnSlower_Click);
            // 
            // btnFaster
            // 
            this.btnFaster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(90)))));
            this.btnFaster.FlatAppearance.BorderSize = 0;
            this.btnFaster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFaster.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnFaster.ForeColor = System.Drawing.Color.White;
            this.btnFaster.Location = new System.Drawing.Point(120, 15);
            this.btnFaster.Name = "btnFaster";
            this.btnFaster.Size = new System.Drawing.Size(113, 35);
            this.btnFaster.TabIndex = 1;
            this.btnFaster.Text = "Быстрее";
            this.btnFaster.UseVisualStyleBackColor = false;
            this.btnFaster.Click += new System.EventHandler(this.btnFaster_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(90)))));
            this.btnPause.FlatAppearance.BorderSize = 0;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPause.ForeColor = System.Drawing.Color.White;
            this.btnPause.Location = new System.Drawing.Point(20, 15);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(90, 35);
            this.btnPause.TabIndex = 0;
            this.btnPause.Text = "Пауза";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1594, 700);
            this.Controls.Add(this.controlPanel);
            this.ForeColor = System.Drawing.Color.Cyan;
            this.Name = "Form1";
            this.Text = "Солнечная система";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Timer animationTimer;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Button btnFaster;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Button btnSlower;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label lblZoom;
    }
}