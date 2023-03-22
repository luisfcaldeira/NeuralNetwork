namespace MyNeuralNetwork.Domain.Entities.Commons.Fields
{
    public class NeuralValue<T> where T : struct
    {
        public virtual T Value { get; internal set; }

        public NeuralValue(T value)
        {
            Value = value;
        }

        public static NeuralValue<T> operator -(NeuralValue<T> v1, NeuralValue<T> v2) 
        {
            T op = (dynamic)v1.Value - (dynamic)v2.Value;
            return new (op);
        }

        public static NeuralValue<T> operator +(NeuralValue<T> v1, NeuralValue<T> v2)
        {
            T op = (dynamic)v1.Value + (dynamic)v2.Value;
            return new (op);
        }

        public static NeuralValue<T> operator *(NeuralValue<T> v1, NeuralValue<T> v2)
        {
            T op = (dynamic)v1.Value * (dynamic)v2.Value;
            return new(op);
        }

        public static NeuralValue<T> operator +(NeuralValue<T> v1, T v2)
        {
            T op = (dynamic)v1.Value + (dynamic)v2;
            return new(op);
        }

        public static NeuralValue<T> operator +(T v1, NeuralValue<T> v2)
        {
            T op = (dynamic)v2.Value + (dynamic)v1;
            return new(op);
        }

        public static NeuralValue<T> operator *(NeuralValue<T> v1, T v2)
        {
            T op = (dynamic)v1.Value * (dynamic)v2;
            return new(op);
        }

        public static NeuralValue<T> operator *(T v1, NeuralValue<T> v2)
        {
            T op = (dynamic)v2.Value * (dynamic)v1;
            return new(op);
        }

        public static NeuralValue<T> operator -(NeuralValue<T> v1, T v2)
        {
            T op = (dynamic)v1.Value - (dynamic)v2;
            return new(op);
        }

        public static NeuralValue<T> operator -(T v1, NeuralValue<T> v2)
        {
            T op = (dynamic)v1 - (dynamic)v2.Value;
            return new(op);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
        internal void Increment(NeuralValue<T> v)
        {
            Value = (dynamic)Value + (dynamic)v.Value;
        }

        internal void Increment(T v)
        {
            Value = (dynamic)Value + (dynamic)v;
        }

        internal void Decrement(NeuralValue<T> v)
        {
            Value = (dynamic)Value - (dynamic)v.Value;
        }

        internal void Decrement(T v)
        {
            Value = (dynamic)Value - (dynamic)v;
        }

        internal void Set(T value)
        {
            Value = value;
        }
    }
}
