namespace MyNeuralNetwork.Domain.Entities.Commons.Fields
{
    public abstract class Field<T>
    {
        public virtual T Value { get; internal set; }

        
    }
}
