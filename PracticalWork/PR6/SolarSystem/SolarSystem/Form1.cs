using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SolarSystem
{
    public partial class Form1 : Form
    {
        public class Planet
        {
            public string Name { get; set; }
            public float OrbitRadius { get; set; }
            public float Speed { get; set; }
            public float Size { get; set; }
            public Color Color { get; set; }
            public float Angle { get; set; }

            public Planet(string name, float orbitRadius, float speed,
                         float size, Color color, float startAngle)
            {
                Name = name;
                OrbitRadius = orbitRadius;
                Speed = speed;
                Size = size;
                Color = color;
                Angle = startAngle;
            }
        }

        private List<Planet> planets;
        private float centerX, centerY;
        private float zoom = 1.0f;
        private float speedMultiplier = 1.0f;
        private bool isPaused = false;
        private Random random = new Random();
        private List<PointF> stars = new List<PointF>();

        public Form1()
        {
            InitializeComponent();

            this.BackColor = Color.FromArgb(5, 5, 20);

            InitializeSolarSystem();
            SetupForm();
            GenerateStars(200);

            if (animationTimer != null)
            {
                animationTimer.Interval = 25;
                animationTimer.Enabled = true;
                animationTimer.Start();
            }
        }

        private void SetupForm()
        {
            this.DoubleBuffered = true;
            centerX = this.ClientSize.Width / 2;
            centerY = this.ClientSize.Height / 2;

            if (lblSpeed != null)
                lblSpeed.Text = "1,0x";

            if (lblZoom != null)
                lblZoom.Text = "100%";

            if (controlPanel != null)
                controlPanel.BackColor = Color.FromArgb(40, 40, 60);

        }

        private void InitializeSolarSystem()
        {
            planets = new List<Planet>
            {
                new Planet("Меркурий", 1.2f, 0.08f, 5, Color.Gray, 0),
                new Planet("Венера", 1.7f, 0.05f, 7, Color.Orange, 30),
                new Planet("Земля", 2.2f, 0.04f, 8, Color.DodgerBlue, 60),
                new Planet("Марс", 2.7f, 0.03f, 7, Color.Red, 90),
                new Planet("Юпитер", 3.4f, 0.015f, 16, Color.SandyBrown, 120),
                new Planet("Сатурн", 4.1f, 0.01f, 14, Color.Gold, 150),
                new Planet("Уран", 4.8f, 0.007f, 12, Color.LightBlue, 180),
                new Planet("Нептун", 5.5f, 0.005f, 12, Color.DarkBlue, 210)
            };
        }

        private void GenerateStars(int count)
        {
            stars.Clear();
            for (int i = 0; i < count; i++)
            {
                stars.Add(new PointF(
                    random.Next(0, this.ClientSize.Width),
                    random.Next(0, this.ClientSize.Height)
                ));
            }
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            if (planets == null) return;

            if (!isPaused)
            {
                foreach (var planet in planets)
                {
                    planet.Angle += planet.Speed * speedMultiplier;
                    if (planet.Angle > 360) planet.Angle -= 360;
                }
                this.Invalidate();
            }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (planets == null) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            DrawStars(g);

            DrawOrbits(g);

            DrawSun(g);

            DrawPlanets(g);

            DrawTitle(g);
        }

        private void DrawStars(Graphics g)
        {
            foreach (var star in stars)
            {
                int brightness = random.Next(100, 255);
                int size = random.Next(1, 3);
                g.FillEllipse(new SolidBrush(Color.FromArgb(brightness, 255, 255, 255)),
                    star.X, star.Y, size, size);
            }
        }

        private void DrawOrbits(Graphics g)
        {
            Pen orbitPen = new Pen(Color.FromArgb(80, 100, 100, 120), 1);
            orbitPen.DashStyle = DashStyle.Dash;

            foreach (var planet in planets)
            {
                float radius = 40 * planet.OrbitRadius * zoom;
                g.DrawEllipse(orbitPen,
                    centerX - radius,
                    centerY - radius,
                    radius * 2,
                    radius * 2);
            }
            orbitPen.Dispose();
        }

        private void DrawSun(Graphics g)
        {
            int sunSize = (int)(50 * zoom);
            if (sunSize < 30) sunSize = 30;
            if (sunSize > 80) sunSize = 80;

            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(centerX - sunSize / 2, centerY - sunSize / 2, sunSize, sunSize);

            PathGradientBrush brush = new PathGradientBrush(path);
            brush.CenterColor = Color.Yellow;
            brush.SurroundColors = new Color[] { Color.Orange };

            g.FillEllipse(brush, centerX - sunSize / 2, centerY - sunSize / 2, sunSize, sunSize);

            for (int i = 1; i <= 3; i++)
            {
                int alpha = 50 - i * 10;
                g.DrawEllipse(new Pen(Color.FromArgb(alpha, 255, 255, 0), i),
                    centerX - sunSize / 2 - i * 2,
                    centerY - sunSize / 2 - i * 2,
                    sunSize + i * 4,
                    sunSize + i * 4);
            }

            Font sunFont = new Font("Segoe UI", 12, FontStyle.Bold);
            SizeF textSize = g.MeasureString("СОЛНЦЕ", sunFont);
            g.DrawString("СОЛНЦЕ", sunFont, Brushes.White,
                centerX - textSize.Width / 2, centerY - sunSize / 2 - 30);

            sunFont.Dispose();
            path.Dispose();
            brush.Dispose();
        }

        private void DrawPlanets(Graphics g)
        {
            foreach (var planet in planets)
            {
                float radius = 40 * planet.OrbitRadius * zoom;
                float x = centerX + (float)Math.Cos(planet.Angle * Math.PI / 180) * radius;
                float y = centerY + (float)Math.Sin(planet.Angle * Math.PI / 180) * radius;

                float planetSize = planet.Size * zoom;

                if (planetSize < 4) planetSize = 4;
                if (planetSize > 30) planetSize = 30;

                if (planet.Name == "Сатурн")
                {
                    DrawSaturnRings(g, x, y, planetSize);
                }

                g.FillEllipse(new SolidBrush(planet.Color),
                    x - planetSize / 2,
                    y - planetSize / 2,
                    planetSize,
                    planetSize);

                g.FillEllipse(new SolidBrush(Color.FromArgb(100, 255, 255, 255)),
                    x - planetSize / 4,
                    y - planetSize / 4,
                    planetSize / 5,
                    planetSize / 5);

                if (zoom > 0.5f)
                {
                    Font planetFont = new Font("Segoe UI", 8, FontStyle.Bold);
                    SizeF textSize = g.MeasureString(planet.Name, planetFont);
                    g.DrawString(planet.Name, planetFont, Brushes.White,
                        x - textSize.Width / 2, y - planetSize / 2 - 15);
                    planetFont.Dispose();
                }
            }
        }

        private void DrawSaturnRings(Graphics g, float x, float y, float planetSize)
        {
            Pen ringPen = new Pen(Color.FromArgb(150, 200, 180, 100), 2);
            g.DrawEllipse(ringPen,
                x - planetSize * 0.8f,
                y - planetSize * 0.4f,
                planetSize * 1.6f,
                planetSize * 0.5f);
            ringPen.Dispose();
        }

        private void DrawTitle(Graphics g)
        {
            Font titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
            SizeF textSize = g.MeasureString("Солнечная система", titleFont);
            g.DrawString("Солнечная система", titleFont, Brushes.White,
                this.ClientSize.Width / 2 - textSize.Width / 2, 70);
            titleFont.Dispose();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            centerX = this.ClientSize.Width / 2;
            centerY = this.ClientSize.Height / 2;

            GenerateStars(200);
            this.Invalidate();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            isPaused = !isPaused;
            btnPause.Text = isPaused ? "Старт" : "Пауза";
            btnPause.BackColor = isPaused ? Color.FromArgb(90, 70, 70) : Color.FromArgb(70, 70, 90);
        }

        private void btnFaster_Click(object sender, EventArgs e)
        {
            speedMultiplier *= 1.1f;
            if (speedMultiplier > 5.0f) speedMultiplier = 5.0f;
            UpdateSpeedLabel();
        }

        private void btnSlower_Click(object sender, EventArgs e)
        {
            speedMultiplier *= 0.9f;
            if (speedMultiplier < 0.2f) speedMultiplier = 0.2f;
            UpdateSpeedLabel();
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            zoom *= 1.1f;
            if (zoom > 3.0f) zoom = 3.0f;

            if (lblZoom != null)
                lblZoom.Text = $"{Math.Round(zoom * 100)}%";

            this.Invalidate();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            zoom *= 0.9f;
            if (zoom < 0.3f) zoom = 0.3f;

            if (lblZoom != null)
                lblZoom.Text = $"{Math.Round(zoom * 100)}%";

            this.Invalidate();
        }

        private void UpdateSpeedLabel()
        {
            if (lblSpeed == null) return;
            lblSpeed.Text = $"{speedMultiplier:0.0}x";
        }

    }
}