namespace MyNeuralNetwork.Domain.Entities.Commons.Fields
{
    public abstract class NeuralValue<T>
    {
        public virtual T Value { get; internal set; } 
    }
}
