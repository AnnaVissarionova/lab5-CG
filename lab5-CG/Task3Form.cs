using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace lab5_CG
{
    public partial class Task3Form : Form
    {
        private List<PointF> anchorPoints = new List<PointF>();     // Опорные точки
        private List<PointF> controlPoints = new List<PointF>();    // Контрольные точки
        private List<bool> smoothPoints = new List<bool>();         // Флаги гладкости точек
        private PointF? selectedPoint = null;
        private int selectedIndex = -1;
        private const int pointRadius = 4;
        private bool showControlPolygon = true;
        private bool showTangentVectors = true;

        // Область рисования
        private Rectangle drawingArea;
        private const int infoPanelWidth = 300;
        private const int margin = 10;

        // Новые цвета и шрифты
        private Color anchorPointColor = Color.FromArgb(0, 120, 215);     // Синий
        private Color controlPointColor = Color.FromArgb(0, 120, 215);     // Оранжево-красный
        private Color smoothPointColor = Color.FromArgb(16, 137, 62);     // Зеленый
        private Color angularPointColor = Color.FromArgb(192, 0, 0);      // Красный
        private Color curveColor = Color.Black;            // Темно-синий
        private Color controlLineColor = Color.FromArgb(160, 160, 160);   // Серый
        private Color tangentColor = Color.FromArgb(200, 45, 45);         // Красный
        private Color textColor = Color.FromArgb(30, 30, 30);             // Темно-серый
        private Color infoBackground = Color.FromArgb(250, 250, 255);     // Светло-голубой фон
        private Color drawingAreaBorder = Color.FromArgb(200, 200, 220);  // Цвет границы области рисования

        private Font infoFont = new Font("Segoe UI", 10, FontStyle.Regular);
        private Font pointFont = new Font("Segoe UI", 8, FontStyle.Bold);

        public Task3Form()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            this.Paint += Form1_Paint;
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
            this.KeyDown += Form1_KeyDown;
            this.MouseDoubleClick += Form1_MouseDoubleClick;
            this.SizeChanged += Form1_SizeChanged;

            // Инициализируем область рисования
            UpdateDrawingArea();
        }

        private void UpdateDrawingArea()
        {
            drawingArea = new Rectangle(
                infoPanelWidth + margin,
                margin,
                this.ClientSize.Width - infoPanelWidth - 2 * margin,
                this.ClientSize.Height - 2 * margin
            );
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            UpdateDrawingArea();
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Рисуем границу области рисования
            using (Pen borderPen = new Pen(drawingAreaBorder, 2))
            {
                g.DrawRectangle(borderPen, drawingArea);
            }

            // Рисуем информационную панель
            DrawInfoPanel(g);

            // Рисуем кривые Безье в области рисования
            DrawBezierSpline(g);

            // Рисуем точки в области рисования
            DrawPoints(g);
        }

        private void DrawInfoPanel(Graphics g)
        {
            // Фон информационной панели
            using (Brush backgroundBrush = new SolidBrush(infoBackground))
            {
                g.FillRectangle(backgroundBrush, 0, 0, infoPanelWidth, this.ClientSize.Height);
            }

            // Граница информационной панели
            using (Pen borderPen = new Pen(Color.FromArgb(180, 180, 200), 1))
            {
                g.DrawLine(borderPen, infoPanelWidth, 0, infoPanelWidth, this.ClientSize.Height);
            }

            // Информация
            string info =
                         "Управление:\n" +
                         "* Добавить точку - ЛКМ\n" +
                         "* Переместить точку - ЛКМ по точке\n" +
                         "* Удалить точку - ПКМ по точке\n" +
                         "* Изменить гладкость/угловатость - дважды \nкликнуть на точку\n" +
                         "* Показать/скрыть контрольные точки - C\n" +
                        "* Показать/скрыть касательные - T\n" +
                         "* Очистить - R\n\n";

            using (Brush textBrush = new SolidBrush(textColor))
            {
                g.DrawString(info, infoFont, textBrush, 15, 20);
            }

            // Примеры цветов точек
            int yPos = this.ClientSize.Height - 150;
            DrawColorExample(g, "Гладкие точки", smoothPointColor, 20, yPos);
            DrawColorExample(g, "Угловые точки", angularPointColor, 20, yPos + 25);
            DrawColorExample(g, "Контрольные точки", controlPointColor, 20, yPos + 50);
        }

        private void DrawColorExample(Graphics g, string text, Color color, int x, int y)
        {
            using (Brush colorBrush = new SolidBrush(color))
            using (Brush textBrush = new SolidBrush(textColor))
            {
                g.FillEllipse(colorBrush, x, y, 8, 8);
                g.DrawEllipse(Pens.Black, x, y, 8, 8);
                g.DrawString(text, new Font("Segoe UI", 9), textBrush, x + 18, y - 2);
            }
        }

        private void DrawBezierSpline(Graphics g)
        {
            if (anchorPoints.Count < 2) return;

            // Рисуем составную кривую Безье
            for (int i = 0; i < anchorPoints.Count - 1; i++)
            {
                PointF p0 = anchorPoints[i];
                PointF p1 = controlPoints[i * 2];
                PointF p2 = controlPoints[i * 2 + 1];
                PointF p3 = anchorPoints[i + 1];

                // Рисуем контрольный полигон
                if (showControlPolygon)
                {
                    using (Pen controlPen = new Pen(controlLineColor, 1.5f))
                    {
                        controlPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        controlPen.DashPattern = new float[] { 4, 3 };
                        g.DrawLine(controlPen, p0, p1);
                        g.DrawLine(controlPen, p2, p3);
                    }
                }

                // Рисуем касательные векторы
                if (showTangentVectors)
                {
                    using (Pen tangentPen = new Pen(tangentColor, 1.5f))
                    {
                        tangentPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                        g.DrawLine(tangentPen, p0, p1);
                        g.DrawLine(tangentPen, p3, p2);
                    }
                }

                // Рисуем кривую Безье
                using (Pen curvePen = new Pen(curveColor, 3))
                {
                    curvePen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                    DrawBezierCurve(g, curvePen, p0, p1, p2, p3);
                }
            }
        }

        private void DrawBezierCurve(Graphics g, Pen pen, PointF p0, PointF p1, PointF p2, PointF p3)
        {
            List<PointF> points = new List<PointF>();

            for (float t = 0; t <= 1; t += 0.01f)
            {
                float x = Bezier(t, p0.X, p1.X, p2.X, p3.X);
                float y = Bezier(t, p0.Y, p1.Y, p2.Y, p3.Y);
                points.Add(new PointF(x, y));
            }

            if (points.Count > 1)
            {
                g.DrawLines(pen, points.ToArray());
            }
        }

        private float Bezier(float t, float p0, float p1, float p2, float p3)
        {
            float u = 1 - t;
            float u2 = u * u;
            float u3 = u2 * u;
            float t2 = t * t;
            float t3 = t2 * t;

            return u3 * p0 + 3 * u2 * t * p1 + 3 * u * t2 * p2 + t3 * p3;
        }

        private void DrawPoints(Graphics g)
        {
            // Рисуем опорные точки
            for (int i = 0; i < anchorPoints.Count; i++)
            {
                Color pointColor = smoothPoints[i] ? smoothPointColor : angularPointColor;
                string label = $"P{i}";
                DrawPoint(g, anchorPoints[i], pointColor, label);
            }

            // Контрольные точки рисуем только если включено отображение полигона
            if (showControlPolygon)
            {
                for (int i = 0; i < controlPoints.Count; i++)
                {
                    DrawPoint(g, controlPoints[i], controlPointColor, $"V{i}");
                }
            }
        }

        private void DrawPoint(Graphics g, PointF point, Color color, string label)
        {
            // Рисуем внешний круг
            using (Brush outerBrush = new SolidBrush(Color.FromArgb(100, color)))
            {
                g.FillEllipse(outerBrush, point.X - pointRadius - 2, point.Y - pointRadius - 2,
                             pointRadius * 2 + 4, pointRadius * 2 + 4);
            }

            // Рисуем основной круг
            using (Brush brush = new SolidBrush(color))
            {
                g.FillEllipse(brush, point.X - pointRadius, point.Y - pointRadius,
                             pointRadius * 2, pointRadius * 2);
            }

            // Рисуем обводку
            using (Pen pen = new Pen(Color.Black, 1.2f))
            {
                g.DrawEllipse(pen, point.X - pointRadius, point.Y - pointRadius,
                             pointRadius * 2, pointRadius * 2);
            }

            // Подписываем точки
            using (Brush textBrush = new SolidBrush(textColor))
            {
                g.DrawString(label, pointFont, textBrush, point.X + pointRadius + 3, point.Y - pointRadius);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // Проверяем, что клик в области рисования
            if (!drawingArea.Contains(e.Location))
                return;

            if (e.Button == MouseButtons.Left)
            {
                // Проверяем, кликнули ли на существующую опорную точку
                for (int i = 0; i < anchorPoints.Count; i++)
                {
                    PointF point = anchorPoints[i];
                    double distance = Math.Sqrt(Math.Pow(point.X - e.X, 2) + Math.Pow(point.Y - e.Y, 2));

                    if (distance <= pointRadius)
                    {
                        selectedPoint = point;
                        selectedIndex = i;
                        return;
                    }
                }

                // Если не нашли существующую точку, добавляем новую ОПОРНУЮ точку
                // По умолчанию новые точки - гладкие
                AddAnchorPoint(new PointF(e.X, e.Y), true);
                this.Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                // Удаляем опорную точку (только если останется минимум 2 точки)
                for (int i = 0; i < anchorPoints.Count; i++)
                {
                    PointF point = anchorPoints[i];
                    double distance = Math.Sqrt(Math.Pow(point.X - e.X, 2) + Math.Pow(point.Y - e.Y, 2));

                    if (distance <= pointRadius && anchorPoints.Count > 2)
                    {
                        RemoveAnchorPoint(i);
                        this.Invalidate();
                        return;
                    }
                }
            }
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Проверяем, что клик в области рисования
            if (!drawingArea.Contains(e.Location))
                return;

            if (e.Button == MouseButtons.Left)
            {
                // Переключаем гладкость точки по двойному клику
                for (int i = 0; i < anchorPoints.Count; i++)
                {
                    PointF point = anchorPoints[i];
                    double distance = Math.Sqrt(Math.Pow(point.X - e.X, 2) + Math.Pow(point.Y - e.Y, 2));

                    if (distance <= pointRadius)
                    {
                        ToggleSmoothPoint(i);
                        this.Invalidate();
                        return;
                    }
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedPoint.HasValue && selectedIndex >= 0)
            {
                // Ограничиваем перемещение областью рисования
                PointF newPoint = new PointF(
                    Math.Max(drawingArea.Left, Math.Min(e.X, drawingArea.Right)),
                    Math.Max(drawingArea.Top, Math.Min(e.Y, drawingArea.Bottom))
                );

                anchorPoints[selectedIndex] = newPoint;
                // При перемещении опорной точки пересчитываем контрольные точки
                UpdateControlPoints();
                this.Invalidate();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            selectedPoint = null;
            selectedIndex = -1;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.C:
                    showControlPolygon = !showControlPolygon;
                    this.Invalidate();
                    break;
                case Keys.T:
                    showTangentVectors = !showTangentVectors;
                    this.Invalidate();
                    break;
                case Keys.R:
                    ResetPoints();
                    this.Invalidate();
                    break;
                case Keys.S:
                    // Клавиша S - сделать все точки гладкими
                    MakeAllPointsSmooth();
                    this.Invalidate();
                    break;
                case Keys.A:
                    // Клавиша A - сделать все точки угловыми
                    MakeAllPointsAngular();
                    this.Invalidate();
                    break;
            }
        }

        private void AddAnchorPoint(PointF point, bool isSmooth)
        {
            anchorPoints.Add(point);
            smoothPoints.Add(isSmooth);
            UpdateControlPoints();
        }

        private void RemoveAnchorPoint(int index)
        {
            anchorPoints.RemoveAt(index);
            smoothPoints.RemoveAt(index);
            UpdateControlPoints();
        }

        private void ToggleSmoothPoint(int index)
        {
            if (index > 0 && index < anchorPoints.Count - 1) // Нельзя изменить первую и последнюю точки
            {
                smoothPoints[index] = !smoothPoints[index];
                UpdateControlPoints();
            }
        }

        private void MakeAllPointsSmooth()
        {
            for (int i = 0; i < smoothPoints.Count; i++)
            {
                smoothPoints[i] = true;
            }
            UpdateControlPoints();
        }

        private void MakeAllPointsAngular()
        {
            for (int i = 0; i < smoothPoints.Count; i++)
            {
                if (i > 0 && i < smoothPoints.Count - 1) // Первую и последнюю не трогаем
                {
                    smoothPoints[i] = false;
                }
            }
            UpdateControlPoints();
        }

        private void UpdateControlPoints()
        {
            controlPoints.Clear();
            if (anchorPoints.Count < 2) return;

            const float minLength = 5f;
            const float maxLength = 150f;
            const float baseLength = 60f;
            const float adaptiveFactor = 0.4f; // 40% от расстояния

            // Обрабатываем первую точку
            if (anchorPoints.Count >= 2)
            {
                PointF p0 = anchorPoints[0];
                PointF p1 = anchorPoints[1];

                // АДАПТИВНАЯ длина: зависит от расстояния до соседней точки
                float distance = Distance(p0, p1);
                float length = Math.Min(distance * adaptiveFactor, baseLength);

                Vector direction = new Vector(p1.X - p0.X, p1.Y - p0.Y);
                direction = direction.Normalize() * Math.Max(minLength, Math.Min(length, maxLength));

                controlPoints.Add(new PointF(p0.X + direction.X, p0.Y + direction.Y));
            }

            // Обрабатываем промежуточные точки
            for (int i = 1; i < anchorPoints.Count - 1; i++)
            {
                PointF prev = anchorPoints[i - 1];
                PointF current = anchorPoints[i];
                PointF next = anchorPoints[i + 1];

                float distPrev = Distance(prev, current);
                float distNext = Distance(current, next);

                // АДАПТИВНАЯ длина: берем минимальное расстояние до соседей
                float length = Math.Min(Math.Min(distPrev, distNext) * adaptiveFactor, baseLength);

                // Ограничиваем длину
                length = Math.Max(minLength, Math.Min(length, maxLength));

                if (smoothPoints[i])
                {
                    Vector direction = new Vector(next.X - prev.X, next.Y - prev.Y);
                    direction = direction.Normalize() * length;
                    controlPoints.Add(new PointF(current.X - direction.X, current.Y - direction.Y));
                    controlPoints.Add(new PointF(current.X + direction.X, current.Y + direction.Y));
                }
                else
                {
                    Vector dirIn = new Vector(current.X - prev.X, current.Y - prev.Y);
                    Vector dirOut = new Vector(next.X - current.X, next.Y - current.Y);

                    // Для угловых точек - отдельные адаптивные длины
                    float lengthIn = Math.Min(distPrev * adaptiveFactor, baseLength);
                    float lengthOut = Math.Min(distNext * adaptiveFactor, baseLength);

                    lengthIn = Math.Max(minLength, Math.Min(lengthIn, maxLength));
                    lengthOut = Math.Max(minLength, Math.Min(lengthOut, maxLength));

                    dirIn = dirIn.Normalize() * lengthIn;
                    dirOut = dirOut.Normalize() * lengthOut;

                    controlPoints.Add(new PointF(current.X - dirIn.X, current.Y - dirIn.Y));
                    controlPoints.Add(new PointF(current.X + dirOut.X, current.Y + dirOut.Y));
                }
            }

            // Обрабатываем последнюю точку
            if (anchorPoints.Count >= 2)
            {
                PointF pLast = anchorPoints[anchorPoints.Count - 1];
                PointF pPrev = anchorPoints[anchorPoints.Count - 2];

                // АДАПТИВНАЯ длина: зависит от расстояния до соседней точки
                float distance = Distance(pPrev, pLast);
                float length = Math.Min(distance * adaptiveFactor, baseLength);

                Vector direction = new Vector(pLast.X - pPrev.X, pLast.Y - pPrev.Y);
                direction = direction.Normalize() * Math.Max(minLength, Math.Min(length, maxLength));
                controlPoints.Add(new PointF(pLast.X - direction.X, pLast.Y - direction.Y));
            }
        }

        private float Distance(PointF p1, PointF p2)
        {
            return (float)Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        private void ResetPoints()
        {
            anchorPoints.Clear();
            controlPoints.Clear();
            smoothPoints.Clear();
            this.Invalidate();
        }
    }

    // Вспомогательный класс для векторных операций
    public struct Vector
    {
        public float X, Y;

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float Length => (float)Math.Sqrt(X * X + Y * Y);

        public Vector Normalize()
        {
            float length = Length;
            if (length > 0)
                return new Vector(X / length, Y / length);
            return new Vector(0, 0);
        }

        public static Vector operator *(Vector v, float scalar)
        {
            return new Vector(v.X * scalar, v.Y * scalar);
        }
    }
}