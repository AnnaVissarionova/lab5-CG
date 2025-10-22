using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab5_CG
{
    public partial class Task1bForm : Form
    {
        private Button btnLoadFile;
        private Button btnClear;
        private PictureBox canvas;
        private TrackBar angleDiffSlider;
        private string axiom;
        private double angle;
        private double initialDirection;
        private Dictionary<char, string> rules = new Dictionary<char, string>();
        private List<LineSegment> segments = new List<LineSegment>();
        private NumericUpDown numGenerations;

        public Task1bForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "L-System Fractal Tree with Gradient";
            this.Width = 800;
            this.Height = 800;
            this.SizeChanged += Task1bForm_SizeChanged;

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
            numGenerations.Maximum = 8;
            numGenerations.Value = 4;
            numGenerations.Location = new Point(300, 10);
            numGenerations.Width = 50;
            numGenerations.ValueChanged += NumGenerations_ValueChanged;

            canvas = new PictureBox();
            canvas.Location = new Point(10, 70);
            canvas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            canvas.Size = new Size(this.Width - 40, this.Height - 110);
            canvas.BorderStyle = BorderStyle.FixedSingle;
            canvas.Paint += Canvas_Paint;

            angleDiffSlider = new TrackBar();
            angleDiffSlider.Minimum = 0;
            angleDiffSlider.Maximum = 180;
            angleDiffSlider.Value = 30; // стартовое значение
            angleDiffSlider.TickFrequency = 10;
            angleDiffSlider.SmallChange = 1;
            angleDiffSlider.LargeChange = 10;
            angleDiffSlider.Width = 300;
            angleDiffSlider.Location = new Point(400, 10);

            // 🔹 Обновляем фрактал при изменении угла
            angleDiffSlider.Scroll += (s, e) =>
            {
                if (!string.IsNullOrEmpty(axiom))
                {
                    float randomPoss = 0f; // уже не нужно, но параметр передаём
                    float angleDiff = angleDiffSlider.Value;
                    GenerateFractal(randomPoss, angleDiff);
                    canvas.Invalidate();
                }
            };

            this.Controls.Add(angleDiffSlider);
            this.Controls.Add(btnClear);
            this.Controls.Add(btnLoadFile);
            this.Controls.Add(numGenerations);
            this.Controls.Add(canvas);
        }

        private void Task1bForm_SizeChanged(object sender, EventArgs e)
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
                GenerateFractal(0f, (float)angleDiffSlider.Value);
                canvas.Invalidate();
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            numGenerations.Value = 1;
            segments.Clear();
            canvas.Invalidate();
        }

        private void LoadLSystem(string file)
        {
            var lines = System.IO.File.ReadAllLines(file);
            if (lines.Length < 1) return;

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
                GenerateFractal(0f, (float)angleDiffSlider.Value);
                canvas.Invalidate();
            }
        }



        private string GenerateSequence(string current, int iterations)
        {
            return Task1aForm.GenerateSequence(current, iterations, rules);
        }

        private void GenerateFractal(float randomPoss, float angleDiff)
        {
            segments.Clear();
            string sequence = GenerateSequence(axiom, (int)numGenerations.Value);

            float currentWidth = 3.0f; // Начальная толщина
            int currentDepth = 0;

            float step = 30f; // Базовый шаг

            // Стек для состояний 
            Stack<State> stateStack = new Stack<State>();
            // Стек для обработки вложенных выражений
            Stack<ExpressionContext> expressionStack = new Stack<ExpressionContext>();

            // Начальное состояние
            State currentState = new State(
                new PointF(0, 0),
                initialDirection,
                currentWidth,
                currentDepth
            );

            Random random = new Random();
            // Обрабатываем последовательность рекурсивно через стек
            expressionStack.Push(new ExpressionContext(sequence, 0, currentWidth, currentDepth));

            while (expressionStack.Count > 0)
            {
                var context = expressionStack.Pop();
                string currentExpr = context.Expression;
                int index = context.Index;

                while (index < currentExpr.Length)
                {
                    char c = currentExpr[index];
                    index++;

                    switch (c)
                    {
                        case 'F':
                            // Рисуем линию вперед
                            PointF newPos = new PointF(
                                currentState.Position.X + (float)(step * Math.Cos(currentState.Angle * Math.PI / 180)),
                                currentState.Position.Y - (float)(step * Math.Sin(currentState.Angle * Math.PI / 180))
                            );
                            segments.Add(new LineSegment(currentState.Position, newPos, currentWidth, currentDepth));
                            currentState = new State(newPos, currentState.Angle, currentWidth, currentDepth);
                            break;

                        case '+':
                            currentState = new State(
                                currentState.Position,
                                currentState.Angle + angleDiff,
                                currentWidth,
                                currentDepth
                            );
                            break;

                        case '-':
                            currentState = new State(
                                currentState.Position,
                                currentState.Angle - angleDiff,
                                currentWidth,
                                currentDepth
                            );
                            break;

                        case '[':
                            // Сохраняем текущее состояние и текущее выражение
                            stateStack.Push(currentState);
                            // Сохраняем оставшуюся часть выражения для возврата
                            expressionStack.Push(new ExpressionContext(currentExpr, index, currentWidth, currentDepth));
                            // Начинаем новое выражение с пустой строки
                            currentExpr = "";
                            index = 0;
                            currentWidth *= 0.9f;
                            currentDepth++;
                            break;

                        case ']':
                            // Восстанавливаем состояние и предыдущее выражение
                            if (stateStack.Count > 0)
                            {
                                currentState = stateStack.Pop();
                                //segments.Add(new LineSegment(currentState.Position, newPos, currentWidth, currentDepth)); // Точка возврата
                            }
                            if (expressionStack.Count > 0)
                            {
                                var prevContext = expressionStack.Pop();
                                currentExpr = prevContext.Expression;
                                index = prevContext.Index;
                                currentWidth = prevContext.Width;
                                currentDepth = prevContext.Depth;
                            }
                            break;
                    }
                }
            }

        }

        private List<LineSegment> ScaleSegmentsToCanvas(List<LineSegment> segments, int canvasWidth, int canvasHeight)
        {
            if (segments.Count == 0) return segments;

            // Находим границы всех сегментов
            float minX = float.MaxValue, maxX = float.MinValue;
            float minY = float.MaxValue, maxY = float.MinValue;

            foreach (var segment in segments)
            {
                minX = Math.Min(minX, Math.Min(segment.Start.X, segment.End.X));
                maxX = Math.Max(maxX, Math.Max(segment.Start.X, segment.End.X));
                minY = Math.Min(minY, Math.Min(segment.Start.Y, segment.End.Y));
                maxY = Math.Max(maxY, Math.Max(segment.Start.Y, segment.End.Y));
            }

            float width = maxX - minX;
            float height = maxY - minY;

            if (width == 0) width = 1;
            if (height == 0) height = 1;

            // Отступы от краев
            float padding = 150f;
            float availableWidth = canvasWidth - 2 * padding;
            float availableHeight = canvasHeight - 2 * padding;

            // Масштаб с сохранением пропорций
            float scaleX = availableWidth / width;
            float scaleY = availableHeight / height;
            float scale = Math.Min(scaleX, scaleY);

            // Центрируем
            float offsetX = padding + (availableWidth - width * scale) / 2 - minX * scale;
            float offsetY = padding + (availableHeight - height * scale) / 2 - minY * scale;

            // Масштабируем все сегменты
            List<LineSegment> scaled = new List<LineSegment>();
            foreach (var segment in segments)
            {
                PointF scaledStart = new PointF(
                    segment.Start.X * scale + offsetX,
                    segment.Start.Y * scale + offsetY
                );
                PointF scaledEnd = new PointF(
                    segment.End.X * scale + offsetX,
                    segment.End.Y * scale + offsetY
                );
                float scaledWidth = segment.Width * scale;

                scaled.Add(new LineSegment(scaledStart, scaledEnd, scaledWidth, segment.Depth));
            }

            return scaled;
        }

        private Color GetColorForDepth(int depth, int maxDepth)
        {
            // Градиент от коричневого к зеленому в зависимости от глубины
            if (maxDepth == 0) maxDepth = 1;

            float ratio = (float)depth / maxDepth;

            // Коричневый (101, 67, 33) -> Зеленый (34, 139, 34)
            int r = (int)(101 + (34 - 101) * ratio);
            int g = (int)(67 + (139 - 67) * ratio);
            int b = (int)(33 + (34 - 33) * ratio);

            // Ограничиваем значения
            r = Math.Max(0, Math.Min(255, r));
            g = Math.Max(0, Math.Min(255, g));
            b = Math.Max(0, Math.Min(255, b));

            return Color.FromArgb(r, g, b);
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            if (segments.Count == 0) return;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Масштабируем сегменты под холст
            List<LineSegment> scaledSegments = ScaleSegmentsToCanvas(segments, canvas.Width, canvas.Height);

            // Находим максимальную глубину для градиента
            int maxDepth = segments.Max(s => s.Depth);

            // Рисуем все сегменты
            foreach (var segment in scaledSegments)
            {
                Color color = GetColorForDepth(segment.Depth, maxDepth);

                using (Pen pen = new Pen(color, Math.Max(1, segment.Width)))
                {
                    pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    e.Graphics.DrawLine(pen, segment.Start, segment.End);
                }
            }
        }

        // Класс для хранения состояния
        private class State
        {
            public PointF Position { get; }
            public double Angle { get; }
            public float Width { get; }
            public int Depth { get; }


            public State(PointF position, double angle, float width, int depth)
            {
                Position = position;
                Angle = angle;
                Width = width;
                Depth = depth;
            }
        }

        public class ExpressionContext
        {
            public string Expression { get; }
            public int Index { get; }
            public float Width { get; }
            public int Depth { get; }

            public ExpressionContext(string expression, int index, float width, int depth)
            {
                Expression = expression;
                Index = index;
                Width = width;
                Depth = depth;
            }
        }

        // Класс для хранения сегмента линии
        private class LineSegment
        {
            public PointF Start { get; }
            public PointF End { get; }

            //public float Length { get; }
            public float Width { get; }
            public int Depth { get; }

            public LineSegment(PointF start, PointF end, float width, int depth)
            {
                Start = start;
                End = end;
                //Length = length;
                Width = width;
                Depth = depth;
            }
        }
    }
}