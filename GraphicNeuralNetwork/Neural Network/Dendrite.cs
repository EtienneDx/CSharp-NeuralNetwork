namespace Neural_Network
{
    public class Dendrite
    {
        /// <summary>
        /// The weight of this dendrite
        /// </summary>
        public double Weight { get; set; }

        public double DeltaWeight { get; set; }

        public Dendrite()
        {
            // Starts at a random value
            Weight = CryptoRandom.RandomValue;
        }
    }
}