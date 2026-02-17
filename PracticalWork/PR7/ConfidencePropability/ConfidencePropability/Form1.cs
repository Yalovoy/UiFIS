using System;
using System.Windows.Forms;

namespace ConfidenceProbability
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            string input = txtErrorProbability.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Введите вероятность ошибки!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            input = input.Replace('.', ',');

            if (!double.TryParse(input, out double pError))
            {
                MessageBox.Show("Введите число (можно с точкой или запятой)!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pError < 0 || pError > 1)
            {
                MessageBox.Show("Вероятность должна быть в диапазоне от 0 до 1!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double confidence = 1 - pError;

            string resultText = "";
            resultText += $"Вероятность грубой ошибки: {pError:F4}\r\n";
            resultText += "Доверительная вероятность = 1 - P_ош\r\n";
            resultText += $"= 1 - {pError:F4}\r\n";
            resultText += $"= {confidence:F4}\r\n";
            resultText += $"= {confidence:P2}";

            lblResult.Text = resultText;
        }
    }
}