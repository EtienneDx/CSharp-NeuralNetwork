using Neural_Network;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphicNeuralNetwork.Exemple
{
    public partial class NetworkForm : Form
    {
        NeuralNetwork network;
        int neuronWidth = 25;
        int neuronHeight = 25;

        public Action stopTrainingAction;
        public Action startTrainingAction;
        public Action refreshAction;
        public Action toJsonAction;
        public Action testNetworkAction;
        public Action costGraphAction;

        public NetworkForm()
        {
            InitializeComponent();
            networkPanel.Paint += new PaintEventHandler(PaintNetwork);
            startTraining.Click += StartTraining_Click;
            stopTraining.Click += StopTraining_Click;
            refresh.Click += Refresh_Click;
            toJson.Click += ToJson_Click;
            testNetwork.Click += TestNetwork_Click;
            costGraph.Click += CostGraph_Click;
        }

        private void CostGraph_Click(object sender, EventArgs e)
        {
            if (costGraphAction != null)
                costGraphAction.Invoke();
        }

        private void TestNetwork_Click(object sender, EventArgs e)
        {
            if (testNetworkAction != null)
                testNetworkAction.Invoke();
        }

        private void ToJson_Click(object sender, EventArgs e)
        {
            if (toJsonAction != null)
                toJsonAction.Invoke();
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            RefreshNetwork();
            if (refreshAction != null)
                refreshAction.Invoke();
        }

        private void StartTraining_Click(object sender, EventArgs e)
        {
            if (startTrainingAction != null)
                startTrainingAction.Invoke();
        }

        private void StopTraining_Click(object sender, EventArgs e)
        {
            if (stopTrainingAction != null)
                stopTrainingAction.Invoke();
        }

        public void SetProgress(string t)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke((Action<string>)SetProgress, t);
                    return;
                }
                progress.Text = t;
            }
            catch { }
        }

        public void RefreshNetwork()
        {
            networkPanel.Invalidate();
        }

        public void DrawNetwork(NeuralNetwork network, int neuronWidth = 25, int neuronHeight = 25)
        {
            this.network = network;
            this.neuronWidth = neuronWidth;
            this.neuronHeight = neuronHeight;

            networkPanel.Invalidate();
        }

        void PaintNetwork(object sender, PaintEventArgs e)
        {
            if (network == null) return;

            Graphics panel = networkPanel.CreateGraphics();

            Pen p = new Pen(Color.Black);
            SolidBrush sb = new SolidBrush(Color.Black);

            float layerSpacing = networkPanel.Width / network.LayerCount;

            float prevNeuronSpacing = 0;

            int prevNeuronCount = 0;

            int prevNeuronPerSpace = 1;

            for (int i = 0; i < network.LayerCount; i++)
            {
                Layer layer = network.Layers[i];

                float x = (i + 0.5f) * layerSpacing;

                int neuronCount = layer.NeuronCount > 20 ? 20 : layer.NeuronCount;

                float neuronSpacing = networkPanel.Height / neuronCount;

                for (int j = 0; j < neuronCount; j++)
                {
                    Neuron n = layer.Neurons[j];

                    float y = (j + 0.5f) * neuronSpacing;

                    panel.DrawEllipse(p, x - neuronWidth / 2f, y - neuronHeight / 2f, neuronWidth, neuronHeight);
                    panel.FillEllipse(sb, x - neuronWidth / 2f, y - neuronHeight / 2f, neuronWidth, neuronHeight);

                    if (i > 0)
                    {
                        int l = 0;
                        double weight = 0;
                        for (int k = 0; k < n.Dendrites.Count; k++)
                        {
                            l++;

                            Dendrite d = n.Dendrites[k];
                            Pen dendritePen;

                            weight += d.Weight;

                            if (l >= prevNeuronPerSpace)
                            {
                                weight /= l;
                                if (weight == 0) return;
                                if (weight > 0)
                                    dendritePen = new Pen(Color.Black, (float)NeuralNetUtils.Sigmoid(weight) * 3f);
                                else
                                    dendritePen = new Pen(Color.Red, (float)NeuralNetUtils.Sigmoid(-weight) * 3f);

                                int neuronNum = (int)((float)k / network.Layers[i - 1].NeuronCount * prevNeuronCount);

                                panel.DrawLine(dendritePen,
                                    new Point((int)(layerSpacing * (i - 0.5f)), (int)(prevNeuronSpacing * (neuronNum + 0.5f))),
                                    new Point((int)x, (int)y));

                                weight = 0;
                                l = 0;
                            }
                        }
                    }
                }
                
                prevNeuronPerSpace = (int)(layer.NeuronCount / neuronSpacing);
                prevNeuronCount = neuronCount;
                prevNeuronSpacing = neuronSpacing;
            }
        }
    }
}
