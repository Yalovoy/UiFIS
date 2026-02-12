using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.Http;
using System.Xml;
using System.Threading.Tasks;
using System.Globalization;

namespace CurrencyConverter
{
    public partial class Form1 : Form
    {
        private Dictionary<string, decimal> exchangeRates = new Dictionary<string, decimal>();
        private DateTime exchangeRateDate;
        private bool ratesLoaded = false;

        private readonly Dictionary<string, string> currencyNames = new Dictionary<string, string>
        {
            { "RUB", "Российский рубль" },
            { "USD", "Доллар США" },
            { "EUR", "Евро" },
            { "CNY", "Китайский юань" },
            { "KRW", "Южнокорейская вона" }
        };

        public Form1()
        {
            InitializeComponent();
            InitializeForm();
            _ = LoadExchangeRatesAsync();
        }

        private void InitializeForm()
        {
            this.Text = "Конвертер валют";
            this.Size = new System.Drawing.Size(480, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

            labelRatesTitle.Text = "Курсы валют к RUB";

            foreach (var currency in currencyNames)
            {
                string displayText = currencyNames[currency.Key];
                comboBoxFrom.Items.Add(displayText);
                comboBoxTo.Items.Add(displayText);
            }

            comboBoxFrom.SelectedIndex = 0; 
            comboBoxTo.SelectedIndex = 1;   

            buttonSwapCurrencies.Click += ButtonSwapCurrencies_Click;
            buttonUpdateRates.Click += ButtonUpdateRates_Click;
            textBoxAmount.TextChanged += TextBoxAmount_TextChanged;
            comboBoxFrom.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            comboBoxTo.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

            textBoxAmount.KeyPress += TextBoxAmount_KeyPress;
        }

        private async Task LoadExchangeRatesAsync()
        {
            try
            {
                labelStatus.Text = "Загрузка курсов...";
                buttonUpdateRates.Enabled = false;

                using (HttpClient client = new HttpClient())
                {
                    string xmlData = await client.GetStringAsync("https://www.cbr.ru/scripts/XML_daily.asp");

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlData);
                    XmlNode dateNode = xmlDoc.SelectSingleNode("//ValCurs/@Date");
                    if (dateNode != null)
                    {
                        exchangeRateDate = DateTime.ParseExact(dateNode.Value, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        labelDate.Text = $"Дата курса: {exchangeRateDate.ToString("dd.MM.yyyy")}";
                    }


                    XmlNodeList valuteNodes = xmlDoc.SelectNodes("//Valute");


                    exchangeRates.Clear();
                    listBoxRates.Items.Clear(); 

                    exchangeRates["RUB"] = 1m;

                    List<string> rateDisplay = new List<string>();

                    foreach (XmlNode node in valuteNodes)
                    {
                        string charCode = node.SelectSingleNode("CharCode").InnerText;
                        string valueStr = node.SelectSingleNode("Value").InnerText;
                        string nominalStr = node.SelectSingleNode("Nominal").InnerText;

                        valueStr = valueStr.Replace(",", ".");

                        if (decimal.TryParse(valueStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value) &&
                            int.TryParse(nominalStr, out int nominal))
                        {
                           
                            decimal rate = value / nominal;

                            if (currencyNames.ContainsKey(charCode))
                            {
                                exchangeRates[charCode] = rate;

                                rateDisplay.Add($"1 {charCode} = {rate:F4} RUB");
                            }
                        }
                    }

                    rateDisplay.Sort();
                    foreach (string rate in rateDisplay)
                    {
                        listBoxRates.Items.Add(rate);
                    }

                    if (!exchangeRates.ContainsKey("KRW"))
                    {
                        exchangeRates["KRW"] = 0.067m;
                        listBoxRates.Items.Add($"1 KRW = 0.0670 RUB (примерно)");
                    }

                    ratesLoaded = true;
                    labelStatus.Text = "Курсы загружены";

                    buttonUpdateRates.Enabled = true;

                    await ConvertCurrencyAsync();
                }
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"Ошибка загрузки: {ex.Message}";

                SetTestRates();
                ratesLoaded = true;
                buttonUpdateRates.Enabled = true;
            }
        }

        private void ButtonSwapCurrencies_Click(object sender, EventArgs e)
        {
            if (comboBoxFrom.SelectedIndex >= 0 && comboBoxTo.SelectedIndex >= 0)
            {
                int fromIndex = comboBoxFrom.SelectedIndex;
                int toIndex = comboBoxTo.SelectedIndex;

                comboBoxFrom.SelectedIndex = toIndex;
                comboBoxTo.SelectedIndex = fromIndex;

                _ = ConvertCurrencyAsync();
            }
        }

        private async void ButtonUpdateRates_Click(object sender, EventArgs e)
        {
            await LoadExchangeRatesAsync();
        }

        private async void TextBoxAmount_TextChanged(object sender, EventArgs e)
        {
            if (textBoxAmount.Text.Length > 0 && decimal.TryParse(textBoxAmount.Text.Replace(",", "."),
                NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            {
                await ConvertCurrencyAsync();
            }
        }

        private async void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFrom.SelectedIndex >= 0 && comboBoxTo.SelectedIndex >= 0 && ratesLoaded)
            {
                await ConvertCurrencyAsync();
            }
        }

        private async Task ConvertCurrencyAsync()
        {
            if (!ratesLoaded) return;

            try
            {
                string fromCurrency = GetCurrencyCodeByDisplayName(comboBoxFrom.SelectedItem?.ToString());
                string toCurrency = GetCurrencyCodeByDisplayName(comboBoxTo.SelectedItem?.ToString());

                if (string.IsNullOrEmpty(fromCurrency) || string.IsNullOrEmpty(toCurrency))
                {
                    textBoxResult.Text = "Ошибка выбора валют";
                    return;
                }

                if (!decimal.TryParse(textBoxAmount.Text.Replace(",", "."),
                    NumberStyles.Any, CultureInfo.InvariantCulture, out decimal amount))
                {
                    textBoxResult.Text = "Введите число";
                    return;
                }

                if (!exchangeRates.ContainsKey(fromCurrency) || !exchangeRates.ContainsKey(toCurrency))
                {
                    textBoxResult.Text = "Курс не найден";
                    return;
                }

                decimal fromRateToRub = exchangeRates[fromCurrency]; 
                decimal toRateToRub = exchangeRates[toCurrency];     

                decimal amountInRubles = amount * fromRateToRub;
                decimal result = amountInRubles / toRateToRub;

                textBoxResult.Text = result.ToString("N2");
            }
            catch (Exception ex)
            {
                textBoxResult.Text = "Ошибка расчета";
                labelStatus.Text = $"Ошибка: {ex.Message}";
            }
        }

        private string GetCurrencyCodeByDisplayName(string displayName)
        {
            if (string.IsNullOrEmpty(displayName)) return "";

            foreach (var currency in currencyNames)
            {
                if (currency.Value == displayName)
                {
                    return currency.Key;
                }
            }

            return "";
        }

        private void SetTestRates()
        {
            exchangeRates.Clear();
            listBoxRates.Items.Clear(); 

            exchangeRates["RUB"] = 1m;
            exchangeRates["USD"] = 77.70m;
            exchangeRates["EUR"] = 90.34m;
            exchangeRates["CNY"] = 10.96m;
            exchangeRates["KRW"] = 0.067m;

            listBoxRates.Items.Add("1 USD = 77.70 RUB");
            listBoxRates.Items.Add("1 EUR = 90.34 RUB");
            listBoxRates.Items.Add("1 CNY = 10.96 RUB");
            listBoxRates.Items.Add("1 KRW = 0.0670 RUB");

            exchangeRateDate = DateTime.Now;
            labelDate.Text = $"Дата курса: {exchangeRateDate.ToString("dd.MM.yyyy")} (тестовые)";
            labelStatus.Text = "Используются тестовые курсы";
        }

        private void TextBoxAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            if (e.KeyChar == ',')
            {
                e.KeyChar = '.';
            }

            if ((e.KeyChar == '.' || e.KeyChar == ',') &&
                ((sender as TextBox)?.Text.Contains(".") == true ||
                 (sender as TextBox)?.Text.Contains(",") == true))
            {
                e.Handled = true;
            }
        }
    }
}