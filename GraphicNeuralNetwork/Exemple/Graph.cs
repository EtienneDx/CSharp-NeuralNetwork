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
    public partial class Graph : Form
    {
        Dictionary<string, List<double>> GraphData { get; set; } = new Dictionary<string, List<double>>();

        int separationsCount = 5;

        int MaxCount
        {
            get
            {
                int i = 0;
                foreach (var kv in GraphData)
                {
                    i = Math.Max(i, kv.Value.Count());
                }
                return i;
            }
        }

        double MaxValue
        {
            get
            {
                double b = double.NegativeInfinity;
                foreach (var kv in GraphData)
                {
                    b = Math.Max(b, kv.Value.Max());
                }
                return b;
            }
        }

        double MinValue
        {
            get
            {
                double b = double.PositiveInfinity;
                foreach (var kv in GraphData)
                {
                    b = Math.Min(b, kv.Value.Min());
                }
                return b;
            }
        }

        double ValueCover
        {
            get
            {
                return MaxValue - MinValue;
            }
        }

        float ValuePerSeparation
        {
            get
            {
                return (float)ValueCover / separationsCount;
            }
        }

        float ChartHeight
        {
            get
            {
                return chart.Height - 20;
            }
        }

        float ChartWidth
        {
            get
            {
                return chart.Width;
            }
        }

        float SpacePerSeparation
        {
            get
            {
                return ChartHeight / separationsCount;
            }
        }

        public Graph()
        {
            InitializeComponent();
            chart.Paint += Chart_Paint;
            FormClosing += Graph_FormClosing;
        }

        private void Graph_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void Chart_Paint(object sender, PaintEventArgs e)
        {
            if (MaxCount == 0) return;
            Graphics panel = chart.CreateGraphics();

            int x = 0;
            int y;

            Pen pen = new Pen(Color.Black);

            labels.Controls.Clear();
            for (int i = 0; i < separationsCount; i++)
            {
                y = (int)GetY(MinValue + ValuePerSeparation * i);
                Label l = new Label()
                {
                    Location = new Point(x, y),
                    Text = (MinValue + ValuePerSeparation * i).ToString()
                };
                labels.Controls.Add(l);
                Point p1 = new Point(x, y);
                Point p2 = new Point((int)ChartWidth, y);
                try
                {
                    panel.DrawLine(pen, p1, p2);
                }
                catch { }
            }

            int xSpace = (int)Math.Max((ChartWidth / MaxCount), 1);

            int startValue = Math.Max(0, MaxCount - (int)ChartWidth);

            foreach (var kv in GraphData)
            {
                Point lastP = Point.Empty;
                for (int i = startValue; i < kv.Value.Count; i++)
                {
                    Point point = new Point((i - startValue) * xSpace, (int)GetY(kv.Value[i]));
                    if (i > startValue)
                    {
                        panel.DrawLine(pen, lastP, point);
                    }
                    lastP = point;
                }
            }
        }

        private float GetY(double value)
        {
            return ChartHeight - (((float)(value - MinValue) / ValuePerSeparation) * SpacePerSeparation);
        }

        public void SetGraphData(string dataName, List<double> value)
        {
            if (GraphData.ContainsKey(dataName))
                GraphData[dataName] = value;
            else
                GraphData.Add(dataName, value);
            chart.Invalidate();
        }

        public void RemoveGraphData(string dataName)
        {
            if (GraphData.ContainsKey(dataName))
                GraphData.Remove(dataName);
            chart.Invalidate();
        }
    }
}
