using System;
using System.Configuration;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        KellSCM.Controller control;

        private void Form1_Load(object sender, EventArgs e)
        {
            label3.Text = ConfigurationManager.AppSettings["comNum"];
            control = new KellSCM.Controller();
            control.Readed += Control_Readed;
            comboBox1.SelectedIndex = 1;
            timer1.Start();
        }

        private void Control_Readed(object sender, KellSCM.ReadDataArgs e)
        {
            label4.Text = e.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            control.Send(comboBox1.SelectedIndex, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            control.Send(comboBox1.SelectedIndex, false);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            control.Send(comboBox1.SelectedIndex, false, true);
        }
    }
}
