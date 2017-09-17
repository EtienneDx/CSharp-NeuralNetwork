using GraphicNeuralNetwork.MNIST;
using Neural_Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicNeuralNetwork.Exemple
{
    public partial class TestingForm : Form
    {
        Timer timer;

        int loop = 0;

        int worked = 0;

        bool timerRunning = false;

        public TestingForm()
        {
            InitializeComponent();
            next.Click += Next_Click;
            run100.Click += Run100_Click;
            FormClosing += TestingForm_FormClosing;
        }

        private void Run100_Click(object sender, EventArgs e)
        {
            loop = 0;
            worked = 0;
            timerRunning = true;

            timer = new Timer()
            {
                Interval = 150
            };
            timer.Tick += (s, ev) => Next();
            timer.Start();
        }

        private void TestingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if(!timerRunning)
                Next();
        }

        public void Next()
        {
            DigitImage img = Program.RandomTestingData;
            TrainingItem ti = MnistReader.ConvertSingleImage(img);
            Image realImg = img.ToBitMap();
            input.Image = new Bitmap(realImg, input.Size);
            byte b = 0;
            for (int i = 0; i < 4; i++)
            {
                b += (byte)(ti.outputs[i] > 0.5 ? Math.Pow(2, i) : 0);
            }
            byte o = Program.GetOutput(ti.inputs);
            output.Text = (timerRunning ? "Test in progress : \n" : "") + "Found : " + o + "\nExpected : " + b;

            if (timerRunning)
            {
                loop++;
                if (o == b)
                    worked++;
                if(loop >= 100)
                {
                    timerRunning = false;
                    timer.Stop();
                    output.Text = "Test finished\nSuccess : " + worked + " %";
                }
            }
        }
    }
}
