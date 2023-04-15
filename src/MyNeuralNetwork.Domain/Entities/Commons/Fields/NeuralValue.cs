using System;

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
            T op = (T)Convert.ChangeType(Convert.ToDouble(v1.Value) - Convert.ToDouble(v2.Value), typeof(T));
            return new NeuralValue<T>(op);
        }

        public static NeuralValue<T> operator +(NeuralValue<T> v1, NeuralValue<T> v2)
        {
            T op = (T)Convert.ChangeType(Convert.ToDouble(v1.Value) + Convert.ToDouble(v2.Value), typeof(T));
            return new NeuralValue<T>(op);
        }

        public static NeuralValue<T> operator *(NeuralValue<T> v1, NeuralValue<T> v2)
        {
            T op = (T)Convert.ChangeType(Convert.ToDouble(v1.Value) * Convert.ToDouble(v2.Value), typeof(T));
            return new NeuralValue<T>(op);
        }

        public static NeuralValue<T> operator +(NeuralValue<T> v1, T v2)
        {
            T op = (T)Convert.ChangeType(Convert.ToDouble(v1.Value) + Convert.ToDouble(v2), typeof(T));
            return new NeuralValue<T>(op);
        }

        public static NeuralValue<T> operator +(T v1, NeuralValue<T> v2)
        {
            T op = (T)Convert.ChangeType(Convert.ToDouble(v1) + Convert.ToDouble(v2.Value), typeof(T));
            return new NeuralValue<T>(op);
        }

        public static NeuralValue<T> operator *(NeuralValue<T> v1, T v2)
        {
            T op = (T)Convert.ChangeType(Convert.ToDouble(v1.Value) * Convert.ToDouble(v2), typeof(T));
            return new NeuralValue<T>(op);
        }

        public static NeuralValue<T> operator *(T v1, NeuralValue<T> v2)
        {
            T op = (T)Convert.ChangeType(Convert.ToDouble(v1) * Convert.ToDouble(v2.Value), typeof(T));
            return new NeuralValue<T>(op);
        }

        public static NeuralValue<T> operator -(NeuralValue<T> v1, T v2)
        {
            T op = (T)Convert.ChangeType(Convert.ToDouble(v1.Value) - Convert.ToDouble(v2), typeof(T));
            return new NeuralValue<T>(op);
        }

        public static NeuralValue<T> operator -(T v1, NeuralValue<T> v2)
        {
            T op = (T)Convert.ChangeType(Convert.ToDouble(v1) - Convert.ToDouble(v2.Value), typeof(T));
            return new NeuralValue<T>(op);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
        internal void Increment(NeuralValue<T> v)
        {
            Value = (T)Convert.ChangeType(Convert.ToDouble(Value) + Convert.ToDouble(v.Value), typeof(T));
        }

        internal void Increment(T v)
        {
            Value = (T)Convert.ChangeType(Convert.ToDouble(Value) + Convert.ToDouble(v), typeof(T));
        }

        internal void Decrement(NeuralValue<T> v)
        {
            Value = (T)Convert.ChangeType(Convert.ToDouble(Value) - Convert.ToDouble(v.Value), typeof(T));
        }

        internal void Decrement(T v)
        {
            Value = (T)Convert.ChangeType(Convert.ToDouble(Value) - Convert.ToDouble(v), typeof(T));
        }

        internal void Set(T value)
        {
            Value = value;
        }
    }
}
