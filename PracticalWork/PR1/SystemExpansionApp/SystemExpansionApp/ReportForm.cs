using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace SystemExpansionApp
{
    public partial class ReportForm : Form
    {
        private DatabaseHelper dbHelper;

        public ReportForm()
        {
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = "Отчеты по предложениям";
            this.Size = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            dbHelper = new DatabaseHelper();

            TabControl tabControl = new TabControl { Dock = DockStyle.Fill };

            TabPage tabStats = new TabPage("Статистика по подразделениям");
            DataGridView dgvStats = new DataGridView
            {
                Name = "dgvStats",
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White
            };
            tabStats.Controls.Add(dgvStats);

            TabPage tabSummary = new TabPage("Сводный отчет");
            RichTextBox rtbSummary = new RichTextBox
            {
                Name = "rtbSummary",
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Font = new Font("Courier New", 10)
            };
            tabSummary.Controls.Add(rtbSummary);

            Panel printPanel = new Panel { Dock = DockStyle.Bottom, Height = 50 };
            Button btnPrint = new Button
            {
                Text = "📄 Печать отчета",
                Size = new Size(150, 30),
                Location = new Point(270, 10),
                Font = new Font("Arial", 10),
                BackColor = Color.LightBlue
            };
            btnPrint.Click += BtnPrint_Click;
            printPanel.Controls.Add(btnPrint);
            tabSummary.Controls.Add(printPanel);

            tabControl.TabPages.Add(tabStats);
            tabControl.TabPages.Add(tabSummary);

            this.Controls.Add(tabControl);

            LoadStatistics(dgvStats);
            LoadSummary(rtbSummary);
        }

        private void LoadStatistics(DataGridView dgv)
        {
            DataTable stats = dbHelper.GetStatistics();
            dgv.DataSource = stats;

            if (dgv.Columns.Contains("Общая стоимость"))
            {
                dgv.Columns["Общая стоимость"].DefaultCellStyle.Format = "N0";
            }
            if (dgv.Columns.Contains("Средняя стоимость"))
            {
                dgv.Columns["Средняя стоимость"].DefaultCellStyle.Format = "N0";
            }
        }

        private void LoadSummary(RichTextBox rtb)
        {
            try
            {
                DataTable allProposals = dbHelper.GetAllProposals();

                rtb.Clear();
                rtb.SelectionFont = new Font("Arial", 14, FontStyle.Bold);
                rtb.AppendText("ОТЧЕТ ПО ПРЕДЛОЖЕНИЯМ О РАСШИРЕНИИ ИС\n\n");

                rtb.SelectionFont = new Font("Arial", 10);
                rtb.AppendText($"Дата формирования: {DateTime.Now:dd.MM.yyyy HH:mm}\n");
                rtb.AppendText($"Всего предложений: {allProposals.Rows.Count}\n\n");

                decimal totalCost = 0;
                foreach (DataRow row in allProposals.Rows)
                {
                    string costStr = row["Стоимость"].ToString().Replace(" ₽", "").Replace(" ", "");
                    if (decimal.TryParse(costStr, out decimal cost))
                    {
                        totalCost += cost;
                    }
                }
                rtb.AppendText($"Общая стоимость всех предложений: {totalCost:N0} ₽\n\n");

                rtb.SelectionFont = new Font("Arial", 11, FontStyle.Bold);
                rtb.AppendText("РАСПРЕДЕЛЕНИЕ ПО ПРИОРИТЕТАМ:\n");
                rtb.SelectionFont = new Font("Arial", 10);

                DataView view = new DataView(allProposals);

                string[] priorities = { "Высокий", "Средний", "Низкий" };
                foreach (string priority in priorities)
                {
                    view.RowFilter = $"[Приоритет] = '{priority}'";
                    if (view.Count > 0)
                    {
                        rtb.AppendText($"  {priority}: {view.Count} предложений\n");
                    }
                }

                rtb.AppendText("\n");

                rtb.SelectionFont = new Font("Arial", 11, FontStyle.Bold);
                rtb.AppendText("САМЫЕ ДОРОГИЕ ПРЕДЛОЖЕНИЯ:\n");
                rtb.SelectionFont = new Font("Arial", 10);

                view.Sort = "[Стоимость] DESC";
                int count = Math.Min(5, allProposals.Rows.Count);
                for (int i = 0; i < count; i++)
                {
                    rtb.AppendText($"  {i + 1}. {view[i]["Предложение"]} - {view[i]["Стоимость"]}\n");
                }
            }
            catch (Exception ex)
            {
                rtb.Text = $"Ошибка при формировании отчета: {ex.Message}";
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|PDF файлы (*.pdf)|*.pdf",
                Title = "Сохранить отчет",
                FileName = $"Отчет_предложения_{DateTime.Now:yyyyMMdd_HHmm}.txt"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string reportText = (this.Controls.Find("rtbSummary", true)[0] as RichTextBox).Text;
                    File.WriteAllText(saveDialog.FileName, reportText);

                    MessageBox.Show($"Отчет сохранен в файл:\n{saveDialog.FileName}",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
  