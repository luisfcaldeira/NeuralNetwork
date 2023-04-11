using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using NUnit.Framework;

namespace MyNeuralNetwork.Domain.Tests.Entities.Commons.Fields.Numerics
{
    public class FloatFieldTests
    {
        class FloatFieldTest : NeuralFloatValue
        {
            public FloatFieldTest() : base(0)
            {
            }
        }

        [Test]
        public void TestIfMultiply()
        {
            var value1 = 2;
            var value2 = 3;
            int expectedValue = value1 * value2;

            FloatFieldTest field1 = GenerateField(value1);
            FloatFieldTest field2 = GenerateField(value2);

            Assert.That(field1.Value, Is.EqualTo(value1));
            Assert.That(field2.Value, Is.EqualTo(value2));

            Assert.That((field1 * field2).Value, Is.EqualTo(expectedValue));
            Assert.That((field1.Value * field2).Value, Is.EqualTo(expectedValue));
            Assert.That((field1 * field2.Value).Value, Is.EqualTo(expectedValue));
        }

        [Test]
        public void TestIfSumValues()
        {
            var value1 = 2;
            var value2 = 3;
            int expectedValue = value1 + value2;

            FloatFieldTest field1 = GenerateField(value1);
            FloatFieldTest field2 = GenerateField(value2);

            Assert.That(field1.Value, Is.EqualTo(value1));
            Assert.That(field2.Value, Is.EqualTo(value2));

            Assert.That((field1 + field2).Value, Is.EqualTo(expectedValue));
            Assert.That((field1.Value + field2).Value, Is.EqualTo(expectedValue));
            Assert.That((field1 + field2.Value).Value, Is.EqualTo(expectedValue));
        }

        [Test]
        public void TestIfSubtractvalues()
        {
            var value1 = 2;
            var value2 = 3;
            int expectedValue = value1 - value2;

            FloatFieldTest field1 = GenerateField(value1);
            FloatFieldTest field2 = GenerateField(value2);

            Assert.That(field1.Value, Is.EqualTo(value1));
            Assert.That(field2.Value, Is.EqualTo(value2));

            Assert.That((field1 - field2).Value, Is.EqualTo(expectedValue));
            Assert.That((field1.Value - field2).Value, Is.EqualTo(expectedValue));
            Assert.That((field1 - field2.Value).Value, Is.EqualTo(expectedValue));
        }

        private static FloatFieldTest GenerateField(int value)
        {
            var field = new FloatFieldTest();
            field.GetType().GetProperty("Value").SetValue(field, value);
            return field;
        }
    }
}
