using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab5_CG
{
    public partial class Task1aForm : Form
    {
        private Button btnLoadFile;
        private Button btnClear;
        private PictureBox canvas;
        private string axiom;
        private double angle;
        private double initialDirection;
        private Dictionary<char, string> rules = new Dictionary<char, string>();
        private List<PointF> points = new List<PointF>();
        private NumericUpDown numGenerations;

        public Task1aForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "L-System Fractal";
            this.Width = 800;
            this.Height = 800;
            this.SizeChanged += Task1aForm_SizeChanged;

            btnLoadFile = new Button();
            btnLoadFile.Text = "Load L-System File";
            btnLoadFile.Location = new Point(10, 10);
            btnLoadFile.Click += BtnLoadFile_Click;

            btnClear = new Button();
            btnClear.Text = "Clear";
            btnClear.Location = new Point(150, 10);
            btnClear.Click += BtnClear_Click;

            numGenerations = new NumericUpDown();
            numGenerations.Minimum = 0;
            numGenerations.Maximum = 10; // можно увеличить
            numGenerations.Value = 1; // значение по умолчанию
            numGenerations.Location = new Point(300, 10);
            numGenerations.Width = 50;
            numGenerations.ValueChanged += NumGenerations_ValueChanged;

            canvas = new PictureBox();
            canvas.Location = new Point(10, 50);
            canvas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right; // Растягиваем
            canvas.Size = new Size(this.Width - 40, this.Height - 110); // Динамический размер
            canvas.BorderStyle = BorderStyle.FixedSingle;
            canvas.Paint += Canvas_Paint;

            this.Controls.Add(btnClear);
            this.Controls.Add(btnLoadFile);
            this.Controls.Add(numGenerations);
            this.Controls.Add(canvas);
        }

        private void Task1aForm_SizeChanged(object sender, EventArgs e)
        {
            if (canvas != null)
            {
                canvas.Invalidate();
            }
        }

        private void BtnLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadLSystem(ofd.FileName);
                string result = GenerateSequence(axiom, (int)numGenerations.Value);
                points = GeneratePoints(result); 
                canvas.Invalidate();
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            numGenerations.Value = 1;
            points.Clear();
            canvas.Invalidate(); // перерисовать пустой холст
        }

        private void LoadLSystem(string file)
        {
            var lines = System.IO.File.ReadAllLines(file);
            if (lines.Length < 1) return;

            // Первая строка: <атом> <угол> <начальное направление>
            var parts = lines[0].Split(' ');
            axiom = parts[0];
            angle = double.Parse(parts[1]);
            initialDirection = double.Parse(parts[2]);

            rules.Clear();
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                var ruleParts = lines[i].Split("=");
                if (ruleParts.Length == 2)
                {
                    char symbol = ruleParts[0][0];
                    rules[symbol] = ruleParts[1];
                }
            }
        }

        private void NumGenerations_ValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(axiom))
            {
                string result = GenerateSequence(axiom, (int)numGenerations.Value);
                points = GeneratePoints(result);
                canvas.Invalidate();
            }
        }

        private string GenerateSequence(string current, int iterations)
        {
            Random rand = new Random();
            for (int i = 0; i < iterations; i++)
            {
                string next = "";
                foreach (char c in current)
                {
                    if (rules.ContainsKey(c))
                    {
                        // Можно добавить случайность (например 50%)
                        if (rand.NextDouble() < 1.0) // 1.0 = всегда применять
                            next += rules[c];
                        else
                            next += c;
                    }
                    else
                    {
                        next += c;
                    }
                }
                current = next;
            }
            return current;
        }

        private List<PointF> GeneratePoints(string sequence)
        {
            List<PointF> pts = new List<PointF>();
            Stack<(PointF point, double dir)> stack = new Stack<(PointF, double)>();
            PointF currentPos = new PointF(0, 0);
            double currentAngle = initialDirection;

            pts.Add(currentPos);

            float step = 10f;

            foreach (char c in sequence)
            {
                switch (c)
                {
                    case 'F':
                        currentPos = new PointF(
                            currentPos.X + (float)(step * Math.Cos(currentAngle * Math.PI / 180)),
                            currentPos.Y + (float)(step * Math.Sin(currentAngle * Math.PI / 180))
                        );
                        pts.Add(currentPos);
                        break;
                    case '+':
                        currentAngle += angle;
                        break;
                    case '-':
                        currentAngle -= angle;
                        break;
                    case '[':
                        stack.Push((currentPos, currentAngle));
                        break;
                    case ']':
                        if (stack.Count > 0)
                        {
                            var state = stack.Pop();
                            currentPos = state.point;
                            currentAngle = state.dir;
                        }
                        break;
                }
            }

            return pts;
        }

        private List<PointF> ScalePointsToCanvas(List<PointF> pts, int canvasWidth, int canvasHeight)
        {
            if (pts.Count == 0) return pts;

            float minX = float.MaxValue, maxX = float.MinValue, minY = float.MaxValue, maxY = float.MinValue;
            foreach (var p in pts)
            {
                if (p.X < minX) minX = p.X;
                if (p.X > maxX) maxX = p.X;
                if (p.Y < minY) minY = p.Y;
                if (p.Y > maxY) maxY = p.Y;
            }

            float width = maxX - minX;
            float height = maxY - minY;

            if (width == 0) width = 1;
            if (height == 0) height = 1;

            // отступы
            float paddingPercent = 0.2f;
            float paddingHorizontal = canvasWidth * paddingPercent;
            float paddingVertical = canvasHeight * paddingPercent;

            float availableWidth = canvasWidth - 2 * paddingHorizontal;
            float availableHeight = canvasHeight - 2 * paddingVertical;

            // Вычисляем масштаб с сохранением пропорций
            float scaleX = availableWidth / width;
            float scaleY = availableHeight / height;
            float scale = Math.Min(scaleX, scaleY);

            // Центрируем
            float offsetX = paddingHorizontal + (availableWidth - width * scale) / 2;
            float offsetY = paddingVertical + (availableHeight - height * scale) / 2;

            List<PointF> scaled = new List<PointF>();
            foreach (var p in pts)
            {
                float x = (p.X - minX) * scale + offsetX;
                float y = (p.Y - minY) * scale + offsetY;
                scaled.Add(new PointF(x, y));
            }
            return scaled;
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            if (points.Count < 2) return;

            List<PointF> scaledPoints = ScalePointsToCanvas(points, canvas.Width, canvas.Height);

            using (Pen pen = new Pen(Color.Blue, 1))
            {
                for (int i = 1; i < scaledPoints.Count; i++)
                {
                    e.Graphics.DrawLine(pen, scaledPoints[i - 1], scaledPoints[i]);
                }
            }
        }

        private void Task1aForm_Load(object sender, EventArgs e)
        {

        }
    }
}
