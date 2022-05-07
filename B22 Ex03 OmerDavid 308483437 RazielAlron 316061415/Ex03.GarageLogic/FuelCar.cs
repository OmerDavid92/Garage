using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class FuelCar : FuelVehicle
    {
        public enum CarColor
        {
            Red,
            White,
            Green,
            Blue
        }

        public CarColor m_CarColor { get; set; }
        public float m_CargoVolume { get; }

        private const int m_NumOfWheels = 4;

        public static List<string> m_DataMembers = new List<string> { "Car Color", "Cargo Volume" };

        public FuelCar(
            CarColor i_CarColor,
            float i_CargoVolume,
            FuelType i_FuelType,
            float i_CurrentFuelInLiters,
            float i_MaxFuelInLiters,
            string i_Model,
            string i_LisenceNumber,
            float i_RemainingEnergySourcePrecentage, 
            string i_WheelManufacturer,
            float i_WheelMaxAirPressure,
            float i_CurrentMaxAirPressure)
            : base(i_FuelType,
                  i_CurrentFuelInLiters,
                  i_MaxFuelInLiters,
                  i_Model,
                  i_LisenceNumber,
                  i_RemainingEnergySourcePrecentage,
                  i_WheelManufacturer,
                  i_WheelMaxAirPressure,
                  i_CurrentMaxAirPressure,
                  m_NumOfWheels)
        {
            m_CarColor = i_CarColor;
            m_CargoVolume = i_CargoVolume;
        }

        public static List<string> GetDataMembers()
        {
            FuelVehicle.GetDataMembers().AddRange(m_DataMembers);
            return m_DataMembers;
        }

        public virtual List<string> GetVehicleProperties()
        {
            List<string> vehicleProperties = new List<string>();

            vehicleProperties.AddRange(base.GetVehicleProperties());
            vehicleProperties.Add($"Car Color: {m_CarColor}");
            vehicleProperties.Add($"Cargo Volume: {m_CargoVolume}");

            return vehicleProperties;
        }

        public static bool TryParse(List<string> i_DataMembers, out FuelCar o_FuelCarVehicle)
        {
            bool successfulParse = true;
            FuelVehicle o_FuelVehicle = null;
            o_FuelCarVehicle = null;
            int enumSelection = 0;
            int CarColorEnumMaxValue = (int)Enum.GetValues(typeof(CarColor)).Cast<CarColor>().Max();
            float cargoVolume = 0;

            successfulParse = FuelVehicle.TryParse(i_DataMembers.GetRange(0, 9), out o_FuelVehicle);
            if (successfulParse)
            {
                successfulParse = int.TryParse(i_DataMembers[9], out enumSelection);
                if (CarColorEnumMaxValue > enumSelection && enumSelection > 0 && successfulParse)
                {
                    successfulParse = float.TryParse(i_DataMembers[10], out cargoVolume);
                    if (successfulParse)
                    {
                        o_FuelCarVehicle = new FuelCar(
                            (CarColor)enumSelection,
                            cargoVolume,
                            o_FuelVehicle.m_FuelType,
                            o_FuelVehicle.m_CurrentFuelInLiters,
                            o_FuelVehicle.m_MaxFuelInLiters,
                            o_FuelVehicle.m_Model,
                            o_FuelVehicle.m_LisenceNumber,
                            o_FuelVehicle.m_RemainingEnergySourcePrecentage,
                            o_FuelVehicle.m_Wheels[0].m_Manufacturer,
                            o_FuelVehicle.m_Wheels[0].m_MaxAirPressure,
                            o_FuelVehicle.m_Wheels[0].m_CurrentAirPressure);
                        
                    }
                }
            }

            return successfulParse;
        }
    }
}
