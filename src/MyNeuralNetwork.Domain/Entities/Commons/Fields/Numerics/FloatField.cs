namespace MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics
{
    public abstract class FloatField : Field<float>
    {
        public static float operator -(FloatField v1, FloatField v2)
        {
            return v1.Value - v2.Value;
        }

        public static float operator +(FloatField v1, FloatField v2)
        {
            return v1.Value + v2.Value;
        }

        public static float operator *(FloatField v1, FloatField v2)
        {
            return v1.Value * v2.Value;
        }
        public static float operator -(FloatField v1, float v2)
        {
            return v1.Value - v2;
        }

        public static float operator +(FloatField v1, float v2)
        {
            return v1.Value + v2;
        }

        public static float operator *(FloatField v1, float v2)
        {
            return v1.Value * v2;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
