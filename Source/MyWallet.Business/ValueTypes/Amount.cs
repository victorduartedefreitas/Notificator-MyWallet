using System;

namespace MyWallet.Business.ValueTypes
{
    public sealed class Amount
    {
        private decimal _value;

        public Amount(decimal value)
        {
            if (value < 0)
                throw new Exception($"The {value} is not a valid amount. It should be a positive value.");

            _value = value;
        }

        public override string ToString()
        {
            return _value.ToString("C");
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj is decimal)
                return (decimal)obj == _value;
            if (obj is Amount)
                return ((Amount)obj)._value == _value;
            return false;
        }

        public override int GetHashCode()
        {
            return -1939223833 + _value.GetHashCode();
        }

        public static implicit operator Amount(decimal value)
        {
            return new Amount(value);
        }

        public static implicit operator decimal(Amount value)
        {
            return value._value;
        }

        public static Amount operator -(Amount amount1, Amount amount2)
        {
            return amount1._value - amount2._value;
        }

        public static Amount operator +(Amount amount1, Amount amount2)
        {
            return amount1._value + amount2._value;
        }

        public static bool operator <(Amount amount1, Amount amount2)
        {
            return amount1._value < amount2._value;
        }

        public static bool operator >(Amount amount1, Amount amount2)
        {
            return amount1._value > amount2._value;
        }

        public static bool operator <=(Amount amount1, Amount amount2)
        {
            return amount1._value <= amount2._value;
        }

        public static bool operator >=(Amount amount1, Amount amount2)
        {
            return amount1._value >= amount2._value;
        }

    }
}
