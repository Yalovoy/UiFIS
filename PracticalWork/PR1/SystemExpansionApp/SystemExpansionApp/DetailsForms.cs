using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SystemExpansionApp
{
    public partial class DetailsForm : Form
    {
        private DatabaseHelper dbHelper;
        private int proposalId;

        public DetailsForm(int id)
        {
            proposalId = id;
            InitializeForm();
            LoadProposalDetails();
        }

        private void InitializeForm()
        {
            this.Text = "Подробная информация о предложении";
            this.Size = new Size(550, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9);

            dbHelper = new DatabaseHelper();

            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30),
                BackColor = Color.White
            };

            Label mainTitleLabel = new Label
            {
                Text = "Подробная информация о предложении",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 51, 102),
                Location = new Point(0, 0),
                Size = new Size(490, 35),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label subTitleLabel = new Label
            {
                Text = "ПОДРОБНАЯ ИНФОРМАЦИЯ О ПРЕДЛОЖЕНИИ",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(0, 40),
                Size = new Size(490, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Panel infoPanel = new Panel
            {
                Location = new Point(0, 80),
                Size = new Size(490, 220),
                BackColor = Color.FromArgb(248, 248, 248),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(20)
            };

            TableLayoutPanel infoTable = new TableLayoutPanel
            {
                Location = new Point(10, 10),
                Size = new Size(450, 190),
                ColumnCount = 2,
                RowCount = 6,
                BackColor = Color.Transparent
            };

            infoTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            infoTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            AddInfoRow(infoTable, "ID:", "lblIdValue", 0);
            AddInfoRow(infoTable, "Подразделение:", "lblDepartmentValue", 1);
            AddInfoRow(infoTable, "Предложение:", "lblProposalValue", 2);
            AddInfoRow(infoTable, "Приоритет:", "lblPriorityValue", 3);
            AddInfoRow(infoTable, "Стоимость:", "lblCostValue", 4);
            AddInfoRow(infoTable, "Срок реализации:", "lblDateValue", 5);

            infoPanel.Controls.Add(infoTable);

            Label justificationTitle = new Label
            {
                Text = "ОБОСНОВАНИЕ:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 51, 102),
                Location = new Point(0, 310),
                Size = new Size(490, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Panel justificationPanel = new Panel
            {
                Location = new Point(0, 340),
                Size = new Size(490, 100),
                BackColor = Color.FromArgb(248, 248, 248),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(15)
            };

            TextBox txtJustification = new TextBox
            {
                Name = "txtJustification",
                Location = new Point(5, 5),
                Size = new Size(460, 80),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(248, 248, 248),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(64, 64, 64)
            };

            justificationPanel.Controls.Add(txtJustification);

            Button btnClose = new Button
            {
                Text = "Закрыть",
                Location = new Point(195, 450),
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.OK
            };
            btnClose.Click += (s, e) => this.Close();

            mainPanel.Controls.Add(mainTitleLabel);
            mainPanel.Controls.Add(subTitleLabel);
            mainPanel.Controls.Add(infoPanel);
            mainPanel.Controls.Add(justificationTitle);
            mainPanel.Controls.Add(justificationPanel);
            mainPanel.Controls.Add(btnClose);

            this.Controls.Add(mainPanel);
        }

        private void AddInfoRow(TableLayoutPanel table, string labelText, string valueName, int row)
        {
            Label label = new Label
            {
                Text = labelText,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 5, 10, 5)
            };

            Label value = new Label
            {
                Name = valueName,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(0, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Margin = new Padding(10, 5, 0, 5)
            };

            table.Controls.Add(label, 0, row);
            table.Controls.Add(value, 1, row);
        }

        private void LoadProposalDetails()
        {
            DataRow proposalData = dbHelper.GetProposalDetails(proposalId);
            if (proposalData != null)
            {
                (this.Controls.Find("lblIdValue", true)[0] as Label).Text = proposalData["id"].ToString();
                (this.Controls.Find("lblDepartmentValue", true)[0] as Label).Text = proposalData["department"].ToString();

                Label proposalLabel = this.Controls.Find("lblProposalValue", true)[0] as Label;
                proposalLabel.Text = proposalData["proposal_name"].ToString();

                Label priorityLabel = this.Controls.Find("lblPriorityValue", true)[0] as Label;
                priorityLabel.Text = proposalData["priority"].ToString();

                if (priorityLabel.Text == "Высокий")
                {
                    priorityLabel.ForeColor = Color.Red;
                    priorityLabel.Font = new Font(priorityLabel.Font, FontStyle.Bold);
                }
                else if (priorityLabel.Text == "Средний")
                {
                    priorityLabel.ForeColor = Color.Orange;
                    priorityLabel.Font = new Font(priorityLabel.Font, FontStyle.Regular);
                }

                decimal cost = Convert.ToDecimal(proposalData["cost"]);
                (this.Controls.Find("lblCostValue", true)[0] as Label).Text =
                    string.Format("{0:N0} ₽", cost);

                if (proposalData["implementation_date"] != DBNull.Value)
                {
                    DateTime implementationDate = Convert.ToDateTime(proposalData["implementation_date"]);
                    (this.Controls.Find("lblDateValue", true)[0] as Label).Text =
                        implementationDate.ToString("dd.MM.yyyy");
                }
                else
                {
                    (this.Controls.Find("lblDateValue", true)[0] as Label).Text = "Не указан";
                }

                TextBox justificationBox = this.Controls.Find("txtJustification", true)[0] as TextBox;
                string justification = proposalData["justification"].ToString();

                if (string.IsNullOrWhiteSpace(justification))
                {
                    justificationBox.Text = "Обоснование не предоставлено";
                    justificationBox.ForeColor = Color.Gray;
                    justificationBox.Font = new Font(justificationBox.Font, FontStyle.Italic);
                }
                else
                {
                    justificationBox.Text = justification;
                }
            }
            else
            {
                MessageBox.Show("Не удалось загрузить данные предложения", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}