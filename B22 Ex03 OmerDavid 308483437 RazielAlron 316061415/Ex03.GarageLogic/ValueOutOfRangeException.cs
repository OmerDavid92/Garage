namespace Ex03.GarageLogic
{
    using System;

    public class ValueOutOfRangeException : Exception
    {
        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue)
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }

        public float m_MaxValue { get; }

        public float m_MinValue { get; }

        public override string Message
        {
            get
            {
                return $"Value Out Of Range Exception: The Max Value is: {m_MaxValue} and the Min value is {m_MinValue}";
            }
        }
    }
}
