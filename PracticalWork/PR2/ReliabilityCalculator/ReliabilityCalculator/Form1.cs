using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReliabilityCalculator
{
    public partial class MainForm : Form
    {
        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private int selectedVariant = 1;

        public MainForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = "Расчет показателей безотказности системы";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 10)
            };

            tabPage1 = new TabPage("Задание 1");
            InitializeTask1Tab();
            tabControl.TabPages.Add(tabPage1);

            tabPage2 = new TabPage("Задание 2");
            InitializeTask2Tab();
            tabControl.TabPages.Add(tabPage2);

            tabPage3 = new TabPage("Задание 3");
            InitializeTask3Tab();
            tabControl.TabPages.Add(tabPage3);

            this.Controls.Add(tabControl);
        }

        private void InitializeTask1Tab()
        {
            Label titleLabel = new Label
            {
                Text = "Задание 1. Расчет средней наработки на отказ для одной системы",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(20, 20),
                Size = new Size(800, 30)
            };
            tabPage1.Controls.Add(titleLabel);

            Label descriptionLabel = new Label
            {
                Text = "Система отказала 6 раз. Время работы до каждого отказа:\n" +
                       "1-й отказ: 185 часов, 2-й: 342 часа, 3-й: 268 часов,\n" +
                       "4-й: 220 часов, 5-й: 96 часов, 6-й: 102 часа",
                Font = new Font("Arial", 11),
                ForeColor = Color.Black,
                Location = new Point(20, 60),
                Size = new Size(800, 60)
            };
            tabPage1.Controls.Add(descriptionLabel);

            Button calculateButton1 = new Button
            {
                Text = "Рассчитать наработку на отказ",
                Font = new Font("Arial", 11, FontStyle.Bold),
                BackColor = Color.LightBlue,
                ForeColor = Color.Black,
                Location = new Point(20, 140),
                Size = new Size(250, 40),
                Cursor = Cursors.Hand
            };
            calculateButton1.Click += CalculateButton1_Click;
            tabPage1.Controls.Add(calculateButton1);

            Label resultLabel1 = new Label
            {
                Text = "Результат: ",
                Font = new Font("Arial", 12),
                ForeColor = Color.Black,
                Location = new Point(20, 200),
                Size = new Size(800, 30),
                Visible = false
            };
            tabPage1.Controls.Add(resultLabel1);

            Label formulaLabel = new Label
            {
                Text = "Формула: T₀ = (Σtᵢ) / n",
                Font = new Font("Arial", 11, FontStyle.Italic),
                ForeColor = Color.DarkGreen,
                Location = new Point(20, 250),
                Size = new Size(800, 30)
            };
            tabPage1.Controls.Add(formulaLabel);
        }

        private void CalculateButton1_Click(object sender, EventArgs e)
        {

            double[] times = { 185, 342, 268, 220, 96, 102 };
            int n = times.Length;

            double sum = 0;
            foreach (double time in times)
            {
                sum += time;
            }

            double T0 = sum / n;

            foreach (Control control in tabPage1.Controls)
            {
                if (control is Label label && label.Text.StartsWith("Результат:"))
                {
                    label.Text = $"Результат: Средняя наработка на отказ T₀ = {T0:F2} часов";
                    label.Visible = true;
                    label.ForeColor = Color.DarkRed;
                    label.Font = new Font("Arial", 12, FontStyle.Bold);
                    break;
                }
            }
        }

        private void InitializeTask2Tab()
        {
            Label titleLabel = new Label
            {
                Text = "Задание 2. Расчет наработки на отказ для группы систем",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(20, 20),
                Size = new Size(800, 30)
            };
            tabPage2.Controls.Add(titleLabel);

            Label descriptionLabel = new Label
            {
                Text = "Данные по трем системам:\n" +
                       "Система 1: t₁ = 358 часов, n₁ = 4 отказа\n" +
                       "Система 2: t₂ = 385 часов, n₂ = 3 отказа\n" +
                       "Система 3: t₃ = 400 часов, n₃ = 2 отказа",
                Font = new Font("Arial", 11),
                ForeColor = Color.Black,
                Location = new Point(20, 60),
                Size = new Size(800, 80)
            };
            tabPage2.Controls.Add(descriptionLabel);


            Button calculateButton2 = new Button
            {
                Text = "Рассчитать наработку на отказ",
                Font = new Font("Arial", 11, FontStyle.Bold),
                BackColor = Color.LightGreen,
                ForeColor = Color.Black,
                Location = new Point(20, 160),
                Size = new Size(250, 40),
                Cursor = Cursors.Hand
            };
            calculateButton2.Click += CalculateButton2_Click;
            tabPage2.Controls.Add(calculateButton2);

            Label resultLabel2 = new Label
            {
                Text = "Результат: ",
                Font = new Font("Arial", 12),
                ForeColor = Color.Black,
                Location = new Point(20, 220),
                Size = new Size(800, 30),
                Visible = false
            };
            tabPage2.Controls.Add(resultLabel2);

            Label formulaLabel = new Label
            {
                Text = "Формула: T₀ = (ΣTⱼ) / (Σnⱼ)",
                Font = new Font("Arial", 11, FontStyle.Italic),
                ForeColor = Color.DarkGreen,
                Location = new Point(20, 270),
                Size = new Size(800, 30)
            };
            tabPage2.Controls.Add(formulaLabel);
        }

        private void CalculateButton2_Click(object sender, EventArgs e)
        {
            double[] Tj = { 358, 385, 400 };
            int[] nj = { 4, 3, 2 };

            double sumTj = 0;
            double sumNj = 0;

            for (int i = 0; i < Tj.Length; i++)
            {
                sumTj += Tj[i];
                sumNj += nj[i];
            }

            double T0 = sumTj / sumNj;

            foreach (Control control in tabPage2.Controls)
            {
                if (control is Label label && label.Text.StartsWith("Результат:"))
                {
                    label.Text = $"Результат: Наработка на отказ T₀ = {T0:F2} часов";
                    label.Visible = true;
                    label.ForeColor = Color.DarkRed;
                    label.Font = new Font("Arial", 12, FontStyle.Bold);
                    break;
                }
            }
        }

        private void InitializeTask3Tab()
        {
            Label titleLabel = new Label
            {
                Text = "Задание 3. Сравнение надежности двух систем по коэффициенту готовности",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(20, 20),
                Size = new Size(800, 30)
            };
            tabPage3.Controls.Add(titleLabel);

            Label descriptionLabel = new Label
            {
                Text = "Выберите вариант данных из таблицы и рассчитайте коэффициенты готовности для двух систем",
                Font = new Font("Arial", 11),
                ForeColor = Color.Black,
                Location = new Point(20, 60),
                Size = new Size(800, 30)
            };
            tabPage3.Controls.Add(descriptionLabel);

            Label variantLabel = new Label
            {
                Text = "Выберите вариант (1-16):",
                Font = new Font("Arial", 11),
                ForeColor = Color.Black,
                Location = new Point(20, 100),
                Size = new Size(200, 30)
            };
            tabPage3.Controls.Add(variantLabel);

            ComboBox variantComboBox = new ComboBox
            {
                Location = new Point(220, 100),
                Size = new Size(100, 30),
                Font = new Font("Arial", 11)
            };

            for (int i = 1; i <= 16; i++)
            {
                variantComboBox.Items.Add($"Вариант {i}");
            }
            variantComboBox.SelectedIndex = 0;
            variantComboBox.SelectedIndexChanged += (s, e) =>
            {
                selectedVariant = variantComboBox.SelectedIndex + 1;
            };
            tabPage3.Controls.Add(variantComboBox);

            Button calculateButton3 = new Button
            {
                Text = "Сравнить надежность систем",
                Font = new Font("Arial", 11, FontStyle.Bold),
                BackColor = Color.LightSalmon,
                ForeColor = Color.Black,
                Location = new Point(20, 150),
                Size = new Size(250, 40),
                Cursor = Cursors.Hand
            };
            calculateButton3.Click += CalculateButton3_Click;
            tabPage3.Controls.Add(calculateButton3);

            Label system1Label = new Label
            {
                Text = "Система 1: ",
                Font = new Font("Arial", 12),
                ForeColor = Color.Black,
                Location = new Point(20, 210),
                Size = new Size(800, 30),
                Visible = false
            };
            tabPage3.Controls.Add(system1Label);

            Label system2Label = new Label
            {
                Text = "Система 2: ",
                Font = new Font("Arial", 12),
                ForeColor = Color.Black,
                Location = new Point(20, 250),
                Size = new Size(800, 30),
                Visible = false
            };
            tabPage3.Controls.Add(system2Label);

            Label comparisonLabel = new Label
            {
                Text = "Сравнение: ",
                Font = new Font("Arial", 12),
                ForeColor = Color.Black,
                Location = new Point(20, 290),
                Size = new Size(800, 30),
                Visible = false
            };
            tabPage3.Controls.Add(comparisonLabel);

            Label formulaLabel = new Label
            {
                Text = "Формула: Kг = T₀ / (T₀ + Tв)",
                Font = new Font("Arial", 11, FontStyle.Italic),
                ForeColor = Color.DarkGreen,
                Location = new Point(20, 340),
                Size = new Size(800, 30)
            };
            tabPage3.Controls.Add(formulaLabel);
        }

        private void CalculateButton3_Click(object sender, EventArgs e)
        {
            double[,] variantData = {
                {24, 16, 400, 32},
                {84, 24, 184, 32},
                {225, 8, 64, 24},
                {20, 6, 16, 8},
                {58, 2, 16, 8},
                {516, 19, 160, 8},
                {287, 16, 8, 4},
                {464, 64, 8, 16},
                {96, 12, 48, 8},
                {4, 3, 104, 8},
                {37, 3, 272, 8},
                {101, 3, 336, 8},
                {29, 4, 370, 8},
                {12, 5, 384, 7},
                {3, 24, 56, 8},
                {304, 16, 4, 8}
            };

            int index = selectedVariant - 1;

            double T01 = variantData[index, 0];
            double Tv1 = variantData[index, 1];
            double T02 = variantData[index, 2];
            double Tv2 = variantData[index, 3];

            double Kg1 = T01 / (T01 + Tv1);
            double Kg2 = T02 / (T02 + Tv2);

            string comparison;
            Color comparisonColor;

            if (Kg1 > Kg2)
            {
                comparison = $"Система 1 более надежная (Kг₁ > Kг₂)";
                comparisonColor = Color.Green;
            }
            else if (Kg2 > Kg1)
            {
                comparison = $"Система 2 более надежная (Kг₂ > Kг₁)";
                comparisonColor = Color.Green;
            }
            else
            {
                comparison = $"Системы равнозначны по надежности";
                comparisonColor = Color.Blue;
            }

            foreach (Control control in tabPage3.Controls)
            {
                if (control is Label label)
                {
                    if (label.Text.StartsWith("Система 1:"))
                    {
                        label.Text = $"Система 1: T₀ = {T01} ч, Tв = {Tv1} ч, Kг = {Kg1:F4}";
                        label.Visible = true;
                        label.ForeColor = Color.DarkBlue;
                    }
                    else if (label.Text.StartsWith("Система 2:"))
                    {
                        label.Text = $"Система 2: T₀ = {T02} ч, Tв = {Tv2} ч, Kг = {Kg2:F4}";
                        label.Visible = true;
                        label.ForeColor = Color.DarkBlue;
                    }
                    else if (label.Text.StartsWith("Сравнение:"))
                    {
                        label.Text = $"Сравнение: {comparison}";
                        label.Visible = true;
                        label.ForeColor = comparisonColor;
                        label.Font = new Font("Arial", 12, FontStyle.Bold);
                    }
                }
            }
        }
    }
}
