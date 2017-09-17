using System;
using System.Collections.Generic;

namespace Neural_Network
{
    public partial class NeuralNetwork
    {
        /// <summary>
        /// Loads a neural network from a previously stored NetworkInfos object
        /// </summary>
        /// <param name="infos">The network infos</param>
        public NeuralNetwork(NetworkInfos infos)
        {
            LearningRate = infos.learningRate;
            Momentum = infos.momentum;
            genCount = infos.genCount;
            _trainedMillis = infos.trainedMillis;
            Costs = infos.costs;

            if (infos.layers.Count < 2)
                throw new Exception("Invalid data, less than two layers found!");

            Layers = new List<Layer>
            {
                //first layer is appart
                new Layer(infos.layers[0], true, -1)
            };
            for (int i = 0; i < infos.layers.Count - 1; i++)
            {
                Layer layer = new Layer(infos.biases[i], infos.weights[i]);
                Layers.Add(layer);
            }
        }

        /// <summary>
        /// Creates a new neural network
        /// </summary>
        /// <param name="learningRate"></param>
        /// <param name="momentum"></param>
        /// <param name="layers">the number on neurons on each layer, as an array</param>
        public NeuralNetwork(double learningRate, double momentum, int[] layers)
        {
            // A neural network has to be made of at least two layers, inputs and output
            if (layers.Length < 2)
                throw new Exception("A neural network needs at least 2 layers!");

            // Assign data
            LearningRate = learningRate;
            Momentum = momentum;
            Layers = new List<Layer>();

            for (int l = 0; l < layers.Length; l++)
            {
                Layer layer = new Layer(layers[l], l == 0, l > 0 ? layers[l - 1] : -1);
                Layers.Add(layer);
            }
        }
    }
}
