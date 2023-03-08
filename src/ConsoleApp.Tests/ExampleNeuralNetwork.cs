using MyNeuralNetwork.Domain.Entities.Nets.Collections.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Interfaces.Networks;
using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using MyNeuralNetwork.Domain.Entities.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp.Tests
{
    public class ExampleNeuralNetwork : IComparable<ExampleNeuralNetwork>, INeuralNetwork
    {
        //fundamental 
        private int[] layers;//layers
        public float[][] neurons;//neurons
        private float[][] biases;//biasses
        private float[][][] weights;//weights
        private int[] activations;//layers

        //genetic
        public float fitness = 0;//fitness

        //backprop
        public float learningRate = 0.01f;//learning rate
        public float cost = 0;

        private float[][] deltaBiases;//biasses
        private float[][][] deltaWeights;//weights
        private int deltaCount;

        public LayerCollection Layers { get; private set; } = new LayerCollection();

        public ExampleNeuralNetwork(int[] layers, string[] layerActivations)
        {
            this.layers = new int[layers.Length];
            for (int i = 0; i < layers.Length; i++)
            {
                this.layers[i] = layers[i];
            }
            activations = new int[layers.Length - 1];
            for (int i = 0; i < layers.Length - 1; i++)
            {
                string action = layerActivations[i];
                switch (action)
                {
                    case "sigmoid":
                        activations[i] = 0;
                        break;
                    case "tanh":
                        activations[i] = 1;
                        break;
                    case "relu":
                        activations[i] = 2;
                        break;
                    case "leakyrelu":
                        activations[i] = 3;
                        break;
                    default:
                        activations[i] = 2;
                        break;
                }
            }

            InitNeurons();
            InitBiases();
            InitWeights();
        }

        private void InitNeurons()//create empty storage array for the neurons in the network.
        {
            List<float[]> neuronsList = new List<float[]>();
            for (int i = 0; i < layers.Length; i++)
            {
                neuronsList.Add(new float[layers[i]]);
            }
            neurons = neuronsList.ToArray();
        }

        private void InitBiases()//initializes and populates array for the biases being held within the network.
        {
            List<float[]> biasList = new List<float[]>();
            for (int i = 0; i < layers.Length; i++)
            {
                float[] bias = new float[layers[i]];
                for (int j = 0; j < layers[i]; j++)
                {
                    bias[j] = MyRandom.Range(-0.5f, 0.5f);
                    bias[j] = 1;
                }
                biasList.Add(bias);
            }
            biases = biasList.ToArray();
        }

        private void InitWeights()//initializes random array for the weights being held in the network.
        {
            List<float[][]> weightsList = new List<float[][]>();
            for (int i = 1; i < layers.Length; i++)
            {
                List<float[]> layerWeightsList = new List<float[]>();
                int neuronsInPreviousLayer = layers[i - 1];
                for (int j = 0; j < neurons[i].Length; j++)
                {
                    float[] neuronWeights = new float[neuronsInPreviousLayer];
                    for (int k = 0; k < neuronsInPreviousLayer; k++)
                    {
                        //float sd = 1f / ((neurons[i].Length + neuronsInPreviousLayer) / 2f);
                        neuronWeights[k] = MyRandom.Range(-0.5f, 0.5f);
                        neuronWeights[k] = 1;
                    }
                    layerWeightsList.Add(neuronWeights);
                }
                weightsList.Add(layerWeightsList.ToArray());
            }
            weights = weightsList.ToArray();
        }

        public float[] Predict(Input[] inputs)
        {
            float[] inputsInFloat = new float[inputs.Length];

            for (int i = 0; i < inputs.Length; i++)
            {
                inputsInFloat[i] = inputs[i].Value;
            }

            return FeedForward(inputsInFloat);
        }

        public float[] FeedForward(float[] inputs)//feed forward, inputs >==> outputs.
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                neurons[0][i] = inputs[i];
            }
            for (int i = 1; i < layers.Length; i++)
            {
                int layer = i - 1;
                for (int j = 0; j < neurons[i].Length; j++)
                {
                    float value = 0f;
                    for (int k = 0; k < neurons[i - 1].Length; k++)
                    {
                        value += weights[i - 1][j][k] * neurons[i - 1][k];
                    }
                    neurons[i][j] = activate(value + biases[i][j]);
                }
            }
            return neurons[neurons.Length - 1];
        }

        public float activate(float value)
        {
            return (float)Math.Tanh(value);
        }


        public void BackPropagate(float[] inputs, float[] expected)//backpropogation;
        {
            float[] output = FeedForward(inputs);//runs feed forward to ensure neurons are populated correctly

            cost = 0;
            for (int i = 0; i < output.Length; i++)
            {
                cost += (float)Math.Pow(output[i] - expected[i], 2);//calculated cost of network
            }
            cost /= 2;//this value is not used in calculions, rather used to identify the performance of the network

            float[][] gamma;

            gamma = CreateGamma();

            CalculateGamma(expected, output, gamma);

            CalculateLastLayerWeightAndBias(gamma);

            CalculateWeightAndBiasOfHiddenLayers(gamma);
        }

        private float[][] CreateGamma()
        {
            float[][] gamma;
            List<float[]> gammaList = new List<float[]>();
            for (int i = 0; i < layers.Length; i++)
            {
                gammaList.Add(new float[layers[i]]);
            }
            gamma = gammaList.ToArray();//gamma initialization
            return gamma;
        }
        private void CalculateGamma(float[] expected, float[] output, float[][] gamma)
        {
            int layer = layers.Length - 2;
            for (int numNeuron = 0; numNeuron < output.Length; numNeuron++)
            {
                gamma[layers.Length - 1][numNeuron] = (output[numNeuron] - expected[numNeuron]) * activateDer(output[numNeuron], layer);//Gamma calculation
            }
        }
        private void CalculateLastLayerWeightAndBias(float[][] gamma)
        {
            int lastLayer = layers.Length - 1;
            int hiddenLayer = layers.Length - 2;
            for (int neuronIndex = 0; neuronIndex < layers[lastLayer]; neuronIndex++)//calculates the w' and b' for the last layer in the network
            {
                for (int hiddenNeuronIndex = 0; hiddenNeuronIndex < layers[hiddenLayer]; hiddenNeuronIndex++)
                {
                    biases[hiddenLayer][hiddenNeuronIndex] -= gamma[lastLayer][neuronIndex] * learningRate;
                    weights[hiddenLayer][neuronIndex][hiddenNeuronIndex] -= gamma[lastLayer][neuronIndex] * neurons[hiddenLayer][hiddenNeuronIndex] * learningRate; 
                }
            }
        }
        private void CalculateWeightAndBiasOfHiddenLayers(float[][] gamma)
        {
            int lastHiddenLayerIndex = layers.Length - 2;
            for (int hiddenLayerIndex = lastHiddenLayerIndex; hiddenLayerIndex > 0; hiddenLayerIndex--)//runs on all hidden layers
            {
                var previousLayerIndex = hiddenLayerIndex - 1;
                var nextHiddenLayerIndex = hiddenLayerIndex + 1;

                for (int hiddenNeuronIndex = 0; hiddenNeuronIndex < layers[hiddenLayerIndex]; hiddenNeuronIndex++)//outputs
                {
                    float gammaSum = 0;
                    for (int nextGammaIndex = 0; nextGammaIndex < gamma[nextHiddenLayerIndex].Length; nextGammaIndex++)
                    {
                        gammaSum += gamma[nextHiddenLayerIndex][nextGammaIndex] * weights[hiddenLayerIndex][nextGammaIndex][hiddenNeuronIndex];
                    }
                    gamma[hiddenLayerIndex][hiddenNeuronIndex] = gammaSum * activateDer(neurons[hiddenLayerIndex][hiddenNeuronIndex], previousLayerIndex);//calculate gamma
                }
                for (int j = 0; j < layers[hiddenLayerIndex]; j++)//itterate over outputs of layer
                {
                    for (int k = 0; k < layers[previousLayerIndex]; k++)//itterate over inputs to layer
                    {
                        biases[previousLayerIndex][k] -= gamma[hiddenLayerIndex][j] * learningRate;//modify biases of network
                        weights[previousLayerIndex][j][k] -= gamma[hiddenLayerIndex][j] * neurons[previousLayerIndex][k] * learningRate;//modify weights of network
                    }
                }
            }
        }

        public float activateDer(float value, int layer)//all activation function derivatives
        {
            switch (activations[layer])
            {
                case 0:
                    return sigmoidDer(value);
                case 1:
                    return tanhDer(value);
                case 2:
                    return reluDer(value);
                case 3:
                    return leakyreluDer(value);
                default:
                    return reluDer(value);
            }
        }

        public float sigmoid(float x)//activation functions and their corrosponding derivatives
        {
            float k = (float)Math.Exp(x);
            return k / (1.0f + k);
        }
        public float tanh(float x)
        {
            return (float)Math.Tanh(x);
        }
        public float relu(float x)
        {
            return (0 >= x) ? 0 : x;
        }
        public float leakyrelu(float x)
        {
            return (0 >= x) ? 0.01f * x : x;
        }
        public float sigmoidDer(float x)
        {
            return x * (1 - x);
        }
        public float tanhDer(float x)
        {
            return 1 - (x * x);
        }
        public float reluDer(float x)
        {
            return (0 >= x) ? 0 : 1;
        }
        public float leakyreluDer(float x)
        {
            return (0 >= x) ? 0.01f : 1;
        }

        public void Mutate(int chance, float val)//used as a simple mutation function for any genetic implementations.
        {
            for (int i = 0; i < biases.Length; i++)
            {
                for (int j = 0; j < biases[i].Length; j++)
                {
                    biases[i][j] = (MyRandom.Range(0f, chance) <= 5) ? biases[i][j] += MyRandom.Range(-val, val) : biases[i][j];
                }
            }

            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[i].Length; j++)
                {
                    for (int k = 0; k < weights[i][j].Length; k++)
                    {
                        weights[i][j][k] = (MyRandom.Range(0f, chance) <= 5) ? weights[i][j][k] += MyRandom.Range(-val, val) : weights[i][j][k];
                    }
                }
            }
        }

        public int CompareTo(ExampleNeuralNetwork other) //Comparing For NeuralNetworks performance.
        {
            if (other == null) return 1;

            if (fitness > other.fitness)
                return 1;
            else if (fitness < other.fitness)
                return -1;
            else
                return 0;
        }

        public ExampleNeuralNetwork copy(ExampleNeuralNetwork nn) //For creatinga deep copy, to ensure arrays are serialzed.
        {
            for (int i = 0; i < biases.Length; i++)
            {
                for (int j = 0; j < biases[i].Length; j++)
                {
                    nn.biases[i][j] = biases[i][j];
                }
            }
            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[i].Length; j++)
                {
                    for (int k = 0; k < weights[i][j].Length; k++)
                    {
                        nn.weights[i][j][k] = weights[i][j][k];
                    }
                }
            }
            return nn;
        }

        public void Load(string path)//this loads the biases and weights from within a file into the neural network.
        {
            TextReader tr = new StreamReader(path);
            int NumberOfLines = (int)new FileInfo(path).Length;
            string[] ListLines = new string[NumberOfLines];
            int index = 1;
            for (int i = 1; i < NumberOfLines; i++)
            {
                ListLines[i] = tr.ReadLine();
            }
            tr.Close();
            if (new FileInfo(path).Length > 0)
            {
                for (int i = 0; i < biases.Length; i++)
                {
                    for (int j = 0; j < biases[i].Length; j++)
                    {
                        biases[i][j] = float.Parse(ListLines[index]);
                        index++;
                    }
                }

                for (int i = 0; i < weights.Length; i++)
                {
                    for (int j = 0; j < weights[i].Length; j++)
                    {
                        for (int k = 0; k < weights[i][j].Length; k++)
                        {
                            weights[i][j][k] = float.Parse(ListLines[index]); ;
                            index++;
                        }
                    }
                }
            }
        }

        public void Save(string path)//this is used for saving the biases and weights within the network to a file.
        {
            File.Create(path).Close();
            StreamWriter writer = new StreamWriter(path, true);

            for (int i = 0; i < biases.Length; i++)
            {
                for (int j = 0; j < biases[i].Length; j++)
                {
                    writer.WriteLine(biases[i][j]);
                }
            }

            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[i].Length; j++)
                {
                    for (int k = 0; k < weights[i][j].Length; k++)
                    {
                        writer.WriteLine(weights[i][j][k]);
                    }
                }
            }
            writer.Close();
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            for(var layerCount = 0; layerCount < neurons.Length; layerCount++)
            {
                stringBuilder.Append($"L{layerCount}:");
                float[] neuronsOfThisLayer = neurons[layerCount];
                for (var neuronCount = 0; neuronCount < neuronsOfThisLayer.Length; neuronCount++)
                {
                    stringBuilder.Append($"N{neuronCount}:v:{neuronsOfThisLayer[neuronCount]};");
                    if(layerCount > 0)
                    {
                        stringBuilder.Append($"w1:{weights[layerCount - 1][neuronCount][0]};");
                        stringBuilder.Append($"w2:{weights[layerCount - 1][neuronCount][1]};");
                    }
                    stringBuilder.Append($"b:{biases[layerCount][neuronCount]};");
                }
                stringBuilder.AppendLine("");
            }

            return stringBuilder.ToString();
        }
    }
}
