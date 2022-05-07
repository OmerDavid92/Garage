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
            List<string> newList = ElectricVehicle.GetDataMembers();
            
            newList.AddRange(m_DataMembers);

            return newList;
        }

        public override List<string> GetVehicleProperties()
        {
            List<string> vehicleProperties = new List<string>();

            vehicleProperties.AddRange(base.GetVehicleProperties());
            vehicleProperties.Add($"Car Color: {m_CarColor}");
            vehicleProperties.Add($"Cargo Volume: {m_CargoVolume}");
            vehicleProperties.Add($"Battery Max Hours: {m_BatteryMaxHours}");
            vehicleProperties.Add($"Battery Hours Left: {m_BatteryHoursLeft}");

            return vehicleProperties;
        }

        public static ElectricCar Parse(List<string> i_DataMembers)
        {
            bool successfulParse = true;
            ElectricVehicle electricVehicle = null;
            int enumSelection = 0;
            int CarColorEnumMaxValue = (int)Enum.GetValues(typeof(CarColor)).Cast<CarColor>().Max();
            float cargoVolume = 0;

            electricVehicle = ElectricVehicle.Parse(i_DataMembers.GetRange(0, 8));
            successfulParse = int.TryParse(i_DataMembers[8], out enumSelection);
            if (!successfulParse || CarColorEnumMaxValue < enumSelection || enumSelection < 0)
            {
                throw new FormatException("Invalid car color");
            }

            successfulParse = float.TryParse(i_DataMembers[9], out cargoVolume);
            if (!successfulParse)
            {
                throw new FormatException("Invalid cargo volume");
            }

            return new ElectricCar(
                (CarColor)enumSelection,
                cargoVolume,
                electricVehicle.m_BatteryMaxHours,
                electricVehicle.m_BatteryHoursLeft,
                electricVehicle.m_Model,
                electricVehicle.m_LicenseNumber,
                electricVehicle.m_RemainingEnergySourcePrecentage,
                electricVehicle.m_Wheels[0].m_Manufacturer,
                electricVehicle.m_Wheels[0].m_MaxAirPressure,
                electricVehicle.m_Wheels[0].m_CurrentAirPressure);
        }
    }
}
