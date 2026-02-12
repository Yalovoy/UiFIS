using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NetworkTerminal
{
    public partial class Form1 : Form
    {
        private Point switchPos = new Point(350, 200);
        private Dictionary<string, Point> pcPositions = new Dictionary<string, Point>
        {
            { "ПК1", new Point(80, 80) },
            { "ПК2", new Point(620, 80) },
            { "ПК3", new Point(80, 320) },
            { "ПК4", new Point(620, 320) }
        };

        private List<AnimatedPacket> packets = new List<AnimatedPacket>();
        private int packetCounter = 0;
        private int totalPacketsSent = 0;
        private int totalPacketsDelivered = 0;
        private Random rand = new Random();
        private Label lblStats;
        

        public Form1()
        {
            InitializeComponent();
            FindControls();
            SetupForm();
            SetupTimers();
        }

        private void FindControls()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is PictureBox pb)
                {
                    if (pb.Name == "pc1") pc1 = pb;
                    if (pb.Name == "pc2") pc2 = pb;
                    if (pb.Name == "pc3") pc3 = pb;
                    if (pb.Name == "pc4") pc4 = pb;
                }

                if (ctrl is Label && ctrl.Text.Contains("Отправлено:"))
                    lblStats = ctrl as Label;
            }

            if (imageListPackets.Images.Count == 0)
            {
                MessageBox.Show("Добавьте изображения в imageListPackets!");
            }
        }

        private void SetupForm()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
        }

        private void SetupTimers()
        {
            animationTimer.Interval = 30;
            animationTimer.Tick += animationTimer_Tick;

            packetGeneratorTimer.Interval = GetPacketInterval();
            packetGeneratorTimer.Tick += packetGeneratorTimer_Tick;
        }

        private int GetPacketInterval()
        {
            int packetsPerSecond = (int)numericUpDownSpeed.Value;
            if (packetsPerSecond < 1) packetsPerSecond = 1;
            if (packetsPerSecond > 100) packetsPerSecond = 100;
            return 1000 / packetsPerSecond;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            packetGeneratorTimer.Start();
            animationTimer.Start();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            Log("Запуск передачи пакетов...");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            packetGeneratorTimer.Stop();
            animationTimer.Stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;

            Log($"\nИТОГОВАЯ СТАТИСТИКА:");
            Log($"   Всего отправлено: {totalPacketsSent}");
            Log($"   Всего доставлено: {totalPacketsDelivered}");
            Log($"   Активных пакетов: {packets.Count}\n");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            consoleLog.Clear();
        }

        private void numericUpDownSpeed_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownSpeed.Value < 1)
                numericUpDownSpeed.Value = 1;
            if (numericUpDownSpeed.Value > 100)
                numericUpDownSpeed.Value = 100;

            packetGeneratorTimer.Interval = GetPacketInterval();
            Log($"[{DateTime.Now:HH:mm:ss.fff}] Скорость: {numericUpDownSpeed.Value} пакетов/сек");
        }


        private void packetGeneratorTimer_Tick(object sender, EventArgs e)
        {
            GeneratePacket();
        }

        private void GeneratePacket()
        {
            if (packets.Count > 20) return;

            string[] pcNames = { "ПК1", "ПК2", "ПК3", "ПК4" };
            int srcIndex = rand.Next(pcNames.Length);
            int dstIndex;
            do { dstIndex = rand.Next(pcNames.Length); } while (dstIndex == srcIndex);

            string srcName = pcNames[srcIndex];
            string dstName = pcNames[dstIndex];

            PictureBox pb = new PictureBox
            {
                Size = new Size(32, 20),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
                Tag = ++packetCounter
            };

            if (imageListPackets.Images.Count > 0)
            {
                int imageIndex = rand.Next(imageListPackets.Images.Count);
                pb.Image = imageListPackets.Images[imageIndex];
            }
            else
            {
                pb.BackColor = Color.DodgerBlue;
            }

            animationPanel.Controls.Add(pb);

            AnimatedPacket packet = new AnimatedPacket
            {
                Id = packetCounter,
                Pb = pb,
                From = pcPositions[srcName],
                To = switchPos,
                TargetPC = pcPositions[dstName],
                SrcName = srcName,
                DstName = dstName,
                Progress = 0f,
                Size = rand.Next(64, 1518),
                CreationTime = DateTime.Now,
                AtSwitch = false
            };

            packets.Add(packet);
            totalPacketsSent++;

            Log($"[{DateTime.Now:HH:mm:ss.fff}] Пакет #{packet.Id}: {srcName} -> SWITCH, {packet.Size} байт");
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            MovePackets();
            UpdateStats();
        }

        private void MovePackets()
        {
            for (int i = packets.Count - 1; i >= 0; i--)
            {
                var pkt = packets[i];

                pkt.Progress += 0.02f;

                int x = (int)(pkt.From.X + (pkt.To.X - pkt.From.X) * pkt.Progress);
                int y = (int)(pkt.From.Y + (pkt.To.Y - pkt.From.Y) * pkt.Progress);

                pkt.Pb.Location = new Point(x - 16, y - 10); 

                if (pkt.Progress >= 1.0f)
                {
                    if (!pkt.AtSwitch && pkt.To == switchPos)
                    {
                        Log($"[{DateTime.Now:HH:mm:ss.fff}] Пакет #{pkt.Id} на SWITCH");
                        pkt.AtSwitch = true;
                        pkt.From = switchPos;
                        pkt.To = pkt.TargetPC;
                        pkt.Progress = 0f;


                        if (imageListPackets.Images.Count > 2)
                            pkt.Pb.Image = imageListPackets.Images[2]; 
                    }
                    else
                    {
                        int deliveryTime = (int)(DateTime.Now - pkt.CreationTime).TotalMilliseconds;
                        Log($"[{DateTime.Now:HH:mm:ss.fff}] Пакет #{pkt.Id} доставлен на {pkt.DstName} (задержка: {deliveryTime} мс)");

                        animationPanel.Controls.Remove(pkt.Pb);
                        pkt.Pb.Dispose();

                        packets.RemoveAt(i);
                        totalPacketsDelivered++;
                    }
                }
            }
        }

        private void UpdateStats()
        {
            if (lblStats != null)
            {
                lblStats.Text = $"Отправлено: {totalPacketsSent}\n" +
                                $"Доставлено: {totalPacketsDelivered}\n" +
                                $"Активно: {packets.Count}";
            }
        }

        private void Log(string message)
        {
            if (consoleLog.InvokeRequired)
            {
                consoleLog.Invoke(new Action(() =>
                {
                    consoleLog.AppendText(message + "\n");
                    consoleLog.ScrollToCaret();
                }));
            }
            else
            {
                consoleLog.AppendText(message + "\n");
                consoleLog.ScrollToCaret();
            }
        }
    }

    public class AnimatedPacket
    {
        public int Id { get; set; }
        public PictureBox Pb { get; set; }
        public Point From { get; set; }
        public Point To { get; set; }
        public Point TargetPC { get; set; }
        public string SrcName { get; set; }
        public string DstName { get; set; }
        public float Progress { get; set; }
        public int Size { get; set; }
        public DateTime CreationTime { get; set; }
        public bool AtSwitch { get; set; }
    }
}