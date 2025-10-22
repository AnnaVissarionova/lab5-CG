using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab5_CG
{
    public partial class Task2Form : Form
    {
        private List<float[]> heightMaps;
        private int currentStep;
        private Random random;
        private float roughness;
        private float initialHeight;
        private int segments;
        private bool makeBeautiful = false;

        public Task2Form()
        {
            InitializeComponent();
            InitializeParameters();
        }

        private void InitializeParameters()
        {
            heightMaps = new List<float[]>();
            random = new Random();
            roughness = 1.0f;
            initialHeight = 150f;
            segments = 1;
            currentStep = -1;
        }

        private void GenerateTerrain()
        {
            if (currentStep < 0)
            {
                // Начальный шаг - ДВЕ точки
                float[] initialMap = new float[2];
                initialMap[0] = initialHeight + (float)(random.NextDouble() * 80 - 40);
                initialMap[1] = initialHeight + (float)(random.NextDouble() * 80 - 40);
                heightMaps.Add(initialMap);
                currentStep = 0;
                segments = 1; // На первом шаге 1 сегмент (одна линия)
            }
            else
            {
                float[] currentMap = heightMaps[currentStep];
                float[] newMap = new float[currentMap.Length * 2 - 1];

                for (int i = 0; i < currentMap.Length; i++)
                {
                    newMap[i * 2] = currentMap[i];
                }

                float range = roughness * 100 / ((currentStep + 1) * 0.8f);

                for (int i = 0; i < currentMap.Length - 1; i++)
                {
                    int midIndex = i * 2 + 1;
                    float midpoint = (currentMap[i] + currentMap[i + 1]) / 2;
                    float displacement = (float)(random.NextDouble() * 2 - 1) * range;
                    newMap[midIndex] = Math.Max(50, midpoint + displacement);
                }

                heightMaps.Add(newMap);
                currentStep++;
                segments = segments * 2;
            }

            UpdateLabels();
            pictureBox.Invalidate();
        }

        private void ResetTerrain()
        {
            heightMaps.Clear();
            currentStep = -1;
            segments = 1;
            UpdateLabels();
            pictureBox.Invalidate();
        }

        private void UpdateLabels()
        {
            lblStep.Text = $"Шаг: {(currentStep >= 0 ? currentStep + 1 : 0)}";
            lblPoints.Text = $"Точек: {(heightMaps.Count > 0 ? heightMaps[currentStep].Length : 0)}";
            lblSegments.Text = $"Сегментов: {segments}";
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int width = pictureBox.Width;
            int height = pictureBox.Height;

            DrawSkyBackground(g, width, height);

            if (makeBeautiful)
            {
                DrawSun(g, width);
                DrawClouds(g, width);
            }

            if (cbShowGrid.Checked)
            {
                DrawGrid(g, width, height);
            }

            if (heightMaps.Count > 0 && currentStep >= 0)
            {
                float[] currentMap = heightMaps[currentStep];
                DrawMountains(g, currentMap, width, height);

                if (cbShowLines.Checked)
                {
                    DrawTerrainLine(g, currentMap, width, height, currentStep == 0);
                }
            }
        }

        private void DrawSkyBackground(Graphics g, int width, int height)
        {
            using (LinearGradientBrush skyBrush = new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, height),
                Color.FromArgb(135, 137, 207),  // Темно-голубой сверху
                Color.FromArgb(165, 215, 227))) // Светло-голубой снизу
            {
                g.FillRectangle(skyBrush, 0, 0, width, height);
            }
        }

        private void DrawMountains(Graphics g, float[] heightMap, int width, int panelHeight)
        {
            if (heightMap.Length < 2) return;

            // Создаем полигон для гор
            PointF[] points = new PointF[heightMap.Length + 2];

            // Левая нижняя точка
            points[0] = new PointF(20, panelHeight - 20);

            // Точки гор
            for (int i = 0; i < heightMap.Length; i++)
            {
                float x = (float)i / (heightMap.Length - 1) * (width - 40) + 20;
                float y = panelHeight - 20 - heightMap[i];
                points[i + 1] = new PointF(x, y);
            }

            // Правая нижняя точка
            points[heightMap.Length + 1] = new PointF(width - 20, panelHeight - 20);

            float maxHeight = heightMap.Max();
            float minHeight = heightMap.Min();
            float gradientTop = panelHeight - 20 - maxHeight;
            float gradientBottom = panelHeight - 20;

            using (LinearGradientBrush mountainBrush = new LinearGradientBrush(
                new Point(0, (int)gradientBottom),  
                new Point(0, (int)gradientTop),     
                Color.FromArgb(4, 57, 21),                 // Темно-зеленый
                Color.FromArgb(59, 118, 100)))                // Светло-зеленый 
            {
                g.FillPolygon(mountainBrush, points);
            }
        }

        private void DrawTerrainLine(Graphics g, float[] heightMap, int width, int panelHeight, bool isFirstStep)
        {
            if (isFirstStep)
            {
                // На первом шаге - простая линия
                using (Pen linePen = new Pen(Color.DarkBlue, 2f))
                {
                    float x1 = 20;
                    float y1 = panelHeight - 20 - heightMap[0];
                    float x2 = width - 20;
                    float y2 = panelHeight - 20 - heightMap[heightMap.Length - 1];

                    g.DrawLine(linePen, x1, y1, x2, y2);
                }
            }
            else
            {
                // На последующих шагах - ломаная
                using (Pen linePen = new Pen(Color.DarkBlue, 2f))
                {
                    for (int i = 0; i < heightMap.Length - 1; i++)
                    {
                        float x1 = (float)i / (heightMap.Length - 1) * (width - 40) + 20;
                        float y1 = panelHeight - 20 - heightMap[i];
                        float x2 = (float)(i + 1) / (heightMap.Length - 1) * (width - 40) + 20;
                        float y2 = panelHeight - 20 - heightMap[i + 1];

                        g.DrawLine(linePen, x1, y1, x2, y2);
                    }
                }
            }
        }

        private void DrawGrid(Graphics g, int width, int height)
        {
            using (Pen gridPen = new Pen(Color.FromArgb(100, 255, 255, 255), 1)) 
            {
                // Вертикальные линии
                for (int i = 0; i <= 10; i++)
                {
                    float x = 20 + i * (width - 40) / 10;
                    g.DrawLine(gridPen, x, 20, x, height - 20);
                }

                // Горизонтальные линии
                for (int i = 0; i <= 10; i++)
                {
                    float y = 20 + i * (height - 40) / 10;
                    g.DrawLine(gridPen, 20, y, width - 20, y);
                }
            }

        }

        private void DrawSun(Graphics g, int width)
        {
            float scale = width / 800f;

            int sunSize = (int)(100 * scale);
            int sunX = (int)(30 * scale);
            int sunY = (int)(30 * scale);

            using (LinearGradientBrush sunBrush = new LinearGradientBrush(
                new Point(sunX, sunY),
                new Point(sunX, sunY + sunSize+5),
                Color.FromArgb(255, 36, 139),
                Color.FromArgb(255, 170, 5)))   
            {
                g.FillEllipse(sunBrush, sunX, sunY, sunSize, sunSize);
            }
        }

        private void DrawClouds(Graphics g, int width)
        {
            using (Brush cloudBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 255)))
            {
                float scale = width / 800f;

                // Облако 1
                int cloud1X = (int)(250 * scale);
                g.FillEllipse(cloudBrush, cloud1X, (int)(50 * scale), (int)(100 * scale), (int)(60 * scale));
                g.FillEllipse(cloudBrush, cloud1X + (int)(25 * scale), (int)(35 * scale), (int)(90 * scale), (int)(50 * scale));
                g.FillEllipse(cloudBrush, cloud1X - (int)(25 * scale), (int)(45 * scale), (int)(80 * scale), (int)(50 * scale));

                // Облако 2
                int cloud2X = (int)(450 * scale);
                g.FillEllipse(cloudBrush, cloud2X, (int)(80 * scale), (int)(80 * scale), (int)(60 * scale));
                g.FillEllipse(cloudBrush, cloud2X + (int)(20 * scale), (int)(65 * scale), (int)(90 * scale), (int)(60 * scale));
                g.FillEllipse(cloudBrush, cloud2X - (int)(20 * scale), (int)(75 * scale), (int)(80 * scale), (int)(40 * scale));
                g.FillEllipse(cloudBrush, cloud2X + (int)(35 * scale), (int)(85 * scale), (int)(90 * scale), (int)(40 * scale));

                // Облако 3
                int cloud3X = (int)(630 * scale);
                g.FillEllipse(cloudBrush, cloud3X, (int)(30 * scale), (int)(100 * scale), (int)(60 * scale));
                g.FillEllipse(cloudBrush, cloud3X + (int)(25 * scale), (int)(15 * scale), (int)(85 * scale), (int)(45 * scale));
                g.FillEllipse(cloudBrush, cloud3X - (int)(25 * scale), (int)(25 * scale), (int)(75 * scale), (int)(35 * scale));
            }
        }

        private void UpdateRoughness(object sender, EventArgs e)
        {
            roughness = (float)trackBarRoughness.Value / 10f;
            lblRoughnessValue.Text = roughness.ToString("F1");
        }

        private void UpdateInitialHeight(object sender, EventArgs e)
        {
            initialHeight = (float)trackBarInitialHeight.Value;
            lblInitialHeightValue.Text = initialHeight.ToString();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateTerrain();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetTerrain();
        }

        private void cbShowGrid_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox.Invalidate();
        }

        private void cbShowLines_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox.Invalidate();
        }
        private void cbMakeBeautiful_CheckedChanged(object sender, EventArgs e)
        {
            makeBeautiful = cbMakeBeautiful.Checked;

            if (makeBeautiful)
            {
                cbShowGrid.Checked = false;
                cbShowLines.Checked = false;
            }
            else
            {
                cbShowGrid.Checked = true;
                cbShowLines.Checked = true;
            }

            pictureBox.Invalidate();
        }
    }
}