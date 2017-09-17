using Neural_Network;
using System;
using System.Collections.Generic;
using System.IO;

namespace GraphicNeuralNetwork.MNIST
{
    public class MnistReader
    {
        public static List<TrainingItem> ConvertTrainingData(List<DigitImage> imgs)
        {
            List<TrainingItem> ret = new List<TrainingItem>();

            for (int i = 0; i < imgs.Count; i++)
            {
                ret.Add(ConvertSingleImage(imgs[i]));
            }

            return ret;
        }

        public static TrainingItem ConvertSingleImage(DigitImage img)
        {
            List<double> inputs = new List<double>();
            List<double> outputs = new List<double>();

            for (int j = 0; j < 28; j+= 2)
            {
                for (int k = 0; k < 28; k+= 2)
                {
                    double d = img.pixels[j][k] + img.pixels[j + 1][k] + img.pixels[j][k + 1] + img.pixels[j + 1][k + 1];

                    inputs.Add(d / (4 * 255));
                }
            }

            for (byte j = 0; j < 4; j++)
            {
                outputs.Add((img.label & (byte)Math.Pow(2, j)) > 0 ? 1 : 0);
            }

            return new TrainingItem(inputs, outputs);
        }

        public static List<DigitImage> ReadImages(string imagesPath, string labelPaths, int imagesCount)
        {
            FileStream ifsLabels = new FileStream(labelPaths, FileMode.Open);

            FileStream ifsImages = new FileStream(imagesPath, FileMode.Open);

            BinaryReader brLabels = new BinaryReader(ifsLabels);
            BinaryReader brImages = new BinaryReader(ifsImages);

            int magic1 = brImages.ReadInt32();
            int numImages = brImages.ReadInt32();
            int numRows = brImages.ReadInt32();
            int numCols = brImages.ReadInt32();

            int magic2 = brLabels.ReadInt32();
            int numLabels = brLabels.ReadInt32();

            byte[][] pixels = new byte[28][];
            for (int i = 0; i < pixels.Length; ++i)
                pixels[i] = new byte[28];

            List<DigitImage> ret = new List<DigitImage>();

            for (int di = 0; di < imagesCount; ++di)
            {
                for (int i = 0; i < 28; ++i)
                {
                    for (int j = 0; j < 28; ++j)
                    {
                        byte b = brImages.ReadByte();
                        pixels[i][j] = b;
                    }
                }

                byte lbl = brLabels.ReadByte();

                ret.Add(new DigitImage(pixels, lbl));
            } // each image

            ifsImages.Close();
            brImages.Close();
            ifsLabels.Close();
            brLabels.Close();

            return ret;
        }
    }
}
