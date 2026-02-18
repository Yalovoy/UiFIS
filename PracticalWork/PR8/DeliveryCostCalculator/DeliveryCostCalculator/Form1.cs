using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DeliveryCostCalculator
{
    public partial class Form1 : Form
    {
        private DeliveryService _deliveryService;
        private List<RouteInfo> _history;
        private BindingSource _historyBindingSource;


        public Form1()
        {
            InitializeComponent();
            InitializeService();
            SetupHistoryGrid();
            LoadTransportTypes();
            AttachEvents();
            SetStatus("Готов", Color.Orange);
        }

        private void InitializeService()
        {
            _deliveryService = new DeliveryService();

            _deliveryService.ErrorOccurred += (s, msg) =>
            {
                if (InvokeRequired)
                    Invoke(new Action(() => MessageBox.Show(msg, "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error)));
                else
                    MessageBox.Show(msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            _deliveryService.StatusChanged += (s, msg) =>
            {
                if (InvokeRequired)
                    Invoke(new Action(() => UpdateStatus(msg)));
                else
                    UpdateStatus(msg);
            };
        }

        private void SetupHistoryGrid()
        {
            _history = new List<RouteInfo>();
            _historyBindingSource = new BindingSource { DataSource = _history };

            dgvHistory.AutoGenerateColumns = false;
            dgvHistory.Columns.Clear();

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FromAddress",
                HeaderText = "Откуда",
                Width = 150
            });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ToAddress",
                HeaderText = "Куда",
                Width = 150
            });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DistanceKm",
                HeaderText = "Расстояние (км)",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "F1" }
            });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Cost",
                HeaderText = "Стоимость (руб)",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "F2" }
            });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TransportType",
                HeaderText = "Транспорт",
                Width = 80
            });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CalculationTime",
                HeaderText = "Время расчета",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy HH:mm" }
            });

            dgvHistory.DataSource = _historyBindingSource;
        }

        private void LoadTransportTypes()
        {
            cmbTransport.DisplayMember = "ToString";
            cmbTransport.DataSource = _deliveryService.GetTransportRates();
        }

        private void AttachEvents()
        {
            btnCalculate.Click += BtnCalculate_Click;
            btnClear.Click += BtnClear_Click;
            btnSwap.Click += BtnSwap_Click;
            dgvHistory.CellDoubleClick += DgvHistory_CellDoubleClick;
        }

        private void SetStatus(string text, Color color)
        {
            if (lblStatus.InvokeRequired)
            {
                lblStatus.Invoke(new Action(() =>
                {
                    lblStatus.Text = text;
                    lblStatus.ForeColor = color;
                }));
            }
            else
            {
                lblStatus.Text = text;
                lblStatus.ForeColor = color;
            }
        }

        private async void BtnCalculate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFrom.Text) || string.IsNullOrWhiteSpace(txtTo.Text))
            {
                MessageBox.Show("Заполните поля 'Откуда' и 'Куда'", "Ошибка ввода",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbTransport.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип транспорта", "Ошибка ввода",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnCalculate.Enabled = false;
            btnSwap.Enabled = false;
            SetStatus("Выполняется расчет...", Color.DodgerBlue);

            try
            {
                string transportType = (cmbTransport.SelectedItem as TransportRate)?.Name ?? "Автомобиль";

                var result = await _deliveryService.CalculateDeliveryAsync(
                    txtFrom.Text.Trim(),
                    txtTo.Text.Trim(),
                    transportType);

                if (result != null)
                {
                    DisplayResult(result);
                    _history.Add(result);
                    _historyBindingSource.ResetBindings(false);
                    if (_history.Count > 0)
                        dgvHistory.FirstDisplayedScrollingRowIndex = _history.Count - 1;
                    SetStatus("Расчет выполнен успешно", Color.Green);
                }
                else
                {
                    SetStatus("Готов", Color.Orange);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Готов", Color.Orange);
            }
            finally
            {
                btnCalculate.Enabled = true;
                btnSwap.Enabled = true;
            }
        }

        private void BtnSwap_Click(object sender, EventArgs e)
        {
            string temp = txtFrom.Text;
            txtFrom.Text = txtTo.Text;
            txtTo.Text = temp;
            rtbResult.Clear();
            SetStatus("Готов", Color.Orange);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtFrom.Clear();
            txtTo.Clear();
            if (cmbTransport.Items.Count > 0)
                cmbTransport.SelectedIndex = 0;
            rtbResult.Clear();
            SetStatus("Готов", Color.Orange);
        }

        private void DgvHistory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < _history.Count)
            {
                var route = _history[e.RowIndex];
                txtFrom.Text = route.FromAddress;
                txtTo.Text = route.ToAddress;

                foreach (TransportRate rate in cmbTransport.Items)
                {
                    if (rate.Name == route.TransportType)
                    {
                        cmbTransport.SelectedItem = rate;
                        break;
                    }
                }

                DisplayResult(route);
                SetStatus("Готов", Color.Orange);
            }
        }

        private void DisplayResult(RouteInfo route)
        {
            rtbResult.Clear();

            rtbResult.SelectionFont = new Font("Arial", 12, FontStyle.Bold);
            rtbResult.SelectedText = "РЕЗУЛЬТАТ РАСЧЕТА\r\n\r\n";

            rtbResult.SelectionFont = new Font("Arial", 10, FontStyle.Regular);
            rtbResult.SelectedText = $"Откуда: {route.FromAddress}\r\n";
            rtbResult.SelectedText = $"Координаты: {route.FromCoords}\r\n";
            rtbResult.SelectedText = $"Куда: {route.ToAddress}\r\n";
            rtbResult.SelectedText = $"Координаты: {route.ToCoords}\r\n";
            rtbResult.SelectedText = $"Транспорт: {route.TransportType}\r\n\r\n";

            rtbResult.SelectionFont = new Font("Arial", 10, FontStyle.Bold);
            rtbResult.SelectedText = $"Расстояние: {route.DistanceKm:F1} км\r\n";
            rtbResult.SelectedText = $"Время в пути: {FormatDuration(route.DurationHours)}\r\n";
            rtbResult.SelectedText = $"Стоимость доставки: {route.Cost:F2} руб.\r\n\r\n";

            rtbResult.SelectionFont = new Font("Arial", 8, FontStyle.Italic);
            rtbResult.SelectedText = $"Рассчитано: {route.CalculationTime:dd.MM.yyyy HH:mm:ss}";
        }

        private string FormatDuration(double hours)
        {
            int totalMinutes = (int)(hours * 60);
            int h = totalMinutes / 60;
            int m = totalMinutes % 60;

            if (h > 0 && m > 0)
                return $"{h} ч {m} мин";
            else if (h > 0)
                return $"{h} ч";
            else
                return $"{m} мин";
        }

        private void UpdateStatus(string message)
        {
            rtbResult.SelectionColor = Color.Gray;
            rtbResult.SelectionFont = new Font("Arial", 8, FontStyle.Italic);
            rtbResult.SelectedText = $"> {message}\r\n";
        }
    }
}