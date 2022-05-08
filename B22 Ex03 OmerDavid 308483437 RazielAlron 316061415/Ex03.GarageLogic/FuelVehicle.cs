namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FuelVehicle : Vehicle
    {
        public static List<string> m_DataMembers = new List<string> { "Fuel Type: 0 - Soler / 1 - Octan95 / 2 - Octan96 / 3 - Octan98", "Current Fuel In Liters", "Max Fuel In Liters" };

        public FuelVehicle(
            FuelType i_FuelType,
            float i_CurrentFuelInLiters,
            float i_MaxFuelInLiters,
            string i_Model,
            string i_LisenceNumber,
            string i_WheelManufacturer,
            float i_WheelMaxAirPressure,
            float i_CurrentMaxAirPressure,
            int i_NumOfWheels)
            : base(
                i_Model,
                i_LisenceNumber,
                i_WheelManufacturer,
                i_WheelMaxAirPressure,
                i_CurrentMaxAirPressure,
                i_NumOfWheels)
        {
            m_FuelType = i_FuelType;
            m_CurrentFuelInLiters = i_CurrentFuelInLiters;
            m_MaxFuelInLiters = i_MaxFuelInLiters;
            UpdateRemainingEnergySourcePrecentage(i_CurrentFuelInLiters, i_MaxFuelInLiters);
        }

        public enum FuelType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        public FuelType m_FuelType { get; }

        public float m_CurrentFuelInLiters { get; private set; }

        public float m_MaxFuelInLiters { get; }

        public static List<string> GetDataMembers()
        {
            List<string> newList = new List<string>(Vehicle.GetDataMembers());
            
            newList.AddRange(m_DataMembers);

            return newList;
        }

        public static FuelVehicle Parse(List<string> i_DataMembers)
        {
            bool successfulParse = true;
            Vehicle vehicle = null;
            int enumSelection = 0;
            int fuelTypeEnumMaxValue = (int)Enum.GetValues(typeof(FuelType)).Cast<FuelType>().Max();
            float currentFuelInLiters = 0;
            float maxFuelInLiters = 0;

            vehicle = Vehicle.Parse(i_DataMembers.GetRange(0, 5));
            successfulParse = int.TryParse(i_DataMembers[5], out enumSelection);
            if (!successfulParse || fuelTypeEnumMaxValue < enumSelection || enumSelection < 0)
            {
                throw new FormatException("Invalid fuel type");
            }

            successfulParse = float.TryParse(i_DataMembers[6], out currentFuelInLiters);
            if (!successfulParse)
            {
                throw new FormatException("Invalid current fuel amount");
            }

            successfulParse = float.TryParse(i_DataMembers[7], out maxFuelInLiters);
            if (!successfulParse)
            {
                throw new FormatException("Invalid max fuel amount");
            }

            return new FuelVehicle(
                (FuelType)enumSelection,
                currentFuelInLiters,
                maxFuelInLiters,
                vehicle.m_Model,
                vehicle.m_LicenseNumber,
                vehicle.m_Wheels[0].m_Manufacturer,
                vehicle.m_Wheels[0].m_MaxAirPressure,
                vehicle.m_Wheels[0].m_CurrentAirPressure,
                1);
        }

        public void RefuelByAmount(FuelType i_FuelType, float i_AmountToFuelInLiters)
        {
            if (i_FuelType != m_FuelType)
            {
                throw new ArgumentException($"Fuel Type Exception: The actual FuelType is: {m_FuelType}, But the input was {i_FuelType}");
            }

            if ((i_AmountToFuelInLiters + m_CurrentFuelInLiters) > m_MaxFuelInLiters)
            {
                throw new ValueOutOfRangeException(m_MaxFuelInLiters, 0); 
            }

            m_CurrentFuelInLiters += i_AmountToFuelInLiters;
            UpdateRemainingEnergySourcePrecentage(m_CurrentFuelInLiters, m_MaxFuelInLiters);
        }

        public override List<string> GetVehicleProperties()
        {
            List<string> vehicleProperties = new List<string>();

            vehicleProperties.AddRange(base.GetVehicleProperties());
            vehicleProperties.Add($"Fuel Type: {m_FuelType}");
            vehicleProperties.Add($"Current Fuel In Liters: {m_CurrentFuelInLiters}");
            vehicleProperties.Add($"Max Fuel In Liters: {m_MaxFuelInLiters}");

            return vehicleProperties;
        }
    }
}
