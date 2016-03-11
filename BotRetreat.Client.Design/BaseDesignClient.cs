using System;

namespace BotRetreat.Client.Design
{
    public class BaseDesignClient
    {
        private readonly Random _randomGenerator = new Random();

        protected Int16 RandomInt16()
        {
            return (Int16)_randomGenerator.Next(0, 1000);
        }

        protected Int32 RandomInt32()
        {
            return _randomGenerator.Next(0, 10000);
        }

        protected Byte RandomByte()
        {
            return (Byte)_randomGenerator.Next(0, 255);
        }

        protected TimeSpan RandomTimeSpan()
        {
            return TimeSpan.FromSeconds(RandomByte());
        }

        protected DateTime RandomDateTime()
        {
            return new DateTime(1800 + RandomByte(), RandomByte() % 12, RandomByte() % 28, RandomByte() % 24, RandomByte() % 60, RandomByte() % 60);
        }

        protected TEnum RandomEnum<TEnum>() where TEnum : struct
        {
            var enumValues = Enum.GetValues(typeof(TEnum));
            var randomValueIndex = _randomGenerator.Next(0, enumValues.Length);
            return (TEnum)enumValues.GetValue(randomValueIndex);
        }
    }
}