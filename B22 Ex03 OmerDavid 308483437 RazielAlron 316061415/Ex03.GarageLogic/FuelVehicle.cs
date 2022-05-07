using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelVehicle : Vehicle
    {
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

        public static List<string> m_DataMembers = new List<string> { "Fuel Type: 0 - Soler/1 - Octan95/2 - Octan96/3 - Octan98", "Current Fuel In Liters", "Max Fuel In Liters" };
        public FuelVehicle(
            FuelType i_FuelType,
            float i_CurrentFuelInLiters,
            float i_MaxFuelInLiters,
            string i_Model,
            string i_LisenceNumber,
            float i_RemainingEnergySourcePrecentage,
            string i_WheelManufacturer,
            float i_WheelMaxAirPressure,
            float i_CurrentMaxAirPressure,
            int i_NumOfWheels)
            : base(i_Model,
                i_LisenceNumber,
                i_RemainingEnergySourcePrecentage,
                  i_WheelManufacturer,
                  i_WheelMaxAirPressure,
                  i_CurrentMaxAirPressure,
                  i_NumOfWheels)
        {
            m_FuelType = i_FuelType;
            m_CurrentFuelInLiters = i_CurrentFuelInLiters;
            m_MaxFuelInLiters = i_MaxFuelInLiters;
        }

        public static List<string> GetDataMembers()
        {
            List<string> newList = Vehicle.GetDataMembers();
            
            newList.AddRange(m_DataMembers);

            return newList;
        }

        public void RefuelByAmount(FuelType i_FuelType, float i_AmountToFuelInLiters)
        {
            if (i_FuelType != m_FuelType)
            {
                //EXCEPTION
            }
            if ((i_AmountToFuelInLiters + m_CurrentFuelInLiters) > m_MaxFuelInLiters)
            {
                //EXCEPTION
            }

            m_CurrentFuelInLiters += i_AmountToFuelInLiters;
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

        public static bool TryParse(List<string> i_DataMembers, out FuelVehicle o_FuelVehicle)
        {
            bool successfulParse = true;
            Vehicle o_Vehicle = null;
            o_FuelVehicle = null;
            int enumSelection = 0;
            int fuelTypeEnumMaxValue = (int)Enum.GetValues(typeof(FuelType)).Cast<FuelType>().Max();
            float currentFuelInLiters = 0;
            float maxFuelInLiters = 0;

            successfulParse = Vehicle.TryParse(i_DataMembers.GetRange(0, 6), out o_Vehicle);
            if (successfulParse)
            {
                successfulParse = int.TryParse(i_DataMembers[6], out enumSelection);
                if (fuelTypeEnumMaxValue > enumSelection && enumSelection > 0 && successfulParse)
                {
                    successfulParse = float.TryParse(i_DataMembers[7], out currentFuelInLiters);
                    if (successfulParse)
                    {
                        successfulParse = float.TryParse(i_DataMembers[8], out maxFuelInLiters);
                        if (successfulParse)
                        {
                            o_FuelVehicle = new FuelVehicle(
                                (FuelType)enumSelection,
                                currentFuelInLiters,
                                maxFuelInLiters,
                                o_Vehicle.m_Model,
                                o_Vehicle.m_LicenseNumber,
                                o_Vehicle.m_RemainingEnergySourcePrecentage,
                                o_Vehicle.m_Wheels[0].m_Manufacturer,
                                o_Vehicle.m_Wheels[0].m_MaxAirPressure,
                                o_Vehicle.m_Wheels[0].m_CurrentAirPressure,
                                1);
                        }
                    }
                }
            }

            return successfulParse;
        }
    }
}
