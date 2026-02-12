using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReliabilityAppV14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private double CalculateOperationalReadiness(double T, double Tb, double t)
        {
            double Kg = T / (T + Tb);
            double lambda = 1.0 / T;
            return Kg * Math.Exp(-lambda * t);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                double T = Convert.ToDouble(txtT.Text);
                double Tb = Convert.ToDouble(txtTb.Text);
                double t = Convert.ToDouble(txtTime.Text);

                if (T <= 0 || Tb <= 0 || t < 0)
                {
                    MessageBox.Show("Все значения должны быть положительными!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                double result = CalculateOperationalReadiness(T, Tb, t);
                txtResult.Text = result.ToString("F6");
                ShowCalculationDetails(T, Tb, t, result);
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректные числовые значения!", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowCalculationDetails(double T, double Tb, double t, double Kog)
        {
            double Kg = T / (T + Tb);
            double lambda = 1.0 / T;
            double Kp = Tb / (T + Tb);

            richTextBox1.Clear();
            richTextBox1.AppendText("ДЕТАЛИ РАСЧЕТА:\n\n");
            richTextBox1.AppendText($"1. Коэффициент готовности:\n");
            richTextBox1.AppendText($"   Kг = T / (T + Tв) = {T} / ({T} + {Tb}) = {Kg:F6}\n\n");
            richTextBox1.AppendText($"2. Интенсивность отказов:\n");
            richTextBox1.AppendText($"   λ = 1 / T = 1 / {T} = {lambda:F6} 1/час\n\n");
            richTextBox1.AppendText($"3. Коэффициент простоя:\n");
            richTextBox1.AppendText($"   Kп = Tв / (T + Tв) = {Tb} / ({T} + {Tb}) = {Kp:F6}\n\n");
            richTextBox1.AppendText($"4. Коэффициент оперативной готовности:\n");
            richTextBox1.AppendText($"   Kог(t) = Kг * e^(-λ*t)\n");
            richTextBox1.AppendText($"   = {Kg:F6} * e^(-{lambda:F6} * {t})\n");
            richTextBox1.AppendText($"   = {Kog:F6}\n\n");
            richTextBox1.AppendText($"Вероятность работоспособности через {t} ч: {Kog * 100:F2}%");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtT.Clear();
            txtTb.Clear();
            txtTime.Clear();
            txtResult.Clear();
            richTextBox1.Clear();
        }

  
    }
}