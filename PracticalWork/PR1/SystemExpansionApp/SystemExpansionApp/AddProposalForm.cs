using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SystemExpansionApp
{
    public partial class AddProposalForm : Form
    {
        private DatabaseHelper dbHelper;
        private bool isEditMode = false;
        private int proposalId = 0;

        public AddProposalForm()
        {
            InitializeForm();
        }

        public AddProposalForm(DataRow proposalData)
        {
            InitializeForm();
            isEditMode = true;
            LoadProposalData(proposalData);
        }

        private void InitializeForm()
        {
            this.Text = isEditMode ? "Редактирование предложения" : "Добавление нового предложения";
            this.Size = new Size(500, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            dbHelper = new DatabaseHelper();

            // Основной контейнер
            TableLayoutPanel mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                ColumnCount = 2,
                RowCount = 8
            };

            // Подразделение
            mainPanel.Controls.Add(new Label { Text = "Подразделение:", TextAlign = ContentAlignment.MiddleRight }, 0, 0);
            ComboBox cmbDepartment = new ComboBox { Name = "cmbDepartment", Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbDepartment.Items.AddRange(dbHelper.GetDepartments().ToArray());
            mainPanel.Controls.Add(cmbDepartment, 1, 0);

            // Название предложения
            mainPanel.Controls.Add(new Label { Text = "Предложение:", TextAlign = ContentAlignment.MiddleRight }, 0, 1);
            TextBox txtProposal = new TextBox { Name = "txtProposal", Dock = DockStyle.Fill };
            mainPanel.Controls.Add(txtProposal, 1, 1);

            // Приоритет
            mainPanel.Controls.Add(new Label { Text = "Приоритет:", TextAlign = ContentAlignment.MiddleRight }, 0, 2);
            ComboBox cmbPriority = new ComboBox { Name = "cmbPriority", Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbPriority.Items.AddRange(dbHelper.GetPriorities().ToArray());
            mainPanel.Controls.Add(cmbPriority, 1, 2);

            // Стоимость
            mainPanel.Controls.Add(new Label { Text = "Стоимость (₽):", TextAlign = ContentAlignment.MiddleRight }, 0, 3);
            TextBox txtCost = new TextBox { Name = "txtCost", Dock = DockStyle.Fill };
            mainPanel.Controls.Add(txtCost, 1, 3);

            // Срок реализации
            mainPanel.Controls.Add(new Label { Text = "Срок реализации:", TextAlign = ContentAlignment.MiddleRight }, 0, 4);
            DateTimePicker dtpDate = new DateTimePicker { Name = "dtpDate", Dock = DockStyle.Fill, Format = DateTimePickerFormat.Short };
            dtpDate.Value = DateTime.Now.AddMonths(3);
            mainPanel.Controls.Add(dtpDate, 1, 4);

            // Обоснование
            mainPanel.Controls.Add(new Label { Text = "Обоснование:", TextAlign = ContentAlignment.MiddleRight }, 0, 5);
            TextBox txtJustification = new TextBox { Name = "txtJustification", Multiline = true, Dock = DockStyle.Fill, Height = 80 };
            mainPanel.Controls.Add(txtJustification, 1, 5);

            // Статус
            mainPanel.Controls.Add(new Label { Text = "Статус:", TextAlign = ContentAlignment.MiddleRight }, 0, 6);
            ComboBox cmbStatus = new ComboBox { Name = "cmbStatus", Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbStatus.Items.AddRange(dbHelper.GetStatuses().ToArray());
            mainPanel.Controls.Add(cmbStatus, 1, 6);

            // Панель с кнопками
            Panel buttonPanel = new Panel { Dock = DockStyle.Bottom, Height = 60, Padding = new Padding(10) };

            Button btnSave = new Button
            {
                Text = isEditMode ? "Сохранить изменения" : "Добавить предложение",
                Size = new Size(180, 35),
                Location = new Point(100, 10),
                BackColor = Color.LightGreen
            };
            btnSave.Click += BtnSave_Click;

            Button btnCancel = new Button
            {
                Text = "Отмена",
                Size = new Size(100, 35),
                Location = new Point(290, 10),
                BackColor = Color.LightCoral
            };
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            buttonPanel.Controls.AddRange(new Control[] { btnSave, btnCancel });

            this.Controls.Add(mainPanel);
            this.Controls.Add(buttonPanel);
        }

        private void LoadProposalData(DataRow data)
        {
            proposalId = Convert.ToInt32(data["id"]);

            (this.Controls.Find("cmbDepartment", true)[0] as ComboBox).Text = data["department"].ToString();
            (this.Controls.Find("txtProposal", true)[0] as TextBox).Text = data["proposal_name"].ToString();
            (this.Controls.Find("cmbPriority", true)[0] as ComboBox).Text = data["priority"].ToString();
            (this.Controls.Find("txtCost", true)[0] as TextBox).Text = data["cost"].ToString();

            DateTimePicker dtp = this.Controls.Find("dtpDate", true)[0] as DateTimePicker;
            if (data["implementation_date"] != DBNull.Value)
            {
                dtp.Value = Convert.ToDateTime(data["implementation_date"]);
            }

            (this.Controls.Find("txtJustification", true)[0] as TextBox).Text = data["justification"].ToString();
            (this.Controls.Find("cmbStatus", true)[0] as ComboBox).Text = data["status"].ToString();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string department = (this.Controls.Find("cmbDepartment", true)[0] as ComboBox).Text;
                string proposalName = (this.Controls.Find("txtProposal", true)[0] as TextBox).Text;
                string priority = (this.Controls.Find("cmbPriority", true)[0] as ComboBox).Text;
                decimal cost = decimal.Parse((this.Controls.Find("txtCost", true)[0] as TextBox).Text);
                DateTime implementationDate = (this.Controls.Find("dtpDate", true)[0] as DateTimePicker).Value;
                string justification = (this.Controls.Find("txtJustification", true)[0] as TextBox).Text;
                string status = (this.Controls.Find("cmbStatus", true)[0] as ComboBox).Text;

                if (string.IsNullOrWhiteSpace(proposalName))
                {
                    MessageBox.Show("Введите название предложения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(department))
                {
                    MessageBox.Show("Выберите подразделение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool success;
                if (isEditMode)
                {
                    success = dbHelper.UpdateProposal(proposalId, department, proposalName, priority, cost,
                        implementationDate, justification, status);
                }
                else
                {
                    success = dbHelper.AddProposal(department, proposalName, priority, cost,
                        implementationDate, justification, status);
                }

                if (success)
                {
                    MessageBox.Show(isEditMode ? "Изменения сохранены" : "Предложение добавлено",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректную стоимость", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}