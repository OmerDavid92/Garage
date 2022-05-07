using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public float m_MaxValue { get; }
        public float m_MinValue { get; }

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue)
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }

        public string Message()
        {
            return $"Value Out Of Range Exception: The Max Value is: {m_MaxValue} and the Min value is {m_MinValue}";
        } 
    }

    public class FuelTypeException : Exception
    {
        public FuelVehicle.FuelType m_ActualFuelType { get; }
        public FuelVehicle.FuelType m_UserChoiseFuelType { get; }

        public FuelTypeException(FuelVehicle.FuelType i_ActualFuelType, FuelVehicle.FuelType i_UserChoiseFuelType)
        {
            m_ActualFuelType = i_ActualFuelType;
            m_UserChoiseFuelType = i_UserChoiseFuelType;
        }
        public string Message()
        {
            return $"Fuel Type Exception: The actual FuelType is: {m_ActualFuelType}, But the input was {m_UserChoiseFuelType}";
        }
    }
}
