using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SystemExpansionApp
{
    public partial class MainForm : Form
    {
        private DatabaseHelper dbHelper;
        private DataTable proposalsTable;

        public MainForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = "Предложения по расширению ИС";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9);

            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.White
            };

            Label titleLabel = new Label
            {
                Text = "Предложения по расширению ИС",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 51, 102),
                Location = new Point(0, 0),
                Size = new Size(860, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label subtitleLabel = new Label
            {
                Text = "Формирование предложений о расширении информационной системы",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(0, 45),
                Size = new Size(860, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Panel separatorLine = new Panel
            {
                Location = new Point(0, 80),
                Size = new Size(860, 2),
                BackColor = Color.FromArgb(200, 200, 200)
            };

            Panel tableContainer = new Panel
            {
                Location = new Point(0, 90),
                Size = new Size(860, 350),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(1)
            };

            DataGridView dataGridView = new DataGridView
            {
                Name = "dataGridViewProposals",
                Location = new Point(1, 1),
                Size = new Size(856, 346),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 10),
                AllowUserToResizeRows = false,
                GridColor = Color.FromArgb(240, 240, 240)
            };

            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 102, 204);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersHeight = 40;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            dataGridView.RowTemplate.Height = 35;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dataGridView.DefaultCellStyle.Padding = new Padding(5, 0, 5, 0);

            dataGridView.CellDoubleClick += DataGridView_CellDoubleClick;
            dataGridView.DataBindingComplete += DataGridView_DataBindingComplete;

            tableContainer.Controls.Add(dataGridView);

            Panel bottomSeparator = new Panel
            {
                Location = new Point(0, 450),
                Size = new Size(860, 2),
                BackColor = Color.FromArgb(200, 200, 200)
            };

            Panel buttonPanel = new Panel
            {
                Location = new Point(0, 460),
                Size = new Size(860, 100),
                BackColor = Color.White
            };

            Button btnAdd = CreateMenuButton("Добавить предложение", 50, 10);
            btnAdd.Click += BtnAdd_Click;

            Button btnDetails = CreateMenuButton("Просмотр деталей", 320, 10);
            btnDetails.Click += BtnDetails_Click;

            Button btnReport = CreateMenuButton("Сформировать отчет", 590, 10);
            btnReport.Click += BtnReport_Click;

            Button btnExit = CreateMenuButton("Выход", 380, 50);
            btnExit.BackColor = Color.FromArgb(220, 80, 80);
            btnExit.ForeColor = Color.White;
            btnExit.Click += (s, e) => Application.Exit();

            buttonPanel.Controls.AddRange(new Control[] { btnAdd, btnDetails, btnReport, btnExit });

            mainPanel.Controls.Add(titleLabel);
            mainPanel.Controls.Add(subtitleLabel);
            mainPanel.Controls.Add(separatorLine);
            mainPanel.Controls.Add(tableContainer);
            mainPanel.Controls.Add(bottomSeparator);
            mainPanel.Controls.Add(buttonPanel);

            this.Controls.Add(mainPanel);

            dbHelper = new DatabaseHelper();
            LoadProposals();
        }

        private Button CreateMenuButton(string text, int x, int y)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(250, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand,
                Margin = new Padding(10)
            };
        }

        private void DataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (dataGridView != null)
            {
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dataGridView.Columns.Contains("ID"))
                {
                    dataGridView.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView.Columns["ID"].Width = 60;
                }

                if (dataGridView.Columns.Contains("Приоритет"))
                {
                    dataGridView.Columns["Приоритет"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView.Columns["Приоритет"].Width = 100;
                }

                if (dataGridView.Columns.Contains("Стоимость"))
                {
                    dataGridView.Columns["Стоимость"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView.Columns["Стоимость"].Width = 120;
                }

                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells["Приоритет"].Value?.ToString() == "Высокий")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 240, 240);
                        row.Cells["Приоритет"].Style.ForeColor = Color.Red;
                        row.Cells["Приоритет"].Style.Font = new Font(dataGridView.Font, FontStyle.Bold);
                    }
                    else if (row.Cells["Приоритет"].Value?.ToString() == "Средний")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 250, 240);
                        row.Cells["Приоритет"].Style.ForeColor = Color.Orange;
                    }
                }
            }
        }

        private void LoadProposals()
        {
            proposalsTable = dbHelper.GetAllProposals();
            DataGridView dataGridView = this.Controls.Find("dataGridViewProposals", true)[0] as DataGridView;

            if (dataGridView != null && proposalsTable != null)
            {
                dataGridView.DataSource = null;
                dataGridView.Rows.Clear();
                dataGridView.Columns.Clear();

                dataGridView.Columns.Add("ID", "ID");
                dataGridView.Columns.Add("Подразделение", "Подразделение");
                dataGridView.Columns.Add("Предложение", "Предложение");
                dataGridView.Columns.Add("Приоритет", "Приоритет");
                dataGridView.Columns.Add("Стоимость", "Стоимость");

                foreach (DataRow row in proposalsTable.Rows)
                {
                    string proposalText = row["Предложение"].ToString();
                    if (proposalText.Length > 35)
                    {
                        proposalText = proposalText.Substring(0, 32) + "...";
                    }

                    string cost = row["Стоимость"].ToString();
                    if (cost.EndsWith(" ₽"))
                    {
                        cost = cost.Replace(" ₽", " ₽");
                    }

                    dataGridView.Rows.Add(
                        row["ID"],
                        row["Подразделение"],
                        proposalText,
                        row["Приоритет"],
                        cost
                    );
                }

                dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dataGridView.Columns["Предложение"].Width = 400;
                dataGridView.Sort(dataGridView.Columns["Стоимость"], System.ComponentModel.ListSortDirection.Descending);
            }
        }

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ShowProposalDetails();
            }
        }

        private void ShowProposalDetails()
        {
            DataGridView dataGridView = this.Controls.Find("dataGridViewProposals", true)[0] as DataGridView;
            if (dataGridView.SelectedRows.Count > 0)
            {
                int selectedId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["ID"].Value);
                DetailsForm detailsForm = new DetailsForm(selectedId);
                detailsForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите предложение для просмотра деталей",
                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddProposalForm addForm = new AddProposalForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadProposals();
            }
        }

        private void BtnDetails_Click(object sender, EventArgs e)
        {
            ShowProposalDetails();
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            ReportForm reportForm = new ReportForm();
            reportForm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
