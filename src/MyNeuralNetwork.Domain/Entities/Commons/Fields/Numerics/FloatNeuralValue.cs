namespace MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics
{
    public class FloatNeuralValue : NeuralValue<float>
    {
        public FloatNeuralValue(FloatNeuralValue floatNeuralValue)
        {
            Value = floatNeuralValue.Value;
        }

        public FloatNeuralValue(float value)
        {
            Value = value;
        }

        public static FloatNeuralValue operator -(float v1, FloatNeuralValue v2)
        {
            return new (v1 - v2.Value);
        }

        public static FloatNeuralValue operator +(float v1, FloatNeuralValue v2)
        {
            return new (v1 + v2.Value);
        }

        public static FloatNeuralValue operator *(float v1, FloatNeuralValue v2)
        {
            return new (v1 * v2.Value);
        }

        public static FloatNeuralValue operator -(FloatNeuralValue v1, float v2)
        {
            return new (v1.Value - v2);
        }

        public static FloatNeuralValue operator +(FloatNeuralValue v1, float v2)
        {
            return new (v1.Value + v2);
        }

        public static FloatNeuralValue operator *(FloatNeuralValue v1, float v2)
        {
            return new (v1.Value * v2);
        }

        public static FloatNeuralValue operator -(FloatNeuralValue v1, FloatNeuralValue v2)
        {
            return new (v1.Value - v2.Value);
        }

        public static FloatNeuralValue operator +(FloatNeuralValue v1, FloatNeuralValue v2)
        {
            return new (v1.Value + v2.Value);
        }

        public static FloatNeuralValue operator *(FloatNeuralValue v1, FloatNeuralValue v2)
        {
            return new (v1.Value * v2.Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
