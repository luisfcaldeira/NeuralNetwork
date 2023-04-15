namespace MyNeuralNetwork.Domain.Entities.Support.ValueInserters
{
    public abstract class Inserter<T>
    {
        private T[] _values;
        private int _lastItem = 0;

        public Inserter(int sizeOfInput)
        {
            _values = new T[sizeOfInput];
        }

        public Inserter<T> Add(T input)
        {
            _values[_lastItem++] = input;
            return this;
        }

        public T[] Get()
        {
            return _values;
        }
    }
}
