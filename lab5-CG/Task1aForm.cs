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
            this.Width = 900;
            this.Height = 900;

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
            canvas.Size = new Size(860, 800);
            canvas.BorderStyle = BorderStyle.FixedSingle;
            canvas.Paint += Canvas_Paint;

            this.Controls.Add(btnClear);
            this.Controls.Add(btnLoadFile);
            this.Controls.Add(numGenerations);
            this.Controls.Add(canvas);
        }

        private void BtnLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadLSystem(ofd.FileName);

                // Генерация последовательности (можно изменить количество итераций)
                string result = GenerateSequence(axiom, (int)numGenerations.Value);
                points = GeneratePoints(result);

                canvas.Invalidate(); // Перерисовать PictureBox
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
            var lines = File.ReadAllLines(file);
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
                    rules[ruleParts[0][0]] = ruleParts[1];
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
            PointF currentPos = new PointF(canvas.Width / 2, canvas.Height / 2);
            double currentAngle = initialDirection;

            pts.Add(currentPos);

            float step = 5f; // длина линии

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
                        var state = stack.Pop();
                        currentPos = state.point;
                        currentAngle = state.dir;
                        pts.Add(currentPos);
                        break;
                }
            }

            pts = ScalePointsToCanvas(pts);

            return pts;
        }

        private List<PointF> ScalePointsToCanvas(List<PointF> pts)
        {
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
            float scale = Math.Min(canvas.Width / width, canvas.Height / height) * 0.9f;

            List<PointF> scaled = new List<PointF>();
            foreach (var p in pts)
            {
                float x = (p.X - minX) * scale + 10;
                float y = (p.Y - minY) * scale + 10;
                scaled.Add(new PointF(x, y));
            }
            return scaled;
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            if (points.Count < 2) return;
            using (Pen pen = new Pen(Color.Blue, 1))
            {
                for (int i = 1; i < points.Count; i++)
                {
                    e.Graphics.DrawLine(pen, points[i - 1], points[i]);
                }
            }
        }

        private void Task1aForm_Load(object sender, EventArgs e)
        {

        }
    }
}
