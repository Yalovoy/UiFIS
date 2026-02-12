using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReliabilityCalculatorV7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            richTextBoxResult.Font = new Font("Consolas", 10);
            richTextBoxResult.ReadOnly = true;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                double N = double.Parse(textBoxN.Text.Replace(",", "."),
                    System.Globalization.CultureInfo.InvariantCulture);
                double Mt = double.Parse(textBoxMt.Text.Replace(",", "."),
                    System.Globalization.CultureInfo.InvariantCulture);
                double v = double.Parse(textBoxVx.Text.Replace(",", "."),
                    System.Globalization.CultureInfo.InvariantCulture);
                double t = double.Parse(textBoxT.Text.Replace(",", "."),
                    System.Globalization.CultureInfo.InvariantCulture);

                double sigma = v * Mt;

                double Up = (t - Mt) / sigma;

                double P = NormalCDF(Up);

                double Q = P;

                double failedCount = Q * N;

                string result = $"РАСЧЕТ ПОКАЗАТЕЛЕЙ НАДЕЖНОСТИ\n";
                result += $"================================\n\n";
                result += $"ИСХОДНЫЕ ДАННЫЕ:\n";
                result += $"Количество изделий (N): {N} шт.\n";
                result += $"Средняя наработка (Mt): {Mt} ч\n";
                result += $"Коэффициент вариации (ν): {v}\n";
                result += $"Наработка для расчета (t): {t} ч\n\n";

                result += $"РЕЗУЛЬТАТЫ РАСЧЕТА:\n";
                result += $"================================\n\n";

                result += $"1. Среднее квадратическое отклонение:\n";
                result += $"   σ = ν × Mt = {v} × {Mt} = {sigma:F1} час\n\n";

                result += $"2. Квантиль нормированного нормального распределения:\n";
                result += $"   U_p = (t - Mt) / σ = ({t} - {Mt}) / {sigma:F1} = {Up:F4}\n\n";

                result += $"3. Функция Лапласа (нормированная функция распределения):\n";
                result += $"   Φ(U_p) = Φ({Up:F4}) = {P:F4}\n\n";

                result += $"4. Вероятность отказа:\n";
                result += $"   Q(t) = Φ(U_p) = {Q:F4} или {Q * 100:F2}%\n\n";

                result += $"5. Количество отказавших изделий:\n";
                result += $"   n(t) = Q(t) × N = {Q:F4} × {N} = {failedCount:F1} из {N}\n\n";

                result += $"================================\n";
                result += $"Расчет выполнен для нормального распределения.\n";

                richTextBoxResult.Text = result;

                CalculateForInterval(N, Mt, sigma);
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка расчета: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateForInterval(double N, double Mt, double sigma)
        {
            try
            {
                double t_start = 200;
                double t_end = 250;

                double Up_start = (t_start - Mt) / sigma;
                double Up_end = (t_end - Mt) / sigma;

                double Q_start = NormalCDF(Up_start);
                double Q_end = NormalCDF(Up_end);

                double Q_interval = Q_end - Q_start;
                double failedInInterval = Q_interval * N;

                string intervalInfo = $"\n\nДОПОЛНИТЕЛЬНЫЙ РАСЧЕТ ДЛЯ ИНТЕРВАЛА [200, 250] ч:\n";
                intervalInfo += $"================================================\n\n";

                intervalInfo += $"1. Для tₐ = {t_start} часов:\n";
                intervalInfo += $"   U_p(tₐ) = (tₐ - M_t) / σ = ({t_start} - {Mt}) / {sigma:F1} = {Up_start:F4}\n";
                intervalInfo += $"   Φ(U_p(tₐ)) = Φ({Up_start:F4}) = {Q_start:F4}\n\n";

                intervalInfo += $"2. Для tᵦ = {t_end} часов:\n";
                intervalInfo += $"   U_p(tᵦ) = (tᵦ - M_t) / σ = ({t_end} - {Mt}) / {sigma:F1} = {Up_end:F4}\n";
                intervalInfo += $"   Φ(U_p(tᵦ)) = Φ({Up_end:F4}) = {Q_end:F4}\n\n";

                intervalInfo += $"3. Вероятность отказа в интервале:\n";
                intervalInfo += $"   ΔQ = Q(tᵦ) - Q(tₐ) = Φ(U_p(tᵦ)) - Φ(U_p(tₐ))\n";
                intervalInfo += $"   ΔQ = {Q_end:F4} - {Q_start:F4} = {Q_interval:F4}\n\n";

                intervalInfo += $"4. Количество отказавших в интервале:\n";
                intervalInfo += $"   Δn = ΔQ × N = {Q_interval:F4} × {N} = {failedInInterval:F2} долот\n\n";

                intervalInfo += $"================================================\n";
                intervalInfo += $"Ответ: {failedInInterval:F2} долот";

                richTextBoxResult.AppendText(intervalInfo);
            }
            catch (Exception ex)
            {
                richTextBoxResult.AppendText($"\n\nОшибка при расчете интервала: {ex.Message}");
            }
        }

        private double NormalCDF(double x)
        {
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;
            double p = 0.3275911;

            int sign = 1;
            if (x < 0)
                sign = -1;
            x = Math.Abs(x) / Math.Sqrt(2.0);

            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

            return 0.5 * (1.0 + sign * y);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}