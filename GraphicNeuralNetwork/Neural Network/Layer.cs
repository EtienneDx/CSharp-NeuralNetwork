using System.Collections.Generic;

namespace Neural_Network
{
    public class Layer
    {
        /// <summary>
        /// The neurons on that layer
        /// </summary>
        public List<Neuron> Neurons { get; set; }
        /// <summary>
        /// The amount of neurons on that layer
        /// </summary>
        public int NeuronCount
        {
            get
            {
                return Neurons.Count;
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="numNeurons">Amount of neurons</param>
        /// <param name="isFirstLayer">Is this the first layer</param>
        /// <param name="numNeuronsPrevLayer">The amount of neurons on previous layer(useless if it is the first layer)</param>
        public Layer(int numNeurons, bool isFirstLayer, int numNeuronsPrevLayer)
        {
            // Create neurons list
            Neurons = new List<Neuron>(numNeurons);

            for (int n = 0; n < numNeurons; n++)
            {
                // Create each neuron
                Neuron nn = new Neuron();
                Neurons.Add(nn);

                // First layer -> No bias
                if (isFirstLayer)
                    nn.Bias = 0;
                // Else, need for dendrites, one per neuron on previous layer
                else
                    for (int d = 0; d < numNeuronsPrevLayer; d++)
                        nn.Dendrites.Add(new Dendrite());
            }
        }

        public Layer(List<double> biases, List<List<double>> weights)
        {
            Neurons = new List<Neuron>(biases.Count);
            for (int i = 0; i < biases.Count; i++)
            {
                // Create each neuron
                Neuron nn = new Neuron(biases[i], weights[i]);
                Neurons.Add(nn);
            }
        }

        /// <summary>
        /// Apply the prev layer output to this layer, and store the value inside each neuron
        /// </summary>
        /// <param name="previousLayer">The outputs of the previous layer, or the inputs if it is the first layer</param>
        /// <param name="isFirstLayer">Is it the first layer?</param>
        /// <returns></returns>
        public List<double> Run(List<double> previousLayer, bool isFirstLayer = false)
        {
            List<double> ret = new List<double>();
            for (int i = 0; i < NeuronCount; i++)
            {
                if (isFirstLayer)
                    Neurons[i].OutputValue = previousLayer[i];
                else
                    ret.Add(Neurons[i].Run(previousLayer));
            }
            // First layer, output == inputs
            if (isFirstLayer)
                return previousLayer;
            return ret;
        }
    }
}