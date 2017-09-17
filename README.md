# CSharp-NeuralNetwork
A simple csharp neural network, and a working MNIST exemple.


## The Neural Network code
The code for the Neurons, Layer and Dendrites have been inspired of https://code.msdn.microsoft.com/windowsdesktop/Basis-of-Neural-Networks-f12ce1e9, but has been revisited to allow me a better understanding of each component, and I've rewritten the whole NeuralNetwork component.

I tried to comment the code as much as possible, to keep everything clear.

### How to use it
The way to use it is more or less easy, as this stays a on-going project, on a difficult subject.

To create a network, it's pretty basic : 

    NeuralNetwork network = new NeuralNetwork(0.01, 0.9, new int[]{ 2, 2, 1 });

Basically, the 0.01 is the learning rate, the 0.9 is the momentum, and the array correspond to each layer (here, 2 inputs, 2 neurons on a hidden layer, and 1 output).

To use the network, it's also simple : 

    List<double> inputs = new List<double>(){ 0.55, 0.2 };
    double[] outputs = network.Run(inputs);

The inputs represent the input to each neuron, and it returns the output.

Here's the interesting part, the training, which is simply the Train function, that can take different arguments : 

    public double Train(List<double> inputs, List<double> expectedOut, bool returnCost = false, bool addCostToTotal = false);
    // This function is used by other train function, since that's where the calculation are made
    
    public double Train(TrainingItem item, bool returnCost = false);
    // This function just refers to the one above, but using a TrainingItem instead of a list of inputs and one of outputs
    
    public void Train(TrainingItem item, Action then, bool addCostToTotal = false);
    // This function is the same again, just have a callback
    
    public void Train(List<TrainingItem> items, Action<int, double> genCallBack = null, bool randomizeTrainingData = false, int genLength = -1);
    // Here's the interesting one, this function trains the network over multiple items, randomly or not, split into 'generations', which will probably be renamed Epochs, since that's what they are
    // This function launches an async process that would never end, so here's the function to stop it : 
    public void StopTraining();
    
The training algorithm isn't optimal, but I'll keep working on it.

### Details about the code
All algorithm used here are more or less the conclusions I made as a young hobbyist, that haven't mastered all the maths yet, so it can seems pretty messy, but it's working. Most details can be found in the code.

## The MNIST example
Using this system, I've managed to trained a network to have about 85% of success over 100 digits, with about half an hour of training. Haven't tried long (12h+) training yet.

The network has one hidden layer of 15 neurons, an input layer of 196 neurons (I scale down, to allow testing, each image to fasten the training process, to a 14x14 size) and a binary output layer of 4 outputs.
