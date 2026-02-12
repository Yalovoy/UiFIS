using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FieldOfWondersGame
{
    public partial class Form1 : Form
    {
        private string originalWord = "";
        private List<char> shuffledLetters = new List<char>();
        private List<Button> mixedLetterButtons = new List<Button>();
        private List<Button> resultLetterSlots = new List<Button>();
        private int attempts = 0;
        private bool gameStarted = false;

        private readonly List<string> dictionary = new List<string>
        {
            "ПРОГРАММА", "КОМПЬЮТЕР", "ИНТЕРНЕТ", "ВИЗУАЛИЗАЦИЯ",
            "ФОРМА", "КЛАВИАТУРА", "МЫШЬ", "ОКНО"
        };

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            this.Text = "Поле чудес";
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            buttonStart.Click += ButtonStart_Click;
            buttonCheck.Click += ButtonCheck_Click;
            buttonNewGame.Click += ButtonNewGame_Click;
            buttonGenerateWord.Click += ButtonGenerateWord_Click;
        }

        private void ButtonGenerateWord_Click(object sender, EventArgs e)
        {
            if (!gameStarted)
            {
                GenerateAndSetWord();
            }
            else
            {
                var confirmResult = MessageBox.Show(
                    "Игра уже идет. Начать новую игру с новым словом?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    ResetGame();
                    GenerateAndSetWord();
                }
            }
        }

        private void GenerateAndSetWord()
        {
            Random random = new Random();
            int index = random.Next(dictionary.Count);
            string randomWord = dictionary[index];

            textBoxInputWord.Text = randomWord;

            StartNewGame(randomWord);
        }


        private void ButtonStart_Click(object sender, EventArgs e)
        {
            string inputWord = textBoxInputWord.Text.Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(inputWord))
            {
                MessageBox.Show("Введите слово для начала игры или нажмите 'Генерировать слово'!");
                return;
            }

            if (inputWord.Length < 3)
            {
                MessageBox.Show("Слово должно содержать минимум 3 буквы!");
                return;
            }

            if (!inputWord.All(char.IsLetter))
            {
                MessageBox.Show("Слово должно содержать только буквы!");
                return;
            }

            StartNewGame(inputWord);
        }

        private void StartNewGame(string word)
        {
            originalWord = word;
            shuffledLetters = ShuffleLetters(word.ToList());
            attempts = 0;
            gameStarted = true;

            SetupMixedLettersPanel();
            SetupResultPanel();

            ShowGameElements();

            labelResult.Text = "";
            labelResult.Visible = false;
        }

        private List<char> ShuffleLetters(List<char> letters)
        {
            Random random = new Random();
            List<char> shuffled = new List<char>(letters);

            for (int i = shuffled.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                char temp = shuffled[i];
                shuffled[i] = shuffled[j];
                shuffled[j] = temp;
            }

            return shuffled;
        }

        private void SetupMixedLettersPanel()
        {
            flowLayoutPanelMixed.Controls.Clear();
            mixedLetterButtons.Clear();

            foreach (char letter in shuffledLetters)
            {
                Button letterButton = new Button
                {
                    Text = letter.ToString(),
                    Size = new Size(40, 40),
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    BackColor = Color.LightBlue,
                    ForeColor = Color.Black,
                    Tag = letter,
                    Margin = new Padding(5),
                    Cursor = Cursors.Hand
                };

                letterButton.Click += MixedLetterButton_Click;
                mixedLetterButtons.Add(letterButton);
                flowLayoutPanelMixed.Controls.Add(letterButton);
            }
        }

        private void SetupResultPanel()
        {
            flowLayoutPanelResult.Controls.Clear();
            resultLetterSlots.Clear();

            for (int i = 0; i < originalWord.Length; i++)
            {
                Button slotButton = new Button
                {
                    Text = "",
                    Size = new Size(40, 40),
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    Tag = -1,
                    Margin = new Padding(5),
                    Cursor = Cursors.Hand
                };

                slotButton.Click += ResultSlotButton_Click;
                resultLetterSlots.Add(slotButton);
                flowLayoutPanelResult.Controls.Add(slotButton);
            }
        }

        private void MixedLetterButton_Click(object sender, EventArgs e)
        {
            if (!gameStarted) return;

            Button clickedButton = sender as Button;
            if (clickedButton == null) return;

            char letter = (char)clickedButton.Tag;
            int buttonIndex = mixedLetterButtons.IndexOf(clickedButton);

            for (int i = 0; i < resultLetterSlots.Count; i++)
            {
                if (string.IsNullOrEmpty(resultLetterSlots[i].Text))
                {
                    resultLetterSlots[i].Text = letter.ToString();
                    resultLetterSlots[i].Tag = buttonIndex;
                    resultLetterSlots[i].BackColor = Color.LightGreen;
                    break;
                }
            }

            UpdateGameState();
        }

        private void ResultSlotButton_Click(object sender, EventArgs e)
        {
            if (!gameStarted) return;

            Button clickedSlot = sender as Button;
            if (clickedSlot == null || string.IsNullOrEmpty(clickedSlot.Text)) return;

            int buttonIndex = (int)clickedSlot.Tag;
            if (buttonIndex >= 0 && buttonIndex < mixedLetterButtons.Count)
            {
                mixedLetterButtons[buttonIndex].Enabled = true;
                mixedLetterButtons[buttonIndex].BackColor = Color.LightBlue;
            }

            clickedSlot.Text = "";
            clickedSlot.Tag = -1;
            clickedSlot.BackColor = Color.White;

            UpdateGameState();
        }

        private void ButtonCheck_Click(object sender, EventArgs e)
        {
            if (!gameStarted) return;

            attempts++;

            string userWord = "";
            foreach (Button slot in resultLetterSlots)
            {
                userWord += slot.Text;
            }

            if (userWord == originalWord)
            {
                labelResult.Text = "Правильно!";
                labelResult.ForeColor = Color.Green;
                labelResult.Font = new Font("Arial", 12, FontStyle.Bold);

                foreach (Button slot in resultLetterSlots)
                {
                    slot.BackColor = Color.Green;
                    slot.ForeColor = Color.White;
                }

                foreach (Button mixedButton in mixedLetterButtons)
                {
                    mixedButton.Enabled = false;
                    mixedButton.BackColor = Color.LightGray;
                }

                buttonCheck.Enabled = false;
            }
            else
            {
                labelResult.Text = $"Неверно! Правильное слово: {originalWord}";
                labelResult.ForeColor = Color.Red;
                labelResult.Font = new Font("Arial", 12, FontStyle.Bold);

                for (int i = 0; i < resultLetterSlots.Count; i++)
                {
                    if (i < userWord.Length && i < originalWord.Length)
                    {
                        if (userWord[i] == originalWord[i])
                        {
                            resultLetterSlots[i].BackColor = Color.Green;
                        }
                        else
                        {
                            resultLetterSlots[i].BackColor = Color.Red;
                        }
                    }
                }
            }

            labelResult.Visible = true;
        }

        private void ButtonNewGame_Click(object sender, EventArgs e)
        {
            ResetGame();
            textBoxInputWord.Text = "";
            textBoxInputWord.Focus();
        }

        private void ResetGame()
        {
            originalWord = "";
            shuffledLetters.Clear();
            mixedLetterButtons.Clear();
            resultLetterSlots.Clear();
            attempts = 0;
            gameStarted = false;

            flowLayoutPanelMixed.Controls.Clear();
            flowLayoutPanelResult.Controls.Clear();

            HideGameElements();

            labelResult.Text = "";
            labelResult.Visible = false;
        }

        private void ShowGameElements()
        {
            labelMixedTitle.Visible = true;
            flowLayoutPanelMixed.Visible = true;
            labelResultTitle.Visible = true;
            flowLayoutPanelResult.Visible = true;
            buttonCheck.Visible = true;
            buttonNewGame.Visible = true;

            buttonStart.Enabled = false;
            textBoxInputWord.Enabled = false;
        }

        private void HideGameElements()
        {
            labelMixedTitle.Visible = false;
            flowLayoutPanelMixed.Visible = false;
            labelResultTitle.Visible = false;
            flowLayoutPanelResult.Visible = false;
            buttonCheck.Visible = false;
            buttonNewGame.Visible = false;

            buttonStart.Enabled = true;
            textBoxInputWord.Enabled = true;
        }

        private void UpdateGameState()
        {
            bool allSlotsFilled = resultLetterSlots.All(slot => !string.IsNullOrEmpty(slot.Text));
            buttonCheck.Enabled = allSlotsFilled;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}