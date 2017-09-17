using System.Collections.Generic;

namespace Neural_Network
{
    public class Neuron
    {
        /// <summary>
        /// All the dendrites connecting to the previous layer
        /// </summary>
        public List<Dendrite> Dendrites { get; set; }
        /// <summary>
        /// The actual output value of this neuron
        /// </summary>
        public double OutputValue { get; set; }
        /// <summary>
        /// The value before sigmoid
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        /// Used during teaching
        /// </summary>
        public double Delta { get; set; }
        /// <summary>
        /// The delta for the bias
        /// </summary>
        public double DeltaBias { get; set; }
        /// <summary>
        /// The Bias of this neuron
        /// </summary>
        public double Bias { get; set; }

        public Neuron()
        {
            Bias = CryptoRandom.RandomValue;
            Dendrites = new List<Dendrite>();
        }

        public Neuron(double bias, List<double> weights)
        {
            Bias = bias;
            Dendrites = new List<Dendrite>(weights.Count);

            for (int i = 0; i < weights.Count; i++)
            {
                Dendrite d = new Dendrite()
                {
                    Weight = weights[i]
                };
                Dendrites.Add(d);
            }
        }

        /// <summary>
        /// Adds up all the value for the previous layer, considering the weights of the dendrites, and scaling it via a sigmoid function
        /// </summary>
        /// <param name="prevLayer">The previous layer outputs</param>
        /// <returns>The neuron output</returns>
        public double Run(List<double> prevLayer)
        {
            Value = NeuralNetUtils.AddWeigthedList(prevLayer, Dendrites) + Bias;
            OutputValue = NeuralNetUtils.Sigmoid(Value);
            return OutputValue;
        }
    }
}