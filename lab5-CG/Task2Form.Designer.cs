namespace lab5_CG
{
    partial class Task2Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.trackBarRoughness = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRoughnessValue = new System.Windows.Forms.Label();
            this.trackBarInitialHeight = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.lblInitialHeightValue = new System.Windows.Forms.Label();
            this.lblStep = new System.Windows.Forms.Label();
            this.lblPoints = new System.Windows.Forms.Label();
            this.lblSegments = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbShowGrid = new System.Windows.Forms.CheckBox();
            this.cbShowLines = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRoughness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarInitialHeight)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(12, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(600, 424);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(618, 12);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(170, 35);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "Следующий шаг";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(618, 53);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(170, 35);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Сбросить";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // trackBarRoughness
            // 
            this.trackBarRoughness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarRoughness.Location = new System.Drawing.Point(6, 40);
            this.trackBarRoughness.Maximum = 20;
            this.trackBarRoughness.Minimum = 1;
            this.trackBarRoughness.Name = "trackBarRoughness";
            this.trackBarRoughness.Size = new System.Drawing.Size(158, 45);
            this.trackBarRoughness.TabIndex = 3;
            this.trackBarRoughness.Value = 5;
            this.trackBarRoughness.Scroll += new System.EventHandler(this.UpdateRoughness);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Шероховатость:";
            // 
            // lblRoughnessValue
            // 
            this.lblRoughnessValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRoughnessValue.AutoSize = true;
            this.lblRoughnessValue.Location = new System.Drawing.Point(109, 20);
            this.lblRoughnessValue.Name = "lblRoughnessValue";
            this.lblRoughnessValue.Size = new System.Drawing.Size(31, 13);
            this.lblRoughnessValue.TabIndex = 5;
            this.lblRoughnessValue.Text = "0.5";
            // 
            // trackBarInitialHeight
            // 
            this.trackBarInitialHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarInitialHeight.Location = new System.Drawing.Point(6, 40);
            this.trackBarInitialHeight.Maximum = 300;
            this.trackBarInitialHeight.Minimum = 50;
            this.trackBarInitialHeight.Name = "trackBarInitialHeight";
            this.trackBarInitialHeight.Size = new System.Drawing.Size(158, 45);
            this.trackBarInitialHeight.TabIndex = 6;
            this.trackBarInitialHeight.Value = 150;
            this.trackBarInitialHeight.Scroll += new System.EventHandler(this.UpdateInitialHeight);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Начальная высота:";
            // 
            // lblInitialHeightValue
            // 
            this.lblInitialHeightValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInitialHeightValue.AutoSize = true;
            this.lblInitialHeightValue.Location = new System.Drawing.Point(117, 20);
            this.lblInitialHeightValue.Name = "lblInitialHeightValue";
            this.lblInitialHeightValue.Size = new System.Drawing.Size(25, 13);
            this.lblInitialHeightValue.TabIndex = 8;
            this.lblInitialHeightValue.Text = "150";
            // 
            // lblStep
            // 
            this.lblStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStep.AutoSize = true;
            this.lblStep.Location = new System.Drawing.Point(618, 100);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(46, 13);
            this.lblStep.TabIndex = 9;
            this.lblStep.Text = "Шаг: 0";
            // 
            // lblPoints
            // 
            this.lblPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPoints.AutoSize = true;
            this.lblPoints.Location = new System.Drawing.Point(618, 120);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(58, 13);
            this.lblPoints.TabIndex = 10;
            this.lblPoints.Text = "Точек: 0";
            // 
            // lblSegments
            // 
            this.lblSegments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSegments.AutoSize = true;
            this.lblSegments.Location = new System.Drawing.Point(618, 140);
            this.lblSegments.Name = "lblSegments";
            this.lblSegments.Size = new System.Drawing.Size(79, 13);
            this.lblSegments.TabIndex = 11;
            this.lblSegments.Text = "Сегментов: 0";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.trackBarRoughness);
            this.groupBox1.Controls.Add(this.lblRoughnessValue);
            this.groupBox1.Location = new System.Drawing.Point(618, 170);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 80);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.trackBarInitialHeight);
            this.groupBox2.Controls.Add(this.lblInitialHeightValue);
            this.groupBox2.Location = new System.Drawing.Point(618, 256);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 80);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Начальные значения";

            // 
            // cbShowGrid
            // 
            this.cbShowGrid.AutoSize = true;
            this.cbShowGrid.Checked = true;
            this.cbShowGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowGrid.Location = new System.Drawing.Point(6, 19);
            this.cbShowGrid.Name = "cbShowGrid";
            this.cbShowGrid.Size = new System.Drawing.Size(104, 17);
            this.cbShowGrid.TabIndex = 14;
            this.cbShowGrid.Text = "Показывать сетку";
            this.cbShowGrid.UseVisualStyleBackColor = true;
            this.cbShowGrid.CheckedChanged += new System.EventHandler(this.cbShowGrid_CheckedChanged);
            // 
            // cbShowLines
            // 
            this.cbShowLines.AutoSize = true;
            this.cbShowLines.Checked = true;
            this.cbShowLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowLines.Location = new System.Drawing.Point(6, 42);
            this.cbShowLines.Name = "cbShowLines";
            this.cbShowLines.Size = new System.Drawing.Size(130, 17);
            this.cbShowLines.TabIndex = 15;
            this.cbShowLines.Text = "Показывать ломаную";
            this.cbShowLines.UseVisualStyleBackColor = true;
            this.cbShowLines.CheckedChanged += new System.EventHandler(this.cbShowLines_CheckedChanged);

            // 
            // cbMakeBeautiful
            // 
            this.cbMakeBeautiful = new System.Windows.Forms.CheckBox();
            this.cbMakeBeautiful.AutoSize = true;
            this.cbMakeBeautiful.Location = new System.Drawing.Point(6, 65);
            this.cbMakeBeautiful.Name = "cbMakeBeautiful";
            this.cbMakeBeautiful.Size = new System.Drawing.Size(113, 17);
            this.cbMakeBeautiful.TabIndex = 16;
            this.cbMakeBeautiful.Text = "красота";
            this.cbMakeBeautiful.UseVisualStyleBackColor = true;
            this.cbMakeBeautiful.CheckedChanged += new System.EventHandler(this.cbMakeBeautiful_CheckedChanged);

            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.cbShowGrid);
            this.groupBox3.Controls.Add(this.cbShowLines);
            this.groupBox3.Controls.Add(this.cbMakeBeautiful); // Добавляем эту строку
            this.groupBox3.Location = new System.Drawing.Point(618, 342);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(170, 85); // Увеличиваем высоту
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Визуализация";

            // 
            // Task2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblSegments);
            this.Controls.Add(this.lblPoints);
            this.Controls.Add(this.lblStep);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.pictureBox);
            this.Name = "Task2Form";
            this.Text = "Midpoint Displacement - Генерация горного массива";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRoughness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarInitialHeight)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TrackBar trackBarRoughness;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRoughnessValue;
        private System.Windows.Forms.TrackBar trackBarInitialHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblInitialHeightValue;
        private System.Windows.Forms.Label lblStep;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.Label lblSegments;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbShowGrid;
        private System.Windows.Forms.CheckBox cbShowLines;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbMakeBeautiful;
    }
}