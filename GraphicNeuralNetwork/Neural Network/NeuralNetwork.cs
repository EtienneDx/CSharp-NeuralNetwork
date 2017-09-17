using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Neural_Network
{
    public partial class NeuralNetwork
    {
        /// <summary>
        /// All the layers of the neural network
        /// </summary>
        public List<Layer> Layers { get; set; }

        /// <summary>
        /// The rate at which the network will learn. The smaller the more accurate, but also the slower.
        /// </summary>
        public double LearningRate { get; set; }

        /// <summary>
        /// The momentum for the training algorithm
        /// </summary>
        public double Momentum { get; set; }

        /// <summary>
        /// All the data used to train the network
        /// </summary>
        List<TrainingItem> trainingData;

        /// <summary>
        /// Currently in training?
        /// </summary>
        bool isTraining = false;

        /// <summary>
        /// Number of generations of training
        /// </summary>
        public int genCount = 0;

        /// <summary>
        /// This generation total cost
        /// </summary>
        double cost = 0;
        
        Random rand = new Random();

        /// <summary>
        /// How long was the network trained for, updated after the end of the actual train session
        /// </summary>
        ulong _trainedMillis = 0;

        /// <summary>
        /// The watch controlling the milliseconds spent learning
        /// </summary>
        Stopwatch trainingWatch = new Stopwatch();

        /// <summary>
        /// The list of all the costs since the first generation
        /// </summary>
        public List<double> Costs { get; private set; } = new List<double>();

        /// <summary>
        /// How long was the network trained for, including the current train session
        /// </summary>
        public ulong TrainedMillis
        {
            get
            {
                return _trainedMillis + (ulong)trainingWatch.ElapsedMilliseconds;
            }
        }

        /// <summary>
        /// A random training item in the training dataset
        /// </summary>
        TrainingItem RandomTrainingData
        {
            get
            {
                return trainingData[rand.Next(trainingData.Count)];
            }
        }

        /// <summary>
        /// The number of layers of the network
        /// </summary>
        public int LayerCount
        {
            get
            {
                return Layers.Count;
            }
        }

        /// <summary>
        /// The last layer of the network
        /// </summary>
        public Layer OutputLayer
        {
            get
            {
                return Layers.Last();
            }
        }

        /// <summary>
        /// The first layer of the network
        /// </summary>
        public Layer InputLayer
        {
            get
            {
                return Layers[0];
            }
        }

        /// <summary>
        /// Runs the neural network with choosen inputs
        /// </summary>
        /// <param name="inputs">The inputs, should be of equal length than the first layer</param>
        /// <returns>The processed data</returns>
        public double[] Run(List<double> inputs)
        {
            // Gotta be the same length
            if (inputs.Count != Layers[0].NeuronCount)
                throw new Exception("The number of inputs and neurons on the first layer is different!");
            // No layer before the inputs, stays the same, just to assign the values to the neurons
            List<double> prevLayer = inputs;

            for (int i = 0; i < LayerCount; i++)
            {
                // Apply data from each previous layer to the next one
                prevLayer = Layers[i].Run(prevLayer, i == 0);
            }
            // Last prevLayer contains the output of the last layer -- the output layer, so all done
            return prevLayer.ToArray();
        }

        /// <summary>
        /// Trains the network over a single training item
        /// </summary>
        /// <param name="inputs">The inputs of each neuron on the first layer</param>
        /// <param name="expectedOut">The expected output for the network</param>
        /// <param name="returnCost">Should it return the cost?</param>
        /// <param name="addCostToTotal">Should it add the cost to the total cost of this generation?</param>
        /// <returns>The cost if returnCost is true</returns>
        public double Train(List<double> inputs, List<double> expectedOut, bool returnCost = false, bool addCostToTotal = false)
        {
            if (inputs.Count != Layers[0].NeuronCount)
                throw new Exception("Unexpected number of inputs, " + inputs.Count + " found instead of " + Layers[0].NeuronCount);
            if (expectedOut.Count != Layers.Last().NeuronCount)
                throw new Exception("Unexpected number of outputs, " + expectedOut.Count + " found instead of " + Layers.Last().NeuronCount);

            double[] realOut = null;// Only needed if we use the cost, otherwise, we use the in neuron stored value

            if (returnCost || addCostToTotal)
                realOut = Run(inputs);
            else
                Run(inputs);

            // Deltas of the output layer
            for (int i = 0; i < OutputLayer.NeuronCount; i++)
            {
                Neuron n = OutputLayer.Neurons[i];
                n.Delta = (n.OutputValue - expectedOut[i]) * n.OutputValue * (1 - n.OutputValue);// Replace n.value by its value before sigmoid
            }

            // Other layers
            for (int i = LayerCount - 2; i > 0; i--)
            {
                Layer layer = Layers[i];
                Layer nextLayer = Layers[i + 1];
                for (int j = 0; j < layer.NeuronCount; j++)
                {
                    Neuron n = layer.Neurons[j];
                    n.Delta = 0;

                    for (int nn = 0; nn < nextLayer.NeuronCount; nn++)
                    {
                        Neuron nextNeuron = nextLayer.Neurons[nn];
                        n.Delta += nextNeuron.Dendrites[j].Weight * nextNeuron.Delta;// that's good
                    }

                    n.Delta *= n.OutputValue * (1 - n.OutputValue);// same as l 137
                }
            }

            // Change in weight and bias
            for (int i = LayerCount - 1; i > 0; i--)
            {
                Layer layer = Layers[i];
                Layer prevLayer = Layers[i - 1];
                for (int j = 0; j < layer.NeuronCount; j++)
                {
                    Neuron n = layer.Neurons[j];
                    n.Bias -= LearningRate * n.Delta + Momentum * n.DeltaBias;

                    n.DeltaBias = LearningRate * n.Delta + Momentum * n.DeltaBias;

                    for (int k = 0; k < n.Dendrites.Count; k++)
                    {
                        double d = LearningRate * prevLayer.Neurons[k].OutputValue * n.Delta;
                        n.Dendrites[k].Weight -= d + Momentum * n.Dendrites[k].DeltaWeight;
                        // += seems to increase the cost over time -> solution for maximising it?
                        n.Dendrites[k].DeltaWeight = d + Momentum * n.Dendrites[k].DeltaWeight;
                    }
                }
            }

            if (returnCost)
            {
                return Cost(realOut, expectedOut);
            }
            else if (addCostToTotal)
                cost += Cost(realOut, expectedOut);
            return 0;
        }

        /// <summary>
        /// Trains the network over a single Training Item
        /// </summary>
        /// <param name="item">The item used to train the network</param>
        /// <param name="returnCost">Should it return the cost?</param>
        /// <returns>The cost if returnCost is true</returns>
        public double Train(TrainingItem item, bool returnCost = false)
        {
            return Train(item.inputs, item.outputs, returnCost);
        }

        /// <summary>
        /// Trains the network over a single Training Item
        /// </summary>
        /// <param name="item">The item used to train the network</param>
        /// <param name="then">The action to be executed after the training</param>
        /// <param name="addCostToTotal">Should it add the cost to this generation total?</param>
        public void Train(TrainingItem item, Action then, bool addCostToTotal = false)
        {
            Train(item.inputs, item.outputs, false, addCostToTotal);
            if (then != null)
                then.Invoke();
        }

        /// <summary>
        /// Lauches a training session over the given dataset
        /// </summary>
        /// <param name="items">The dataset used to train the network</param>
        /// <param name="genCallBack">The action to be executed after each generation end being trained</param>
        /// <param name="randomizeTrainingData">Should the data be shuffled and split into multiple generations?</param>
        /// <param name="genLength">The number of items in a generation</param>
        public void Train(List<TrainingItem> items, Action<int, double> genCallBack = null, bool randomizeTrainingData = false, int genLength = -1)
        {
            trainingData = items;
            isTraining = true;
            trainingWatch.Reset();
            trainingWatch.Start();
            TrainNext(-1, genCallBack, randomizeTrainingData, genLength == -1 ? items.Count : genLength);
        }

        /// <summary>
        /// Stops the training session as soon as possible (after the actual item being trained is finished)
        /// </summary>
        public void StopTraining()
        {
            trainingWatch.Stop();
            _trainedMillis += (ulong)trainingWatch.ElapsedMilliseconds;
            trainingWatch.Reset();
            isTraining = false;
        }

        /// <summary>
        /// Trains the next item, and handles the generations cost, callback, etc...
        /// </summary>
        /// <param name="i">The actual item id</param>
        /// <param name="genCallBack">The callback between each generations</param>
        /// <param name="randomizeTrainingData">Should the data be shuffled?</param>
        /// <param name="genLength">if the data is shuffled, how many items should there be in a generation?</param>
        async void TrainNext(int i, Action<int, double> genCallBack, bool randomizeTrainingData = false, int genLength = 50)
        {
            i = i == -1 ? 1 : (i + 1) % (randomizeTrainingData ? genLength : trainingData.Count);
            if (i == 0)
            {
                genCount++;

                cost /= (randomizeTrainingData ? genLength : trainingData.Count);

                // Generations costs list
                Costs.Add(cost);

                // Callback
                if (genCallBack != null)
                {
                    genCallBack.Invoke(genCount, cost);
                }
                cost = 0;
            }
            // Launch next training item
            await Task.Run(() => Train((randomizeTrainingData ? RandomTrainingData : trainingData[i]), () =>
            {
                if (isTraining == false) return;
                TrainNext(i, genCallBack, randomizeTrainingData, genLength);
            }, true));
        }

        /// <summary>
        /// Calculates the cost for a single output and expected output
        /// </summary>
        /// <param name="realOut">The real output</param>
        /// <param name="expectedOut">The expected output</param>
        /// <returns>The cost</returns>
        private double Cost(double[] realOut, List<double> expectedOut)
        {
            if (realOut.Length != expectedOut.Count)
                throw new Exception("Unexpected length difference...");
            double mag = 0;
            for (int i = 0; i < realOut.Length; i++)
            {
                mag += Math.Pow(realOut[i] - expectedOut[i], 2);
            }
            return Math.Sqrt(mag) / 2;//We take the magnitude of the realOut.length dimensions vector
        }
    }
}