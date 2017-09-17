using Newtonsoft.Json;
using System.Collections.Generic;

namespace Neural_Network
{
    public class NetworkInfos
    {
        public List<int> layers;

        public List<List<List<double>>> weights;

        public List<List<double>> biases;

        public double learningRate;

        public double momentum;

        public int genCount;

        public ulong trainedMillis;

        public List<double> costs;

        public NetworkInfos()
        { }

        public NetworkInfos(NeuralNetwork network)
        {
            learningRate = network.LearningRate;
            genCount = network.genCount;
            trainedMillis = network.TrainedMillis;
            momentum = network.Momentum;
            costs = network.Costs;

            layers = new List<int>();

            weights = new List<List<List<double>>>();

            biases = new List<List<double>>();

            for (int i = 0; i < network.LayerCount; i++)
            {
                Layer layer = network.Layers[i];

                layers.Add(layer.NeuronCount);

                if (i == 0) continue;

                List<List<double>> layerWeights = new List<List<double>>();

                List<double> layerBiases = new List<double>();

                for (int j = 0; j < layer.NeuronCount; j++)
                {
                    Neuron neuron = layer.Neurons[j];

                    layerBiases.Add(neuron.Bias);

                    List<double> neuronWeights = new List<double>();

                    for (int k = 0; k < neuron.Dendrites.Count; k++)
                    {
                        neuronWeights.Add(neuron.Dendrites[k].Weight);
                    }

                    layerWeights.Add(neuronWeights);
                }

                biases.Add(layerBiases);
                weights.Add(layerWeights);
            }
        }

        public string GetJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
