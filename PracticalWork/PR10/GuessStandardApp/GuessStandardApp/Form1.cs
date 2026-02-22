using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GuessStandardApp
{
    public partial class Form1 : Form
    {
        public class Question
        {
            public string Text { get; set; }
            public string[] Options { get; set; }
            public int CorrectOptionIndex { get; set; }

            public Question(string text, string[] options, int correctOptionIndex)
            {
                Text = text;
                Options = options;
                CorrectOptionIndex = correctOptionIndex;
            }
        }

        private List<Question> questions;
        private int currentQuestionIndex = 0;
        private int correctAnswersCount = 0;
        private RadioButton[] optionRadioButtons;

        private int[] userAnswers;

        private bool testCompleted = false;

        public Form1()
        {
            InitializeComponent();
            InitializeQuestions();
            SetupRadioButtonArray();
            userAnswers = new int[questions.Count];
            for (int i = 0; i < userAnswers.Length; i++)
                userAnswers[i] = -1; 

            ShowCurrentQuestion();
            UpdateProgress();
            UpdateBackButtonState();
        }

        private void InitializeQuestions()
        {
            questions = new List<Question>
            {
                new Question(
                    "Какой стандарт определяет представление чисел с плавающей точкой?",
                    new string[] { "ISO 9001", "IEEE 754", "ASCII", "USB 3.0"  },
                    1 // IEEE 754 
                ),
                new Question(
                    "Какой стандарт описывает базовый набор символов?",
                    new string[] { "Unicode", "ASCII", "UTF-8", "ISO 8859-1" },
                    1 // ASCII
                ),
                new Question(
                    "Какой стандарт определяет требования к системе менеджмента качества?",
                    new string[] { "ISO 9001", "IEEE 802.11", "GMP", "HACCP" },
                    0 // ISO 9001
                ),
                new Question(
                     "Какой стандарт описывает протоколы для Wi-Fi?",
                    new string[] { "IEEE 802.3", "IEEE 802.11", "IEEE 1394", "Bluetooth"  },
                    1 // IEEE 802.11 
                ),
                new Question(
                    "Какой стандарт кодировки включает символы всех письменностей мира?",
                    new string[] { "ASCII", "KOI-8", "Unicode", "Windows-1251" },
                    2 // Unicode 
                )
            };
        }

        private void SetupRadioButtonArray()
        {
            optionRadioButtons = new RadioButton[] { rbOption1, rbOption2, rbOption3, rbOption4 };
        }

        private void ShowCurrentQuestion()
        {
            if (currentQuestionIndex < questions.Count && !testCompleted)
            {
                Question q = questions[currentQuestionIndex];

                lblQuestionTitle.Text = $"Вопрос {currentQuestionIndex + 1} из {questions.Count}";
                lblQuestionText.Text = q.Text;

                for (int i = 0; i < 4; i++)
                {
                    if (i < q.Options.Length)
                    {
                        optionRadioButtons[i].Text = q.Options[i];
                        optionRadioButtons[i].Visible = true;
                        optionRadioButtons[i].Checked = (userAnswers[currentQuestionIndex] == i);
                    }
                }
                btnNext.Enabled = true;
            }
        }

        private void UpdateProgress()
        {
            lblProgress.Text = $"Правильных ответов: {correctAnswersCount} из {questions.Count}";
        }

        private void UpdateBackButtonState()
        {
            btnBack.Enabled = (currentQuestionIndex > 0 && !testCompleted);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (testCompleted) return;

            int selectedIndex = -1;
            for (int i = 0; i < 4; i++)
            {
                if (optionRadioButtons[i].Checked)
                {
                    selectedIndex = i;
                    break;
                }
            }

            if (selectedIndex == -1)
            {
                MessageBox.Show("Пожалуйста, выберите вариант ответа!",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            userAnswers[currentQuestionIndex] = selectedIndex;

            if (selectedIndex == questions[currentQuestionIndex].CorrectOptionIndex)
            {
                correctAnswersCount++;
            }

            UpdateProgress();
            currentQuestionIndex++;

            if (currentQuestionIndex < questions.Count)
            {
                ShowCurrentQuestion();
            }
            else
            {
                testCompleted = true;
                ShowFinalResult();
            }

            UpdateBackButtonState();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (testCompleted) return;

            if (currentQuestionIndex > 0)
            {
                if (userAnswers[currentQuestionIndex - 1] != -1)
                {
                    if (userAnswers[currentQuestionIndex - 1] == questions[currentQuestionIndex - 1].CorrectOptionIndex)
                    {
                        correctAnswersCount--;
                    }
                }

                currentQuestionIndex--;
                ShowCurrentQuestion();
                UpdateProgress();
                UpdateBackButtonState();
            }
        }

        private void ShowFinalResult()
        {
            btnNext.Enabled = false;
            btnBack.Enabled = false;

            foreach (var rb in optionRadioButtons)
            {
                rb.Enabled = false;
            }

            double percentage = (double)correctAnswersCount / questions.Count * 100;

            string message = $"Тест завершен!\n\n" +
                           $"Правильных ответов: {correctAnswersCount} из {questions.Count}\n" +
                           $"Процент: {percentage:F1}%\n\n" +
                           $"Хотите пройти тест заново?";

            DialogResult result = MessageBox.Show(message, "Результат теста",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                RestartTest();
            }
            else
            {
                Application.Exit();
            }
        }

        private void RestartTest()
        {
            currentQuestionIndex = 0;
            correctAnswersCount = 0;
            testCompleted = false;

            for (int i = 0; i < userAnswers.Length; i++)
                userAnswers[i] = -1;

            btnNext.Enabled = true;
            btnBack.Enabled = false; 
            foreach (var rb in optionRadioButtons)
            {
                rb.Enabled = true;
                rb.Checked = false;
            }

            ShowCurrentQuestion();
            UpdateProgress();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Начать тест заново?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                RestartTest();
            }
        }
    }
}