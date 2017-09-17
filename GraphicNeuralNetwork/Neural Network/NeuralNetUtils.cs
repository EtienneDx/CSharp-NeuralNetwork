using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Neural_Network
{
    public static class NeuralNetUtils
    {
        public static double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        public static double AddWeigthedList(List<double> list, List<Dendrite> weights)
        {
            if (list.Count != weights.Count)
                throw new Exception("List and Weights have different counts!");
            double ret = 0;
            for (int i = 0; i < list.Count; i++)
            {
                ret += weights[i].Weight * list[i];
            }
            return ret;
        }

        internal static List<TrainingItem> LoadJSONTrainingData(string path)
        {
            List<TrainingItem> data = new List<TrainingItem>();

            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                data = JsonConvert.DeserializeObject<List<TrainingItem>>(json);
            }

            return data;
        }

        public static string DoubleArrayToString(double[] arr)
        {
            string ret = "[";
            for (int i = 0; i < arr.Length; i++)
            {
                ret += (ret == "[" ? "" : "; ") + arr[i].ToString("F3");
            }
            return ret + "]";
        }
    }
}