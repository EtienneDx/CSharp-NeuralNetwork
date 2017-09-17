using GraphicNeuralNetwork.Exemple;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GraphicNeuralNetwork.MNIST;
using System.IO;
using Neural_Network;
using Newtonsoft.Json;

namespace GraphicNeuralNetwork
{
    static class Program
    {
        const string networkDataPath = "networkData.json";

        static NetworkForm form;

        static TestingForm testForm;

        static Graph graphForm;

        static NeuralNetwork network;

        static List<DigitImage> testingData = null;

        static Random rand = new Random();

        public static DigitImage RandomTestingData
        {
            get
            {
                return testingData[rand.Next(testingData.Count)];
            }
        }

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /******LOAD DATA******/
            List<TrainingItem> trainData =
                MnistReader.ConvertTrainingData(
                    MnistReader.ReadImages("train-images.idx3-ubyte", "train-labels.idx1-ubyte", 60000));

            /***Test form***/

            testForm = new TestingForm();

            /***Graph***/

            graphForm = new Graph();

            /*****Create Network Form*****/

            form = new NetworkForm();

            if (!TryLoadNetwork())
                network = new NeuralNetwork(0.01, 0.9, new int[] { 196, 15, 4 });

            form.DrawNetwork(network);

            form.startTrainingAction += () => network.Train(trainData, (i, cost) =>
            {
                form.SetProgress("Trained for " + TimeSpan.FromMilliseconds(network.TrainedMillis).ToString("c") + " - Gen " + i + " - Cost : " + cost);
                if (graphForm.Visible)
                {
                    graphForm.SetGraphData("Cost", network.Costs);
                }
            }, true, 10000);

            form.stopTrainingAction += network.StopTraining;

            form.toJsonAction += () =>
            {
                File.WriteAllText(networkDataPath, new NetworkInfos(network).GetJSON());
            };

            form.testNetworkAction += TestNetwork;

            form.costGraphAction += () =>
            {
                graphForm.SetGraphData("Cost", network.Costs);
                graphForm.Show();
            };

            form.FormClosed += (object o, FormClosedEventArgs a) => network.StopTraining();

            /*****END*****/

            /***RUN***/
            Application.Run(form);
        }

        private static bool TryLoadNetwork()
        {
            if (File.Exists(networkDataPath))
            {
                string json = File.ReadAllText(networkDataPath);
                NetworkInfos infos = JsonConvert.DeserializeObject<NetworkInfos>(json);
                network = new NeuralNetwork(infos);
                return true;
            }
            return false;
        }

        private static void TestNetwork()
        {
            network.StopTraining();
            if (testingData == null)
            {
                testingData = MnistReader.ReadImages("t10k-images.idx3-ubyte", "t10k-labels.idx1-ubyte", 2000);
            }
            testForm.Show();
            testForm.Next();
        }

        public static byte GetOutput(List<double> inputs)
        {
            double[] outputs = network.Run(inputs);
            byte b = 0;
            for (int i = 0; i < 4; i++)
            {
                b += (byte)(outputs[i] > 0.6 ? Math.Pow(2, i) : 0);
            }
            return b;
        }
    }
}
