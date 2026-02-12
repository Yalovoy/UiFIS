using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MotionAnalyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeChart();
        }

        private void InitializeChart()
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            ChartArea chartArea = new ChartArea("MotionChart");
            chartArea.AxisX.Title = "Время, t (с)";
            chartArea.AxisY.Title = "Путь, S (м)";
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisY.Minimum = 0;
            chartArea.BackColor = Color.White;

            chart1.ChartAreas.Add(chartArea);

            Series lineSeries = new Series("Path")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Blue,
                BorderWidth = 2,
                MarkerStyle = MarkerStyle.None
            };
            chart1.Series.Add(lineSeries);

            Series pointSeries = new Series("KeyPoints")
            {
                ChartType = SeriesChartType.Point,
                Color = Color.Red,
                MarkerSize = 8,
                MarkerStyle = MarkerStyle.Circle
            };
            chart1.Series.Add(pointSeries);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                double v0 = double.Parse(txtV0.Text.Replace(",", "."),
                    System.Globalization.CultureInfo.InvariantCulture);
                double a = double.Parse(txtAcceleration.Text.Replace(",", "."),
                    System.Globalization.CultureInfo.InvariantCulture);
                double t = double.Parse(txtTime.Text.Replace(",", "."),
                    System.Globalization.CultureInfo.InvariantCulture);

                if (t <= 0)
                {
                    MessageBox.Show("Время движения должно быть положительным числом!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CalculateMotion(v0, a, t);
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения!",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка расчета: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateMotion(double v0, double a, double t)
        {
            double v = v0 + a * t;

            double s = v0 * t + (a * t * t) / 2;

            string motionType = DetermineMotionType(v0, a);

            string description = GenerateDescription(v0, a, t, v, s);

            txtMotionType.Text = motionType;
            txtDistance.Text = s.ToString("F2");
            txtFinalSpeed.Text = v.ToString("F2");

            UpdateEquation(v0, a);

            BuildChart(v0, a, t);
        }

        private string DetermineMotionType(double v0, double a)
        {
            if (a == 0)
            {
                if (v0 == 0)
                    return "Покой";
                else
                    return "Равномерное движение";
            }
            else if (a > 0)
            {
                if (v0 == 0)
                    return "Равноускоренное из состояния покоя";
                else if (v0 > 0)
                    return "Равноускоренное движение";
                else
                    return "Равноускоренное с началом в противоположную сторону";
            }
            else
            {
                if (v0 > 0)
                    return "Равнозамедленное движение";
                else if (v0 == 0)
                    return "Невозможно (отрицательное ускорение при нулевой скорости)";
                else
                    return "Ускорение в противоположном направлении";
            }
        }

        private string GenerateDescription(double v0, double a, double t, double v, double s)
        {
            string description = "";

            if (a == 0)
            {
                if (v0 == 0)
                    description = "Тело находится в состоянии покоя. Пройденный путь равен нулю.";
                else
                    description = $"Тело движется равномерно со скоростью {Math.Abs(v0):F2} м/с. ";
                description += $"За время {t:F1} с тело прошло {s:F2} м.";
            }
            else if (a > 0)
            {
                if (v0 == 0)
                    description = $"Тело начинает движение из состояния покоя с ускорением {a:F2} м/с². ";
                else if (v0 > 0)
                    description = $"Тело движется равноускоренно. Начальная скорость {v0:F2} м/с, ускорение {a:F2} м/с². ";
                else
                    description = $"Тело начинает движение в противоположную сторону с ускорением {a:F2} м/с². ";

                description += $"Конечная скорость: {v:F2} м/с. Пройденный путь: {s:F2} м.";

                if (v0 < 0 && v > 0)
                {
                    double tStop = -v0 / a;
                    description += $"\nТело остановилось через {tStop:F2} с и начало движение в обратном направлении.";
                }
            }
            else
            {
                description = $"Тело движется равнозамедленно. Начальная скорость {v0:F2} м/с, ускорение {a:F2} м/с². ";
                description += $"Конечная скорость: {v:F2} м/с. Пройденный путь: {s:F2} м.";

                if (v0 > 0 && v < 0)
                {
                    double tStop = -v0 / a;
                    double sStop = v0 * tStop + (a * tStop * tStop) / 2;
                    description += $"\nТело остановилось через {tStop:F2} с, пройдя {sStop:F2} м, и начало движение в обратном направлении.";
                }
                else if (v0 > 0 && v == 0)
                {
                    double tStop = -v0 / a;
                    double sStop = v0 * tStop + (a * tStop * tStop) / 2;
                    description += $"\nТело полностью остановилось через {tStop:F2} с, пройдя {sStop:F2} м.";
                }
            }

            if (v > 0)
                description += $"\nВ конечный момент тело движется в положительном направлении.";
            else if (v < 0)
                description += $"\nВ конечный момент тело движется в отрицательном направлении.";
            else
                description += $"\nВ конечный момент тело остановилось.";

            return description;
        }

        private void UpdateEquation(double v0, double a)
        {
            string sign = a >= 0 ? "+" : "-";
            double absA = Math.Abs(a);

            if (v0 == 0 && a == 0)
            {
                lblEquation.Text = "S(t) = 0";
            }
            else if (a == 0)
            {
                lblEquation.Text = $"S(t) = {v0:F2}t";
            }
            else if (v0 == 0)
            {
                lblEquation.Text = $"S(t) = {sign} {absA:F2}t²/2";
            }
            else
            {
                lblEquation.Text = $"S(t) = {v0:F2}t {sign} {absA:F2}t²/2";
            }
        }

        private void BuildChart(double v0, double a, double tMax)
        {
            chart1.Series["Path"].Points.Clear();
            chart1.Series["KeyPoints"].Points.Clear();

            int pointsCount = 100;
            double step = tMax / pointsCount;

            for (int i = 0; i <= pointsCount; i++)
            {
                double time = i * step;
                double distance = v0 * time + (a * time * time) / 2;

                chart1.Series["Path"].Points.AddXY(time, distance);

                if (i % 10 == 0)
                {
                    chart1.Series["KeyPoints"].Points.AddXY(time, distance);
                }
            }

            chart1.Series["KeyPoints"].Points.AddXY(0, 0);
            double finalDistance = v0 * tMax + (a * tMax * tMax) / 2;
            chart1.Series["KeyPoints"].Points.AddXY(tMax, finalDistance);

            chart1.ChartAreas[0].RecalculateAxesScale();

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Minimum = Math.Min(0, chart1.ChartAreas[0].AxisY.Minimum);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtV0.Text = "";
            txtAcceleration.Text = "";
            txtTime.Text = "";

            txtMotionType.Text = "";
            txtDistance.Text = "";
            txtFinalSpeed.Text = "";

            lblEquation.Text = "S(t) = ";

            chart1.Series["Path"].Points.Clear();
            chart1.Series["KeyPoints"].Points.Clear();

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 10;
            chart1.ChartAreas[0].AxisY.Maximum = 10;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblEquation.Text = "S(t) = ";
        }

    }
}