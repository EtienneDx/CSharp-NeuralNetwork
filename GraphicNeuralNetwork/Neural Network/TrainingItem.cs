using System.Collections.Generic;

namespace Neural_Network
{
    [System.Serializable]
    public class TrainingItem
    {
        public List<double> inputs;

        public List<double> outputs;

        public TrainingItem(List<double> inputs, List<double> outputs)
        {
            this.inputs = inputs;
            this.outputs = outputs;
        }
    }
}