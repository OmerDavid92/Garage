using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class ElectricMotor : ElectricVehicle
    {
        public enum LicenseType
        {
            A,
            A1,
            B1,
            BB
        }

        public LicenseType m_LicenseType { get; }
        public int m_EngineCapacity { get; }

        private const int m_NumOfWheels = 2;

        public static List<string> m_DataMembers = new List<string> { "LicenseType: A/A1/B1/BB", "Engine Capacity" };

        public ElectricMotor(
            LicenseType i_LicenseType,
            int i_EngineCapacity,
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
            m_LicenseType = i_LicenseType;
            m_EngineCapacity = i_EngineCapacity;
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
            vehicleProperties.Add($"License Type: {m_LicenseType}");
            vehicleProperties.Add($"Engine Capacity: {m_EngineCapacity}");

            return vehicleProperties;
        }

        public static ElectricMotor Parse(List<string> i_DataMembers)
        {
            bool successfulParse = true;
            ElectricVehicle electricVehicle = null;
            int enumSelection = 0;
            int LicenseTypeEnumMaxValue = (int)Enum.GetValues(typeof(LicenseType)).Cast<LicenseType>().Max();
            int engineCapacity = 0;

            electricVehicle = ElectricVehicle.Parse(i_DataMembers.GetRange(0, 8));
            successfulParse = int.TryParse(i_DataMembers[8], out enumSelection);
            if (!successfulParse || LicenseTypeEnumMaxValue < enumSelection || enumSelection < 0)
            {
                throw new FormatException("Invalid license type");
            }

            successfulParse = int.TryParse(i_DataMembers[9], out engineCapacity);
            if (!successfulParse)
            {
                throw new FormatException("Invalid engine capacity");
            }

            return new ElectricMotor(
                (LicenseType)enumSelection,
                engineCapacity,
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
