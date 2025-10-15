namespace lab5_CG
{
    partial class MainForm
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
            this.btnLSystemBasic = new System.Windows.Forms.Button();
            this.btnLSystemTree = new System.Windows.Forms.Button();
            this.btnMidpointDisplacement = new System.Windows.Forms.Button();
            this.btnBezierSpline = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLSystemBasic
            // 
            this.btnLSystemBasic.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLSystemBasic.Location = new System.Drawing.Point(50, 80);
            this.btnLSystemBasic.Name = "btnLSystemBasic";
            this.btnLSystemBasic.Size = new System.Drawing.Size(300, 50);
            this.btnLSystemBasic.TabIndex = 0;
            this.btnLSystemBasic.Text = "1. L-системы (Базовые)";
            this.btnLSystemBasic.UseVisualStyleBackColor = true;
            this.btnLSystemBasic.Click += new System.EventHandler(this.btnLSystemBasic_Click);
            // 
            // btnLSystemTree
            // 
            this.btnLSystemTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLSystemTree.Location = new System.Drawing.Point(50, 150);
            this.btnLSystemTree.Name = "btnLSystemTree";
            this.btnLSystemTree.Size = new System.Drawing.Size(300, 50);
            this.btnLSystemTree.TabIndex = 1;
            this.btnLSystemTree.Text = "1.б L-системы (Фрактальное дерево)";
            this.btnLSystemTree.UseVisualStyleBackColor = true;
            this.btnLSystemTree.Click += new System.EventHandler(this.btnLSystemTree_Click);
            // 
            // btnMidpointDisplacement
            // 
            this.btnMidpointDisplacement.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMidpointDisplacement.Location = new System.Drawing.Point(50, 220);
            this.btnMidpointDisplacement.Name = "btnMidpointDisplacement";
            this.btnMidpointDisplacement.Size = new System.Drawing.Size(300, 50);
            this.btnMidpointDisplacement.TabIndex = 2;
            this.btnMidpointDisplacement.Text = "2. Алгоритм Midpoint Displacement";
            this.btnMidpointDisplacement.UseVisualStyleBackColor = true;
            this.btnMidpointDisplacement.Click += new System.EventHandler(this.btnMidpointDisplacement_Click);
            // 
            // btnBezierSpline
            // 
            this.btnBezierSpline.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBezierSpline.Location = new System.Drawing.Point(50, 290);
            this.btnBezierSpline.Name = "btnBezierSpline";
            this.btnBezierSpline.Size = new System.Drawing.Size(300, 50);
            this.btnBezierSpline.TabIndex = 3;
            this.btnBezierSpline.Text = "3. Кубические сплайны Безье";
            this.btnBezierSpline.UseVisualStyleBackColor = true;
            this.btnBezierSpline.Click += new System.EventHandler(this.btnBezierSpline_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(100, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Выберите задание:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 380);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBezierSpline);
            this.Controls.Add(this.btnMidpointDisplacement);
            this.Controls.Add(this.btnLSystemTree);
            this.Controls.Add(this.btnLSystemBasic);
            this.Name = "MainForm";
            this.Text = "Фракталы и Сплайны";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btnLSystemBasic;
        private System.Windows.Forms.Button btnLSystemTree;
        private System.Windows.Forms.Button btnMidpointDisplacement;
        private System.Windows.Forms.Button btnBezierSpline;
        private System.Windows.Forms.Label label1;
        #endregion
    }
}