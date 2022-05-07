using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricCar : ElectricVehicle
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
        public ElectricCar(
            CarColor i_CarColor,
            float i_CargoVolume,
            float i_BatteryMaxHours,
            float i_BatteryHoursLeft,
            string i_Model,
            string i_LisenceNumber,
            float i_RemainingEnergySourcePrecentage,
            string i_WheelManufacturer,
            float i_WheelMaxAirPressure,
            float i_CurrentMaxAirPressure)
            : base(i_BatteryMaxHours,
                i_BatteryHoursLeft,
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
            ElectricVehicle.GetDataMembers().AddRange(m_DataMembers);
            return m_DataMembers;
        }

        public virtual List<string> GetVehicleProperties()
        {
            List<string> vehicleProperties = new List<string>();

            vehicleProperties.AddRange(base.GetVehicleProperties());
            vehicleProperties.Add($"Car Color: {m_CarColor}");
            vehicleProperties.Add($"Cargo Volume: {m_CargoVolume}");
            vehicleProperties.Add($"Battery Max Hours: {m_BatteryMaxHours}");
            vehicleProperties.Add($"Battery Hours Left: {m_BatteryHoursLeft}");

            return vehicleProperties;
        }

        public static bool TryParse(List<string> i_DataMembers, out ElectricCar o_ElectricCar)
        {
            bool successfulParse = true;
            ElectricVehicle o_ElectricVehicle = null;
            o_ElectricCar = null;
            int enumSelection = 0;
            int CarColorEnumMaxValue = (int)Enum.GetValues(typeof(CarColor)).Cast<CarColor>().Max();
            float cargoVolume = 0;

            successfulParse = ElectricVehicle.TryParse(i_DataMembers.GetRange(0, 8), out o_ElectricVehicle);
            if (successfulParse)
            {
                successfulParse = int.TryParse(i_DataMembers[8], out enumSelection);
                if (CarColorEnumMaxValue > enumSelection && enumSelection > 0 && successfulParse)
                {
                    successfulParse = float.TryParse(i_DataMembers[9], out cargoVolume);
                    if (successfulParse)
                    {
                        o_ElectricCar = new ElectricCar(
                            (CarColor)enumSelection,
                            cargoVolume,
                            o_ElectricVehicle.m_BatteryMaxHours,
                            o_ElectricVehicle.m_BatteryHoursLeft,
                            o_ElectricVehicle.m_Model,
                            o_ElectricVehicle.m_LisenceNumber,
                            o_ElectricVehicle.m_RemainingEnergySourcePrecentage,
                            o_ElectricVehicle.m_Wheels[0].m_Manufacturer,
                            o_ElectricVehicle.m_Wheels[0].m_MaxAirPressure,
                            o_ElectricVehicle.m_Wheels[0].m_CurrentAirPressure);
                    }
                }
            }

            return successfulParse;
        }
    }
}
