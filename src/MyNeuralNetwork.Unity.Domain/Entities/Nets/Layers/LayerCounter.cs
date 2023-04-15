namespace MyNeuralNetwork.Domain.Entities.Nets.Layers
{
    public class LayerCounter
    {
        private int _counter = 0;
        public int Counter
        {
            get
            {
                return _counter++;
            }
        }
    }
}
