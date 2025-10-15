using System;
using System.Drawing;
using System.Windows.Forms;

namespace lab5_CG
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnLSystemBasic_Click(object sender, EventArgs e)
        {
            Task1aForm form = new Task1aForm();
            form.ShowDialog();
        }

        private void btnLSystemTree_Click(object sender, EventArgs e)
        {
            Task1bForm form = new Task1bForm();
            form.ShowDialog();
        }

        private void btnMidpointDisplacement_Click(object sender, EventArgs e)
        {
            Task2Form form = new Task2Form();
            form.ShowDialog();
        }

        private void btnBezierSpline_Click(object sender, EventArgs e)
        {
            Task3Form form = new Task3Form();
            form.ShowDialog();
        }
    }
}