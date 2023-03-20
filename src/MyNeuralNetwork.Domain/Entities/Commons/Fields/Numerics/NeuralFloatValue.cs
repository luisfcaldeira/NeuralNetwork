using MyNeuralNetwork.Domain.Entities.Nets.IO.Inputs;
using System;

namespace MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics
{
    public class NeuralFloatValue : NeuralValue<float>
    {
        public NeuralFloatValue(NeuralFloatValue floatNeuralValue)
        {
            Value = floatNeuralValue.Value;
        }

        public NeuralFloatValue(float value)
        {
            Value = value;
        }

        public NeuralFloatValue() : this(0) { }

        public static NeuralFloatValue operator -(float v1, NeuralFloatValue v2)
        {
            return new (v1 - v2.Value);
        }

        public static NeuralFloatValue operator +(float v1, NeuralFloatValue v2)
        {
            return new (v1 + v2.Value);
        }

        public static NeuralFloatValue operator *(float v1, NeuralFloatValue v2)
        {
            return new (v1 * v2.Value);
        }

        public static NeuralFloatValue operator -(NeuralFloatValue v1, float v2)
        {
            return new (v1.Value - v2);
        }

        public static NeuralFloatValue operator +(NeuralFloatValue v1, float v2)
        {
            return new (v1.Value + v2);
        }

        public static NeuralFloatValue operator *(NeuralFloatValue v1, float v2)
        {
            return new (v1.Value * v2);
        }

        public static NeuralFloatValue operator -(NeuralFloatValue v1, NeuralFloatValue v2)
        {
            return new (v1.Value - v2.Value);
        }

        public static NeuralFloatValue operator +(NeuralFloatValue v1, NeuralFloatValue v2)
        {
            return new (v1.Value + v2.Value);
        }

        public static NeuralFloatValue operator *(NeuralFloatValue v1, NeuralFloatValue v2)
        {
            return new (v1.Value * v2.Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        internal void Decrement(NeuralFloatValue v)
        {
            Value -= v.Value;
        }

        internal void Decrement(float v)
        {
            Value -= v;
        }

        internal void Set(float value)
        {
            Value = value;
        }
    }
}
