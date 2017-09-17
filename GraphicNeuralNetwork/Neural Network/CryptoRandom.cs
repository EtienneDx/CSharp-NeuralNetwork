using System;
using System.Security.Cryptography;

namespace Neural_Network
{
    public static class CryptoRandom
    {
        private static Random rand;

        public static double RandomValue
        {
            get
            {
                return rand.NextDouble();
            }
        }

        static CryptoRandom()
        {
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            rand = new Random(rngCsp.GetHashCode());
        }

    }
}